using Code.Core.Events;
using Code.Core.EventSystems;
using UnityEngine;

namespace Code.Feedbacks
{
    public class CameraShakeFeedback : Feedback
    {
        [SerializeField] private float shakePower = 1f;
        [SerializeField] private GameEventChannelSO gameEventChannelSO;
        
        public override void CreateFeedback()
        {
            CameraShakeEvent evt = GameEvents.CameraShakeEvent;
            evt.shakePower = shakePower;
            gameEventChannelSO.RaiseEvent(evt);
        }

        public override void StopFeedback()
        { }
    }
}