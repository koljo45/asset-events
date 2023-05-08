using UnityEngine;

namespace SaintStudio.AssetEvents
{
    [CreateAssetMenu(fileName = "StringEvent", menuName = "SaintStudio/ScriptableObjects/Events/StringEvent", order = 1)]
    public class StringEvent : TypedEvent<string, StringEvent>
    {
    }
}