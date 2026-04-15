using Code.Core.Events;
using Code.Core.EventSystems;
using UnityEngine;

namespace Code.Core
{
    public class ClearStage : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO gameEventChannelSO;

        public void Clear(bool isLastStage)
        {
            StageClearEvent evt = GameEvents.StageClearEvent;
            evt.isLastStage = isLastStage;
            gameEventChannelSO.RaiseEvent(evt);
        }
    }
}