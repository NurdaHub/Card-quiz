using DG.Tweening;
using TMPro;
using UnityEngine;

public class TaskTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI taskTMPro;
    
    private string defaultTaskText = "Find ";
    private float fadeDuration = 1.2f;
    private int endValue = 1;
    private float disabledTextAlpha = 0;

    private void OnEnable()
    {
        taskTMPro.DOFade(endValue, fadeDuration);
    }

    public void TaskInit(string taskText)
    {
        taskTMPro.text = defaultTaskText + taskText;
    }

    private void OnDisable()
    {
        taskTMPro.alpha = disabledTextAlpha;
    }
}