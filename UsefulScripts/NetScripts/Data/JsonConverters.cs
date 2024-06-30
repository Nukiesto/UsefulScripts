// Solutions to prevent serialization errors. Seen in https://forum.unity.com/threads/jsonserializationexception-self-referencing-loop-detected.1264253/
// Newtonsoft struggles serializing structs like Vector3 because it has a property .normalized
// that references Vector3, and thus entering a self-reference loop throwing circular reference error.

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace UsefulScripts.NetScripts.Data
{
    public sealed class NewtonsoftColorConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            JObject obj = new JObject
            (
            new JProperty("r", value.r),
            new JProperty("g", value.g),
            new JProperty("b", value.b),
            new JProperty("a", value.a)
            );
 
            obj.WriteTo(writer);
        }
 
        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Color
            (
            obj.Value<float>("r"),
            obj.Value<float>("g"),
            obj.Value<float>("b"),
            obj.Value<float>("a")
            );
        }
    }
 
    public sealed class NewtonsoftQuaternionConverter : JsonConverter<Quaternion>
    {
        public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
        {
            JObject obj = new JObject
            (
            new JProperty("x", value.x),
            new JProperty("y", value.y),
            new JProperty("z", value.z),
            new JProperty("w", value.w)
            );
 
            obj.WriteTo(writer);
        }
 
        public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Quaternion
            (
            obj.Value<float>("x"),
            obj.Value<float>("y"),
            obj.Value<float>("z"),
            obj.Value<float>("w")
            );
        }
    }
 
    public sealed class NewtonsoftVector2Converter : JsonConverter<Vector2>
    {
        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            JObject obj = new JObject
            (
            new JProperty("x", value.x),
            new JProperty("y", value.y)
            );
 
            obj.WriteTo(writer);
        }
 
        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector2
            (
            obj.Value<float>("x"),
            obj.Value<float>("y")
            );
        }
    }
 
    public sealed class NewtonsoftVector2IntConverter : JsonConverter<Vector2Int>
    {
        public override void WriteJson(JsonWriter writer, Vector2Int value, JsonSerializer serializer)
        {
            JObject obj = new JObject
            (
            new JProperty("x", value.x),
            new JProperty("y", value.y)
            );
 
            obj.WriteTo(writer);
        }
 
        public override Vector2Int ReadJson(JsonReader reader, Type objectType, Vector2Int existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector2Int
            (
            obj.Value<int>("x"),
            obj.Value<int>("y")
            );
        }
    }
 
    public sealed class NewtonsoftVector3Converter : JsonConverter<Vector3>
    {
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            JObject obj = new JObject
            (
            new JProperty("x", value.x),
            new JProperty("y", value.y),
            new JProperty("z", value.z)
            );
 
            obj.WriteTo(writer);
        }
 
        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector3
            (
            obj.Value<float>("x"),
            obj.Value<float>("y"),
            obj.Value<float>("z")
            );
        }
    }
 
    public sealed class NewtonsoftVector3IntConverter : JsonConverter<Vector3Int>
    {
        public override void WriteJson(JsonWriter writer, Vector3Int value, JsonSerializer serializer)
        {
            JObject obj = new JObject
            (
            new JProperty("x", value.x),
            new JProperty("y", value.y),
            new JProperty("z", value.z)
            );
 
            obj.WriteTo(writer);
        }
 
        public override Vector3Int ReadJson(JsonReader reader, Type objectType, Vector3Int existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector3Int
            (
            obj.Value<int>("x"),
            obj.Value<int>("y"),
            obj.Value<int>("z")
            );
        }
    }
 
    public sealed class NewtonsoftVector4Converter : JsonConverter<Vector4>
    {
        public override void WriteJson(JsonWriter writer, Vector4 value, JsonSerializer serializer)
        {
            JObject obj = new JObject
            (
            new JProperty("x", value.x),
            new JProperty("y", value.y),
            new JProperty("z", value.z),
            new JProperty("w", value.w)
            );
 
            obj.WriteTo(writer);
        }
 
        public override Vector4 ReadJson(JsonReader reader, Type objectType, Vector4 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Vector4
            (
            obj.Value<float>("x"),
            obj.Value<float>("y"),
            obj.Value<float>("z"),
            obj.Value<float>("w")
            );
        }
    }
}