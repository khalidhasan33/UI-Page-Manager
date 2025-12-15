using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UIPackage.DataAssets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

namespace UIPackage.UI
{
    public class ListToPopupAttribute : PropertyAttribute
    {
        public Type myType;
        public string propertyName;

        public ListToPopupAttribute(Type _myType, string _propertyName)
        {
            myType = _myType;
            propertyName = _propertyName;
        }
    }

    public class UIViewSceneLoader : MonoBehaviour, ISerializationCallbackReceiver
    {
        public static List<string> TMPList;
        [HideInInspector] public List<string> Popuplist;
        [ListToPopup(typeof(UIViewSceneLoader), "TMPList")]
        public string TargetScene;

        public UINode node;

        [SerializeField]
        private ShowAnimations showAnimations = ShowAnimations.Fade;

        [SerializeField]
        private Ease showEaseAnimations = Ease.Linear;

        [SerializeField]
        private float transitionDelay = 0;

        [SerializeField]
        private float transitionDuration = 0.4f;

        [SerializeField]
        private bool isAdditive;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Tween tween;

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

            TweenCallback onEnd = () =>
            {
                if (isAdditive)
                    SceneManager.LoadScene(TargetScene, LoadSceneMode.Additive);
                else
                    SceneManager.LoadScene(TargetScene);

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

                    Fade(1f, transitionDuration, onEnd, showEaseAnimations);
                    break;

                case ShowAnimations.SlideUp:
                    rectTransform.offsetMax = new Vector2(0, -height);
                    rectTransform.offsetMin = new Vector2(0, -height);

                    gameObject.SetActive(true);
                    SlideVertical(0, transitionDuration, onEnd, showEaseAnimations);
                    break;

                case ShowAnimations.SlideDown:
                    rectTransform.offsetMax = new Vector2(0, height);
                    rectTransform.offsetMin = new Vector2(0, height);

                    gameObject.SetActive(true);
                    SlideVertical(0, transitionDuration, onEnd, showEaseAnimations);
                    break;

                case ShowAnimations.SlideLeft:
                    rectTransform.offsetMax = new Vector2(width, 0);
                    rectTransform.offsetMin = new Vector2(width, 0);

                    gameObject.SetActive(true);
                    SlideHorizontal(0, transitionDuration, onEnd, showEaseAnimations);
                    break;

                case ShowAnimations.SlideRight:
                    rectTransform.offsetMax = new Vector2(-width, 0);
                    rectTransform.offsetMin = new Vector2(-width, 0);

                    gameObject.SetActive(true);
                    SlideHorizontal(0, transitionDuration, onEnd, showEaseAnimations);
                    break;

                default:
                    break;
            }
        }

        public void InstantHide()
        {
            gameObject.SetActive(false);
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

        public List<string> GetAllScenesInBuild()
        {
            List<string> allScenes = new List<string>();

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string ScenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string SceneName = System.IO.Path.GetFileNameWithoutExtension(ScenePath);
                allScenes.Add(SceneName);
            }

            return allScenes;
        }
        #endregion

        #region Unity callbacks
        void Start()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
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

        public void OnBeforeSerialize()
        {
            Popuplist = GetAllScenesInBuild();
            TMPList = Popuplist;
        }

        public void OnAfterDeserialize() {}
        #endregion
    }
}
