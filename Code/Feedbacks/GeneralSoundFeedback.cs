using System.Collections.Generic;
using System.Linq;
using Code.Entities;
using UnityEngine;

namespace Code.Feedbacks
{
    public class GeneralSoundFeedback : Feedback
    {
       [SerializeField] private ActionData actionData;
        
        private Dictionary<string, SoundFeedback> _soundDictionary = new Dictionary<string, SoundFeedback>(); 

        private void Awake()
        {
            GetComponents<SoundFeedback>().ToList()
                .ForEach(sound => _soundDictionary.Add(sound.HitBulletName, sound));
        }

        public override void CreateFeedback()
        {
            _soundDictionary.GetValueOrDefault(actionData.BulletName)
                .CreateFeedback();
        }

        public override void StopFeedback()
        { }
    }
}