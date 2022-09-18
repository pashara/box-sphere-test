using Project.Battlers;
using UnityEngine;
using static Project.Configs.PrefabsProviding.PrefabKeys;

namespace Project.Configs.PrefabsProviding
{
    public class PrefabsProviderSO : ScriptableObject, IPrefabsProvider
    {
        [SerializeField] private CharacterWrapper _characterWrapper;


        public GameObject GetGameObject(int id)
        {
            switch (id)
            {
                case (int) PrefabKeys.CharacterWrapper:
                    return _characterWrapper.gameObject;
            }
            
            Debug.LogError($"Not configured prefab by ID {id}");
            return null;
        }
    }
    
    public interface IPrefabsProvider
    {
        GameObject GetGameObject(int id);
    }

    public enum PrefabKeys
    {
        None = 0,
        CharacterWrapper = 1
    }
}