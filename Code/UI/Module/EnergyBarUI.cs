using System;
using Code.Core.Events;
using Code.Core.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Module
{
    public class EnergyBarUI : MonoBehaviour
    {
        [SerializeField] private Image barImage;
        [SerializeField] private TextMeshProUGUI percentText;
        [SerializeField] private GameEventChannelSO playerChannel;
        [SerializeField] private float duration = 0.3f;

        private Tween _fillTween;
        
        private void Awake()
        {
            playerChannel.AddListener<EnergyChangeEvent>(HandleEnergyChange);
        }

        private void OnDestroy()
        {
            if (_fillTween != null)
                _fillTween.Kill();
            
            playerChannel.RemoveListener<EnergyChangeEvent>(HandleEnergyChange);
        }

        private void HandleEnergyChange(EnergyChangeEvent evt)
        {
            float ratio = evt.currentEnergy / evt.maxEnergy;
            percentText.text = evt.currentEnergy.ToString("F1");
            if(barImage != null)
                _fillTween = DOTween.To(() => barImage.fillAmount, x => barImage.fillAmount = x, ratio, duration);
            else
                barImage.fillAmount = ratio;
        }
    }
}