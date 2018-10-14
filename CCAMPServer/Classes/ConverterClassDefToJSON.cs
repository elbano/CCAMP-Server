using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CCAMPServer.Classes
{
    public class ConverterClassDefToJSON : JsonConverter
    {

        private readonly Type[] _types;

        public ConverterClassDefToJSON(params Type[] types)
        {
            _types = types;
        }


        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken jToken = JToken.FromObject(value);

            if (jToken.Type != JTokenType.Object)
            {
                jToken.WriteTo(writer);
            }
            else
            {
                var currValue = value as SearchParameters;

                var innerClasses = currValue.GetType().GetNestedTypes(BindingFlags.Public);

                JObject jsonOject = (JObject)jToken;
                var lstObjs = new List<JObject>();

                foreach (var currClass in innerClasses)
                {
                    GetData(currClass, lstObjs);
                }

                jsonOject.Add("parameters", new JArray(lstObjs));

                jsonOject.WriteTo(writer);
            }
        }


        private static void GetData(Type type, List<JObject> lstJObjects)
        {
            var nested = type.GetNestedTypes();
            if (nested.Length == 0)
            {
                var data = GetConstantValues(type);
                if (data.Count > 0)
                    GenerateJObjectFromData(data, lstJObjects);

            }
            else
            {
                var data = GetConstantValues(type);

                if (data.Count >0)
                    GenerateJObjectFromData(data, lstJObjects);

                foreach (var nest in nested)
                {
                    GetData(nest, lstJObjects);
                }
            }
        }



        /// <summary>
        /// Return all the values of constants of the specified type
        /// </summary>
        private static Dictionary<string, string> GetConstantValues(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public
                | BindingFlags.Static
                | BindingFlags.FlattenHierarchy);

            var dictionary = new Dictionary<string, string>();

            var data = (fields.Where(fieldInfo => fieldInfo.IsLiteral
                 && !fieldInfo.IsInitOnly
                 && fieldInfo.FieldType == typeof(String)).ToList());

            foreach (var item in data)
            {
                dictionary.Add(item.Name, (string)item.GetRawConstantValue());
            }

            return dictionary;    
        }

        private static void GenerateJObjectFromData(Dictionary<string, string> data, List<JObject> jObjects)
        {
            var parameterList = data.Where(d => d.Key.IndexOf("_PARAMETER") > -1).ToList();
            if (parameterList.Count > 1)
            {
                //parameters with no possible alues
                foreach (var item in parameterList)
                {
                    var json = new JObject();
                    json.Add("parameter", item.Value);
                    jObjects.Add(json);
                }
            }
            else
            {
                var json = new JObject();
                json.Add("parameter", parameterList[0].Value);
                json.Add("values", new JArray(data.Where(d => !d.Key.Equals(parameterList[0].Key)).Select(d => d.Value).ToList()));
                jObjects.Add(json);
            }
        }

    }
}
