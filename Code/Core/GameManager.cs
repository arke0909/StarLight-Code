using Code.Core.Events;
using Code.Core.EventSystems;
using UnityEngine;

namespace Code.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO uiEventChannel;
        [SerializeField] private bool isStartFade = true;
        [SerializeField] private string sceneName;

        private void Start()
        {
            if(isStartFade)
                Fade(false);
        }

        public void Fade(bool isFadeIn)
        {
            FadeEvent evt = GameEvents.FadeEvent;
            evt.isFadeIn = isFadeIn;
            evt.sceneName = sceneName;
            uiEventChannel.RaiseEvent(evt);
        }
    }
}