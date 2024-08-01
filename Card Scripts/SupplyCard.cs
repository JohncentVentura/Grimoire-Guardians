using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyCard : Card
{
    protected override void InitCardCategory()
    {
        category = GameManager.I.cardsSO.supplyCategory;
    }
}
