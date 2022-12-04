using Systems;
using PlayableItems;
using PlayableItems.Logic;
using PlayableItems.Logic.Impl;
using Signals;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private CanvasView _canvasView;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<InitializeStartCardSignal>();
            
            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();

            BindUiInstances();
            
            Container.Bind<CardMovingSystem>().AsSingle().NonLazy();

            Container
                .BindInterfacesAndSelfTo<CardFactory>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<CardInitializeSystem>().AsSingle().NonLazy();

            Container.BindInterfacesTo<GameInitializeSystem>().AsSingle().NonLazy();
        }

        private void BindUiInstances()
        {
            Container.Bind<CanvasView>().FromInstance(_canvasView).AsSingle();
            Container.Bind<PlayerPanelView>().FromInstance(_canvasView.PlayerPanelView).AsSingle();
            Container.Bind<EnemyPanelView>().FromInstance(_canvasView.EnemyPanelView).AsSingle();
        }
    }
}
