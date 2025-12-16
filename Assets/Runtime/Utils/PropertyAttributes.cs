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

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class Required : PropertyAttribute
    {
        public WarningType warningType;

        public Required(WarningType _warningType = WarningType.Error)
        {
            warningType = _warningType;
        }
    }
}
