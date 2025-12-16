using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIPackage.UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIViewTransitionIn : MonoBehaviour
    {
        [SerializeField] private HideAnimations hideAnimation = HideAnimations.Fade;
        [SerializeField] private Ease hideEaseAnimations = Ease.Linear;
        [SerializeField] private float durationHide = 0.4f;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Tween tween;

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
            void onEnd()
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                gameObject.SetActive(false);
            }

            float height = rectTransform.rect.height;
            float width = rectTransform.rect.width;

            switch (hideAnimation)
            {
                case HideAnimations.Fade:
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
        #endregion

        #region Unity callbacks
        void Start()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            Hide();
        }
        #endregion
    }
}
