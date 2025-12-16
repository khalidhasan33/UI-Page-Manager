using UnityEngine;
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
}
