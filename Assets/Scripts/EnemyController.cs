using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{ 
    public EnemyConfig config;
    private Mover mover;
  
    [SerializeField] private SpriteRenderer spriteRenderer = default;
    
    private Shooter[] shooters;
    [SerializeField] private MultipleInstantiator multipleInstantiator;

    private void Start()
    {
        mover = GetComponent<Mover>();
        if (mover != null)
        {
            mover.speed = config.moveSpeed;
        }
        
        if (config.sprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = config.sprite;
        }

       
            shooters = GetComponentsInChildren<Shooter>();
            if (shooters != null && shooters.Length > 0)
            {
                foreach (var shooter in shooters)
                {
                    StartCoroutine(ShootForever(shooter));
                }

            }
        

     
        

        
    }

    public void OnDie()
    {
        if(config != null && config.shouldThrowPickup() && multipleInstantiator != null)
        {
            if(multipleInstantiator.InstantiatorsCount > 1)
            {
                for(int i=0; i<multipleInstantiator.InstantiatorsCount; i++)
                {
                    if (Dice.IsChanceSucess(config.pickupChance))
                    {
                        multipleInstantiator.InstantiateByIndex(i);
                    }
                  
                }
            }
            else
            {
                multipleInstantiator.InstantiateInSequence();
            }
          
        }
        Debug.Log("Im dead");
        GameController.Instance.OnDie(gameObject, config.score);
        StopAllCoroutines();
    }

    private IEnumerator ShootForever(Shooter shooter)
    {
        yield return new WaitForSeconds(shooter.shootingConfig.shootInitialWaitTime);
        while (true)
        {
           
                shooter.DoShoot();
                yield return new WaitForSeconds(shooter.shootingConfig.shootCadence);
            
            
        }

     }
       
    }

  

