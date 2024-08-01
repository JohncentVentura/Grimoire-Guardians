using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCard : Card
{
    protected override void InitCardCategory()
    {   
        category = GameManager.I.cardsSO.spellCategory;
    }
}
