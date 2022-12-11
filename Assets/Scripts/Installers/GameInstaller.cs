using Systems;
using Game;
using Game.Ai;
using PlayableItems.Logic.Impl;
using Signals;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CanvasView canvasView;
        [SerializeField] private IndicatorView indicatorView;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<InitializeStartCardSignal>();
            Container.DeclareSignal<EndMotionSignal>();
            Container.DeclareSignal<StartGameSignal>();
            
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
            Container.BindInterfacesAndSelfTo<IndicatorView>().FromInstance(indicatorView).AsSingle().NonLazy();
            
            BindUiInstances();
            
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

        private void BindUiInstances()
        {
            Container.Bind<CanvasView>().FromInstance(canvasView).AsSingle();
            Container.Bind<PlayerCardHolder>().FromInstance(canvasView.PlayerCardHolder).AsSingle();
            Container.Bind<EnemyCardHolder>().FromInstance(canvasView.EnemyCardHolder).AsSingle();
        }
    }
}
