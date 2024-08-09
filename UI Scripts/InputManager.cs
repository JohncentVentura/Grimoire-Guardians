using System;
using UnityEngine;

//Also manages inputts depending if playing on windows, console, or mobile 
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; } //Only instance of this class to become a singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [HideInInspector] public string[] playerMovementKeys;
    [HideInInspector] public string[] playerCardKeys;
    [HideInInspector] public string playerAttackKey;

    [HideInInspector] public bool canPlayerInput;
    [HideInInspector] public Vector2 playerMoveInput;

    //[HideInInspector] public Vector2 playerMoveInput;
    //[HideInInspector] public string[] cardButtonInputs;

    private void Start()
    {
        playerMovementKeys = new string[] { "Horizontal", "Vertical" };
        playerCardKeys = new string[] { "Fire1", "Fire2", "Fire3", "Fire4" };
        playerAttackKey = "Fire5";
        canPlayerInput = true;
        playerMoveInput = Vector2.zero;
    }

    public void PlayerInvokeButtonDown(string input, Action callback)
    {
        if (canPlayerInput && Input.GetButtonDown(input))
        {
            callback?.Invoke();
        }
    }

}