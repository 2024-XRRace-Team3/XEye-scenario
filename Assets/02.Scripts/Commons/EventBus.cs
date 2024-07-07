using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace EventBus
{
    public interface IEventBus<T>
    {
        public static void Subscribe(T eventType, UnityAction listener){}
        public static void Unsubscribe(T type, UnityAction listener){}
        public static void Publish(T type){}
    }

    public class EventBus<TEnum> : IEventBus<TEnum>
    {
        private readonly static
            IDictionary<TEnum, UnityEvent> Events = new Dictionary<TEnum, UnityEvent>();

        public static void Subscribe(TEnum eventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Events.Add(eventType, thisEvent);
            }
        }

        public static void Unsubscribe(TEnum type, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (Events.TryGetValue(type, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void Publish(TEnum type)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(type, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}