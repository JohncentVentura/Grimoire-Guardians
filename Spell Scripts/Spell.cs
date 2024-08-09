using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Card
{   
    protected override void InitCardCategory()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
        category = GameManager.Instance.cardsData.cardCategoryDict[CardsData.TYPES.SPELL];
    }
}