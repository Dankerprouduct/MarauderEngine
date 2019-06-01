using System.IO;
using Newtonsoft.Json;

namespace MarauderEngine.Utilities
{
    [System.Obsolete("use game data ", true)]
    public class SaveGame<T>
    {
        private string path; 
        private const string folderPath = @"Saves\";
        public SaveGame(string path)
        {
            this.path = path;
        }

        public void SaveGameData(T[] entities, string fileName)
        {
            
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string json = JsonConvert.SerializeObject(entities, Formatting.Indented, settings);
            string newPath = folderPath + @"\" + fileName + ".json";
            (new FileInfo(newPath)).Directory.Create();

            File.WriteAllText(newPath, json);


        }

    }
}
