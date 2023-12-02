using GreenGrey.Analytics;
using GreenGrey.Analytics.Event;
using UnityEngine;
using UnityEngine.UI;

namespace Greengray.Analytics.Example
{
    public class UIEventPanelSample1 : MonoBehaviour
    {
        [SerializeField] private Text m_eventIdLabel;
        [SerializeField] private Text m_eventTypeLabel;
        [SerializeField] private Text m_eventStatusLabel;
        
        private string m_id;
        
        public string Id => m_id;
        
        public void UpdateProperties(GGAnalyticsEvent _event)
        {
            m_id = _event.ID;
            m_eventIdLabel.text = m_id;
            m_eventTypeLabel.text = _event.Name;
            m_eventStatusLabel.text = _event.StatusToString();
        }
    }
}