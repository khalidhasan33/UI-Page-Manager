using DG.Tweening;
using UIPackage.DataAssets;
using UnityEngine;
using UnityEngine.UI;

namespace UIPackage.UI
{
    public enum BottonType
    {
        Back1, Back2, Back3,
        Next1, Next2, Next3,
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
            UIViewsGroup targetView = null;

            switch(buttonType)
            {
                case BottonType.Back1:
                    targetView = view.node.Back1;
                    break;
                case BottonType.Back2:
                    targetView = view.node.Back2;
                    break;
                case BottonType.Back3:
                    targetView = view.node.Back3;
                    break;
                case BottonType.Next1:
                    targetView = view.node.Next1;
                    break;
                case BottonType.Next2:
                    targetView = view.node.Next2;
                    break;
                case BottonType.Next3:
                    targetView = view.node.Next3;
                    break;
            }

            if (viewManager != null && targetView != null)
                viewManager.ChangingView(view.node.viewsGroupName, targetView);
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
