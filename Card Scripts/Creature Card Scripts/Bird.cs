using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bird : CreatureCard
{
    private void Start()
    {
        //InitCard();
    }

    public override void InitCard()
    {
        InitCardCategory();
        manaCost = 3;
        duration = 6;
        cooldown = 4;
        cardName = "Bird";

        cardStatList.Insert(0, new HitPointsStat(10f));
        Debug.Log("HitPoints: "+GetStat(STATS.HitPoints).maxValue);
        Debug.Log("HitPoints: "+GetStat(STATS.HitPoints).currentValue);
    }

}