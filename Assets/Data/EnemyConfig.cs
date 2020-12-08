using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New EnemyConfig", menuName ="Enemies/Config", order =2)]
public class EnemyConfig : ScriptableObject
{
    public float moveSpeed;
    public Sprite sprite;
    
    public int score;
    public int hp;

    [Range(0, 1)]
    public float pickupChance;

    public bool shouldThrowPickup()
    {
        return Dice.IsChanceSucess(pickupChance);
    }

   
}
