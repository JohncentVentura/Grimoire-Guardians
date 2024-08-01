using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlime : SummonCard
{
    public override void InitCard()
    {
        InitCardCategory();
        manaCost = 2;
        duration = 14;
        cooldown = 8;
        cardName = "Simple Slime";
    }
}
