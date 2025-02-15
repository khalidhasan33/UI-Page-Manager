using DG.Tweening;
using UnityEngine;

public class UIViewTransitionIn : MonoBehaviour
{
    [SerializeField]
    private float durationHide;

    private CanvasGroup canvasGroup;
    private Tween fadeTween;

    #region Methods
    public void InstantShow()
    {
        gameObject.SetActive(true);
    }

    public void InstantHide()
    {
        gameObject.SetActive(false);
    }

    public void Hide()
    {
        Fade(0f, durationHide, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        });
    }

    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        if (fadeTween != null)
        {
            fadeTween.Kill(false);
        }

        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }
    #endregion

    #region Unity callbacks
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        Hide();
    }

    void Update()
    {

    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
    #endregion
}
