using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBow : SupplyCard
{
    public override void InitCard()
    {
        InitCardCategory();
        manaCost = 4;
        duration = 14;
        cooldown = 10;
        cardName = "Basic Bow";
    }
}
