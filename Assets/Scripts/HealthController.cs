using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField] private UnityEvent onZeroHealthActions;
    public int health;
 
    public void OnDamage(int damageAmount)
    {
        health -= damageAmount;
        Debug.Log(health);
        if(health <= 0)
        {
            OnZeroHealth();
        }
    }

    public void OnZeroHealth()
    {
        if(onZeroHealthActions != null)
        {
            onZeroHealthActions.Invoke();
        }
    }

    public void SetHealth(int value)
    {
        health = value;
    }
}
