using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCard : Card
{
    protected override void InitCardCategory()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
        category = GameManager.I.cardsSO.skillCategory;
    }
}
