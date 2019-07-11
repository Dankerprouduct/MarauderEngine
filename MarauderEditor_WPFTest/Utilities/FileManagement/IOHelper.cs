using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarauderEditor.Utilities.FileManagement
{
    public static class IOHelper
    {

        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            Formatting = Formatting.Indented
        };

        /// <summary>
        /// Saves the given json in the given folder with the given name.
        /// </summary>
        /// <param name="fileJson"></param>
        /// <param name="filePath"></param>
        public static void SaveFile(string fileJson, string folderPath, string fileName)
        {
            var path = folderPath + "\\" + fileName;
            File.WriteAllText(path, fileJson);


        }

        /// <summary>
        /// Saves the given data class in the given folder with the given name.
        /// </summary>
        /// <param name="fileJson"></param>
        /// <param name="filePath"></param>
        public static void SaveFile<T>(T fileData, string folderPath, string fileName)
        {
            var json = JsonConvert.SerializeObject(fileData, Formatting.Indented, JsonSettings);
            var path = folderPath + "\\" + fileName;
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Loads and returns the folder as a string.
        /// </summary>
        /// <param name="fileJson"></param>
        /// <param name="filePath"></param>
        public static string LoadFile(string folderPath, string fileName)
        {
            var path = folderPath + "\\" + fileName;
            return File.ReadAllText(path);
        }

        /// <summary>
        /// Loads and returns the given file as a string.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string LoadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Loads & returns the file as the given type.
        /// </summary>
        /// <param name="fileJson"></param>
        /// <param name="filePath"></param>
        public static T LoadFile<T>(string folderPath, string fileName)
        {
            var path = folderPath + "\\" + fileName;
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path), JsonSettings);
        }

        /// <summary>
        /// Loads & returns the file as the given type.
        /// </summary>
        /// <param name="fileJson"></param>
        /// <param name="filePath"></param>
        public static T LoadFile<T>(string filePath)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath), JsonSettings);
        }

    }
}
