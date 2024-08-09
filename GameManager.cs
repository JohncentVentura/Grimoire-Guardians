using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //Only instance of this class to become a singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Header("Scriptable Objects")]
    public CardsData cardsData; //Initialized in inspector, contains card objects prefabs, card categories, card types

    private PlayerData playerData;

    private void Start()
    {   
        playerData = PlayerManager.Instance.playerData;
        InitPlayerData();
    }

    private void InitPlayerData(){
        playerData.worldPosition = Vector2.zero;
        playerData.maxHealth = 10;
        playerData.currentHealth = playerData.maxHealth;
        playerData.maxMana = 10;
        playerData.currentMana = playerData.maxMana;
        playerData.manaRegeneration = 0.01f; //0.001f;
        //playerData.moveSpeed = 60;
        playerData.playerDirection = Vector2.right;

        //playerData.deck = new List<Card>();
        playerData.deck.Insert(0, cardsData.blazeBall);
        playerData.deck.Insert(1, cardsData.bird);
        playerData.deck.Insert(2, cardsData.simpleSword);
        playerData.deck.Insert(3, cardsData.beginnersBow);

        playerData.handCards = new List<Card>();
        for (int i = 0; i < playerData.handSize; i++)
        {
            playerData.handCards.Insert(i, playerData.deck[i]);
            playerData.handCards[i].InitCard();
        }

        playerData.activeCreatures = new List<Creature>();
        playerData.activeSpells = new List<Spell>();
        playerData.activeWeapon = null;
    }

    public void OnClickLoadScene(string nextSceneName) //Called in StareMenuScene
    {
        SceneManager.LoadScene(nextSceneName);
    }

    #region Input for Testing & Debugging
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerData.deck.Remove(cardsData.beginnersBow);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            playerData.deck = new List<Card>();
            playerData.deck.Insert(0, cardsData.blazeBall);
            playerData.deck.Insert(1, cardsData.bird);
            playerData.deck.Insert(2, cardsData.simpleSword);
            playerData.deck.Insert(3, cardsData.beginnersBow);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            playerData.moveSpeed = 100;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            playerData.moveSpeed = 60;
        }
    }
    #endregion

}