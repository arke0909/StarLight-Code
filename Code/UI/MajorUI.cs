using System;
using DG.Tweening;
using UnityEngine;

namespace Code.UI
{
    public enum WindowStatus
    {
        None = -1,
        Opened = 0,
        Run = 1,
        Closed = 2,
    }

    public class MajorUI : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected float duration = .3f;

        protected WindowStatus _currentStatus = WindowStatus.Closed;

        protected virtual void Awake()
        {
            Close();
        }

        protected virtual void OnDestroy()
        {
            canvasGroup.DOKill();
        }

        public void ControlMenu()
        {
            if (_currentStatus == WindowStatus.Run) return;

            if (_currentStatus == WindowStatus.Closed)
                Open();
            else if (_currentStatus == WindowStatus.Opened)
                Close();
        }

        protected virtual void Open()
        {
            _currentStatus = WindowStatus.Run;
            SetWindow(true, duration, OnOpenCallback);
        }

        protected virtual void OnOpenCallback()
        {
            _currentStatus = WindowStatus.Opened;
        }

        protected virtual void Close()
        {
            _currentStatus = WindowStatus.Run;
            SetWindow(false, duration, OnCloseCallback);
        }

        protected virtual void OnCloseCallback()
        {
            _currentStatus = WindowStatus.Closed;
        }

        protected virtual void SetWindow(bool isOpen, float setDuration, Action callback = null)
        {
            float alpha = isOpen ? 1f : 0f;

            canvasGroup.DOFade(alpha, setDuration).SetUpdate(true).OnComplete(() => callback?.Invoke());
            canvasGroup.blocksRaycasts = isOpen;
            canvasGroup.interactable = isOpen;
        }
    }
}