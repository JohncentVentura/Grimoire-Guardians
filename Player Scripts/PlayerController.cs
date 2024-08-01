using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components & Children")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Transform centerPosition;
    [HideInInspector] public Transform frontHand; //Starting position of spell card objects
    [HideInInspector] public Transform backHand; //Parent & position of supply card objects
    [HideInInspector] public Transform summonPosition; //Starting position of summon card objects

    [Header("PlayerController & Inputs")]
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Vector2 moveDirection;

    //[Header("Utilities")]
    //public CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        centerPosition = transform.GetChild(0);
        frontHand = centerPosition.transform.GetChild(0);
        backHand = centerPosition.transform.GetChild(1);
        summonPosition = transform.GetChild(1);

        moveInput = Vector2.zero;
        moveDirection = Vector2.right;

        //cinemachineVirtualCamera.Follow = centerPosition.transform;
    }

    private void Update()
    {
        //Input for player movement
        moveInput = InputManager.I.PlayerGetAxisRaw();

        //Input for player attacking
        InputManager.I.PlayerSetButtonDown(InputManager.I.playerAttackKey, () =>
        {
            Debug.Log("Player Attack");
        });

        //Input for playing cards
        for (int i = 0; i < GameManager.I.playerSO.handSize; i++)
        {
            InputManager.I.PlayerSetButtonDown(InputManager.I.playerCardKeys[i], () =>
            {
                PlayCard(i);
            });
        }

        //Cooldown timer & Duration timer of cards in deck
        for (int i = 0; i < GameManager.I.playerSO.deck.Count; i++)
        {
            CardCooldownTimerCountdown(i);
            CardDurationTimerCountdown(i);
        }

        //Mana regeneration timer
        if (GameManager.I.playerSO.currentMana < GameManager.I.playerSO.maxMana)
        {
            GameManager.I.playerSO.currentMana += GameManager.I.playerSO.manaRegeneration;
        }
        else if (GameManager.I.playerSO.currentMana >= GameManager.I.playerSO.maxMana)
        {
            GameManager.I.playerSO.currentMana = GameManager.I.playerSO.maxMana;
        }
    }

    private void FixedUpdate()
    {
        //Player movement
        if (moveInput == Vector2.zero)
        {
            rb.velocity = Vector2.zero * Time.deltaTime * moveInput;
        }
        else if (moveInput != Vector2.zero)
        {
            rb.velocity = GameManager.I.playerSO.moveSpeed * Time.deltaTime * moveInput;
            GameManager.I.playerSO.moveDirection = moveInput;
        }
    }

    #region TODO:_TRANSFER_FUNC_BELOW_TO_CARDS
    public void PlayCard(int i)
    {
        if (GameManager.I.playerSO.currentMana >= GameManager.I.playerSO.handCards[i].manaCost && GameManager.I.playerSO.handCards[i].cooldownTimer <= 0)
        {
            Card cardObject = Instantiate(GameManager.I.playerSO.handCards[i]);

            if (cardObject.category == GameManager.I.cardsSO.spellCategory)
            {
                cardObject.transform.parent = transform.parent;
                cardObject.transform.position = frontHand.transform.position;
            }
            else if (cardObject.category == GameManager.I.cardsSO.summonCategory)
            {
                cardObject.transform.parent = transform.parent;
                cardObject.transform.position = summonPosition.transform.position;
            }
            else if (cardObject.category == GameManager.I.cardsSO.supplyCategory)
            {
                cardObject.transform.parent = backHand.transform;
                cardObject.transform.position = backHand.transform.position;
            }

            cardObject.UseCard();
            GameManager.I.playerSO.currentMana -= GameManager.I.playerSO.handCards[i].manaCost;
            GameManager.I.playerSO.handCards[i].cooldownTimer = GameManager.I.playerSO.handCards[i].cooldown;
            GameManager.I.playerSO.handCards[i].durationTimer = GameManager.I.playerSO.handCards[i].duration;
        }
        else if (GameManager.I.playerSO.currentMana < GameManager.I.playerSO.handCards[i].manaCost)
        {
            Debug.Log("Mana is not enough");
        }
        else if (GameManager.I.playerSO.handCards[i].cooldownTimer > 0)
        {
            Debug.Log(GameManager.I.playerSO.handCards[i].cardName + " is on cooldown");
        }
    }

    public void CardCooldownTimerCountdown(int i)
    {
        if (GameManager.I.playerSO.deck[i].cooldownTimer > 0)
        {
            GameManager.I.playerSO.deck[i].cooldownTimer -= Time.deltaTime;
        }
        else if (GameManager.I.playerSO.deck[i].cooldownTimer < 0)
        {
            GameManager.I.playerSO.deck[i].cooldownTimer = 0;
        }
    }

    public void CardDurationTimerCountdown(int i)
    {
        if (GameManager.I.playerSO.deck[i].durationTimer > 0)
        {
            GameManager.I.playerSO.deck[i].durationTimer -= Time.deltaTime;
        }
        else if (GameManager.I.playerSO.deck[i].durationTimer < 0)
        {
            GameManager.I.playerSO.deck[i].durationTimer = 0;
        }
    }
    #endregion
}