using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Configs.CharacterViewsProviding
{
    public interface ICharacterColorsProvider
    {
        Color Get(ColorType colorType);
    }
    
    public class CharacterColorsProviderSO : ScriptableObject, ICharacterColorsProvider
    {
        [Serializable]
        private class Data
        {
            public ColorType ColorType;
            public Color Color;
        }

        [SerializeField] private List<Data> data;
        
        public Color Get(ColorType colorType)
        {
            return data.FirstOrDefault(x => x.ColorType == colorType)?.Color ?? Color.magenta;
        }
        
    }
}