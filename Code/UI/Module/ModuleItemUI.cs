using System;
using Code.Core.Events;
using Code.Core.EventSystems;
using Code.Modules;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.Module
{
    public class ModuleItemUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameEventChannelSO uiChannel;
        [SerializeField] private Image icon;
        [SerializeField] private Color lockColor;
        
        private ModuleDataSO _moduleData;
        private int _idx;
        
        private RectTransform RectTransform => transform as RectTransform;

        public UnityEvent OnClick;
        
        public void Initialize(int idx ,ModuleDataSO moduleData, bool isActive = false, 
            Action<Vector2, bool> callback = null)
        {
            _moduleData = moduleData;
            _idx = idx;
            if (_moduleData == null)
            {
                icon.color = lockColor;
                return;
            }

            Sprite iconResource = Resources.Load<Sprite>(moduleData.data.iconPath);
            icon.sprite = iconResource;
            
            if(isActive) callback?.Invoke(RectTransform.anchoredPosition, false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (_moduleData != null)
                {
                    OnClick?.Invoke();
                    GenerateModuleUIEvent evt = GameEvents.GenerateModuleUIEvent;
                    evt.moduleData = _moduleData;
                    evt.idx = _idx;
                    evt.anchorPosition = RectTransform.anchoredPosition;
                    uiChannel.RaiseEvent(evt);
                }
            }
        }
    }
}