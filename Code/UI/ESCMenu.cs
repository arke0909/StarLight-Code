using Code.Core.Events;
using Code.Core.EventSystems;
using Code.Players;
using UnityEngine;

namespace Code.UI
{
    public class ESCMenu : MajorUI
    {
        [SerializeField] private PlayerInputSO playerInput;
        [SerializeField] private GameEventChannelSO uiChannel;

        protected override void Awake()
        {
            base.Awake();
            playerInput.OnESCEvent += ControlMenu;
        }

        public void OnInGameExit(string sceneName)
        {
            FadeEvent evt = GameEvents.FadeEvent;
            evt.sceneName = sceneName;
            evt.isFadeIn = true;
            uiChannel.RaiseEvent(evt);
        }

        protected override void OnDestroy()
        {
            playerInput.OnESCEvent -= ControlMenu;
            base.OnDestroy();
        }

        protected override void Open()
        {
            base.Open();
            Time.timeScale = 0f;
        }

        protected override void OnCloseCallback()
        {
            base.OnCloseCallback();
            Time.timeScale = 1f;
        }
    }
}