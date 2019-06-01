using System;
using System.IO;
using Newtonsoft.Json;

namespace MarauderEngine.Utilities
{
    public class LoadGame<T>
    {
        private string path;
        private const string folderPath = @"Saves\";
        public LoadGame(string path)
        {
            this.path = path;
        }


        public T[] LoadGameData(string fileName)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            string newPath = folderPath + @"\" + fileName + ".json";
            
            string contents = File.ReadAllText(newPath);
            T[] tempEntities =  JsonConvert.DeserializeObject<T[]>(contents,settings);
            for (int i = 0; i < tempEntities.Length; i++)
            {
                Console.WriteLine(tempEntities[i].GetType().ToString());
            }
            return tempEntities;
            
        }
    }
}
