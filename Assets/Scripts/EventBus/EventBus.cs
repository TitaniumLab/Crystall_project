using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrystalProject.EventBus
{
    public class CustomEventBus
    {
        private Dictionary<string, List<object>> _siganalCallbacks = new Dictionary<string, List<object>>();

        public void Subscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;
            if (_siganalCallbacks.ContainsKey(key))
                _siganalCallbacks[key].Add(callback);
            else
                _siganalCallbacks.Add(key, new List<object>() { callback });
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;
            if (_siganalCallbacks.ContainsKey(key))
                _siganalCallbacks[key].Remove(callback);
            else
                Debug.LogError($"Signal type of {key} not exist");
        }

        public void Invoke<T>(T signal)
        {
            string key = typeof(T).Name;
            if (_siganalCallbacks.ContainsKey(key))
            {
                foreach (var item in _siganalCallbacks[key])
                {
                    var callback = item as Action<T>;
                    callback?.Invoke(signal);
                }
            }
        }
    }
}