using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeBall : SpellCard
{
    public override void InitCard()
    {
        InitCardCategory();
        manaCost = 1;
        cooldown = 10;
        duration = 6;
        cardName = "Blaze Ball";
    }
    
}
