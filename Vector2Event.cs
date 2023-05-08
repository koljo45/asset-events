using UnityEngine;

namespace SaintStudio.AssetEvents
{
    [CreateAssetMenu(fileName = "Vector2Event", menuName = "SaintStudio/ScriptableObjects/Events/Vector2Event", order = 1)]
    public class Vector2Event : TypedEvent<Vector2, Vector2Event>
    {
    }
}
