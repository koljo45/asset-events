using System.Linq;
using UnityEditor;
using UnityEditor.Search;
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

            if (GUILayout.Button("Find References In Scene"))
            {
                var sceneGameObjects = SearchUtils.FetchGameObjects();
                var arr = sceneGameObjects.ToArray();

                var searchContext = new SearchContext(new[] { SearchService.GetProvider("scene") }, $"ref:{AssetDatabase.GetAssetPath(target)}");
                SearchService.ShowWindow(context: searchContext);
            }
        }
    }
}
