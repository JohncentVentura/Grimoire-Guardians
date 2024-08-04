using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCard : Card
{
    [HideInInspector] public float health;
    [HideInInspector] public float damage;
    [HideInInspector] public float movementSpeed;

    protected override void InitCardCategory()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
        category = GameManager.I.cardsSO.cardCategoriesDictionary["summon"];
    }
}