using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager I { get; private set; } //I means Instance since this class is a singleton
    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    [HideInInspector] public string[] playerMovementKeys;
    [HideInInspector] public string playerAttackKey;
    [HideInInspector] public string[] playerCardKeys;

    [HideInInspector] public bool canPlayerInput;
    //[HideInInspector] public Vector2 playerMoveInput;
    //[HideInInspector] public string[] cardButtonInputs;

    private void Start()
    {
        playerMovementKeys = new string[] { "Horizontal", "Vertical" };
        playerAttackKey = "Fire5";
        playerCardKeys = new string[] { "Fire1", "Fire2", "Fire3", "Fire4" };

        canPlayerInput = true;
    }

    public Vector2 PlayerGetAxisRaw()
    {
        if (canPlayerInput)
        {
            return new Vector2(Input.GetAxisRaw(playerMovementKeys[0]), Input.GetAxisRaw(playerMovementKeys[1])).normalized;
        }

        return Vector2.zero;
    }

    public void PlayerSetButtonDown(string input, Action callback)
    {
        if (canPlayerInput && Input.GetButtonDown(input))
        {   
            callback?.Invoke();
        }
    }
    
}