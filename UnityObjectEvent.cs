using UnityEngine;

namespace SaintStudio.AssetEvents
{
    [CreateAssetMenu(fileName = "UnityObjectEvent", menuName = "SaintStudio/ScriptableObjects/Events/UnityObjectEvent", order = 1)]
    public class UnityObjectEvent : TypedEvent<Object, UnityObjectEvent>
    {
        // Hack so this function can be raised by UnityEvent.
        public void RaiseSerializable(Object value)
        {
            Raise(value);
        }
    }
}
