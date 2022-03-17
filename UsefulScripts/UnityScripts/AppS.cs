using System.IO;
using UnityEngine;

namespace UsefulScripts.UnityScripts
{
    public static class AppS
    {
        public static string GetDirectoryGame
        {
            get
            {
                var dirPath = Directory.GetParent(Application.dataPath)?.FullName;
                return dirPath ?? Application.dataPath;
            }
        }
    }
}