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
        PLAY_SPELL,
        PLAY_SUMMON,
        SWORD_ATK,
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
            case STATES.PLAY_SPELL:
                PlaySpell(isUsingPhysics);
                break;
            case STATES.PLAY_SUMMON:
                PlaySummon(isUsingPhysics);
                break;
            case STATES.SWORD_ATK:
                SwordAttack(isUsingPhysics);
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

    public void ResetState()
    {
        state = STATES.IDLE;
        InputManager.I.canPlayerInput = true;
    }

    private void IdleState(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {
            playerController.rb.velocity = Vector2.zero * Time.fixedDeltaTime * InputManager.I.PlayerGetAxisRaw();
        }
        else
        {
            playerController.animator.Play("IdleState");
        }
    }

    private void MoveState(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {
            playerController.rb.velocity = GameManager.I.playerSO.moveSpeed * Time.fixedDeltaTime * InputManager.I.PlayerGetAxisRaw();
        }
        else
        {
            playerController.animator.Play("MoveState");
        }
    }

    private void PlaySpell(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {

        }
    }

    private void PlaySummon(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {

        }
    }

    private void SwordAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {
            playerController.animator.Play("SwordATKState");
        }
    }

    private void HeavyAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {

        }
    }

    private void BowAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {

        }
    }

    private void StaffAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {

        }
    }

}