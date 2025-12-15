using System.Collections.Generic;
using UnityEngine;

namespace UIPackage.DataAssets
{
    [CreateAssetMenu(fileName = "Data", menuName = "UIPackage/NodeData")]
    public class UINode : ScriptableObject
    {
        public string ID;

        public List<UINode> UINodesView;

        public UINode ActionButton1;
        public UINode ActionButton2;
        public UINode ActionButton3;
        public UINode ActionButton4;
        public UINode ActionButton5;
        public UINode ActionButton6;
        public UINode ActionButton7;
        public UINode ActionButton8;
        public UINode ActionButton9;
        public UINode ActionButton10;
    }
}
