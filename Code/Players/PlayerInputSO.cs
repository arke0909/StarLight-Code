using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Players
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO")]
    public class PlayerInputSO : ScriptableObject, Input.IPlayerActions, Input.IUIActions
    {
        public event Action OnSpaceEvent;
        public event Action OnFrontModuleEvent;
        public event Action OnCentralModuleEvent;
        public event Action OnRearModuleEvent;
        public event Action OnESCEvent;
    
        public Vector2 InputVector { get; private set; }
        
        private Input input;

        private void OnEnable()
        {
            if (input == null)
            {
                input = new Input();
                input.Player.SetCallbacks(this);
                input.UI.SetCallbacks(this);
            }
            
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        public void OnOffInput(bool isActive)
        {
            if (isActive)
            {
                input.Enable();
            }
            else
            {
                input.Disable();
            }
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            InputVector = context.ReadValue<Vector2>();
        }

        public void OnSpace(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnSpaceEvent?.Invoke();
        }

        public void OnFrontModule(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnFrontModuleEvent?.Invoke();
        }

        public void OnCentralModule(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnCentralModuleEvent?.Invoke();
        }

        public void OnRearModule(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnRearModuleEvent?.Invoke();
        }

        public void OnESC(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnESCEvent?.Invoke();
        }
    }
}