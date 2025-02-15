using System.Collections;
using System.Collections.Generic;
using UIPackage.DataAssets;
using UnityEngine;

namespace UIPackage.UI
{
    public class UIViewManager : MonoBehaviour
    {
        public List<UINode> nodes;

        [SerializeField]
        private List<UIView> listView;

        [SerializeField]
        private List<UIViewSceneLoader> listSceneLoader;

        #region Methods
        public void ChangingView(UIViewsGroup currentViewGroup, UIViewsGroup targetViewGroup)
        {
            Debug.Log("Changing View");
            HideViewGroup(currentViewGroup, targetViewGroup);
            ShowViewGroup(targetViewGroup);
        }

        public void HideViewGroup(UIViewsGroup currentViewGroup, UIViewsGroup targetViewGroup)
        {
            for (int i = 0; i < currentViewGroup.viewName.Count; i++)
            {
                for (int j = 0; j < listView.Count; j++)
                {
                    if (currentViewGroup.viewName[i] == listView[j].view)
                    {
                        // Hide view if there is current view on targetview group
                        if (!targetViewGroup.IsViewNameExist(currentViewGroup.viewName[i]))
                            listView[j].Hide();
                    }
                }
            }
        }

        public void ShowViewGroup(UIViewsGroup viewGroup)
        {
            for (int i = 0; i < viewGroup.viewName.Count; i++)
            {
                for (int j = 0; j < listView.Count; j++)
                {
                    if (viewGroup.viewName[i] == listView[j].view)
                    {
                        if (!listView[j].isShow)
                            StartCoroutine(listView[j].Show());
                    }
                }
                for (int j = 0; j < listSceneLoader.Count; j++)
                {
                    if (viewGroup.viewName[i] == listSceneLoader[j].view)
                    {
                        StartCoroutine(listSceneLoader[j].Show());
                    }
                }
            }
        }

        IEnumerator StartFirstView()
        {
            yield return 0.2f;

            ShowViewGroup(nodes[0].viewsGroupName);
        }
        #endregion

        #region Unity callbacks
        private void Start()
        {
            if (listView[0] != null && nodes[0] != null)
            {
                StartCoroutine(StartFirstView());
            }
        }
        #endregion
    }
}
