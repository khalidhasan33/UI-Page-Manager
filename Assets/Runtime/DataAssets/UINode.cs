using System.Collections.Generic;
using UIPackage.UI;
using UnityEngine;

namespace UIPackage.DataAssets
{
    [CreateAssetMenu(fileName = "Data", menuName = "UIPackage/NodeData")]
    public class UINode : ScriptableObject
    {
        public string ID;

        public List<string> UINodesID;

        public UINode ActionButton1;
        public UINode ActionButton2;
        public UINode ActionButton3;
        public UINode ActionButton4;
        public UINode ActionButton5;
        public UINode ActionButton6;
    }
}
