using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupTypes
{
    None,
    Laser,
    Shield

}
[CreateAssetMenu(fileName ="New PickupConfig", menuName = "Player/Pickups", order =1)]
public class PickupConfig : ScriptableObject
{
    public PickupTypes type;
    public int score;
    public float durationInSeconds;

 
}
