using SaintStudio.AssetEvents.VisualScripting;
using SaintStudio.Core.Attributes;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace SaintStudio.AssetEvents
{
    [CreateAssetMenu(fileName = "Event", menuName = "SaintStudio/ScriptableObjects/Events/Event", order = 1)]
    public class AssetEvent : ScriptableObject
    {
        [SerializeField, ReadOnly] private string _lastRaiseTime;

        [field: FormerlySerializedAs("_triggerEventUnit")]
        [field: Tooltip("Set to true if this event should trigger event units in the visual scripting graph.")]
        [field: SerializeField]
        public bool TriggerEventUnit { get; private set; }

        public object Value { get; protected set; }
        private readonly List<Action<AssetEvent>> _listeners = new();

        [ContextMenu(nameof(Raise))]
        public void Raise()
        {
            for (int i = 0; i < _listeners.Count; i++)
                _listeners[i].Invoke(this);

            if (TriggerEventUnit)
                EventBus.Trigger(UnitEventNames.AssetEvent, this);

            _lastRaiseTime = DateTime.Now.ToString();
        }

        public virtual void Raise(object value)
        {
            Value = value;
            Raise();
        }

        public void RegisterHandler(Action<AssetEvent> eventHandler)
        {
            _listeners.Add(eventHandler);
        }

        public void UnregisterHandler(Action<AssetEvent> eventHandler)
        {
            _listeners.Remove(eventHandler);
        }
    }
}