using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBow : SupplyCard
{
    public override void InitCard()
    {
        InitCardCategory();
        type = GameManager.I.cardsSO.supplyCardTypesDict["Bow"];
        manaCost = 4;
        duration = 6;
        cooldown = 10;
        cardName = "Basic Bow";
    }
}
