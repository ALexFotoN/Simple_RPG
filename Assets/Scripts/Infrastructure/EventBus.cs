using System;
using System.Collections.Generic;

public class EventBus
{
    private Dictionary<Type, Delegate> _events = new Dictionary<Type, Delegate>();

    public void Subscribe<T>(Action<T> listener)
    {
        if (_events.ContainsKey(typeof(T)))
            _events[typeof(T)] = Delegate.Combine(_events[typeof(T)], listener);
        else
            _events[typeof(T)] = listener;
    }

    public void Unsubscribe<T>(Action<T> listener)
    {
        if (_events.ContainsKey(typeof(T)))
            _events[typeof(T)] = Delegate.Remove(_events[typeof(T)], listener);
    }

    public void Publish<T>(T eventData)
    {
        if (_events.ContainsKey(typeof(T)))
            (_events[typeof(T)] as Action<T>)?.Invoke(eventData);
    }
}