using LeopotamGroup.Common;
using LeopotamGroup.Events;
using LeopotamGroup.SystemUi.Actions;
using UnityEngine;

namespace LeopotamGroup.Examples.SystemUi.ActionsTest {
    public class ActionsTest : MonoBehaviour {
        [SerializeField]
        string _onClickFilteredGroup;

        int _onClickFilteredGroupId;

        void Awake () {
            // save groupId hash for fast filtering, we dont need to calculate hash each time for performance reason.
            _onClickFilteredGroupId = _onClickFilteredGroup.GetUiActionGroupId ();
        }

        void OnEnable () {
            // Subscribe to scene events pipeline.
            var ueb = Service<UnityEventBus>.Get ();
            ueb.Subscribe<UiClickActionData> (OnClick);
            ueb.Subscribe<UiDragActionData> (OnDrag);
            ueb.Subscribe<UiPressActionData> (OnPress);
            ueb.Subscribe<UiReleaseActionData> (OnRelease);
            ueb.Subscribe<UiEnterActionData> (OnEnter);
            ueb.Subscribe<UiExitActionData> (OnExit);
            ueb.Subscribe<UiSelectActionData> (OnSelect);
            ueb.Subscribe<UiDeselectActionData> (OnDeselect);
            ueb.Subscribe<UiScrollActionData> (OnScroll);
        }

        void OnDisable () {
            // Unsubscribe to scene events pipeline. We should check first - is pipeline still exists or already killed?
            // If you want to change scene - you can ignore unsibscription, UnityEventBus will try to do it automatically.
            // But better to cleanup it correctly in right way and at right time.
            if (Service<UnityEventBus>.IsRegistered) {
                // You can set second parameter to true if you want to decrease memory allocation
                // for same events at current scene.
                var ueb = Service<UnityEventBus>.Get ();
                ueb.Unsubscribe<UiClickActionData> (OnClick, true);
                ueb.Unsubscribe<UiDragActionData> (OnDrag, true);
                ueb.Unsubscribe<UiPressActionData> (OnPress, true);
                ueb.Unsubscribe<UiReleaseActionData> (OnRelease, true);
                ueb.Unsubscribe<UiEnterActionData> (OnEnter, true);
                ueb.Unsubscribe<UiExitActionData> (OnExit, true);
                ueb.Unsubscribe<UiSelectActionData> (OnSelect, true);
                ueb.Unsubscribe<UiDeselectActionData> (OnDeselect, true);
                ueb.Unsubscribe<UiScrollActionData> (OnScroll, true);
            }
        }

        void OnClick (UiClickActionData data) {
            // We can filter messages based on logical group id.
            if (data.GroupId == _onClickFilteredGroupId) {
                Debug.Log ("OnClick: " + data.Sender.name);
            } else {
                Debug.Log ("OnClick: " + data.Sender.name + ", but we will skip it.");
            }
        }

        void OnDrag (UiDragActionData data) {
            Debug.Log ("OnDrag: " + data.EventData.delta);
            var pos = data.Sender.transform.position;
            pos += new Vector3 (data.EventData.delta.x, data.EventData.delta.y, 0f);
            data.Sender.transform.position = pos;
        }

        void OnRelease (UiReleaseActionData data) {
            Debug.Log ("OnRelease: " + data.Sender.name);
        }

        void OnPress (UiPressActionData data) {
            Debug.Log ("OnPress: " + data.Sender.name);
        }

        void OnExit (UiExitActionData data) {
            Debug.Log ("OnExit: " + data.Sender.name);
        }

        void OnEnter (UiEnterActionData data) {
            Debug.Log ("OnEnter: " + data.Sender.name);
        }

        void OnDeselect (UiDeselectActionData data) {
            Debug.Log ("OnDeselect: " + data.Sender.name);
        }

        void OnSelect (UiSelectActionData data) {
            Debug.Log ("OnSelect: " + data.Sender.name);
        }

        void OnScroll (UiScrollActionData data) {
            Debug.Log ("OnScroll: " + data.Sender.name);
        }
    }
}