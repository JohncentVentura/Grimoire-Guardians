using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CardsSO", menuName = "ScriptableObjects/CardsSO", order = 1)]
public class CardsSO : ScriptableObject
{
    #region CARD_STATS
    //Card Categories
    [HideInInspector] public readonly string spellCategory = "Spell";
    [HideInInspector] public readonly string summonCategory = "Summon";
    [HideInInspector] public readonly string supplyCategory = "Supply";
    [HideInInspector] public readonly string skillCategory = "Skill";
    //Card Origin
    [HideInInspector] public readonly string overworldOrigin = "Overworld";
    [HideInInspector] public readonly string underworldOrigin = "Underworld";
    [HideInInspector] public readonly string worldkingdomOrigin = "Worldkingdom";
    [HideInInspector] public readonly string worldTreeOrigin = "Worldtree";
    [HideInInspector] public readonly string[] cardOrigins = { "Overworld", "Underworld", "Worldkingdom", "Worldtree" };
    public List<Sprite> originImageSprites;
    #endregion

    [Header("SPELL CARDS")]
    public SpellCard blazeBall;

    [Header("SUMMON CARDS")]
    public SummonCard bird;

    [Header("SUPPLY CARDS")]
    public SupplyCard basicBlade;
    public SupplyCard basicBow;

    [Header("SKILL CARDS")]
    public SkillCard snowBall;
    public SkillCard simpleSlime;
}