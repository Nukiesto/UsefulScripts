using Leopotam.Group.Common;
using Leopotam.Group.Scripting;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Scripting {
    public class ScriptingTest : MonoBehaviour {
        const string ScriptName = "Scripts/TestScript";

        const string MainFunctionName = "main";

        void Start () {
            var scriptSource = Resources.Load<TextAsset> (ScriptName).text;
            var err = Service<MyScriptManager>.Get ().LoadSource (scriptSource);
            if (err != null) {
                // We have error here.
                Debug.LogWarning (err);
                return;
            }

            // Code was loaded and parsed correctly, we ready to call functions
            ScriptVar retVal;
            err = Service<MyScriptManager>.Get ().CallFunction (MainFunctionName, out retVal, new ScriptVar (1), new ScriptVar ("str"));
            if (err != null) {
                // We have error here.
                Debug.LogWarning (err);
                return;
            }
        }
    }
}