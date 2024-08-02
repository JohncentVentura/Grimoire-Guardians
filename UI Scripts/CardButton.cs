using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{   
    [HideInInspector] RectTransform rectTransform;
    [HideInInspector] public Image borderImage;
    [HideInInspector] public Image backgroundImage;
    [HideInInspector] public Image cardImage;
    [HideInInspector] public Slider cooldownSlider;
    [HideInInspector] public Image cooldownImage;
    [HideInInspector] public TextMeshProUGUI cooldownText;
    [HideInInspector] public Image manaCostBorderImage;
    [HideInInspector] public Image manaCostImage;
    [HideInInspector] public TextMeshProUGUI manaCostText;
    [HideInInspector] public TextMeshProUGUI keyInputText;

    public void InitCardButton()
    {   
        rectTransform = GetComponent<RectTransform>();
        borderImage = rectTransform.Find("BorderImage").GetComponent<Image>();
        backgroundImage = borderImage.GetComponent<RectTransform>().Find("BackgroundImage").GetComponent<Image>();
        cardImage = borderImage.GetComponent<RectTransform>().Find("CardImage").GetComponent<Image>();
        cooldownSlider = rectTransform.Find("CooldownSlider").GetComponent<Slider>();
        cooldownImage = cooldownSlider.GetComponent<RectTransform>().Find("CooldownImage").GetComponent<Image>();
        cooldownText = cooldownSlider.GetComponent<RectTransform>().Find("CooldownText").GetComponent<TextMeshProUGUI>();
        manaCostBorderImage = rectTransform.Find("ManaCostBorderImage").GetComponent<Image>();
        manaCostImage = manaCostBorderImage.GetComponent<RectTransform>().Find("ManaCostImage").GetComponent<Image>();
        manaCostText = manaCostBorderImage.GetComponent<RectTransform>().Find("ManaCostText").GetComponent<TextMeshProUGUI>();
        keyInputText = rectTransform.Find("KeyInputText").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCardData(Card card)
    {
        /*
        for (int j = 0; j < GameManager.I.cardsSO.cardOrigins.Length; j++)
        {
            if (card.origin == GameManager.I.cardsSO.cardOrigins[j])
            {
                borderImage.sprite = GameManager.I.cardsSO.originImageSprites[j];
            }
        }
        */

        cardImage.sprite = card.spriteRenderer.sprite;
        cooldownSlider.maxValue = card.cooldown;
        cooldownSlider.value = card.cooldownTimer;
        manaCostText.text = "" + card.manaCost.ToString();
    }

}
