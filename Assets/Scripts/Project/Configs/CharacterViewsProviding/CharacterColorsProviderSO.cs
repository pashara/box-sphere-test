using System;
using System.Collections.Generic;
using System.Linq;
using ProjectShared;
using UnityEngine;

namespace Project.Configs.CharacterViewsProviding
{
    
    public class CharacterColorsProviderSO : ScriptableObject, ICharacterColorsProvider
    {
        [Serializable]
        private class Data
        {
            public CharacterColorType ColorType;
            public Color Color;
        }

        [SerializeField] private List<Data> data;
        
        public Color Get(CharacterColorType colorType)
        {
            return data.FirstOrDefault(x => x.ColorType == colorType)?.Color ?? Color.magenta;
        }
        
    }
}