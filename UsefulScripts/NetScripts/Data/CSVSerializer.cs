using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace UsefulScripts.NetScripts.Data
{
    public class CSVSerializer
    {
        public static T[] Deserialize<T>(string text)
        {
            return (T[]) CreateArray(typeof(T), ParseCSV(text));
        }

        public static T[] Deserialize<T>(List<string[]> rows)
        {
            return (T[]) CreateArray(typeof(T), rows);
        }

        public static T DeserializeIdValue<T>(string text, int id_col = 0, int value_col = 1)
        {
            return (T) CreateIdValue(typeof(T), ParseCSV(text), id_col, value_col);
        }

        public static T DeserializeIdValue<T>(List<string[]> rows, int id_col = 0, int value_col = 1)
        {
            return (T) CreateIdValue(typeof(T), rows, id_col, value_col);
        }

        private static object CreateArray(Type type, List<string[]> rows)
        {
            var array_value = Array.CreateInstance(type, rows.Count - 1);
            var table = new Dictionary<string, int>();

            for (var i = 0; i < rows[0].Length; i++)
            {
                var id = rows[0][i];
                var id2 = "";
                for (var j = 0; j < id.Length; j++)
                    if (id[j] >= 'a' && id[j] <= 'z' || id[j] >= '0' && id[j] <= '9')
                        id2 += id[j].ToString();
                    else if (id[j] >= 'A' && id[j] <= 'Z')
                        id2 += ((char) (id[j] - 'A' + 'a')).ToString();

                table.Add(id, i);
                if (!table.ContainsKey(id2))
                    table.Add(id2, i);
            }

            for (var i = 1; i < rows.Count; i++)
            {
                var rowdata = Create(rows[i], table, type);
                array_value.SetValue(rowdata, i - 1);
            }

            return array_value;
        }

        private static object Create(string[] cols, Dictionary<string, int> table, Type type)
        {
            var v = Activator.CreateInstance(type);

            var fieldinfo = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var tmp in fieldinfo)
                if (table.ContainsKey(tmp.Name))
                {
                    var idx = table[tmp.Name];
                    if (idx < cols.Length)
                        SetValue(v, tmp, cols[idx]);
                }

            return v;
        }

        private static void SetValue(object v, FieldInfo fieldinfo, string value)
        {
            if (value == null || value == "")
                return;

            if (fieldinfo.FieldType.IsArray)
            {
                var elementType = fieldinfo.FieldType.GetElementType();
                var elem = value.Split(',');
                var array_value = Array.CreateInstance(elementType, elem.Length);
                for (var i = 0; i < elem.Length; i++)
                    if (elementType == typeof(string))
                        array_value.SetValue(elem[i], i);
                    else
                        array_value.SetValue(Convert.ChangeType(elem[i], elementType), i);
                fieldinfo.SetValue(v, array_value);
            }
            else if (fieldinfo.FieldType.IsEnum)
            {
                fieldinfo.SetValue(v, Enum.Parse(fieldinfo.FieldType, value));
            }
            else if (value.IndexOf('.') != -1 &&
                     (fieldinfo.FieldType == typeof(int) || fieldinfo.FieldType == typeof(long) ||
                      fieldinfo.FieldType == typeof(short)))
            {
                var f = (float) Convert.ChangeType(value, typeof(float));
                fieldinfo.SetValue(v, Convert.ChangeType(f, fieldinfo.FieldType));
            }
#if UNITY_EDITOR
            else if (fieldinfo.FieldType == typeof(Sprite))
            {
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(value);
                fieldinfo.SetValue(v, sprite);
            }
#endif
            else if (fieldinfo.FieldType == typeof(string))
            {
                fieldinfo.SetValue(v, value);
            }
            else
            {
                fieldinfo.SetValue(v, Convert.ChangeType(value, fieldinfo.FieldType));
            }
        }

        private static object CreateIdValue(Type type, List<string[]> rows, int id_col = 0, int val_col = 1)
        {
            var v = Activator.CreateInstance(type);

            var table = new Dictionary<string, int>();

            for (var i = 1; i < rows.Count; i++)
                if (rows[i][id_col].Length > 0)
                    table.Add(rows[i][id_col].TrimEnd(' '), i);

            var fieldinfo = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var tmp in fieldinfo)
                if (table.ContainsKey(tmp.Name))
                {
                    var idx = table[tmp.Name];
                    if (rows[idx].Length > val_col)
                        SetValue(v, tmp, rows[idx][val_col]);
                }
                else
                {
                    Debug.Log("Miss " + tmp.Name);
                }

            return v;
        }

        public static List<string[]> ParseCSV(string text, char separator = ',')
        {
            var lines = new List<string[]>();
            var line = new List<string>();
            var token = new StringBuilder();
            var quotes = false;

            for (var i = 0; i < text.Length; i++)
                if (quotes)
                {
                    if (text[i] == '\\' && i + 1 < text.Length && text[i + 1] == '\"' ||
                        text[i] == '\"' && i + 1 < text.Length && text[i + 1] == '\"')
                    {
                        token.Append('\"');
                        i++;
                    }
                    else if (text[i] == '\\' && i + 1 < text.Length && text[i + 1] == 'n')
                    {
                        token.Append('\n');
                        i++;
                    }
                    else if (text[i] == '\"')
                    {
                        line.Add(token.ToString());
                        token = new StringBuilder();
                        quotes = false;
                        if (i + 1 < text.Length && text[i + 1] == separator)
                            i++;
                    }
                    else
                    {
                        token.Append(text[i]);
                    }
                }
                else if (text[i] == '\r' || text[i] == '\n')
                {
                    if (token.Length > 0)
                    {
                        line.Add(token.ToString());
                        token = new StringBuilder();
                    }

                    if (line.Count > 0)
                    {
                        lines.Add(line.ToArray());
                        line.Clear();
                    }
                }
                else if (text[i] == separator)
                {
                    line.Add(token.ToString());
                    token = new StringBuilder();
                }
                else if (text[i] == '\"')
                {
                    quotes = true;
                }
                else
                {
                    token.Append(text[i]);
                }

            if (token.Length > 0) line.Add(token.ToString());
            if (line.Count > 0) lines.Add(line.ToArray());
            return lines;
        }
    }
}