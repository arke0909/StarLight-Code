using Code.Animators;
using UnityEngine;

namespace Code.Entities
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        private Animator _animator;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _animator = GetComponent<Animator>();
        }

        public int GetHash(string hashName)
        {
            return Animator.StringToHash(hashName);
        }
        public void SetParam(AnimParamSO param, bool value) => _animator.SetBool(param.hashValue, value);
        public void SetParam(AnimParamSO param, float value) => _animator.SetFloat(param.hashValue, value);
        public void SetParam(AnimParamSO param, int value) => _animator.SetInteger(param.hashValue, value);
        public void SetParam(AnimParamSO param) => _animator.SetTrigger(param.hashValue);
        public void SetParam(int hash, bool value) => _animator.SetBool(hash, value);
        public void SetParam(int hash, float value) => _animator.SetFloat(hash, value);
        public void SetParam(int hash, int value) => _animator.SetInteger(hash, value);
        public void SetParam(int hash) => _animator.SetTrigger(hash);
    }
}