using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour //Inputs & Cards
{
    [HideInInspector] public InputManager inputManager;
    [HideInInspector] public PlayerManager playerManager;
    [HideInInspector] public PlayerData playerData;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Transform centerPosition;
    [HideInInspector] public Transform frontHand; //Starting position of spell card objects
    [HideInInspector] public Transform backHand;
    [HideInInspector] public Transform equipPosition; //Parent & position of supply card objects

    //[Header("UI")]
    //public CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {
        inputManager = InputManager.Instance;
        playerManager = PlayerManager.Instance;
        playerData = playerManager.playerData;
        playerData.state = PlayerData.STATES.IDLE;

        animator = GetComponent<Animator>();
        animator.SetFloat("Blend", playerData.playerDirection.x);
        rb = GetComponent<Rigidbody2D>();
        centerPosition = transform.GetChild(0);
        frontHand = centerPosition.transform.GetChild(0);
        backHand = centerPosition.transform.GetChild(1);
        equipPosition = transform.GetChild(1);

        //cinemachineVirtualCamera.Follow = centerPosition.transform;
    }

    /* Attack Speed
    public float animSpeed = 1;
    animator.speed = animSpeed;
    if (playerData.activeWeapon) playerData.activeWeapon.animator.speed = animSpeed;
    */

    private void Update()
    {
        if (inputManager.canPlayerInput)
        {
            inputManager.playerMoveInput.x = Input.GetAxisRaw(inputManager.playerMovementKeys[0]);
            inputManager.playerMoveInput.y = Input.GetAxisRaw(inputManager.playerMovementKeys[1]);
            inputManager.playerMoveInput = inputManager.playerMoveInput.normalized;

            for (int i = 0; i < playerData.handCards.Count; i++)
            {
                if (Input.GetButtonDown(inputManager.playerCardKeys[i]))
                {
                    PlayCard(i);
                }
                else if (Input.GetButtonDown(inputManager.playerAttackKey))
                {
                    inputManager.canPlayerInput = false;

                    if (!playerData.activeWeapon)
                    {
                        inputManager.canPlayerInput = true;
                    }
                    else if (playerData.activeWeapon.type == GameManager.Instance.cardsData.weaponTypeDict[CardsData.TYPES.SWORD])
                    {
                        playerData.state = PlayerData.STATES.SWORD_ATK;
                        playerData.activeWeapon.state = Weapon.STATES.ATTACK;
                    }
                    else if (playerData.activeWeapon.type == GameManager.Instance.cardsData.weaponTypeDict[CardsData.TYPES.POLEARM])
                    {

                    }
                    else if (playerData.activeWeapon.type == GameManager.Instance.cardsData.weaponTypeDict[CardsData.TYPES.HEAVY])
                    {

                    }
                    else if (playerData.activeWeapon.type == GameManager.Instance.cardsData.weaponTypeDict[CardsData.TYPES.BOW])
                    {
                        playerData.activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 135f); //Rotates activeWeapon to properly look at target
                        playerData.state = PlayerData.STATES.BOW_ATK;
                        playerData.activeWeapon.state = Weapon.STATES.ATTACK;
                    }
                    else if (playerData.activeWeapon.type == GameManager.Instance.cardsData.weaponTypeDict[CardsData.TYPES.STAFF])
                    {
                        playerData.activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 135f); //Rotates activeWeapon to properly look at target
                    }
                }
                else if (inputManager.playerMoveInput != Vector2.zero)
                {
                    //If moving in y-axis only, playerDirection will save the last x-axis so it cannot become 0
                    if (inputManager.playerMoveInput.x != 0)
                    {
                        playerData.playerDirection = inputManager.playerMoveInput;
                        animator.SetFloat("Blend", playerData.playerDirection.x);

                        if (playerData.activeWeapon)
                        {
                            playerData.activeWeapon.animator.SetFloat("Blend", playerData.playerDirection.x);
                        }
                    }

                    playerData.state = PlayerData.STATES.MOVE;
                }
                else if (inputManager.playerMoveInput == Vector2.zero)
                {
                    playerData.state = PlayerData.STATES.IDLE;
                }
            }
        }

        StateMachine(false);
    }

    private void FixedUpdate() => StateMachine(true);

    private void LateUpdate()
    {
        //Some playerController properties are being animated, we can only override those properties in LateUpdate()
        if (playerData.state == PlayerData.STATES.BOW_ATK || playerData.state == PlayerData.STATES.STAFF_ATK)
        {
            //Follow Mouse Cursor
            float maxDistance = 0.15f;
            Vector3 mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDistance = Vector2.ClampMagnitude(mouseDirection - centerPosition.transform.position, maxDistance);
            equipPosition.transform.position = mouseDistance + centerPosition.transform.position;

            //Rotate to Mouse Cursor
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(centerPosition.transform.position);
            mousePos.x -= objectPos.x;
            mousePos.y -= objectPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            equipPosition.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    #region TODO: Transfer to PlayerManager
    public void PlayCard(int i)
    {
        if (playerData.currentMana >= playerData.handCards[i].manaCost && playerData.handCards[i].cooldownTimer <= 0)
        {
            if (playerData.handCards[i].category == GameManager.Instance.cardsData.cardCategoryDict[CardsData.TYPES.CREATURE])
            {
                Creature cardObject = Instantiate((Creature)playerData.handCards[i]);
                cardObject.transform.parent = transform.parent.Find("Allies");
                cardObject.transform.position = centerPosition.position;
                cardObject.UseCard();
                //cardObject.GetStat(CreatureCard.STATS.HitPoints).currentValue = 1;
            }
            else if (playerData.handCards[i].category == GameManager.Instance.cardsData.cardCategoryDict[CardsData.TYPES.SPELL])
            {
                Spell cardObject = Instantiate((Spell)playerData.handCards[i]);
                cardObject.transform.parent = transform.parent.Find("Spells");
                cardObject.transform.position = frontHand.transform.position;
                cardObject.UseCard();
            }
            else if (playerData.handCards[i].category == GameManager.Instance.cardsData.cardCategoryDict[CardsData.TYPES.WEAPON])
            {
                //Destroy current activeSupply so that there can only be 1 activeSupply
                if (playerData.activeWeapon)
                {
                    Destroy(equipPosition.GetChild(0).gameObject);
                    playerData.activeWeapon = null;
                }

                playerData.activeWeapon = Instantiate((Weapon)playerData.handCards[i]);
                Debug.Log("activeWeapon: " + playerData.activeWeapon);
                playerData.activeWeapon.transform.parent = equipPosition.transform;
                playerData.activeWeapon.transform.position = equipPosition.transform.position;
                playerData.activeWeapon.animator.SetFloat("Blend", playerData.playerDirection.x);
                playerData.activeWeapon.state = Weapon.STATES.IDLE;
                playerData.activeWeapon.UseCard();
            }

            playerData.currentMana -= playerData.handCards[i].manaCost;
            playerData.handCards[i].cooldownTimer = playerData.handCards[i].cooldown;
            playerData.handCards[i].durationTimer = playerData.handCards[i].duration;
        }
        else if (playerData.currentMana < playerData.handCards[i].manaCost)
        {
            Debug.Log("Mana is not enough");
        }
        else if (playerData.handCards[i].cooldownTimer > 0)
        {
            Debug.Log(playerData.handCards[i].cardName + " is on cooldown");
        }
    }
    #endregion

    private void StateMachine(bool isUsingPhysics)
    {
        switch (playerData.state)
        {
            case PlayerData.STATES.IDLE:
                IdleState(isUsingPhysics);
                break;
            case PlayerData.STATES.MOVE:
                MoveState(isUsingPhysics);
                break;
            case PlayerData.STATES.CAST_SPELL:
                CastSpell(isUsingPhysics);
                break;
            case PlayerData.STATES.SUMMON_CREATURE:
                SummonCreature(isUsingPhysics);
                break;
            case PlayerData.STATES.SWORD_ATK:
                SwordAttack(isUsingPhysics);
                break;
            case PlayerData.STATES.POLEARM_ATK:
                PolearmAttack(isUsingPhysics);
                break;
            case PlayerData.STATES.HEAVY_ATK:
                HeavyAttack(isUsingPhysics);
                break;
            case PlayerData.STATES.BOW_ATK:
                BowAttack(isUsingPhysics);
                break;
            case PlayerData.STATES.STAFF_ATK:
                StaffAttack(isUsingPhysics);
                break;
        }
    }

    public void AnimEventResetState() //Called as an event in animation
    {
        playerData.state = PlayerData.STATES.IDLE;
        equipPosition.rotation = Quaternion.identity; //For Bow-type & Staff-type Weapons
        inputManager.canPlayerInput = true;
    }

    private void IdleState(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {
            animator.Play("IdleState");
        }
    }

    private void MoveState(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = playerData.moveSpeed * Time.fixedDeltaTime * inputManager.playerMoveInput;
        }
        else //Called in Update()
        {
            animator.Play("MoveState");
        }
    }

    private void CastSpell(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {

        }
    }

    private void SummonCreature(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {

        }
    }

    private void SwordAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {
            animator.Play("SwordATKState");
        }
    }

    private void PolearmAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {

        }
    }

    private void HeavyAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {

        }
    }

    private void BowAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {
            animator.Play("BowATKState");
        }
    }

    private void StaffAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {

        }
    }

}