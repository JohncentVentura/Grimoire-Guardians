using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCard : Card
{
    public enum STATES
    {
        IDLE,
        ATTACK
    }
    public STATES state;

    protected override void InitCardCategory()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        category = GameManager.I.cardsSO.cardCategoryDict[CardsSO.TYPES.WEAPON];
    }

    protected void SwitchStates(bool isUsingPhysics)
    {
        switch (state)
        {
            case STATES.IDLE:
                IdleState(isUsingPhysics);
                break;
            case STATES.ATTACK:
                AttackState(isUsingPhysics);
                break;
        }
    }

    public void AnimEventResetState() //Called as an event in animation
    {
        state = STATES.IDLE;
        transform.rotation = Quaternion.identity; //For Bow-type & Staff-type Weapons
    }

    protected virtual void IdleState(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {
            animator.Play("IdleState");
        }
    }

    protected virtual void AttackState(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {
            animator.Play("AttackState");
        }
    }
}