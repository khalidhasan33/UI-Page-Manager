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
        Fade
    }

    public enum HideAnimations
    {
        None,
        Fade
    }

    public class UIView : MonoBehaviour
    {
        public UINode node;

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
        private Tween fadeTween;

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

            canvasGroup.alpha = 0f;
            gameObject.SetActive(true);
            onStartShow.Invoke();

            Fade(1f, durationShow, () =>
            {
                isShow = true;
                onFinishedShow.Invoke();
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        public void InstantHide()
        {
            gameObject.SetActive(false);
            isShow = false;
        }

        public void Hide()
        {
            onStartHide.Invoke();
            Fade(0f, durationHide, () =>
            {
                isShow = false;
                onFinishedHide.Invoke();
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
