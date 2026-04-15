using System;
using Code.Core.Events;
using Code.Core.EventSystems;
using UnityEngine;

namespace Code.UI
{
    public class InGameCanvas : MajorUI
    {
        [SerializeField] private GameEventChannelSO gameEventChannelSO;

        protected override void Awake()
        {
            base.Awake();
            gameEventChannelSO.AddListener<ActiveEvent>(HandleActiveEvent);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            gameEventChannelSO.RemoveListener<ActiveEvent>(HandleActiveEvent);
        }

        private void HandleActiveEvent(ActiveEvent evt)
        {
            SetWindow(evt.isActive, duration);
        }
    }
}