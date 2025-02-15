using System.Collections.Generic;
using UIPackage.UI;
using UnityEngine;

namespace UIPackage.DataAssets
{
    [CreateAssetMenu(fileName = "Data", menuName = "UIPackage/ViewsGroupData")]
    public class UIViewsGroup : ScriptableObject
    {
        public List<UIViewEnum> viewName;

        public bool IsViewNameExist(UIViewEnum searchedName)
        {
            for (int i = 0; i < viewName.Count; i++)
            {
                if (searchedName == viewName[i])
                    return true;
            }
            return false;
        }
    }
}
