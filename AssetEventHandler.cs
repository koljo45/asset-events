using UnityEngine;
using UnityEngine.Events;

namespace SaintStudio.AssetEvents
{
    public class AssetEventHandler : MonoBehaviour
    {
        [SerializeField] private AssetEvent _assetEvent;
        [SerializeField] private UnityEvent _handler;

        private void OnEnable()
        {
            _assetEvent?.RegisterHandler(EventHandler);
        }

        private void OnDisable()
        {
            _assetEvent?.UnregisterHandler(EventHandler);
        }

        private void EventHandler(AssetEvent data)
        {
            _handler?.Invoke();
        }
    }
}
