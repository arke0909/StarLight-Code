using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Core.StatSystem;
using Code.Enemies;
using Code.Entities;
using Code.Patterns.PatternDatas;
using UnityEngine;


namespace Code.Patterns
{
    [Serializable]
    public struct PatternData
    {
        public bool isSequence;
        public List<PatternSO> patternList;
    }

    public class PatternComponent : MonoBehaviour, IEntityComponent, IAfterInit
    {
        [SerializeField] private List<PatternData> patternDataList;
        [field: SerializeField] public StatSO Attack { get; private set; }
        [SerializeField] private float delay = 2.5f;

        private Enemy _enemy;
        private Dictionary<string, Pattern> _patternDictionary = new Dictionary<string, Pattern>();

        private int _patternIdx = 0;
        private bool _isActive;
        public bool isUsing = false;

        public void Initialize(Entity entity)
        {
            _enemy = entity as Enemy;
            GetComponentsInChildren<Pattern>().ToList()
                .ForEach(pattern => _patternDictionary.Add(pattern.PatternName, pattern));

            _enemy.OnActiveEvent.AddListener(OnActive);
        }

        public void AfterInit()
        {
            _patternDictionary.Values.ToList()
                .ForEach(pattern => pattern.InitPattern(_enemy, this, Attack));
        }

        private void OnActive(bool isActive)
        {
            if (isActive)
            {
                _isActive = true;
                StartCoroutine(PatternCoroutine());
            }
            else
            {
                _isActive = false;
                StopAllPattern();
            }
        }
        
        private IEnumerator PatternCoroutine()
        {
            while (_isActive)
            {
                yield return new WaitForSeconds(delay);
                isUsing = true;

                PatternData patternData = patternDataList[_patternIdx++ % patternDataList.Count];
                foreach (var patternSO in patternData.patternList)
                {
                    Pattern pattern = _patternDictionary.GetValueOrDefault(patternSO.patternName);
                    pattern.UsePattern(patternSO);
                    if (patternData.isSequence)
                        yield return new WaitUntil(() => !isUsing);
                }
                
                yield return new WaitUntil(() => !isUsing);
            }
        }

        private void StopAllPattern()
        {
            StopAllCoroutines();
            _patternDictionary.Values.ToList()
                .ForEach(pattern => pattern.StopPattern());
        }
    }
}