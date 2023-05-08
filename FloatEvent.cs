using UnityEngine;

namespace SaintStudio.AssetEvents
{
    [CreateAssetMenu(fileName = "FloatEvent", menuName = "SaintStudio/ScriptableObjects/Events/FloatEvent", order = 1)]
    public class FloatEvent : TypedEvent<float, FloatEvent>
    {
    }
}
