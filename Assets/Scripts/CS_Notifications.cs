using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Notifications : CS_Singleton<CS_Notifications> {
    // Hide constructor.
    protected CS_Notifications() { }

    private Dictionary<string, HashSet<Component>> notifications = new Dictionary<string, HashSet<Component>>();
    private List<NotificationEvent> events = new List<NotificationEvent>();

    private static void InitKey(Dictionary<string, HashSet<Component>> dict, string key)
    {
        if (!dict.ContainsKey(key))
        {
            dict.Add(key, new HashSet<Component>());
        }
    }

    private void UpdateCache()
    {
        foreach (NotificationEvent ev in events)
        {
            switch (ev.action)
            {
                case Action.Register:
                    InitKey(notifications, ev.method);
                    notifications[ev.method].Add(ev.component);
                    break;
                case Action.Unregister:
                    InitKey(notifications, ev.method);
                    notifications[ev.method].Remove(ev.component);
                    break;
                default:
                    break;
            }
        }
        events.Clear();
    }

    public void Register(Component observer, string method)
    {
        if (string.IsNullOrEmpty(method)) { return; }

        events.Add(new NotificationEvent(Action.Register, method, observer));
    }

    public void Unregister(Component observer, string method)
    {
        if (string.IsNullOrEmpty(method)) { return; }

        events.Add(new NotificationEvent(Action.Unregister, method, observer));
    }

    public void Post(Component sender, string method)
    {
        Post(sender, method, new Dictionary<string, Component>());
    }

    public void Post(Component sender, string method, Dictionary<string, Component> data)
    {
        UpdateCache();

        if (string.IsNullOrEmpty(method)) { return; }

        data.Add("sender", sender);

        if (!notifications.ContainsKey(method)) { return; }

        List<Component> remove = new List<Component>();

        foreach (Component observer in notifications[method])
        {
            if (observer)
            {
                observer.SendMessage(method, data, SendMessageOptions.DontRequireReceiver);
            } else
            {
                remove.Add(observer);
            }
        }

        foreach (Component observer in remove)
        {
            notifications[method].Remove(observer);
        }
    }

    private enum Action { Register, Unregister }

    private class NotificationEvent
    {
        public Action action;
        public string method;
        public Component component;

        public NotificationEvent(Action action, string method, Component component)
        {
            this.action = action;
            this.method = method;
            this.component = component;
        }
    }
}
