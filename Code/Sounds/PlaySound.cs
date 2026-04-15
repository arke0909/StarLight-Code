using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

namespace Code.Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class PlaySound : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup bgm, sfx;
        [SerializeField] private SoundSO soundData;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private string soundName;
        
        public string SoundName => soundName;

        private void Awake()
        {
            audioSource.outputAudioMixerGroup = soundData.outputType == OutputType.BGM ? bgm : sfx;

            audioSource.resource = soundData.audioResource;

            audioSource.pitch = 1 + soundData.GetRandomPitch();
            audioSource.loop = soundData.isLoop;
            audioSource.volume = soundData.volume;

            audioSource.dopplerLevel = soundData.is3D ? 1f : 0f;
        }

        public void SoundPlay()
            => audioSource.Play();
    }
}