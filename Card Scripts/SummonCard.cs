using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCard : Card
{
    [HideInInspector] public float health;
    [HideInInspector] public float damage;
    [HideInInspector] public float damageInterval;
    [HideInInspector] public float speed;
    
    protected override void InitCardCategory()
    {
        category = GameManager.I.cardsSO.summonCategory;
    }
}
