using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    private Tweener fadeTween;
    private float fadeDuration = 1f;

    public void FadeIn(float value)
    {
        Fade(value, fadeDuration);        
    }

    public void FadeOut()
    {
        Fade(0, fadeDuration);
    }
    
    private void Fade(float value, float duration)
    {
        fadeTween?.Kill();
        fadeTween = fadeImage.DOFade(value, duration);
    }
}
