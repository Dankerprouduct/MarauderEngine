using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Steamworks;


namespace MarauderEngine.Utilities
{
    public class GameData<T>
    {

        public JsonSerializerSettings settings { get; private set; }
        public string folderPath = @"Saves\";

        /// <summary>
        /// give the name of the subfolder inside of "Saves"
        /// Create a new one if needed
        /// </summary>
        /// <param name="path"></param>
        public  GameData()
        {
            settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full
            };            
        }

        public GameData(JsonSerializerSettings settings)
        {
            this.settings = settings;
        }



        /// <summary>
        /// Serializes Data into an indented json file
        /// </summary>
        /// <param name="data">data to serialize</param>
        /// <param name="fileName">name of file to create or overide</param>
        public void SaveData(T[] data, string fileName, string extension = ".json")
        {
            
            string json = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
            string newPath = folderPath + @"\" + fileName + extension;
            (new FileInfo(newPath)).Directory.Create();

            File.WriteAllText(newPath, json);
        }

        /// <summary>
        /// Serializes Data into an indented json file
        /// </summary>
        /// <param name="data">data to serialize</param>
        /// <param name="fileName">name of file to create or overide</param>
        public void SaveData(T data, string fileName, string extension = ".json")
        {

            string json = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
            string newPath = folderPath + @"\" + fileName + extension;
            (new FileInfo(newPath)).Directory.Create();

            File.WriteAllText(newPath, json);
        }

        /// <summary>
        /// Deserializes data into an array
        /// </summary>
        /// <param name="fileName">name of file to deserialize</param>
        /// <returns></returns>
        public T[] LoadData(string fileName, string extension = ".json")
        {
            string newPath = folderPath + @"\" + fileName + extension;

            string contents = File.ReadAllText(newPath);
            T[] tempEntities = JsonConvert.DeserializeObject<T[]>(contents, settings);
            for (int i = 0; i < tempEntities.Length; i++)
            {
                //Console.WriteLine(tempEntities[i].GetType().ToString());
            }
            return tempEntities;

        }

        /// <summary>
        /// Deserializes data into an object
        /// </summary>
        /// <param name="fileName">name of file to deserialize</param>
        /// <returns></returns>
        public T LoadObjectData(string fileName, string extension = ".json")
        {
            string newPath = folderPath + @"\" + fileName + extension;

            string contents = File.ReadAllText(newPath);


            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine($"assembly: {assembly}");
            }

            T tempEntities = JsonConvert.DeserializeObject<T>(contents, settings);
            return tempEntities;

        }

        /// <summary>
        /// Deserializes data into an object
        /// </summary>
        /// <param name="fileName">name of file to deserialize</param>
        /// <returns></returns>
        public T LoadObjectData(string fileName, string extension, string assemblyPath)
        {
            string newPath = folderPath + @"\" + fileName + extension;

            string contents = File.ReadAllText(newPath);
            var assembly = Assembly.LoadFrom(assemblyPath);
            AppDomain.CurrentDomain.Load(assembly.GetName());
             
            settings.SerializationBinder = new CustomAssemblyBinder(assembly);

            T tempEntities = JsonConvert.DeserializeObject<T>(contents, settings);
            return tempEntities;

        }

        /// <summary>
        /// Serializes Data into an indented json file
        /// </summary>
        /// <param name="data">data to serialize</param>
        /// <param name="fileName">name of file to create or overide</param>
        public void SaveZippedData(T data, string fileName, string extension = ".zip")
        {
            var tempFolder = @"Temp\";

            string json = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
            string newPath = folderPath + tempFolder + @"\" + fileName + ".json";
            (new FileInfo(folderPath + tempFolder + @"\")).Directory.Create();
            
            File.WriteAllText(newPath, json);
            ZipFile.CreateFromDirectory(folderPath + tempFolder, folderPath + @"\" + fileName + extension, CompressionLevel.Optimal, false);
            (new FileInfo(newPath)).Delete();
            (new FileInfo(folderPath + tempFolder + @"\")).Directory.Delete();
        }
        
        /// <summary>
        /// Deserializes data into an object
        /// </summary>
        /// <param name="fileName">name of file to deserialize</param>
        /// <returns></returns>
        public T LoadZippedData(string fileName, string extension = ".zip")
        {
            var tempFolder = @"Temp\";
            (new FileInfo(folderPath + tempFolder + @"\")).Directory.Create();
            ZipFile.ExtractToDirectory(folderPath + @"\" + fileName + extension, folderPath + tempFolder + @"\");

            string contents = File.ReadAllText(folderPath + tempFolder + @"\" + fileName + ".json");
            (new FileInfo(folderPath + tempFolder + @"\" + fileName + ".json")).Delete();

            (new FileInfo(folderPath + tempFolder + @"\")).Directory.Delete();

            T tempEntities = JsonConvert.DeserializeObject<T>(contents, settings);

            return tempEntities;

        }
    }

    public class CustomAssemblyBinder : ISerializationBinder
    {
        private Assembly _referencedAssembly;
        private List<Type> _knownTypes = new List<Type>();
        public CustomAssemblyBinder(Assembly assembly)
        {
            _referencedAssembly = assembly;

            foreach (var type in _referencedAssembly.GetTypes())
            {
                _knownTypes.Add(type.UnderlyingSystemType);
            }

            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                _knownTypes.Add(type.UnderlyingSystemType);
            }
        }
        public Type BindToType(string assemblyName, string typeName)
        {
            Console.WriteLine("trying to load:" + typeName);
            return _knownTypes.SingleOrDefault(i => i.FullName == typeName);

        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    } 
}
