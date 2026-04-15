using System;
using System.Collections.Generic;
using System.Linq;
using Code.Entities;
using UnityEngine;

namespace Code.Feedbacks
{
    public class GeneralHitEffectFeedback : Feedback
    {
        [SerializeField] private ActionData actionData;
        
        private Dictionary<string, HitEffectFeedback> _effectDictionary = new Dictionary<string, HitEffectFeedback>();

        private void Awake()
        {
            GetComponents<HitEffectFeedback>().ToList()
                .ForEach(effect => _effectDictionary.Add(effect.HitBulletName, effect));
        }

        public override void CreateFeedback()
        {
            _effectDictionary.GetValueOrDefault(actionData.BulletName)
                .CreateFeedback(actionData.HitPoint,actionData.HitNormal);
        }

        public override void StopFeedback()
        {
            _effectDictionary.GetValueOrDefault(actionData.BulletName)
                .StopFeedback();
        }
    }
}