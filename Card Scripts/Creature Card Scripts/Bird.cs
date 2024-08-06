using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : CreatureCard
{
    public override void InitCard()
    {
        InitCardCategory();
        manaCost = 3;
        duration = 6;
        cooldown = 4;
        cardName = "Bird";
    }
}
