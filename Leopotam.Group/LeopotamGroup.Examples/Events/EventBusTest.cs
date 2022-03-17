using Leopotam.Group.Events;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Events {
    class TestEvent1 {
        public int IntValue;

        public string StringValue;

        public override string ToString () {
            return string.Format ("[IntValue: {0}, StringValue: {1}]", IntValue, StringValue);
        }
    }

    class TestEvent2 {
        public float FloatValue;

        public object ObjectValue;

        public override string ToString () {
            return string.Format ("[FloatValue: {0}, ObjectValue: {1}]", FloatValue, ObjectValue);
        }
    }

    struct TestEvent3 {
        public string StringValue;
    }

    class EventBusTest : MonoBehaviour {
        EventBus _bus;

        void Awake () {
            _bus = new EventBus ();
        }

        void OnEnable () {
            // subscription.
            _bus.Subscribe<TestEvent1> (OnEvent11);
            _bus.Subscribe<TestEvent1> (OnEvent12);
            _bus.Subscribe<TestEvent1> (d => {
                // As lambda.
                Debug.Log ("[EVENT1-SUBSCRIBER3] => " + d);
            });

            _bus.Subscribe<TestEvent2> (OnEvent21);

            _bus.Subscribe<TestEvent3> (OnEvent31);

            _bus.Subscribe<float> (OnEventFloat);

            // test data.
            // class 1
            var data1 = new TestEvent1 {
                IntValue = 1,
                StringValue = "123"
            };

            // class 2
            var data2 = new TestEvent2 {
                FloatValue = 123.456f,
                ObjectValue = "String as object"
            };

            // struct
            var data3 = new TestEvent3 {
                StringValue = "String inside struct"
            };

            // publishing.
            _bus.Publish<TestEvent1> (data1);

            _bus.Publish<TestEvent2> (data2);

            _bus.Publish<TestEvent3> (data3);

            // test simple typed value
            _bus.Publish<float> (1f + 0.2345f);
        }

        void OnDisable () {
            _bus.UnsubscribeAndClearAllEvents ();
        }

        void OnEvent11 (TestEvent1 msg) {
            Debug.Log ("[EVENT1-SUBSCRIBER1] => " + msg);
        }

        void OnEvent12 (TestEvent1 msg) {
            Debug.Log ("[EVENT1-SUBSCRIBER2] => " + msg + " -> interrupt execution flow of " + msg.GetType ().Name);
        }

        void OnEvent21 (TestEvent2 msg) {
            Debug.Log ("[EVENT2-SUBSCRIBER] => " + msg);
        }

        void OnEvent31 (TestEvent3 msg) {
            Debug.Log ("[EVENT3-SUBSCRIBER] => " + msg.StringValue);
        }

        void OnEventFloat (float msg) {
            Debug.Log ("[FLOATEVENT-SUBSCRIBER] => " + msg);
        }
    }
}