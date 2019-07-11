using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MarauderEditor.Utilities.FileManagement
{
    public interface IJsonSerializable<out T>
    {
        T FromJson(string json);

        string ToJson();

    }

    public abstract class JsonSerializable<T>
    {
        public static T FromJson(string json) => JsonConvert.DeserializeObject<T>(json, IOHelper.JsonSettings);

        public string ToJson() => JsonConvert.SerializeObject(this, IOHelper.JsonSettings);
    }
}
