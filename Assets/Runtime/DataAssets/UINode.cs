using UnityEngine;

namespace UIPackage.DataAssets
{
    [CreateAssetMenu(fileName = "Data", menuName = "UIPackage/NodeData")]
    public class UINode : ScriptableObject
    {
        public UIViewsGroup viewsGroupName;

        public UIViewsGroup Back1;
        public UIViewsGroup Back2;
        public UIViewsGroup Back3;

        public UIViewsGroup Next1;
        public UIViewsGroup Next2;
        public UIViewsGroup Next3;
    }
}
