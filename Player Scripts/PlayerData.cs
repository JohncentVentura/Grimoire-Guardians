using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 0)]

public class PlayerData : ScriptableObject
{
    [Header("Stats")]
    public Vector2 worldPosition;
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public float manaRegeneration;
    public float moveSpeed;
    public Vector2 playerDirection = Vector2.right;

    public enum STATES
    {
        IDLE,
        MOVE,
        SUMMON_CREATURE,
        CAST_SPELL,
        SWORD_ATK,
        POLEARM_ATK,
        HEAVY_ATK,
        BOW_ATK,
        STAFF_ATK,
        STUNNED,
    }
    public STATES state;

    [Header("Cards")]
    public List<Card> deck;
    public List<Card> handCards;
    public readonly int handSize = 4;
    public List<Creature> activeCreatures; //Creature cards that are Instantiated
    public List<Spell> activeSpells; //Spell cards that are Instantiated
    public Weapon activeWeapon; //ONLY 1 Weapon card can be Instantiated

}