using UnityEngine;

namespace Code.Sounds
{
    public class BGMPlayer : Monosingleton<BGMPlayer>
    {
        [SerializeField] private SoundSO soundSo;
        [SerializeField] private AudioSource audioSource;

        private void Start()
        {
            audioSource.resource = soundSo.audioResource;
            audioSource.Play();
        }
    }
}