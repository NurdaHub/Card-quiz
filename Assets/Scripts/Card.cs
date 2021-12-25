using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Button cardButton;

    private CardData cardData;
    private bool isCorrect;
    private float scaleDuration = 0.8f;
    private float shakeDuration = 0.5f;
    private float shakeStrength = 8f;
    private float shakeRandomness = 1f;
    private int shakeVibrato = 10;

    public void Init(CardData _cardData, bool _isCorrect, bool isStart)
    {
        cardData = _cardData;
        isCorrect = _isCorrect;
        cardImage.sprite = cardData.sprite;
        cardButton.onClick.AddListener(CheckCard);
        ScaleCard(isStart);
    }
    
    private void ScaleCard(bool isStart)
    {
        if(isStart)
            transform.DOScale(1f, scaleDuration).SetEase(Ease.OutBack);
        else
            transform.localScale = Vector3.one;
    }

    private void CheckCard()
    {
        if(!isCorrect)
            ShakeImage();
    }
    
    private void ShakeImage()
    {
        cardImage.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness, true, false).SetEase(Ease.InBounce);
    }

    public Image GetImage()
    {
        return cardImage;
    }

    public Button GetButton()
    {
        return cardButton;
    }
    
    private void OnDisable()
    {
        cardButton.onClick.RemoveListener(CheckCard);
    }
}