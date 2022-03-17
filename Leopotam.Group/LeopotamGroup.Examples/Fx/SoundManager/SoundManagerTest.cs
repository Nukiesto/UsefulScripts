using Leopotam.Group.Common;
using Leopotam.Group.Fx;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Fx.SoundManager {
    public class SoundManagerTest : MonoBehaviour {
        [SerializeField]
        AudioClip _fxClip = null;

        const string MusicName = "Music/Forest";

        void OnGUI () {
            var sm = Service<Group.Fx.SoundManager>.Get ();
            if (GUILayout.Button ("Turn on music")) {
                sm.PlayMusic (MusicName, true);
            }
            if (GUILayout.Button ("Turn off music")) {
                sm.StopMusic ();
            }
            if (_fxClip != null && GUILayout.Button ("Play FX at channel 1 without interrupt")) {
                sm.PlayFx (_fxClip);
            }
            if (_fxClip != null && GUILayout.Button ("Play FX at channel 1 with interrupt")) {
                sm.PlayFx (_fxClip, SoundFxChannel.First, true);
            }
        }
    }
}