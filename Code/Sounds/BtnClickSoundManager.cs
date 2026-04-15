using EL.Dependencies;
using UnityEngine;

namespace Code.Sounds
{
    public class BtnClickSoundManager : MonoBehaviour
    {
        [SerializeField] private SoundSO sound;
        [Inject] private SoundManager _soundManager;
        
        private void Awake()
        {
            var btns = FindObjectsByType<BtnClickSound>(FindObjectsSortMode.None);

            foreach (var btn in btns)
            {
                btn.Init();
                
                btn.button.onClick.AddListener(() => PlayButtonSound());
            }
        }

        public void PlayButtonSound()
        { 
            _soundManager.PlaySound(sound);
        }
    }
}