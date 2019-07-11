using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace MarauderEditor.UI.SceneHierarchy
{
    public static class JsonTreeViewLoader
    {
        public static void LoadJsonToTreeView(this TreeView treeView, string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return;
            }

            var @object = JObject.Parse(json);
            AddObjectNodes(@object, "JSON", treeView.Items);
        }

        public static void AddObjectNodes(JObject @object, string name, ItemCollection parent)
        {
            var node = new TreeViewItem()
            {
                Name = name,
            };
            parent.Add(node);

            foreach (var property in @object.Properties())
            {
                AddTokenNodes(property.Value, property.Name, node.Items);
            }
        }

        private static void AddArrayNodes(JArray array, string name, ItemCollection parent)
        {
            var node = new TreeViewItem()
            {
                Name = name,
            };
            parent.Add(node);

            for (var i = 0; i < array.Count; i++)
            {
                AddTokenNodes(array[i], string.Format("[{0}]", i), node.Items);
            }
        }

        private static void AddTokenNodes(JToken token, string name, ItemCollection parent)
        {
            if (token is JValue)
            {
                parent.Add(new TreeViewItem()
                {
                    Name = (string.Format("{0}: {1}", name, ((JValue)token).Value))
                });
            }
            else if (token is JArray)
            {
                AddArrayNodes((JArray)token, name, parent);
            }
            else if (token is JObject)
            {
                AddObjectNodes((JObject)token, name, parent);
            }
        }
    }
}
