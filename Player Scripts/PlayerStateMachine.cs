using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour //State & Animations
{
    private PlayerController playerController; //Controls the state
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
    }
    public STATES state;

    private void Start() => playerController = GetComponent<PlayerController>();
    private void Update() => SwitchStates(false);
    private void FixedUpdate() => SwitchStates(true);

    private void SwitchStates(bool isUsingPhysics)
    {
        switch (state)
        {
            case STATES.IDLE:
                IdleState(isUsingPhysics);
                break;
            case STATES.MOVE:
                MoveState(isUsingPhysics);
                break;
            case STATES.CAST_SPELL:
                CastSpell(isUsingPhysics);
                break;
            case STATES.SUMMON_CREATURE:
                SummonCreature(isUsingPhysics);
                break;
            case STATES.SWORD_ATK:
                SwordAttack(isUsingPhysics);
                break;
            case STATES.POLEARM_ATK:
                PolearmAttack(isUsingPhysics);
                break;
            case STATES.HEAVY_ATK:
                HeavyAttack(isUsingPhysics);
                break;
            case STATES.BOW_ATK:
                BowAttack(isUsingPhysics);
                break;
            case STATES.STAFF_ATK:
                StaffAttack(isUsingPhysics);
                break;
        }
    }

    public void ResetState() //An event in animation
    {
        state = STATES.IDLE;
        InputManager.I.canPlayerInput = true;
    }

    private void IdleState(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            playerController.rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {
            playerController.animator.Play("IdleState");
        }
    }

    private void MoveState(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            playerController.rb.velocity = GameManager.I.playerSO.moveSpeed * Time.fixedDeltaTime * InputManager.I.PlayerGetAxisRaw();
        }
        else //Called in Update()
        {
            playerController.animator.Play("MoveState");
        }
    }

    private void CastSpell(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {

        }
        else //Called in Update()
        {

        }
    }

    private void SummonCreature(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {

        }
        else //Called in Update()
        {

        }
    }

    private void SwordAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            playerController.rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {
            playerController.animator.Play("SwordATKState");
        }
    }

    private void PolearmAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {

        }
        else //Called in Update()
        {

        }
    }

    private void HeavyAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {

        }
        else //Called in Update()
        {

        }
    }

    private void BowAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {

        }
        else //Called in Update()
        {

        }
    }

    private void StaffAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {

        }
        else //Called in Update()
        {

        }
    }

}