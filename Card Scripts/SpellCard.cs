using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCard : Card
{   
    protected override void InitCardCategory()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
        category = GameManager.I.cardsSO.cardCategoryDict[CardsSO.TYPES.SPELL];
    }
}