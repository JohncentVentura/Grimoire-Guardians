using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : SpellCard
{
    public override void InitCard()
    {
        InitCardCategory();
        manaCost = 1;
        cooldown = 10;
        duration = 6;
        cardName = "Snow Ball";
    }
}
