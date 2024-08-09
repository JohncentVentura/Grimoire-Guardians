using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Creature : Card
{
    public enum STATS
    {
        HitPoints,
        AttackDamage,
        AttackRange,
        AttackSpeed,
        MovementSpeed
    }

    public List<CardStat> cardStatList;
    public CardStat GetStat(STATS stat) => cardStatList[(int)stat];

    protected override void InitCardCategory()
    {
        cardStatList = new List<CardStat>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        category = GameManager.Instance.cardsData.cardCategoryDict[CardsData.TYPES.CREATURE];
    }

}