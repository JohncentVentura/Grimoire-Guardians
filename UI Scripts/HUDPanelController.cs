using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDPanelController : MonoBehaviour
{   
    private PlayerData playerData;

    [Header("UI")]
    [HideInInspector] RectTransform rectTransform;
    [HideInInspector] public Image playerManaBorderImage;
    [HideInInspector] public Image playerManaImage;
    [HideInInspector] public TextMeshProUGUI playerManaText;
    [HideInInspector] public List<CardButton> cardButtons;

    private void Start()
    {   
        playerData = PlayerManager.Instance.playerData;
        rectTransform = GetComponent<RectTransform>();
        playerManaBorderImage = rectTransform.Find("PlayerManaBorderImage").GetComponent<Image>();
        playerManaImage = playerManaBorderImage.GetComponent<RectTransform>().Find("PlayerManaImage").GetComponent<Image>();
        playerManaText = playerManaBorderImage.GetComponent<RectTransform>().Find("PlayerManaText").GetComponent<TextMeshProUGUI>();
        cardButtons = new List<CardButton>();

        for (int i = 0; i < playerData.handSize; i++)
        {
            cardButtons.Insert(i, rectTransform.Find("CardButton (" + i + ")").GetComponent<CardButton>());
            cardButtons[i].InitCardButton();
            cardButtons[i].UpdateCardData(playerData.handCards[i]);
        }
    }

    private void Update()
    {   
        //Player Mana
        playerManaText.text = "" + playerData.currentMana.ToString("F1");

        for (int i = 0; i < playerData.handSize; i++)
        {
            cardButtons[i].UpdateCardData(playerData.handCards[i]);

            //CardButtons CooldownSlider value
            cardButtons[i].cooldownSlider.value = playerData.handCards[i].cooldownTimer;

            //CardButtons CooldownText
            if (playerData.handCards[i].cooldownTimer > 0)
            {
                cardButtons[i].cooldownText.text = "" + playerData.handCards[i].cooldownTimer.ToString("F1"); //F1 means 1 decimal is shown
            }
            else
            {
                cardButtons[i].cooldownText.text = "";
            }

            //CardButtons Transparency
            if (playerData.currentMana < playerData.handCards[i].manaCost || playerData.handCards[i].cooldownTimer > 0)
            {
                cardButtons[i].borderImage.color = new Color(cardButtons[i].borderImage.color.r, cardButtons[i].borderImage.color.g, cardButtons[i].borderImage.color.b, 0.25f);
                cardButtons[i].backgroundImage.color = new Color(cardButtons[i].backgroundImage.color.r, cardButtons[i].backgroundImage.color.g, cardButtons[i].backgroundImage.color.b, 0.25f);
                cardButtons[i].cardImage.color = new Color(cardButtons[i].cardImage.color.r, cardButtons[i].cardImage.color.g, cardButtons[i].cardImage.color.b, 0.25f);
            }
            else if (playerData.currentMana >= playerData.handCards[i].manaCost && playerData.handCards[i].cooldownTimer <= 0)
            {
                cardButtons[i].borderImage.color = new Color(cardButtons[i].borderImage.color.r, cardButtons[i].borderImage.color.g, cardButtons[i].borderImage.color.b, 1);
                cardButtons[i].backgroundImage.color = new Color(cardButtons[i].backgroundImage.color.r, cardButtons[i].backgroundImage.color.g, cardButtons[i].backgroundImage.color.b, 1);
                cardButtons[i].cardImage.color = new Color(cardButtons[i].cardImage.color.r, cardButtons[i].cardImage.color.g, cardButtons[i].cardImage.color.b, 1);
            }
        }
    }
    
}