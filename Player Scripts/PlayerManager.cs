using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; } //Only instance of this class to become a singleton
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public PlayerData playerData; //Initialized in script, contains data of player position, stats, cards, & decks

    private void Start()
    {
        
    }

    private void Update()
    {
        for (int i = 0; i < playerData.handSize; i++)
        {
            CardCooldownTimerTick(i);
            CardDurationTimerTick(i);
        }

        //Mana Regeneration Timer Tick
        if (playerData.currentMana < playerData.maxMana)
        {
            playerData.currentMana += playerData.manaRegeneration;
        }
        else if (playerData.currentMana >= playerData.maxMana)
        {
            playerData.currentMana = playerData.maxMana;
        }
    }

    public void CardCooldownTimerTick(int i)
    {
        if (playerData.handCards[i].cooldownTimer > 0)
        {
            playerData.handCards[i].cooldownTimer -= Time.deltaTime;
        }
        else if (playerData.handCards[i].cooldownTimer <= 0)
        {
            playerData.handCards[i].cooldownTimer = 0;
        }
    }

    public void CardDurationTimerTick(int i)
    {
        if (playerData.handCards[i].durationTimer > 0)
        {
            playerData.handCards[i].durationTimer -= Time.deltaTime;
        }
        else if (playerData.handCards[i].durationTimer <= 0)
        {
            playerData.handCards[i].durationTimer = 0;
        }
    }
}