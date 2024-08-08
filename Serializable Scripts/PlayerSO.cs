using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/PlayerSO", order = 0)]
public class PlayerSO : ScriptableObject
{
    [Header("Stats")]
    public Vector2 worldPosition;
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public float manaRegeneration;
    public float moveSpeed;
    public Vector2 playerDirection; //Always has a value, cannot become Vector2.Zero

    [Header("Cards")]
    public List<Card> deck;
    public List<Card> handCards;
    public readonly int handSize = 4;
    public List<CreatureCard> activeCreatures; //Creature cards that are Instantiated
    public List<SpellCard> activeSpells; //Spell cards that are Instantiated
    public WeaponCard activeWeapon; //ONLY 1 Weapon card can be Instantiated
}