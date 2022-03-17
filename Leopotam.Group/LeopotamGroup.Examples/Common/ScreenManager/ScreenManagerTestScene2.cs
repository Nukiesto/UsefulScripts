using Leopotam.Group.Common;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Common.ScreenManager {
    public class ScreenManagerTestScene2 : MonoBehaviour {
        void OnGUI () {
            GUILayout.Label ("Second scene loaded!");
            if (GUILayout.Button ("Go back to first scene")) {
                Service<Group.Common.ScreenManager>.Get ().NavigateBack ();
            }
        }
    }
}