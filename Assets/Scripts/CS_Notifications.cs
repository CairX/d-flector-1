using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Notifications : CS_Singleton<CS_Notifications> {
    // Hide constructor.
    protected CS_Notifications() { }

    private Dictionary<string, HashSet<Component>> notifications = new Dictionary<string, HashSet<Component>>();

    public void Register(Component observer, string method)
    {
        if (string.IsNullOrEmpty(method))
        {
            Debug.Log("Can't register method that is null or empty.");
            return;
        }

        if (!notifications.ContainsKey(method))
        {
            notifications.Add(method, new HashSet<Component>());
        }

        notifications[method].Add(observer);
    }

    public void Unregister(Component observer, string method)
    {
        if (string.IsNullOrEmpty(method))
        {
            Debug.Log("Can't unregister method that is null or empty.");
            return;
        }

        if (!notifications.ContainsKey(method))
        {
            Debug.Log("No observers registered for method: " + method);
            return;
        }

        notifications[method].Remove(observer);
    }

    public void Post(Component sender, string method)
    {
        Post(sender, method, null);
    }

    public void Post(Component sender, string method, Hashtable data)
    {
        if (string.IsNullOrEmpty(method))
        {
            Debug.Log("Can't post method that is null or empty.");
            return;
        }

        if (!notifications.ContainsKey(method))
        {
            Debug.Log("No observers registered for method: " + method);
            return;
        }

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
}
