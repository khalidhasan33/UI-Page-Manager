using DG.Tweening;
using System.Collections;
using UIPackage.DataAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UIPackage.UI
{
    public class UIViewSceneLoader : MonoBehaviour
    {
        public UINode node;

        [SerializeField]
        private float transitionDelay;

        [SerializeField]
        private float transitionDuration;

        [SerializeField]
        private string targetScene;

        [SerializeField]
        private bool isAdditive;

        private CanvasGroup canvasGroup;
        private Tween fadeTween;

        [SerializeField]
        private UnityEvent onDelayTrigger;

        [SerializeField]
        private UnityEvent onStartTransition;
        #region Methods
        public void InstantShow()
        {
            gameObject.SetActive(true);
        }

        public IEnumerator Show()
        {
            onDelayTrigger.Invoke();
            yield return new WaitForSeconds(transitionDelay);
            onStartTransition.Invoke();

            canvasGroup.alpha = 0f;
            gameObject.SetActive(true);

            Fade(1f, transitionDuration, () =>
            {
                if (isAdditive)
                    SceneManager.LoadScene(targetScene, LoadSceneMode.Additive);
                else
                    SceneManager.LoadScene(targetScene);

                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        public void InstantHide()
        {
            gameObject.SetActive(false);
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

            gameObject.SetActive(false);
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
