using DG.Tweening;
using UIPackage.DataAssets;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace UIPackage.UI
{
    public enum BehaviorAtStart
    {
        DoNothing,
        Hide,
        PlayShowAnimation,
    }

    public enum ShowAnimations
    { 
        None,
        Fade,
        SlideUp
    }

    public enum HideAnimations
    {
        None,
        Fade,
        SlideDown
    }

    public class UIView : MonoBehaviour
    {
        public UINode node;

        public RectTransform rectTransform;

        [SerializeField]
        private BehaviorAtStart behaviorAtStart;

        [SerializeField]
        private ShowAnimations showAnimations;

        [SerializeField]
        private float transitionDelay;

        [SerializeField]
        private float durationShow;

        [SerializeField]
        private HideAnimations hideAnimations;

        [SerializeField]
        private float durationHide;

        [SerializeField]
        private UnityEvent onDelayTrigger;

        [SerializeField]
        private UnityEvent onStartShow;

        [SerializeField]
        private UnityEvent onFinishedShow;

        [SerializeField]
        private UnityEvent onStartHide;

        [SerializeField]
        private UnityEvent onFinishedHide;

        [HideInInspector]
        public bool isShow;

        private CanvasGroup canvasGroup;
        private Tween tween;

        #region Methods
        public void InstantShow()
        {
            gameObject.SetActive(true);
            isShow = true;
        }

        public IEnumerator Show()
        {
            onDelayTrigger.Invoke();
            yield return new WaitForSeconds(transitionDelay);

            TweenCallback onEnd = () =>
            {
                isShow = true;
                onFinishedShow.Invoke();
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            };

            switch (showAnimations)
            {
                case ShowAnimations.Fade:
                    canvasGroup.alpha = 0f;
                    gameObject.SetActive(true);
                    onStartShow.Invoke();

                    Fade(1f, durationShow, onEnd);
                    break;

                case ShowAnimations.SlideUp:
                    float height = rectTransform.rect.height;

                    rectTransform.offsetMax = new Vector2(0, -height);
                    rectTransform.offsetMin = new Vector2(0, -height);

                    Debug.Log(rectTransform.offsetMax);
                    Debug.Log(rectTransform.offsetMin);

                    gameObject.SetActive(true);
                    onStartShow.Invoke();
                    SlideUp(0, durationShow, onEnd);
                    break;

                default:
                    break;
            }
        }

        public void InstantHide()
        {
            gameObject.SetActive(false);
            isShow = false;
        }

        public void Hide()
        {
            onStartHide.Invoke();


            TweenCallback onEnd = () =>
            {
                isShow = false;
                onFinishedHide.Invoke();
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                gameObject.SetActive(false);
                canvasGroup.alpha = 1f;
            };

            switch (hideAnimations)
            {
                case HideAnimations.Fade:
                    canvasGroup.alpha = 0f;
                    gameObject.SetActive(true);
                    onStartShow.Invoke();

                    Fade(0f, durationHide, onEnd);
                    break;

                case HideAnimations.SlideDown:
                    float height = rectTransform.rect.height;

                    SlideUp(-height, durationHide, onEnd);
                    break;

                default:
                    break;
            }
        }

        private void Fade(float endValue, float duration, TweenCallback onEnd)
        {
            if (tween != null)
            {
                tween.Kill(false);
            }

            tween = canvasGroup.DOFade(endValue, duration);
            tween.onComplete += onEnd;
        }

        private void SlideUp(float endValue, float duration, TweenCallback onEnd)
        {
            if (tween != null)
            {
                tween.Kill(false);
            }

            tween = rectTransform.DOAnchorPosY(endValue, duration).SetEase(Ease.OutQuad);
            tween.onComplete += onEnd;
        }

        private void SlideDown(float endValue, float duration, TweenCallback onEnd)
        {
            if (tween != null)
            {
                tween.Kill(false);
            }

            tween = rectTransform.DOAnchorPosY(endValue, duration).SetEase(Ease.OutQuad);
            tween.onComplete += onEnd;
        }
        #endregion

        #region Unity callbacks
        void Start()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();

            switch (behaviorAtStart) {
                case BehaviorAtStart.Hide:
                    gameObject.SetActive(false);
                break;
            case BehaviorAtStart.PlayShowAnimation:
                    // code block
                break;
            case BehaviorAtStart.DoNothing:
                break;
            }
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
}
