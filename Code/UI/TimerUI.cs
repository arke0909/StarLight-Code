using System;
using Code.Core.Events;
using Code.Core.EventSystems;
using TMPro;
using UnityEngine;
using System.Text;
using Code.UI.RankingBoard;
using DG.Tweening;

namespace Code.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private RankingBoardSystem rankingSystem;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameEventChannelSO gameChannel;
        [SerializeField] private bool _isOn = false;
        [SerializeField] private float blinkTime = 0.1f;
        private StringBuilder _stringBuilder;
        private int _min = 0;
        private float _sec = 0;
        private float _time = 0f;

        private void Awake()
        {
            _stringBuilder = new StringBuilder();

            if (PlayerPrefs.HasKey("StageTime"))
            {
                _time = PlayerPrefs.GetFloat("StageTime");

                _min = (int)_time / 60;
                _sec = _time % 60;

                SetTimerText();
            }

            gameChannel.AddListener<ActiveEvent>(HandleActiveEvent);
            gameChannel.AddListener<PlayerDeadEvent>(HandlePlayerDeadEvent);
            gameChannel.AddListener<EnemyDeadEvent>(HandleEnemyDeadEvent);
            gameChannel.AddListener<StageClearEvent>(HandleStageClear);
        }

        private void OnDestroy()
        {
            gameChannel.RemoveListener<ActiveEvent>(HandleActiveEvent);
            gameChannel.RemoveListener<PlayerDeadEvent>(HandlePlayerDeadEvent);
            gameChannel.RemoveListener<EnemyDeadEvent>(HandleEnemyDeadEvent);
            gameChannel.RemoveListener<StageClearEvent>(HandleStageClear);
        }

        private void HandleActiveEvent(ActiveEvent evt)
            => _isOn = evt.isActive;

        private void HandleStageClear(StageClearEvent evt)
        {
            _time = _sec + 60 * _min;

            if (evt.isLastStage)
            {
                RepeatFade();
                rankingSystem.SubmitScoreCoroutine(_time);
            }
            else
            {
                PlayerPrefs.SetFloat("StageTime", _time);
            }
        }

        private void HandlePlayerDeadEvent(PlayerDeadEvent evt)
        {
            RepeatFade();
        }
        private void HandleEnemyDeadEvent(EnemyDeadEvent evt)
        {
            RepeatFade();
        }

        private void RepeatFade()
        {
            _isOn = false;
            Sequence seq = DOTween.Sequence();
            
            seq.Append(timerText.DOFade(0, blinkTime))
                .Append(timerText.DOFade(1, blinkTime)).SetLoops(-1);
        }

        public void Update()
        {
            if (_isOn)
            {
                _sec += Time.deltaTime;
                if (_sec >= 60f)
                {
                    _min++;
                    _sec = 0;
                }

                SetTimerText();
            }
        }

        private void SetTimerText()
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(_min.ToString("D2")).Append(":").Append(_sec.ToString("00.00"));
            timerText.text = _stringBuilder.ToString();
        }

        private void OnApplicationQuit()
        {
            DeleteTimeRecode();
        }

        [ContextMenu("Delete Time Recode")]
        public void DeleteTimeRecode() => PlayerPrefs.DeleteKey("StageTime");
    }
}