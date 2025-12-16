using DG.Tweening;
using UIPackage.DataAssets;
using UnityEngine;
using UnityEngine.UI;

namespace UIPackage.UI
{
    [RequireComponent(typeof(Button))]
    public class UIButton : MonoBehaviour
    {
        [Header("Required")]
        [SerializeField][Required(WarningType.InspectorWarning)] private UIView view;

        [Header("Config")]
        [SerializeField] private BottonType buttonType;

        private Button button;
        private UIViewManager viewManager;

        #region Unity callbacks
        private void OnClick()
        {
            BounceSmallAnimation();
            UINode targetView = null;

            switch(buttonType)
            {
                case BottonType.ActionButton1:
                    targetView = view.node.ActionButton1;
                    break;
                case BottonType.ActionButton2:
                    targetView = view.node.ActionButton2;
                    break;
                case BottonType.ActionButton3:
                    targetView = view.node.ActionButton3;
                    break;
                case BottonType.ActionButton4:
                    targetView = view.node.ActionButton4;
                    break;
                case BottonType.ActionButton5:
                    targetView = view.node.ActionButton5;
                    break;
                case BottonType.ActionButton6:
                    targetView = view.node.ActionButton6;
                    break;
                case BottonType.ActionButton7:
                    targetView = view.node.ActionButton7;
                    break;
                case BottonType.ActionButton8:
                    targetView = view.node.ActionButton8;
                    break;
                case BottonType.ActionButton9:
                    targetView = view.node.ActionButton9;
                    break;
                case BottonType.ActionButton10:
                    targetView = view.node.ActionButton10;
                    break;
            }

            if (viewManager != null && targetView != null)
                viewManager.ChangingView(view.node.UINodesView, targetView.UINodesView);
        }

        private void OnEnable()
        {
            button = gameObject.GetComponent<Button>();

            if (button != null)
                button.onClick.AddListener(OnClick);

            viewManager = FindAnyObjectByType<UIViewManager>();
        }

        private void OnDisable()
        {
            if (button != null)
                button.onClick.RemoveListener(OnClick);
        }

        public void Execute()
        {
            OnClick();
        }

        public void BounceSmallAnimation()
        {
            Vector3 originalSize = this.gameObject.transform.localScale;
            button.interactable = false;

            this.gameObject.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.1f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    this.gameObject.transform.DOScale(originalSize, 0.1f)
                        .SetEase(Ease.Linear)
                        .OnComplete(() =>
                        {
                            button.interactable = true;
                        });
                });
        }
        #endregion
    }
}
