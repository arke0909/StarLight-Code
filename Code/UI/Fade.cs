using System;
using Code.Core.Events;
using Code.Core.EventSystems;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class Fade : MonoBehaviour
    {
        [SerializeField] private SceneChanger sceneChanger;
        [SerializeField] private GameEventChannelSO uiEventChannel;
        [SerializeField] private GameEventChannelSO gameEventChannel;
        [SerializeField] private Image fadeImage;
        [SerializeField] private float duration;

        private void Awake()
        {
            uiEventChannel.AddListener<FadeEvent>(HandleFadeEvent);
        }

        private void OnDestroy()
        {
            fadeImage.DOKill();
            uiEventChannel.RemoveListener<FadeEvent>(HandleFadeEvent);
        }

        private void HandleFadeEvent(FadeEvent fadeEvt)
        {
            float startValue = fadeEvt.isFadeIn ? -0.5f : 1f;
            float endValue = fadeEvt.isFadeIn ? 1f : -0.5f;

            Color color = fadeImage.color;
            color.a = startValue;
            fadeImage.color = color;

            ActiveEvent activeEvt = GameEvents.ActiveEvent;
            activeEvt.isActive = false;
            gameEventChannel.RaiseEvent(activeEvt);

            fadeImage.DOFade(endValue, duration).SetUpdate(true).OnComplete(() =>
            {
                activeEvt.isActive = !fadeEvt.isFadeIn;
                gameEventChannel.RaiseEvent(activeEvt);

                if (!activeEvt.isActive && !string.IsNullOrEmpty(fadeEvt.sceneName))
                {
                    sceneChanger.ChangeScene(fadeEvt.sceneName);
                }
            });
        }
    }
}