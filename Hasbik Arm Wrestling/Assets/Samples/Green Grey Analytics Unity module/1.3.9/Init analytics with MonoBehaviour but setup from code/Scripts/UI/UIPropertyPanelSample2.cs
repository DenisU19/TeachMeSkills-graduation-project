using System;
using UnityEngine;
using UnityEngine.UI;

namespace Greengray.Analytics.Example
{
    public class UIPropertyPanelSample2 : MonoBehaviour
    {
        [SerializeField] private Text m_propertyName;
        [SerializeField] private Text m_propertyValue;

        public String Name
        {
            set => m_propertyName.text = value;
        }
        
        public String Value
        {
            set => m_propertyValue.text = value;
        }
    }
}