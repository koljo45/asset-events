using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;

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

                StringBuilder query = new StringBuilder();
                query.Append('{');

                foreach (var gameObject in sceneGameObjects)
                {
                    if (GetHasAssetEventReference(gameObject))
                    {
                        query.Append($"id={gameObject.GetInstanceID()},");
                    }
                }
                if (query[^1] != '{')
                    query.Remove(query.Length - 1, 1);

                query.Append('}');

                var searchContext = new SearchContext(new[] { SearchService.GetProvider("scene") }, query.ToString());
                SearchService.ShowWindow(context: searchContext);
            }
        }

        private bool GetHasAssetEventReference(GameObject gameObject)
        {
            var behaviourComponents = gameObject.GetComponents<MonoBehaviour>();

            foreach (var behaviour in behaviourComponents)
            {
                var behaviourType = behaviour.GetType();

                var fields = behaviourType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                foreach (var field in fields)
                {
                    var fieldType = field.FieldType;

                    if (typeof(AssetEvent).IsAssignableFrom(fieldType))
                    {
                        if ((Object)field.GetValue(behaviour) == target)
                        {
                            return true;
                        }
                    }

                    if (typeof(UnityEvent).IsAssignableFrom(fieldType))
                    {
                        var unityEvent = (UnityEvent)field.GetValue(behaviour);
                        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
                        {
                            if (unityEvent.GetPersistentTarget(i) == target)
                            {
                                return true;
                            }
                        }
                    }

                    if (typeof(SerializationData).IsAssignableFrom(fieldType))
                    {
                        var serializationData = (SerializationData)field.GetValue(behaviour);
                        foreach (var objectRef in serializationData.objectReferences)
                        {
                            if (objectRef == target)
                            {
                                return true;
                            }
                        }
                    }
                }

                var properties = behaviourType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                foreach (var property in properties)
                {
                    var propertyType = property.PropertyType;

                    if (typeof(AssetEvent).IsAssignableFrom(propertyType))
                    {
                        if ((Object)property.GetValue(behaviour) == target)
                        {
                            return true;
                        }
                    }

                    if (typeof(UnityEvent).IsAssignableFrom(propertyType))
                    {
                        var unityEvent = (UnityEvent)property.GetValue(behaviour);
                        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
                        {
                            if (unityEvent.GetPersistentTarget(i) == target)
                            {
                                return true;
                            }
                        }
                    }

                    if (typeof(SerializationData).IsAssignableFrom(propertyType))
                    {
                        var serializationData = (SerializationData)property.GetValue(behaviour);
                        foreach (var objectRef in serializationData.objectReferences)
                        {
                            if (objectRef == target)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
