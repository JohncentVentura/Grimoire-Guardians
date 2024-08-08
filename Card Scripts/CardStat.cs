using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStat
{
    public float maxValue;
    public float currentValue;

    public CardStat()
    {

    }

    public float GetMaxValue()
    {
        if (maxValue != 0)
        {
            return maxValue;
        }

        return 0;
    }

    public float GetCurrentValue()
    {
        if (maxValue != 0)
        {
            return currentValue;
        }

        return 0;
    }

    public void SetCurrentValue(float newValue)
    {
        if (maxValue != 0)
        {
            currentValue = newValue;
        }
    }
}

public class HitPointsStat : CardStat
{
    public HitPointsStat(float maxValue)
    {
        this.maxValue = maxValue;
        currentValue = this.maxValue;
    }
}
