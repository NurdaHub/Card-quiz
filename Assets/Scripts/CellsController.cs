using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class CellsController : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private TaskTextController taskTextController;
    [SerializeField] private ParticleSystem starParticleSystem;
    
    private CardData[] cardDataList;
    private List<GameObject> currentGOList = new List<GameObject>();

    private Vector3 scaleEndValue = new Vector3(1.15f, 1.15f, 1f);
    private float scaleDuration = 0.7f;

    public Action onRightButtonCliked;

    public void Init(CardBundleData cardBundleData, bool isStart, int rowCount)
    {
        ClearCells();
        cardDataList = cardBundleData.cardDatas;
        MixCards();
        
        var correctCardIndex = UnityEngine.Random.Range(0, rowCount);

        for (int i = 0; i < rowCount; i++)
        {
            var cardGO = Instantiate(cardPrefab, transform);
            var cardScript = cardGO.GetComponent<Card>();
            var isCorrect = correctCardIndex == i;

            cardScript.Init(cardDataList[i], isCorrect, isStart);

            if (isCorrect)
                cardScript.GetButton().onClick.AddListener(() => RightButtonClicked(cardScript.GetImage()));
            
            currentGOList.Add(cardGO);
        }

        taskTextController.TaskInit(cardDataList[correctCardIndex].identifier);
    }

    public void ActivateTaskText(bool isActive)
    {
        taskTextController.gameObject.SetActive(isActive);
    }

    private void MixCards()
    {
        var random = new System.Random();
        
        for (int i = cardDataList.Length - 1; i >= 1; i--)
        {
            int randomNumber = random.Next(i + 1);
            var tempCardData = cardDataList[randomNumber];
            cardDataList[randomNumber] = cardDataList[i];
            cardDataList[i] = tempCardData;
        }
    }

    private void RightButtonClicked(Image cardImage)
    {
        starParticleSystem.transform.position = cardImage.transform.position;
        starParticleSystem.Play();
        
        cardImage.transform.DOScale(scaleEndValue, scaleDuration).SetEase(Ease.OutBack).OnComplete(delegate
        {
            onRightButtonCliked?.Invoke();
        });
    }

    private void ClearCells()
    {
        for (int i = 0; i < currentGOList.Count; i++)
            Destroy(currentGOList[i]);

        foreach (var currentGameObject in currentGOList)
            Destroy(currentGameObject);

        currentGOList.Clear();
    }
}