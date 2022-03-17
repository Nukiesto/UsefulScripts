using System.Collections;
using Leopotam.Group.Common;
using Leopotam.Group.SystemUi.DataBinding;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.SystemUi.DataBinding {
    /// <summary>
    /// Test user data.
    /// </summary>
    class TestData : IDataSource {
        int _value1;

        string _value2;

        Color _value3;

        bool _value4;

        Texture2D _value5;

        public int Value1 {
            get { return _value1; }
            set {
                if (_value1 != value) {
                    _value1 = value;
                    OnBindedDataChanged (this, "Value1");
                }
            }
        }

        public string Value2 {
            get { return _value2; }
            set {
                if (_value2 != value) {
                    _value2 = value;
                    OnBindedDataChanged (this, "Value2");
                }
            }
        }

        public Color Value3 {
            get { return _value3; }
            set {
                if (_value3 != value) {
                    _value3 = value;
                    OnBindedDataChanged (this, "Value3");
                }
            }
        }

        public bool Value4 {
            get { return _value4; }
            set {
                if (_value4 != value) {
                    _value4 = value;
                    OnBindedDataChanged (this, "Value4");
                }
            }
        }

        public Texture2D Value5 {
            get { return _value5; }
            set {
                if (_value5 != value) {
                    _value5 = value;
                    OnBindedDataChanged (this, "Value5");
                }
            }
        }

        public event OnBindedDataChangedHandler OnBindedDataChanged = delegate { };
    }

    class DataBindingTest : MonoBehaviour {
        [SerializeField]
        Texture2D[] _textures = null;

        IEnumerator Start () {
            // create new data holder.
            var data = new TestData ();
            var data1 = new TestData ();

            // connect data to binding system.
            Service<DataStorage>.Get ().SetDataSource ("TestData", data);
            Service<DataStorage>.Get ().SetDataSource ("TestData1", data1);

            var waiter = new WaitForSeconds (0.1f);

            // and update it in loop.
            while (true) {
                data.Value1 = (data.Value1 + 1) % 100;
                data.Value2 = string.Format ("Super number {0}", data.Value1);
                data.Value3 = Color.Lerp (Color.black, Color.white, data.Value1 / 100f);
                if ((data.Value1 % 5) == 0) {
                    data.Value4 = !data.Value4;
                }
                if (_textures.Length > 0) {
                    data.Value5 = _textures[data.Value1 % _textures.Length];
                    data1.Value5 = _textures[(data.Value1 / 8) % _textures.Length];
                }
                yield return waiter;
            }
        }
    }
}