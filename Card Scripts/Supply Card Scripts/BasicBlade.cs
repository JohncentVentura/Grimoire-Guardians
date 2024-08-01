using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBlade : SupplyCard
{
    public override void InitCard()
    {
        InitCardCategory();
        manaCost = 3;
        duration = 16;
        cooldown = 12;
        cardName = "Basic Blade";
    }
}
