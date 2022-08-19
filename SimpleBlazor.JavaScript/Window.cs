namespace SimpleBlazor.JavaScript
{
    public static class Window
    {
        private static IDictionary<EventType, Action> _eventListeners = new Dictionary<EventType, Action>();
        public static void AddEventListener(EventType eventType, Action callback)
        {
            _eventListeners.Add(eventType, callback);
        }

        public static void DispatchWindowEvent(string eventTypeString)
        {
            var eventType = Enum.Parse<EventType>(eventTypeString, true);
            switch (eventType)
            {
                case EventType.HashChange:
                    _eventListeners.TryGetValue(eventType, out Action action);
                    if (action != null)
                    {
                        action();
                    }
                    break;
            }
        }
    }
}