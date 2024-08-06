using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I { get; private set; } //I means Instance since this class is a singleton
    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    [Header("Scriptable Objects")]
    public PlayerSO playerSO; //Initialized in script, contains data of player position, stats, cards, & decks
    public CardsSO cardsSO; //Initialized in inspector, contains card objects prefabs, card types, & card elements

    private void Start()
    {
        //TEMPORARY INITIALIZATION OF PLAYERSO
        playerSO.worldPosition = Vector2.zero;
        playerSO.maxHealth = 10;
        playerSO.currentHealth = playerSO.maxHealth;
        playerSO.maxMana = 10;
        playerSO.currentMana = playerSO.maxMana;
        playerSO.manaRegeneration = 0.01f; //0.001f;
        playerSO.moveSpeed = 60;
        playerSO.playerDirection = Vector2.right;

        playerSO.deck = new List<Card>();
        playerSO.deck.Insert(0, cardsSO.blazeBall);
        playerSO.deck.Insert(1, cardsSO.bird);
        playerSO.deck.Insert(2, cardsSO.simpleSword);
        playerSO.deck.Insert(3, cardsSO.beginnersBow);

        playerSO.handCards = new List<Card>();
        for (int i = 0; i < playerSO.handSize; i++)
        {
            playerSO.handCards.Insert(i, playerSO.deck[i]);
            playerSO.handCards[i].InitCard();
        }

        playerSO.activeSpells = new List<SpellCard>();
        playerSO.activeCreatures = new List<CreatureCard>();
        playerSO.activeWeapon = null;
    }

    public void LoadNextScene(string nextSceneName)
    {
        SceneManager.LoadScene(nextSceneName);
    }

}