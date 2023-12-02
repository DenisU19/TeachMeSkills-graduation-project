using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GreenGrey.Analytics;
using GreenGrey.Analytics.Core;
using GreenGrey.Analytics.Device.Info;
using GreenGrey.Analytics.Event;
using GreenGrey.Analytics.Logger;
using UnityEngine;
using UnityEngine.UI;

namespace Greengray.Analytics.Example
{
    public class GGAnalyticsConfigurationExampleSample3 : IGGAnalyticsConfiguration
    {
        public string ApiUri => "https://dmp.greengreystudio.com/events";
        public LogLevel LogLevel => LogLevel.DEBUG;
        public string ApiKey => "779a42bdbebeccc7099de0aaff8b7298d4c22638a95028c362da50bcc5da9e67";
        public uint MaxEventsCountToSend => 10;
        public uint SendEventsTimeoutInSec => 600;
        public bool RegisterAppPause => true;
        public bool UsePrettyLogger => false;
    }
    
    public class GGAnalyticsExampleControllerSample3 : MonoBehaviour
    {
        [SerializeField] private Button m_sendEventsButton;
        [SerializeField] private Button m_addEventButton;
        
        [SerializeField] private Button m_saveEventsButton;
        [SerializeField] private Button m_loadEventsButton;

        [SerializeField] private RectTransform m_propertyPanel;
        [SerializeField] private RectTransform m_eventPanel;
        [SerializeField] private Text m_eventsCounters;

        private readonly List<GGAnalyticsEvent> m_events = new List<GGAnalyticsEvent>();

        private void Awake()
        {
            InitButtons();
            TryRegisterEventListeners();
            InitAndSetupAnalytics();
        }

        private void InitAndSetupAnalytics()
        {
            var go = new GameObject("GGAnalytics");
            DontDestroyOnLoad(go);
            GGAnalyticsInstance.Create(go);
            var configuration = new GGAnalyticsConfigurationExampleSample3();
            GGAnalyticsInstance.Setup(configuration);
        }
        
        private void InitButtons()
        {
            m_addEventButton.onClick.AddListener(OnAddButtonClick);

#if GG_DEBUG
            m_sendEventsButton.onClick.AddListener(OnSendButtonClick);
            m_saveEventsButton.onClick.AddListener(OnSaveButtonClick);
            m_loadEventsButton.onClick.AddListener(OnLoadButtonClick);
#else
            m_sendEventsButton.gameObject.SetActive(false);
            m_saveEventsButton.gameObject.SetActive(false);
            m_loadEventsButton.gameObject.SetActive(false);
#endif
        }

        private async void TryRegisterEventListeners()
        {
            GGAnalyticsInstance.EventsProxy.RegisterEventListener(GGAnalyticsSystemInternalMessageType.EVENT_ADDED, OnEventAdded);
            GGAnalyticsInstance.EventsProxy.RegisterEventListener(GGAnalyticsSystemInternalMessageType.EVENT_REMOVED, OnEventRemoved);
            GGAnalyticsInstance.EventsProxy.RegisterEventListener(GGAnalyticsSystemInternalMessageType.EVENT_STATUS_CHANGED, OnEventStatusChanged);
            
            while (!GGAnalyticsInstance.created)
            {
                await Task.Yield();
            }
            RenderDeviceInfo();
        }

        private void OnEventAdded(object _eventParams)
        {
            GGAnalyticsEvent eventItem = (GGAnalyticsEvent) _eventParams; 
            m_events.Add(eventItem);
            
            RenderEvents();
        }

        private void OnEventRemoved(object _eventParams)
        {
            string eventId = (string) _eventParams;

            var eventItem = m_events.Find(_e => _e.ID.Equals(eventId));
            m_events.Remove(eventItem);
            
            RenderEvents();
        }

        private void OnEventStatusChanged(object _eventParams)
        {
            GGAnalyticsEvent eventItem = (GGAnalyticsEvent) _eventParams;
            for (int i = 0; i < m_events.Count; i++)
            {
                var existingEventItem = m_events[i];
                if (existingEventItem.ID.Equals(eventItem.ID))
                    m_events[i] = eventItem;
            }
            RenderEvents();
        }

        private void RenderDeviceInfo()
        {
            IDeviceInfo deviceInfo = GGAnalytics.Instance.GetDeviceInfo();
            RenderDeviceProperties(deviceInfo);
        }

        private void RenderEvents()
        {
            foreach (var eventItem in m_events)
            {
                UpdateOrCreateEventPanel(eventItem);
            }

            UpdateCountersLabel(m_events);
        }

