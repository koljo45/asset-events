using UnityEngine;

namespace SaintStudio.AssetEvents
{
    [CreateAssetMenu(fileName = nameof(IntegerEvent), menuName = "SaintStudio/ScriptableObjects/Events/" + nameof(IntegerEvent))]
    public class IntegerEvent : TypedEvent<int, IntegerEvent>
    {
    }
}
