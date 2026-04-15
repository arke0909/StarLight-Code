using Code.Core.Events;
using Code.Core.EventSystems;
using EL.Dependencies;
using Unity.Cinemachine;
using UnityEngine;

namespace Code.ETC
{
    [Provide]
    public class CameraShaker : MonoBehaviour, IDependencyProvider
    {
        [SerializeField] private GameEventChannelSO gameChannel;

        private CinemachineImpulseSource _impulseSource;

        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
            gameChannel.AddListener<CameraShakeEvent>(HandleCameraShakeEvent);
        }

        private void OnDestroy()
        {
            gameChannel.RemoveListener<CameraShakeEvent>(HandleCameraShakeEvent);
        }

        private void HandleCameraShakeEvent(CameraShakeEvent evt)
        {
            _impulseSource.GenerateImpulse(evt.shakePower);
        }
    }
}