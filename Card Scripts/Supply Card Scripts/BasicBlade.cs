using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBlade : SupplyCard
{
    public override void InitCard()
    {
        InitCardCategory();
        type = GameManager.I.cardsSO.supplyCardTypesDict["Sword"];
        manaCost = 3;
        duration = 6;
        cooldown = 3;
        cardName = "Basic Blade";
    }

    private void Update() => SwitchStates(false);
    private void FixedUpdate() => SwitchStates(true);
}
