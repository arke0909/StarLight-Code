using System;
using Code.Core.Events;
using Code.Core.EventSystems;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class EntityHealthUI : MonoBehaviour
    {
        [SerializeField] private Image barImage;
        [SerializeField] private GameEventChannelSO playerChannel;
        [SerializeField] private float duration = 0.3f;
        [SerializeField] private uint id;

        private Tween _fillTween;
        
        private void Awake()
        {
            playerChannel.AddListener<HpChangeEvent>(HandleHpChange);
        }

        private void OnDestroy()
        {
            if (_fillTween != null)
                _fillTween.Kill();
            
            playerChannel.RemoveListener<HpChangeEvent>(HandleHpChange);
        }

        private void HandleHpChange(HpChangeEvent evt)
        {
            if(id != evt.id) return;
            
            float ratio = evt.currentHp / evt.maxHp;
            if(barImage != null)
                _fillTween = DOTween.To(() => barImage.fillAmount, x => barImage.fillAmount = x, ratio, duration);
            else
                barImage.fillAmount = ratio;
        }
    }
}