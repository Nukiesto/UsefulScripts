using UnityEngine;

namespace UsefulScripts.UnityScripts
{
    public static class AudioS
    {
        /// <summary>
        ///     Проиграть звук если он есть
        /// </summary>
        public static void PlayOneShotMbNull(this AudioSource audioSource, AudioClip audioClip)
        {
            if (audioClip != null)
                audioSource.PlayOneShot(audioClip);
        }

        /// <summary>
        ///     Проиграть звук если он есть
        /// </summary>
        public static void PlayOneShotMbNull(this AudioSource audioSource, AudioClip audioClip, float volume)
        {
            if (audioClip != null)
                audioSource.PlayOneShot(audioClip, volume);
        }
    }
}