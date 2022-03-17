using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UsefulScripts.UnityScripts.UI;

namespace UsefulScripts.UnityScripts.UsefulBehaviours
{
    public class TimerToDelete : MonoBehaviour
    {
        public enum Mode
        {
            Destroy,
            Disable
        }

        [Header("Main")] [SerializeField] private float time;

        [SerializeField] private Mode mode;
        [SerializeField] private bool repeatOnEnable;
        [SerializeField] private bool canOnStart = true;

        [Header("Blend")] [SerializeField] private bool toBlend;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Text text;
        [SerializeField] private TextMesh textMesh;
        [SerializeField] private Color startColor = Color.white;

        private bool _canOnEnable;

        private void Start()
        {
            if (canOnStart)
            {
                _canOnEnable = true;
                StartTimer();
            }
        }

        private void OnEnable()
        {
            if (repeatOnEnable && _canOnEnable)
                StartTimer();
        }

        private void OnDisable()
        {
            StopCoroutine(StartBlend());
            if (spriteRenderer != null)
            {
                var color = spriteRenderer.color;
                color.a = 1;
                spriteRenderer.color = color;
            }
            else
            {
                if (text != null)
                    text.SetAlpha(1);

                if (textMesh != null)
                    textMesh.SetAlpha(1);
            }
        }

        public event UnityAction OnTimeOutEvent;

        public void SetColor(Color color)
        {
            startColor = color;
        }

        public void SetTime(float timeSet)
        {
            time = timeSet;
        }

        public void StartTimer()
        {
            StartCoroutine(StartDelete());
            if (toBlend)
                StartCoroutine(StartBlend());
        }

        private IEnumerator StartBlend()
        {
            var color = startColor;
            var value = 1f;
            var valuePlus0 = 1f / time;

            //Debug.Log("time: " + time);
            if (spriteRenderer != null)
                color = spriteRenderer.color;
            else if (text != null)
                color = text.color;
            while (true)
            {
                yield return null;
                var valuePlus = valuePlus0 / (1 / Time.deltaTime);
                value -= valuePlus;
                color.a = value;

                if (spriteRenderer != null)
                    spriteRenderer.color = color;
                else if (text != null)
                    text.color = color;
                else if (textMesh != null)
                    textMesh.color = color;

                if (value < 0)
                    yield break;
            }
        }

        private IEnumerator StartDelete()
        {
            while (true)
            {
                yield return new WaitForSeconds(time);

                OnTimeOutEvent?.Invoke();
                if (mode == Mode.Disable)
                    gameObject.SetActive(false);
                else
                    Destroy(gameObject);

                yield break;
            }
        }
    }
}