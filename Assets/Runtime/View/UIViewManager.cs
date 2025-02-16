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
        public void ChangingView(List<string> currentViewGroup, List<string> targetViewGroup)
        {
            Debug.Log("Changing View");
            HideViewGroup(currentViewGroup, targetViewGroup);
            ShowViewGroup(targetViewGroup);
        }

        public void HideViewGroup(List<string> currentViewGroup, List<string> targetViewGroup)
        {
            for (int i = 0; i < currentViewGroup.Count; i++)
            {
                for (int j = 0; j < listView.Count; j++)
                {
                    if (currentViewGroup[i] == listView[j].node.ID)
                    {
                        // Hide view if there is current view on targetview group
                        if (!IsViewNameExist(targetViewGroup, currentViewGroup[i]))
                            listView[j].Hide();
                    }
                }
            }
        }

        public void ShowViewGroup(List<string> viewGroup)
        {
            for (int i = 0; i < viewGroup.Count; i++)
            {
                for (int j = 0; j < listView.Count; j++)
                {
                    if (viewGroup[i] == listView[j].node.ID)
                    {
                        if (!listView[j].isShow)
                            StartCoroutine(listView[j].Show());
                    }
                }
                for (int j = 0; j < listSceneLoader.Count; j++)
                {
                    if (viewGroup[i] == listSceneLoader[j].node.ID)
                    {
                        StartCoroutine(listSceneLoader[j].Show());
                    }
                }
            }
        }

        IEnumerator StartFirstView()
        {
            yield return 0.2f;

            ShowViewGroup(nodes[0].UINodesID);
        }

        public bool IsViewNameExist(List<string> targetViewGroup, string searchedName)
        {
            for (int i = 0; i < targetViewGroup.Count; i++)
            {
                if (searchedName == targetViewGroup[i])
                    return true;
            }
            return false;
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
