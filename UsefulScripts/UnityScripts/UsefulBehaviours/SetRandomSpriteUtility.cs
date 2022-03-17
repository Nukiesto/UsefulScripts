using UnityEngine;
using UsefulScripts.NetScripts;

namespace UsefulScripts.UnityScripts.UsefulBehaviours
{
    public class SetRandomSpriteUtility : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer.sprite = sprites.GetRandom();
        }
    }
}