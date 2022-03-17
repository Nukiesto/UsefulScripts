using System.Collections;
using SimpleAsync.CoroutinesWithoutMb.SimpleAsync;
using UnityEngine;
using UnityEngine.UI;

namespace UsefulScripts.UnityScripts.UsefulBehaviours.UI
{
    public class TextBlend : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private float speed;
        public Color startColor;
        private CoRoutine _coRoutine;
        private bool _pause;
        private Color _prevColor;

        public bool Pause
        {
            get => _pause;
            set
            {
                if (value)
                {
                    _prevColor = text.color;
                    text.color = startColor;
                }
                else
                {
                    text.color = _prevColor;
                }

                _pause = value;
            }
        }

        private void Update()
        {
            if (_coRoutine != null && !Pause)
                if (!_coRoutine.Pump())
                    _coRoutine = null;
        }

        public void StartBlend()
        {
            _coRoutine = new CoRoutine(Blend());
        }

        private IEnumerator Blend()
        {
            var color = text.color;
            while (color.a > 0)
            {
                color.a = Mathf.MoveTowards(color.a, 0, speed);
                text.color = color;
                yield return null;
            }
        }
    }
}