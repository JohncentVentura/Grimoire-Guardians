using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSword : Weapon
{
    public override void InitCard()
    {
        InitCardCategory();
        type = GameManager.Instance.cardsData.weaponTypeDict[CardsData.TYPES.SWORD];
        manaCost = 3;
        duration = 13;
        cooldown = 3;
        cardName = "Simple Sword";
    }

    private void Update() => SwitchStates(false);
    private void FixedUpdate() => SwitchStates(true);
    
}