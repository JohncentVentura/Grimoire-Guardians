using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour //Inputs & Cards
{
    [Header("Player Components & Children")]
    [HideInInspector] public PlayerStateMachine playerStateMachine;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Transform centerPosition;
    [HideInInspector] public Transform frontHand; //Starting position of spell card objects
    [HideInInspector] public Transform backHand;
    [HideInInspector] public Transform equipPosition; //Parent & position of supply card objects

    //[Header("Utilities")]
    //public CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
        playerStateMachine.state = PlayerStateMachine.STATES.IDLE;
        animator = GetComponent<Animator>();
        animator.SetFloat("PlayerDirection", GameManager.I.playerSO.playerDirection.x);
        rb = GetComponent<Rigidbody2D>();
        centerPosition = transform.GetChild(0);
        frontHand = centerPosition.transform.GetChild(0);
        backHand = centerPosition.transform.GetChild(1);
        equipPosition = transform.GetChild(1);

        //cinemachineVirtualCamera.Follow = centerPosition.transform;
    }

    private void Update()
    {
        //Input for player movement
        if (InputManager.I.canPlayerInput && InputManager.I.PlayerGetAxisRaw() != Vector2.zero)
        {
            if (InputManager.I.PlayerGetAxisRaw().x != 0)
            {
                GameManager.I.playerSO.playerDirection = InputManager.I.PlayerGetAxisRaw();
                animator.SetFloat("PlayerDirection", GameManager.I.playerSO.playerDirection.x);

                if (GameManager.I.playerSO.activeSupply)
                {
                    GameManager.I.playerSO.activeSupply.animator.SetFloat("PlayerDirection", GameManager.I.playerSO.playerDirection.x);
                }
            }

            playerStateMachine.state = PlayerStateMachine.STATES.MOVE;
        }
        else if (InputManager.I.canPlayerInput && InputManager.I.PlayerGetAxisRaw() == Vector2.zero)
        {
            playerStateMachine.state = PlayerStateMachine.STATES.IDLE;
        }

        //Input for player attacking
        InputManager.I.PlayerSetButtonDown(InputManager.I.playerAttackKey, () =>
        {
            InputManager.I.canPlayerInput = false;
            if (GameManager.I.playerSO.activeSupply == null)
            {
                Debug.Log("Player can only attack if equipped with a supply card");
                InputManager.I.canPlayerInput = true;
            }
            else if (GameManager.I.playerSO.activeSupply.type == GameManager.I.cardsSO.supplyCardTypesDict["Sword"])
            {
                playerStateMachine.state = PlayerStateMachine.STATES.SWORD_ATK;
                GameManager.I.playerSO.activeSupply.state = SupplyCard.STATES.ATTACK;
            }
            else if (GameManager.I.playerSO.activeSupply.type == GameManager.I.cardsSO.supplyCardTypesDict["Heavy"])
            {

            }
            else if (GameManager.I.playerSO.activeSupply.type == GameManager.I.cardsSO.supplyCardTypesDict["Bow"])
            {

            }
            else if (GameManager.I.playerSO.activeSupply.type == GameManager.I.cardsSO.supplyCardTypesDict["Staff"])
            {

            }
        });

        for (int i = 0; i < GameManager.I.playerSO.handSize; i++)
        {
            //Input for playing cards
            InputManager.I.PlayerSetButtonDown(InputManager.I.playerCardKeys[i], () =>
            {
                PlayCard(i);
            });

            //Cooldown timer
            CardCooldownTimer(i);

            //Duration timer
            CardDurationTimer(i);
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

    #region CARDS
    public void PlayCard(int i)
    {
        if (GameManager.I.playerSO.currentMana >= GameManager.I.playerSO.handCards[i].manaCost && GameManager.I.playerSO.handCards[i].cooldownTimer <= 0)
        {
            if (GameManager.I.playerSO.handCards[i].category == GameManager.I.cardsSO.cardCategoriesDict["Spell"])
            {
                SpellCard cardObject = Instantiate((SpellCard)GameManager.I.playerSO.handCards[i]);
                cardObject.transform.parent = transform.parent.Find("Spells");
                cardObject.transform.position = frontHand.transform.position;
                cardObject.UseCard();
            }
            else if (GameManager.I.playerSO.handCards[i].category == GameManager.I.cardsSO.cardCategoriesDict["Summon"])
            {
                SummonCard cardObject = Instantiate((SummonCard)GameManager.I.playerSO.handCards[i]);
                cardObject.transform.parent = transform.parent.Find("Summons");
                cardObject.transform.position = centerPosition.position;
                cardObject.UseCard();
            }
            else if (GameManager.I.playerSO.handCards[i].category == GameManager.I.cardsSO.cardCategoriesDict["Supply"])
            {
                //Destroy current activeSupply so that there can only be 1 activeSupply
                if (GameManager.I.playerSO.activeSupply)
                {
                    Destroy(equipPosition.GetChild(0).gameObject);
                    GameManager.I.playerSO.activeSupply = null;
                }

                SupplyCard cardObject = Instantiate((SupplyCard)GameManager.I.playerSO.handCards[i]);
                GameManager.I.playerSO.activeSupply = cardObject;
                Debug.Log("activeSupply: " + GameManager.I.playerSO.activeSupply);
                cardObject.transform.parent = equipPosition.transform;
                cardObject.transform.position = equipPosition.transform.position;
                GameManager.I.playerSO.activeSupply.animator.SetFloat("PlayerDirection", GameManager.I.playerSO.playerDirection.x);
                GameManager.I.playerSO.activeSupply.state = SupplyCard.STATES.IDLE;
                cardObject.UseCard();
            }

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

    public void CardCooldownTimer(int i)
    {
        if (GameManager.I.playerSO.handCards[i].cooldownTimer > 0)
        {
            GameManager.I.playerSO.handCards[i].cooldownTimer -= Time.deltaTime;
        }
        else if (GameManager.I.playerSO.handCards[i].cooldownTimer <= 0)
        {
            GameManager.I.playerSO.handCards[i].cooldownTimer = 0;
        }
    }

    public void CardDurationTimer(int i)
    {
        if (GameManager.I.playerSO.handCards[i])
        {
            if (GameManager.I.playerSO.handCards[i].durationTimer > 0)
            {
                GameManager.I.playerSO.handCards[i].durationTimer -= Time.deltaTime;
            }
            else if (GameManager.I.playerSO.handCards[i].durationTimer <= 0)
            {
                GameManager.I.playerSO.handCards[i].durationTimer = 0;
            }
        }
    }
    #endregion

}