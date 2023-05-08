using UnityEngine;

namespace SaintStudio.AssetEvents
{
    [CreateAssetMenu(fileName = "BoolEvent", menuName = "SaintStudio/ScriptableObjects/Events/BoolEvent")]
    public class BoolEvent : TypedEvent<bool, BoolEvent>
    {
    }
}
