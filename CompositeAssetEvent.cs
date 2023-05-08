using System;
using UnityEngine;

namespace SaintStudio.AssetEvents
{
    [CreateAssetMenu(fileName = "CompositeEvent", menuName = "SaintStudio/ScriptableObjects/Events/CompositeEvent")]
    public class CompositeAssetEvent : AssetEvent
    {
        [SerializeField] private AssetEvent[] _baseEvents = Array.Empty<AssetEvent>();

        private void OnEnable()
        {
            foreach (var baseEvent in _baseEvents)
            {
                baseEvent.RegisterHandler(BaseEventRaisedHandler);
            }
        }

        private void OnDisable()
        {
            foreach (var baseEvent in _baseEvents)
            {
                baseEvent.UnregisterHandler(BaseEventRaisedHandler);
            }
        }

        private void BaseEventRaisedHandler(AssetEvent assetEvent)
        {
            Raise();
        }
    }
}
