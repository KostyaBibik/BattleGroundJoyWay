using Systems;
using Game;
using Game.Ai;
using PlayableItems.Logic.Impl;
using Signals;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private IndicatorView indicatorView;
        
        public override void InstallBindings()
        {
            InstallSignals();

            Container.BindInterfacesAndSelfTo<InputHotkeysSystem>().AsSingle().NonLazy();
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
            Container.BindInterfacesAndSelfTo<IndicatorView>().FromInstance(indicatorView).AsSingle().NonLazy();
            
            Container.Bind<CardMovingSystem>().AsSingle().NonLazy();

            Container.Bind<CardService>().AsSingle().NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<CardFactory>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<AiMovingSystem>().AsSingle().NonLazy();

            Container.BindInterfacesTo<CardInitializeSystem>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<IdentifyMotionSystem>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<GameInitializeSystem>().AsSingle().NonLazy();
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<InitializeStartCardSignal>();
            Container.DeclareSignal<EndMotionSignal>();
            Container.DeclareSignal<StartGameSignal>();
            Container.DeclareSignal<WinSignal>();
            Container.DeclareSignal<LoseSignal>();
        }
    }
}
