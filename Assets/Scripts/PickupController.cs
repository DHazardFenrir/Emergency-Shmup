using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] public PickupConfig config;
    public void OnPickedUp()
    {
        GameController.Instance.onPickupPickedUp(this);
    }
    
}