        private void UpdateCountersLabel(List<GGAnalyticsEvent> _events)
        {
            m_eventsCounters.text = CollectEventCountersToString(_events);
        }

        private string CollectEventCountersToString(List<GGAnalyticsEvent> _events)
        {
            int newEvents = _events.Count(e => e.Status == EventStatus.New);
            int pendingEvents = _events.Count(e => e.Status == EventStatus.PendingToSend);
            int sendedEvents = _events.Count(e => e.Status == EventStatus.Sended);
            return $"NEW: {newEvents} PENDING TO SEND: {pendingEvents} SENDED EVENTS: {sendedEvents}";
        }
        
        private void RenderDeviceProperties(IDeviceInfo _deviceInfo)
        {
            var typeAttributes = IntrospectionExtensions.GetTypeInfo(_deviceInfo.GetType()).DeclaredProperties;
            var baseTypeAttributes =
                IntrospectionExtensions.GetTypeInfo(_deviceInfo.GetType()).BaseType?.GetTypeInfo().DeclaredProperties;
            
            if (baseTypeAttributes != null)
                typeAttributes = typeAttributes.Concat(baseTypeAttributes);

            HashSet<string> renderedMethods = new HashSet<string>();
            
            foreach (PropertyInfo typeAttribute in typeAttributes)
            {
                var methodInfo = typeAttribute.GetAccessors(true)[0];
                if(methodInfo.IsPrivate || renderedMethods.Contains(methodInfo.Name))
                    continue;
                
                CreatePropertyPanel(typeAttribute, _deviceInfo);

                renderedMethods.Add(methodInfo.Name);
            }
        }

        private void CreatePropertyPanel(PropertyInfo typeAttribute, IDeviceInfo deviceInfo)
        {
            var container = m_propertyPanel.transform.parent;
            var newPanel = Instantiate(m_propertyPanel, container, true);
            UIPropertyPanelSample3 panelProperties = newPanel.GetComponent<UIPropertyPanelSample3>();
            panelProperties.Name = typeAttribute.Name;
            panelProperties.Value = typeAttribute.GetValue(deviceInfo)?.ToString();
            newPanel.gameObject.SetActive(true);
        }

        private void UpdateOrCreateEventPanel(GGAnalyticsEvent dmpEvent)
        {
            if (TryUpdateEventPanel(dmpEvent))
                return;

            CreateEventPanel(dmpEvent);
        }

        private bool TryUpdateEventPanel(GGAnalyticsEvent dmpEvent)
        {
            List<UIEventPanelSample3> eventPanels = m_eventPanel.transform.parent.GetComponentsInChildren<UIEventPanelSample3>().ToList();

            UIEventPanelSample3 eventPanel = eventPanels.Find(ep => ep.Id == dmpEvent.ID);
            if (eventPanel == null)
                return false;
            
            eventPanel.UpdateProperties(dmpEvent);
            return true;
        }

        private void CreateEventPanel(GGAnalyticsEvent dmpEvent)
        {
            var newPanel = Instantiate(m_eventPanel, m_eventPanel.transform.parent, true);
            UIEventPanelSample3 eventPanel = newPanel.GetComponent<UIEventPanelSample3>();
            eventPanel.UpdateProperties(dmpEvent);
            newPanel.gameObject.GetComponent<RectTransform>().SetAsFirstSibling();
            newPanel.gameObject.SetActive(true);
        }
        
        private void OnAddButtonClick()
        {
            var testParams = CollectEventParams();
            GGAnalyticsInstance.LogEvent("testEvent", testParams);
        }

#if GG_DEBUG
        private void OnSendButtonClick()
        {
            GGAnalytics.Instance.SendEventsForced();
        }
        
        private void OnSaveButtonClick()
        {
            GGAnalytics.Instance.SaveEvents();
        }
        
        private void OnLoadButtonClick()
        {
            GGAnalytics.Instance.LoadEvents();
        }
#endif
        
        private Dictionary<string,object> CollectEventParams()
        {
            return new Dictionary<string, object>()
            {
                ["testParam1"] = "testValue1",
                ["testParam2"] = 2.4,
                ["testParam3"] = 123F,
            };
        }

#if GG_DEBUG
        private IGGAnalyticsInternal GGAnalyticsInstance => GGAnalytics.Instance;
#else
        private IGGAnalytics GGAnalyticsInstance => GGAnalytics.Instance;
#endif
    }
    
}
