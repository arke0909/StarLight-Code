using System;
using DG.Tweening;
using UnityEngine;

namespace Code.Feedbacks
{
    public class BlinkFeedback : Feedback
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private float blinkDuration = 0.15f;
        [SerializeField] private float blinkIntensity = 0.15f;

        private readonly int _blinkHash = Shader.PropertyToID("_BlinkValue");
        private bool _isBlinking = false;

        private Tween _blinkTween;

        private void OnDestroy()
        {
            if(_blinkTween != null)
                _blinkTween.Kill();
        }

        public override void CreateFeedback() 
        {
            if(_isBlinking) return;
            
            _isBlinking = true;
            meshRenderer.material.SetFloat(_blinkHash, blinkIntensity);
            _blinkTween = DOVirtual.DelayedCall(blinkDuration, StopFeedback);
        }

        public override void StopFeedback()
        {
            if (meshRenderer != null)
            {
                meshRenderer.material.SetFloat(_blinkHash, 0);
            }
            _isBlinking = false;
        }
    }
}