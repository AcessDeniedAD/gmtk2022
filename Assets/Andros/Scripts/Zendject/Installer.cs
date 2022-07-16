using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    public PrefabsLoaderManagerFactory PrefabsLoaderManagerFactory;
    public override void InstallBindings()
    {
        Container.Bind<LivingObjectEvents>().AsSingle().NonLazy();
        Container.Bind<DiceManager>().AsSingle();
        Container.Bind<GameMasterManager>().AsSingle().NonLazy();
        Container.Bind<StatesManager>().AsSingle();
        Container.Bind<GameManager>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<PrefabsLoaderManager>().FromIFactory(x => x.To<PrefabsLoaderManagerFactory>().FromScriptableObject(PrefabsLoaderManagerFactory).AsSingle()).AsSingle();
        Container.Bind<HudManager>().AsSingle().NonLazy();
        Container.Bind<PlayerManager>().AsSingle();
        Container.Bind<StartGameInCurrentSceneManager>().AsSingle().NonLazy();

        Container.Bind<DisplayDamagePooler>().AsSingle();
        Container.Bind<BulletsPoolerManager>().AsSingle();
    }
}
