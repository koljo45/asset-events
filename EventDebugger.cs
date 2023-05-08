using UnityEngine;

namespace SaintStudio.AssetEvents
{
    public class EventDebugger : MonoBehaviour
    {
        [SerializeField] private AssetEvent _event;

        private void OnEnable()
        {
            _event.RegisterHandler(OnEventHandler);
        }

        private void OnDisable()
        {
            _event.UnregisterHandler(OnEventHandler);
        }

        private void OnEventHandler(AssetEvent e)
        {
            Debug.Log($"Event debugger: {e.GetType()} = {e.Value}");
        }
    }
}
