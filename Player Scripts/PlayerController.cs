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
        animator.SetFloat("Blend", GameManager.I.playerSO.playerDirection.x);
        rb = GetComponent<Rigidbody2D>();
        centerPosition = transform.GetChild(0);
        frontHand = centerPosition.transform.GetChild(0);
        backHand = centerPosition.transform.GetChild(1);
        equipPosition = transform.GetChild(1);

        //cinemachineVirtualCamera.Follow = centerPosition.transform;
    }

    public float animSpeed = 1;

    private void Update()
    {       
        animator.speed = animSpeed;
        if(GameManager.I.playerSO.activeWeapon) GameManager.I.playerSO.activeWeapon.animator.speed = animSpeed;
    
        //Input for player movement
        if (InputManager.I.canPlayerInput && InputManager.I.PlayerGetAxisRaw() == Vector2.zero)
        {
            playerStateMachine.state = PlayerStateMachine.STATES.IDLE;
        }
        else if (InputManager.I.canPlayerInput && InputManager.I.PlayerGetAxisRaw() != Vector2.zero)
        {
            //If moving in y-axis only, playerDirection will save the last x-axis so it cannot become 0
            if (InputManager.I.PlayerGetAxisRaw().x != 0)
            {
                GameManager.I.playerSO.playerDirection = InputManager.I.PlayerGetAxisRaw();
                animator.SetFloat("Blend", GameManager.I.playerSO.playerDirection.x);

                if (GameManager.I.playerSO.activeWeapon)
                {
                    GameManager.I.playerSO.activeWeapon.animator.SetFloat("Blend", GameManager.I.playerSO.playerDirection.x);
                }
            }

            playerStateMachine.state = PlayerStateMachine.STATES.MOVE;
        }

        for (int i = 0; i < GameManager.I.playerSO.handSize; i++)
        {
            //Input for playing cards
            InputManager.I.PlayerSetButtonDown(InputManager.I.playerCardKeys[i], () =>
            {
                PlayCard(i);
            });

            CardCooldownTimerTick(i);
            CardDurationTimerTick(i);
        }

        //Input for player attacking
        InputManager.I.PlayerSetButtonDown(InputManager.I.playerAttackKey, () =>
        {
            InputManager.I.canPlayerInput = false;
            if (!GameManager.I.playerSO.activeWeapon)
            {
                InputManager.I.canPlayerInput = true;
            }
            else if (GameManager.I.playerSO.activeWeapon.type == GameManager.I.cardsSO.weaponTypeDict[CardsSO.TYPES.SWORD])
            {
                playerStateMachine.state = PlayerStateMachine.STATES.SWORD_ATK;
                GameManager.I.playerSO.activeWeapon.state = WeaponCard.STATES.ATTACK;
            }
            else if (GameManager.I.playerSO.activeWeapon.type == GameManager.I.cardsSO.weaponTypeDict[CardsSO.TYPES.POLEARM])
            {

            }
            else if (GameManager.I.playerSO.activeWeapon.type == GameManager.I.cardsSO.weaponTypeDict[CardsSO.TYPES.HEAVY])
            {

            }
            else if (GameManager.I.playerSO.activeWeapon.type == GameManager.I.cardsSO.weaponTypeDict[CardsSO.TYPES.BOW])
            {
                GameManager.I.playerSO.activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 135f); //Rotates activeWeapon to properly look at target
                playerStateMachine.state = PlayerStateMachine.STATES.BOW_ATK;
                GameManager.I.playerSO.activeWeapon.state = WeaponCard.STATES.ATTACK;
            }
            else if (GameManager.I.playerSO.activeWeapon.type == GameManager.I.cardsSO.weaponTypeDict[CardsSO.TYPES.STAFF])
            {
                GameManager.I.playerSO.activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 135f); //Rotates activeWeapon to properly look at target
            }
        });

        //Mana Regeneration Timer Tick
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
            if (GameManager.I.playerSO.handCards[i].category == GameManager.I.cardsSO.cardCategoryDict[CardsSO.TYPES.CREATURE])
            {   
                CreatureCard cardObject = Instantiate((CreatureCard)GameManager.I.playerSO.handCards[i]);
                cardObject.transform.parent = transform.parent.Find("Allies");
                cardObject.transform.position = centerPosition.position;
                cardObject.UseCard();
                //cardObject.GetStat(CreatureCard.STATS.HitPoints).currentValue = 1;
            }
            else if (GameManager.I.playerSO.handCards[i].category == GameManager.I.cardsSO.cardCategoryDict[CardsSO.TYPES.SPELL])
            {
                SpellCard cardObject = Instantiate((SpellCard)GameManager.I.playerSO.handCards[i]);
                cardObject.transform.parent = transform.parent.Find("Spells");
                cardObject.transform.position = frontHand.transform.position;
                cardObject.UseCard();
            }
            else if (GameManager.I.playerSO.handCards[i].category == GameManager.I.cardsSO.cardCategoryDict[CardsSO.TYPES.WEAPON])
            {
                //Destroy current activeSupply so that there can only be 1 activeSupply
                if (GameManager.I.playerSO.activeWeapon)
                {
                    Destroy(equipPosition.GetChild(0).gameObject);
                    GameManager.I.playerSO.activeWeapon = null;
                }

                GameManager.I.playerSO.activeWeapon = Instantiate((WeaponCard)GameManager.I.playerSO.handCards[i]);
                Debug.Log("activeWeapon: " + GameManager.I.playerSO.activeWeapon);
                GameManager.I.playerSO.activeWeapon.transform.parent = equipPosition.transform;
                GameManager.I.playerSO.activeWeapon.transform.position = equipPosition.transform.position;
                GameManager.I.playerSO.activeWeapon.animator.SetFloat("Blend", GameManager.I.playerSO.playerDirection.x);
                GameManager.I.playerSO.activeWeapon.state = WeaponCard.STATES.IDLE;
                GameManager.I.playerSO.activeWeapon.UseCard();
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

    public void CardCooldownTimerTick(int i)
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

    public void CardDurationTimerTick(int i)
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
    #endregion

}