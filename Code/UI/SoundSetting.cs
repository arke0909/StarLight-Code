using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Code.UI
{
    public class SoundSetting : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider soundSlider;
        [SerializeField] private string volumeName;

        private Slider _bar;
        private float _volume;

        private void Awake()
        {
            _bar = GetComponentInChildren<Slider>();

            if (PlayerPrefs.HasKey(volumeName))
            {
                _volume = PlayerPrefs.GetFloat(volumeName);
                _bar.value = _volume;
            }
        }

        private void Start()
        {
            audioMixer.SetFloat(volumeName, Mathf.Log10(_volume) * 20);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetFloat(volumeName, _volume);
        }

        public void OnValueChange()
        {
            _volume = _bar.value;
            audioMixer.SetFloat(volumeName, Mathf.Log10(_volume) * 20);
        }
    }
}