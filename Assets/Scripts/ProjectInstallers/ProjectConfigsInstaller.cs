using Project.Configs.CharacterViewsProviding;
using Project.Configs.PrefabsProviding;
using UnityEngine;
using Zenject;

namespace ProjectInstallers
{
    public class ProjectConfigsInstaller : MonoInstaller
    {
        [SerializeField] private PrefabsProviderSO prefabsProvider;
        [SerializeField] private CharacterViewsProviderSO characterViewsProvider;
        [SerializeField] private CharacterColorsProviderSO colorsProvider;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo(prefabsProvider.GetType()).FromInstance(prefabsProvider);
            Container.BindInterfacesTo(characterViewsProvider.GetType()).FromInstance(characterViewsProvider);
            Container.BindInterfacesTo(colorsProvider.GetType()).FromInstance(colorsProvider);
        }
    }
}