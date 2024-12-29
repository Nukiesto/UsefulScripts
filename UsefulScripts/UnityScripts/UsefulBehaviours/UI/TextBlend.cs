using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace UsefulScripts.UnityScripts.UsefulBehaviours.UI
{
    public class TextBlend : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float speed;
        
        public Color startColor;
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

        public void StartBlend()
        {
            Blend().Forget();
        }

        private async UniTaskVoid Blend()
        {
            var color = text.color;
            while (color.a > 0)
            {
                color.a = Mathf.MoveTowards(color.a, 0, speed);
                text.color = color;
                await UniTask.Yield();
            }
        }
    }
}