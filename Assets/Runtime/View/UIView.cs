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
        SlideUp,
        SlideDown,
        SlideLeft,
        SlideRight,
    }

    public enum HideAnimations
    {
        None,
        Fade,
        SlideUp,
        SlideDown,
        SlideLeft,
        SlideRight,
    }

    public class UIView : MonoBehaviour
    {
        [Header("Required")]
        public UINode node;

        public RectTransform rectTransform;

        [SerializeField]
        private BehaviorAtStart behaviorAtStart;

        [Header("Animations Show")]
        [SerializeField]
        private ShowAnimations showAnimations;

        [SerializeField]
        private Ease showEaseAnimations;

        [SerializeField]
        private float startTransitionDelay;

        [SerializeField]
        private float durationShow;

        [Header("Animations Hide")]
        [SerializeField]
        private HideAnimations hideAnimations;

        [SerializeField]
        private Ease hideEaseAnimations;

        [SerializeField]
        private float durationHide;

        [Header("Events")]
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
            yield return new WaitForSeconds(startTransitionDelay);

            TweenCallback onEnd = () =>
            {
                isShow = true;
                onFinishedShow.Invoke();
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            };

            float height = rectTransform.rect.height;
            float width = rectTransform.rect.width;

            switch (showAnimations)
            {
                case ShowAnimations.Fade:
                    canvasGroup.alpha = 0f;
                    gameObject.SetActive(true);
                    onStartShow.Invoke();

                    Fade(1f, durationShow, onEnd, showEaseAnimations);
                    break;

                case ShowAnimations.SlideUp:
                    rectTransform.offsetMax = new Vector2(0, -height);
                    rectTransform.offsetMin = new Vector2(0, -height);

                    gameObject.SetActive(true);
                    onStartShow.Invoke();
                    SlideVertical(0, durationShow, onEnd, showEaseAnimations);
                    break;

                case ShowAnimations.SlideDown:
                    rectTransform.offsetMax = new Vector2(0, height);
                    rectTransform.offsetMin = new Vector2(0, height);

                    gameObject.SetActive(true);
                    onStartShow.Invoke();
                    SlideVertical(0, durationShow, onEnd, showEaseAnimations);
                    break;

                case ShowAnimations.SlideLeft:
                    rectTransform.offsetMax = new Vector2(width, 0);
                    rectTransform.offsetMin = new Vector2(width, 0);

                    gameObject.SetActive(true);
                    onStartShow.Invoke();
                    SlideHorizontal(0, durationShow, onEnd, showEaseAnimations);
                    break;

                case ShowAnimations.SlideRight:
                    rectTransform.offsetMax = new Vector2(-width, 0);
                    rectTransform.offsetMin = new Vector2(-width, 0);

                    gameObject.SetActive(true);
                    onStartShow.Invoke();
                    SlideHorizontal(0, durationShow, onEnd, showEaseAnimations);
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

            float height = rectTransform.rect.height;
            float width = rectTransform.rect.width;

            switch (hideAnimations)
            {
                case HideAnimations.Fade:
                    canvasGroup.alpha = 0f;
                    gameObject.SetActive(true);
                    onStartShow.Invoke();

                    Fade(0f, durationHide, onEnd, hideEaseAnimations);
                    break;

                case HideAnimations.SlideDown:
                    SlideVertical(-height, durationHide, onEnd, hideEaseAnimations);
                    break;

                case HideAnimations.SlideUp:
                    SlideVertical(height, durationHide, onEnd, hideEaseAnimations);
                    break;

                case HideAnimations.SlideLeft:
                    SlideHorizontal(-width, durationHide, onEnd, hideEaseAnimations);
                    break;

                case HideAnimations.SlideRight:
                    SlideHorizontal(width, durationHide, onEnd, hideEaseAnimations);
                    break;

                default:
                    break;
            }
        }

        private void Fade(float endValue, float duration, TweenCallback onEnd, Ease ease)
        {
            if (tween != null)
            {
                tween.Kill(false);
            }

            tween = canvasGroup.DOFade(endValue, duration).SetEase(ease);
            tween.onComplete += onEnd;
        }

        private void SlideVertical(float endValue, float duration, TweenCallback onEnd, Ease ease)
        {
            if (tween != null)
            {
                tween.Kill(false);
            }

            tween = rectTransform.DOAnchorPosY(endValue, duration).SetEase(ease);
            tween.onComplete += onEnd;
        }

        private void SlideHorizontal(float endValue, float duration, TweenCallback onEnd, Ease ease)
        {
            if (tween != null)
            {
                tween.Kill(false);
            }

            tween = rectTransform.DOAnchorPosX(endValue, duration).SetEase(ease);
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
