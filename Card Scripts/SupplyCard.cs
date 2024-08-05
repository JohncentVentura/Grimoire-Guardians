using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyCard : Card
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
        category = GameManager.I.cardsSO.cardCategoriesDict["Supply"];
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

    public void ResetState() => state = STATES.IDLE;
    
    protected void IdleState(bool isUsingPhysics)
    {
        if (isUsingPhysics)
        {

        }
        else
        {
            animator.Play("IdleState");
        }
    }

    protected void AttackState(bool isUsingPhysics)
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