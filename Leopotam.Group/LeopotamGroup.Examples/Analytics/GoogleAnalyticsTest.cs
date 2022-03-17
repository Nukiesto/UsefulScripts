using LeopotamGroup.Analytics;
using LeopotamGroup.Common;
using UnityEngine;

namespace LeopotamGroup.Examples.Analytics.GoogleAnalyticsTest {
    public class GoogleAnalyticsTest : MonoBehaviour {
        void OnGUI () {
            var ga = Service<GoogleAnalyticsManager>.Get ();
            if (!ga.IsInited) {
                GUILayout.Label ("Fill TrackerID field for GoogleAnalytics object first!");
                return;
            }

            GUILayout.Label ("Device identifier: " + ga.DeviceHash);

            if (GUILayout.Button ("Track 'Screen Test opened'")) {
                ga.TrackScreen ("Test");
            }
            if (GUILayout.Button ("Track 'Item.001 purchased'")) {
                ga.TrackEvent ("Purchases", "Item.001");
            }
            if (GUILayout.Button ("Track 'Exception raised'")) {
                ga.TrackException ("OMG, app crashed", true);
            }
        }
    }
}