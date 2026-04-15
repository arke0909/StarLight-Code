using System.Collections.Generic;
using System.Linq;
using Code.Sounds;
using UnityEngine;

namespace Code.Entities
{
    public class EntitySound : MonoBehaviour, IEntityComponent
    {
        private Dictionary<string, PlaySound> _playableDictionary;
        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _playableDictionary = new Dictionary<string,PlaySound>();
            GetComponentsInChildren<PlaySound>().ToList()
                .ForEach(sound => _playableDictionary.Add(sound.SoundName, sound));
        }

        public void PlaySfx(string sfxName)
        {
            PlaySound sound = _playableDictionary.GetValueOrDefault(sfxName);
            Debug.Assert(sound != null, $"{sfxName} is not exist in dictionary");
            
            sound.SoundPlay();
        }
    }
}