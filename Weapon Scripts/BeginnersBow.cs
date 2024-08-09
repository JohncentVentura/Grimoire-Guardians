using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginnersBow : Weapon
{
    public override void InitCard()
    {
        InitCardCategory();
        type = GameManager.Instance.cardsData.weaponTypeDict[CardsData.TYPES.BOW];
        manaCost = 4;
        duration = 14;
        cooldown = 4;
        cardName = "Beginner's Bow";
    }

    private void Update() => SwitchStates(false);
    private void FixedUpdate() => SwitchStates(true);
    
}