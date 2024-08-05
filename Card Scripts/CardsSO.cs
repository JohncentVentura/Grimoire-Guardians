using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsSO", menuName = "ScriptableObjects/CardsSO", order = 1)]
public class CardsSO : ScriptableObject
{
    #region CARD_STATS
    [HideInInspector] public readonly Dictionary<string, string> cardCategoriesDict = new() { { "Spell", "Spell" }, { "Summon", "Summon" }, { "Supply", "Supply" } };
    [HideInInspector] public readonly Dictionary<string, string> spellCardTypesDict = new() { { "spell", "Spell" }, { "summon", "Summon" }, { "supply", "Supply" } };
    [HideInInspector] public readonly Dictionary<string, string> summonCardTypesDict = new() { { "spell", "Spell" }, { "summon", "Summon" }, { "supply", "Supply" } };
    [HideInInspector] public readonly Dictionary<string, string> supplyCardTypesDict 
        = new() { { "Sword", "Sword" }, { "Heavy", "Heavy" }, { "Bow", "Bow" }, { "Staff", "Staff" }, { "Polearm", "Polearm" } };
    
    #endregion

    [Header("SPELL CARDS")]
    public SpellCard blazeBall;
    public SpellCard snowBall;

    [Header("SUMMON CARDS")]
    public SummonCard bird;
    public SummonCard simpleSlime;

    [Header("SUPPLY CARDS")]
    public SupplyCard basicBlade;
    public SupplyCard basicBow;

}