using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnersBow : WeaponCard
{
    public override void InitCard()
    {
        InitCardCategory();
        type = GameManager.I.cardsSO.weaponTypeDict[CardsSO.TYPES.BOW];
        manaCost = 4;
        duration = 14;
        cooldown = 4;
        cardName = "Beginner's Bow";
    }
}
