using DG.Tweening;
using UIPackage.DataAssets;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

namespace UIPackage.UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIView : MonoBehaviour
    {
        [Header("Required")]
        [Required(WarningType.InspectorWarning)] public UINode node;

        [Header("Config")]
        [SerializeField] private BehaviorAtStart behaviorAtStart = BehaviorAtStart.Hide;

        [Header("Animations Show Configs")]
        [SerializeField] private ShowAnimations showAnimations = ShowAnimations.Fade;
        [SerializeField] private Ease showEaseAnimations = Ease.Linear;
        [SerializeField] private float startTransitionDelay = 0;
        [SerializeField] private float durationShow = 0.4f;

        [Header("Animations Hide Configs")]
        [SerializeField] private HideAnimations hideAnimations = HideAnimations.Fade;
        [SerializeField] private Ease hideEaseAnimations = Ease.Linear;
        [SerializeField] private float durationHide = 0.4f;

        [Header("Events")]
        [SerializeField] private UnityEvent onDelayTrigger;
        [SerializeField] private UnityEvent onStartShow;
        [SerializeField] private UnityEvent onFinishedShow;
        [SerializeField] private UnityEvent onStartHide;
        [SerializeField] private UnityEvent onFinishedHide;
        [SerializeField] private UnityEvent onAlreadyShown;

        [HideInInspector] public bool isShow;

        private bool isShowing;
        private RectTransform rectTransform;
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
            if (!isShowing)
            {
                isShowing = true;
                onDelayTrigger.Invoke();
                yield return new WaitForSeconds(startTransitionDelay);

                void onEnd()
                {
                    isShow = true;
                    isShowing = false;
                    onFinishedShow.Invoke();
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                }

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

                    case ShowAnimations.SlideFadeUp:
                        canvasGroup.alpha = 0f;
                        rectTransform.offsetMax = new Vector2(0, -height);
                        rectTransform.offsetMin = new Vector2(0, -height);

                        gameObject.SetActive(true);
                        onStartShow.Invoke();
                        canvasGroup.DOFade(1f, durationShow).SetEase(showEaseAnimations);
                        SlideVertical(0, durationShow, onEnd, showEaseAnimations);
                        break;

                    case ShowAnimations.SlideFadeDown:
                        canvasGroup.alpha = 0f;
                        rectTransform.offsetMax = new Vector2(0, height);
                        rectTransform.offsetMin = new Vector2(0, height);

                        gameObject.SetActive(true);
                        onStartShow.Invoke();
                        canvasGroup.DOFade(1f, durationShow).SetEase(showEaseAnimations);
                        SlideVertical(0, durationShow, onEnd, showEaseAnimations);
                        break;

                    case ShowAnimations.SlideFadeLeft:
                        canvasGroup.alpha = 0f;
                        rectTransform.offsetMax = new Vector2(width, 0);
                        rectTransform.offsetMin = new Vector2(width, 0);

                        gameObject.SetActive(true);
                        canvasGroup.alpha = 0f;
                        onStartShow.Invoke();
                        Fade(1f, durationShow, null, showEaseAnimations);
                        SlideHorizontal(0, durationShow, onEnd, showEaseAnimations);
                        break;

                    case ShowAnimations.SlideFadeRight:
                        canvasGroup.alpha = 0f;
                        rectTransform.offsetMax = new Vector2(-width, 0);
                        rectTransform.offsetMin = new Vector2(-width, 0);

                        gameObject.SetActive(true);
                        onStartShow.Invoke();
                        Fade(1f, durationShow, null, showEaseAnimations);
                        SlideHorizontal(0, durationShow, onEnd, showEaseAnimations);
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
        }

        public void InstantHide()
        {
            gameObject.SetActive(false);
            isShow = false;
        }

        public void Hide()
        {
            onStartHide.Invoke();

            void onEnd()
            {
                isShow = false;
                onFinishedHide.Invoke();
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                gameObject.SetActive(false);
            }

            float height = rectTransform.rect.height;
            float width = rectTransform.rect.width;

            switch (hideAnimations)
            {
                case HideAnimations.Fade:
                    Fade(0f, durationHide, onEnd, hideEaseAnimations);
                    break;

                case HideAnimations.SlideFadeDown:
                    SlideVertical(-height, durationHide, onEnd, hideEaseAnimations);
                    canvasGroup.DOFade(0f, durationShow).SetEase(showEaseAnimations);
                    break;

                case HideAnimations.SlideFadeUp:
                    SlideVertical(height, durationHide, onEnd, hideEaseAnimations);
                    canvasGroup.DOFade(0f, durationShow).SetEase(showEaseAnimations);
                    break;

                case HideAnimations.SlideFadeLeft:
                    SlideHorizontal(-width, durationHide, onEnd, hideEaseAnimations);
                    canvasGroup.DOFade(0f, durationShow).SetEase(showEaseAnimations);
                    break;

                case HideAnimations.SlideFadeRight:
                    SlideHorizontal(width, durationHide, onEnd, hideEaseAnimations);
                    canvasGroup.DOFade(0f, durationShow).SetEase(showEaseAnimations);
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
            tween?.Kill(false);

            tween = canvasGroup.DOFade(endValue, duration).SetEase(ease);
            tween.onComplete += onEnd;
        }

        private void SlideVertical(float endValue, float duration, TweenCallback onEnd, Ease ease)
        {
            tween?.Kill(false);

            tween = rectTransform.DOAnchorPosY(endValue, duration).SetEase(ease);
            tween.onComplete += onEnd;
        }

        private void SlideHorizontal(float endValue, float duration, TweenCallback onEnd, Ease ease)
        {
            tween?.Kill(false);

            tween = rectTransform.DOAnchorPosX(endValue, duration).SetEase(ease);
            tween.onComplete += onEnd;
        }

        public void TriggerAlreadyShown()
        {
            onAlreadyShown.Invoke();
        }
        #endregion

        #region Unity callbacks
        void Start()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            canvasGroup = gameObject.GetComponent<CanvasGroup>();

            switch (behaviorAtStart) {
                case BehaviorAtStart.Hide:
                    gameObject.SetActive(false);
                break;
            case BehaviorAtStart.PlayShowAnimation:
                    StartCoroutine(Show());
                break;
            case BehaviorAtStart.DoNothing:
                break;
            }
        }
        #endregion
    }
}
