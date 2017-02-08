using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Notifications : CS_Singleton<CS_Notifications> {
    // Hide constructor for the singleton pattern.
    protected CS_Notifications() { }

    private Dictionary<string, HashSet<Component>> notifications;

    private void Awake()
    {
        notifications = new Dictionary<string, HashSet<Component>>();
    }

    public void Add(Component observer, string method)
    {
        if (!notifications.ContainsKey(method))
        {
            notifications.Add(method, new HashSet<Component>());
        }

        notifications[method].Add(observer);
    }

    public void Remove(Component observer, string method)
    {
        if (!notifications.ContainsKey(method))
        {
            return;
        }

        notifications[method].Remove(observer);
    }

    public void Post(Component sender, string method, Hashtable data)
    {
        foreach (Component observer in notifications[method])
        {
            observer.SendMessage(method, data, SendMessageOptions.DontRequireReceiver);
        }
    }
}
