using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : SummonCard
{
    public override void InitCard()
    {
        InitCardCategory();
        manaCost = 3;
        duration = 12;
        cooldown = 4;
        cardName = "Bird";
    }
}
