using Leopotam.Group.Common;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Fx.FadeManager {
    public class FadeManagerTest : MonoBehaviour {
        Color _color1 = Color.black;
        Color _color2 = Color.clear;

        bool _isLocked;

        void OnGUI () {
            if (!_isLocked) {
                if (GUILayout.Button ("Fade in/ Fade out")) {
                    _isLocked = true;
                    Service<Group.Fx.FadeManager>.Get ().Process (_color1, _color2, 1f, () => {
                        var t = _color1;
                        _color1 = _color2;
                        _color2 = t;
                        _isLocked = false;
                    });
                }
            }
        }
    }
}