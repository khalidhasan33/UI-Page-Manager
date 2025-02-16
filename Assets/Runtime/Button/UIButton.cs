using DG.Tweening;
using UIPackage.DataAssets;
using UnityEngine;
using UnityEngine.UI;

namespace UIPackage.UI
{
    public enum BottonType
    {
        ActionButton1, ActionButton2, ActionButton3, ActionButton4, ActionButton5, ActionButton6
    }

    public class UIButton : MonoBehaviour
    {
        [SerializeField]
        private UIView view;

        [SerializeField]
        private BottonType buttonType;

        [SerializeField]
        private Button button;

        private UINode node;
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
            }

            if (viewManager != null && targetView != null)
                viewManager.ChangingView(view.node.viewName, targetView.viewName);
        }

        private void OnEnable()
        {
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

            this.gameObject.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.1f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    this.gameObject.transform.DOScale(originalSize, 0.1f)
                        .SetEase(Ease.Linear);
                });
        }
        #endregion
    }
}
