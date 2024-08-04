using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/PlayerSO", order = 0)]
public class PlayerSO : ScriptableObject
{
    [Header("Stats")]
    [HideInInspector] public Vector2 worldPosition;
    [HideInInspector] public float maxHealth;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float maxMana;
    [HideInInspector] public float currentMana;
    [HideInInspector] public float manaRegeneration;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public Vector2 moveDirection;

    [Header("Cards")]
    [HideInInspector] public List<Card> deck;
    [HideInInspector] public List<Card> handCards;
    [HideInInspector] public readonly int handSize = 4;
    [HideInInspector] public List<Card> activeSpells;
    [HideInInspector] public List<Card> activeSummons;
    [HideInInspector] public SupplyCard activeSupply; //Can only equip 1 supply card
}