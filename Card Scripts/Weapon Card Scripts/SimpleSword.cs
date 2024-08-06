using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSword : WeaponCard
{
    public override void InitCard()
    {
        InitCardCategory();
        type = GameManager.I.cardsSO.weaponTypeDict[CardsSO.TYPES.SWORD];
        manaCost = 3;
        duration = 13;
        cooldown = 3;
        cardName = "Simple Sword";
    }

    private void Update() => SwitchStates(false);
    private void FixedUpdate() => SwitchStates(true);
}
