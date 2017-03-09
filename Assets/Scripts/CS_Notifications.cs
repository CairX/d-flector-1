using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Notifications : CS_Singleton<CS_Notifications> {
    // Hide constructor.
    protected CS_Notifications() { }

    private Dictionary<string, HashSet<Component>> notifications = new Dictionary<string, HashSet<Component>>();
    private Dictionary<string, HashSet<Component>> cacheRegister = new Dictionary<string, HashSet<Component>>();
    private Dictionary<string, HashSet<Component>> cacheUnregister = new Dictionary<string, HashSet<Component>>();

    private static void InitKey(Dictionary<string, HashSet<Component>> dict, string key)
    {
        if (!dict.ContainsKey(key))
        {
            dict.Add(key, new HashSet<Component>());
        }
    }

    private void UpdateCache()
    {
        foreach (var pair in cacheRegister)
        {
            InitKey(notifications, pair.Key);
            foreach (var observer in pair.Value)
            {
                notifications[pair.Key].Add(observer);
            }
        }
        cacheRegister.Clear();

        foreach (var pair in cacheUnregister)
        {
            foreach (var observer in pair.Value)
            {
                if (notifications.ContainsKey(pair.Key))
                {
                    notifications[pair.Key].Remove(observer);
                }
            }
        }
        cacheUnregister.Clear();
    }

    public void Register(Component observer, string method)
    {
        if (string.IsNullOrEmpty(method))
        {
            Debug.Log("Can't register method that is null or empty.");
            return;
        }

        InitKey(cacheRegister, method);
        cacheRegister[method].Add(observer);
    }

    public void Unregister(Component observer, string method)
    {
        if (string.IsNullOrEmpty(method))
        {
            Debug.Log("Can't unregister method that is null or empty.");
            return;
        }

        InitKey(cacheUnregister, method);
        cacheUnregister[method].Add(observer);
    }

    public void Post(Component sender, string method)
    {
        Post(sender, method, null);
    }

    public void Post(Component sender, string method, Hashtable data)
    {
        UpdateCache();

        if (string.IsNullOrEmpty(method))
        {
            Debug.Log("Can't post method that is null or empty.");
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
