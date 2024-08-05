using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    #region "COMPONENTS & STATS"
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    [HideInInspector] public string category;
    [HideInInspector] public string type;
    [HideInInspector] public float manaCost;
    [HideInInspector] public float cooldown;
    [HideInInspector] public float duration;
    [HideInInspector] public string cardName;

    [HideInInspector] public bool isCardUnlock;
    [HideInInspector] public float cooldownTimer;
    [HideInInspector] public float durationTimer;
    #endregion

    #region PUBLIC FUNCTIONS
    public virtual void InitCard()
    {

    }

    public virtual void UseCard()
    {
        //Debug.Log("Player uses a " + manaCost + " Cost " + category + " card called " + cardName);
    }
    #endregion

    #region PROTECTED FUNCTIONS
    protected virtual void InitCardCategory()
    {

    }
    #endregion

}

/*
public override void InitCard()
{

}

public override void UseCard()
{

}
*/