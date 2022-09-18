using System;
using System.Collections.Generic;
using System.Linq;
using Project.Battlers;
using ProjectShared.Battler;
using UnityEngine;

namespace Project.Configs.CharacterViewsProviding
{
    [CreateAssetMenu]
    public class CharacterViewsProviderSO : ScriptableObject, ICharacterViewProvider
    {
        [Serializable]
        private class Data
        {
            public ViewType ViewType;
            public CharacterView Prefab;
        }

        [SerializeField] private List<Data> data = new();

        public GameObject GetViewPrefab(ViewType viewType)
        {
            var requestData = data.FirstOrDefault(x => x.ViewType == viewType);
            if (requestData != null && requestData.Prefab != null) return requestData.Prefab.gameObject;
            
            Debug.LogError($"Not configured view with key {viewType}");
            return null;

        }
    }
    
    public enum ViewType
    {
        None = 0,
        Circle = 1,
        Rectangle = 2,
    }
    
    public enum ColorType
    {
        None = 0,
        Red = 1,
        Blue = 2,
        Orange = 3,
    }

    public interface ICharacterViewProvider
    {
        GameObject GetViewPrefab(ViewType viewType);
    }
}