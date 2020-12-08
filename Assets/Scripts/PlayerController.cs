using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Boundary
{
    public float xMinimum;
    public float xMaximum;
    public float yMinimum;
    public float yMaximum;
}
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Mover moveComponent;
    public Boundary boundary;
    [SerializeField]private List<Shooter> shooters;
    [SerializeField] private PlayerConfig config;
    [SerializeField] SpeciaController specials;
    [SerializeField] Collider2D playerCollider;
    public OnTriggerEnterDo triggerer;

    private int powerLevel;
    private int unlockedCannons =1;

    public delegate void PowerChanged(int currentPower, int totalPower);
    public event PowerChanged onPowerChanged;

    private void Start()
    {
        moveComponent.speed = speed;
        InputProvider.OnHasShoot += OnHasShoot;
        InputProvider.onDirection += OnDirection;
        
    }

    private void OnDirection(Vector3 direction)
    {
        moveComponent.direction = direction;

    }
 

    // Update is called once per frame
    void Update()
    {
      
        float x = Mathf.Clamp(transform.position.x, boundary.xMinimum, boundary.xMaximum);
        float y = Mathf.Clamp(transform.position.y, boundary.yMinimum, boundary.yMaximum);
        transform.position = new Vector3(x, y);

       
    }

    private void OnHasShoot()
    {
        for(int i=0; i < unlockedCannons; i++)
        {
            var shooter = shooters[i];
            shooter.DoShoot();
        }

        
    }

    public void onPlayerDie()
    {
        GameController.Instance.onPlayerDie();
    }
    public void AddPowerLevel(int powerToAdd)
    {
        powerLevel += powerToAdd;
        var powerConfig = config.GetPowerConfig(powerLevel);
        unlockedCannons = powerConfig.cannonAmount;
        Debug.Log( unlockedCannons);

        if(onPowerChanged != null)
        {
            onPowerChanged.Invoke(powerLevel, config.GetMaxPowerValue());
        }


    }

   public void UnlockSpecial(PickupConfig pickup)
    {
        Debug.Log("Unlock Special");
        specials.UnlockSpecial(pickup);

        if(pickup.type== PickupTypes.Shield)
        {
            EnableCollider(false);
        }
    }

    public void EnableCollider(bool shoudlEnable)
    {
        playerCollider.enabled = shoudlEnable;
        triggerer.IsEnabled = shoudlEnable;

    }

    public int GetMaxPowerLevel()
    {
        return powerLevel;
    }

    public int GeCurrentPowerLevel()
    {
        return config.GetMaxPowerValue();
    }
}
