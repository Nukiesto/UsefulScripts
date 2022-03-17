using System.Collections;
using Leopotam.Group.Common;
using Leopotam.Group.Math;
using UnityEngine;

namespace Leopotam.Group.LeopotamGroup.Examples.Math {
    public class MathTest : MonoBehaviour {
        IEnumerator Start () {
            yield return new WaitForSeconds (1f);
            IntToShortStringTest ();
            FloatToNormalizedStringTest ();
            StringToFloatTest ();
            StringToFloatUncheckedTest ();
            RngTest ();
            SinTest ();
            CosTest ();
            Atan2Test ();
        }

        void IntToShortStringTest () {
            foreach (var item in new [] {
                    0,
                    123,
                    1234,
                    1234567,
                    -1234567,
                    1234567890
                }) {
                Debug.LogFormat ("{0}.ToStringWithSuffix = {1}", item, item.ToStringWithSuffix ());
            }
        }

        void FloatToNormalizedStringTest () {
            foreach (var item in new [] {
                    0f,
                    123f,
                    123.45678f,
                    123.45670f, -123.123f,
                    0.12345f,
                    0.00005f
                }) {
                Debug.LogFormat ("{0:0.#####}.ToStringFast = {1}", item, item.ToStringFast ());
            }
        }

        void StringToFloatTest () {
            foreach (var item in new [] {
                    "0",
                    "123",
                    "123.45678",
                    "123.45670",
                    "-123.123",
                    "0.12345",
                    "0.00005"
                }) {
                Debug.LogFormat ("{0}.ToFloat(invariant culture) = {1:0.#####}", item, item.ToFloat ());
            }
        }

        void StringToFloatUncheckedTest () {
            foreach (var item in new [] {
                    "0",
                    "123",
                    "123.45678",
                    "123.45670",
                    "-123.123",
                    "0.12345",
                    "0.00005"
                }) {
                Debug.LogFormat ("{0}.ToFloatUnchecked(invariant culture, GC optimized, [digits | '.' | '-']) = {1:0.#####}", item,
                    item.ToFloatUnchecked ());
            }
        }

        void RngTest () {
            var rng = Service<Rng>.Get ();
            for (var i = 0; i < 5; i++) {
                Debug.LogFormat ("Rng.GetFloat [0;1]: {0}", rng.GetFloat (true));
            }
            for (var i = 0; i < 5; i++) {
                Debug.LogFormat ("Rng.GetIntStatic [0;100): {0}", rng.GetInt (100));
            }
        }

        void SinTest () {
            Debug.Log (">>>>> sin tests >>>>>");
            const int T = 10000;
            var sw = new System.Diagnostics.Stopwatch ();
            float f;
            float s = 1.345f;

            sw.Reset ();
            sw.Start ();
            for (int i = 0; i < T; i++) {
                f = Mathf.Sin (s);
            }
            sw.Stop ();
            Debug.LogFormat ("mathf.sin time on {0} iterations: {1}", T, sw.ElapsedTicks);

            sw.Reset ();
            sw.Start ();
            for (int i = 0; i < T; i++) {
                f = (float) System.Math.Sin (s);
            }
            sw.Stop ();
            Debug.LogFormat ("system.math.sin time on {0} iterations: {1}", T, sw.ElapsedTicks);

            // Warmup cache.
            f = MathFast.Sin (s);

            sw.Reset ();
            sw.Start ();
            for (int i = 0; i < T; i++) {
                f = MathFast.Sin (s);
            }
            sw.Stop ();
            Debug.LogFormat ("mathfast.sin time on {0} iterations: {1}", T, sw.ElapsedTicks);

            var rng = Service<Rng>.Get ();
            for (int i = 0; i < 10; i++) {
                f = rng.GetFloat () * MathFast.PI_2;
                Debug.LogFormat ("sin({0}) => {1} / {2}", f, Mathf.Sin (f), MathFast.Sin (f));
            }
        }

        void CosTest () {
            Debug.Log (">>>>> cos tests >>>>>");
            const int T = 10000;
            var sw = new System.Diagnostics.Stopwatch ();
            float f;
            float s = 1.345f;

            sw.Reset ();
            sw.Start ();
            for (int i = 0; i < T; i++) {
                f = Mathf.Cos (s);
            }
            sw.Stop ();
            Debug.LogFormat ("mathf.cos time on {0} iterations: {1}", T, sw.ElapsedTicks);

            sw.Reset ();
            sw.Start ();
            for (int i = 0; i < T; i++) {
                f = (float) System.Math.Cos (s);
            }
            sw.Stop ();
            Debug.LogFormat ("system.math.cos time on {0} iterations: {1}", T, sw.ElapsedTicks);

            // Warmup cache.
            f = MathFast.Cos (s);

            sw.Reset ();
            sw.Start ();
            for (int i = 0; i < T; i++) {
                f = MathFast.Cos (s);
            }
            sw.Stop ();
            Debug.LogFormat ("mathfast.cos time on {0} iterations: {1}", T, sw.ElapsedTicks);

            var rng = Service<Rng>.Get ();
            for (int i = 0; i < 10; i++) {
                f = rng.GetFloat () * MathFast.PI_2;
                Debug.LogFormat ("cos({0}) error checking => {1} / {2}", f, Mathf.Cos (f), MathFast.Cos (f));
            }
        }

        void Atan2Test () {
            Debug.Log (">>>>> atan2 tests >>>>>");
            const int T = 10000;
            var sw = new System.Diagnostics.Stopwatch ();
#pragma warning disable 0219
            float f;
#pragma warning restore 0219
            var sy = 1.234f;
            var sx = 2.345f;

            sw.Reset ();
            sw.Start ();
            for (int i = 0; i < T; i++) {
                f = Mathf.Atan2 (sy, sx);
            }
            sw.Stop ();
            Debug.LogFormat ("mathf.atan2 time on {0} iterations: {1}", T, sw.ElapsedTicks);

            // Warmup cache.
            f = MathFast.Atan2 (sy, sx);

            sw.Reset ();
            sw.Start ();
            for (int i = 0; i < T; i++) {
                f = MathFast.Atan2 (sy, sx);
            }
            sw.Stop ();
            Debug.LogFormat ("mathfast.atan2 time on {0} iterations: {1}", T, sw.ElapsedTicks);

            var rng = Service<Rng>.Get ();
            for (int i = 0; i < 10; i++) {
                sy = rng.GetFloat () * MathFast.PI_2;
                sx = rng.GetFloat () * MathFast.PI_2;
                Debug.LogFormat ("atan2({0}, {1}) error checking => {2} / {3}",
                    sy, sx, Mathf.Atan2 (sy, sx) * MathFast.Rad2Deg, MathFast.Atan2 (sy, sx) * MathFast.Rad2Deg);
            }
        }
    }
}