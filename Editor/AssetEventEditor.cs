using UnityEditor;
using UnityEngine;

namespace SaintStudio.AssetEvents.Editor
{
    [CustomEditor(typeof(AssetEvent), true)]
    public class AssetEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button(nameof(AssetEvent.Raise)))
            {
                ((AssetEvent)target).Raise();
            }
        }
    }
}
