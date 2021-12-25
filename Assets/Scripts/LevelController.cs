using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [Header("Level 1")]
    [SerializeField] private CardBundleData firstCardBundleData;
    [SerializeField] private int firstLvlRowCount;
    
    [Header("Level 2")]
    [SerializeField] private CardBundleData secondCardBundleData;
    [SerializeField] private int secondLvlRowCount;
    
    [Header("Level 3")]
    [SerializeField] private CardBundleData thirdCardBundleData;
    [SerializeField] private int thirdLvlRowCount;
    
    [Header("Other")]
    [SerializeField] private CellsController cellsController;
    [SerializeField] private Button restartButton;
    [SerializeField] private ImageFader gameLoadImageFader;
    [SerializeField] private ImageFader restartBackgroundFader;
    
    private int maxLevel = 2;
    private int currentLevel;
    private int rowCount;
    private CardBundleData cardBundleData;

    private void Start()
    {
        cellsController.onRightButtonCliked += SetLevel;
        restartButton.onClick.AddListener(ActivateLoadImage);
        SetLevel();
        cellsController.ActivateTaskText(true);
    }

    private void SetLevel()
    {
        if (currentLevel > maxLevel)
        {
            Restart();
            return;
        }

        GetCurrentCardBundleData(currentLevel);

        var isStart = currentLevel == 0;
        cellsController.Init(cardBundleData, isStart, rowCount);
        currentLevel++;
    }

    private void Restart()
    {
        ActivateRestartButton(true);
        restartBackgroundFader.FadeIn(0.7f);
    }

    private void ActivateLoadImage()
    {
        var fadeValue = 1f;
        gameLoadImageFader.gameObject.SetActive(true);
        gameLoadImageFader.FadeIn(fadeValue);
        StartCoroutine(GameRestart(fadeValue));
    }
    
    private void ActivateRestartButton(bool isActive)
    {
        restartBackgroundFader.gameObject.SetActive(isActive);
    }

    private void GetCurrentCardBundleData(int level)
    {
        switch (level)
        {
            case 0:
                cardBundleData = firstCardBundleData;
                rowCount = firstLvlRowCount;
                break;
            case 1:
                cardBundleData = secondCardBundleData;
                rowCount = secondLvlRowCount;
                break;
            case 2:
                cardBundleData = thirdCardBundleData;
                rowCount = thirdLvlRowCount;
                break;
        }
    }

    private IEnumerator GameRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        currentLevel = 0;
        ActivateRestartButton(false);
        cellsController.ActivateTaskText(false);
        gameLoadImageFader.FadeOut();
        SetLevel();
        StartCoroutine(StartNewGame(waitTime));
    }

    private IEnumerator StartNewGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        cellsController.ActivateTaskText(true);
        gameLoadImageFader.gameObject.SetActive(false);
    }
}
