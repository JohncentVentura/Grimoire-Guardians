using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlime : SkillCard
{
    public override void InitCard()
    {
        InitCardCategory();
        manaCost = 2;
        duration = 6;
        cooldown = 8;
        cardName = "Simple Slime";
    }
}
