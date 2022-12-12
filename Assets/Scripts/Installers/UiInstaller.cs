using Db.Enums;
using UI;
using UI.Panels;
using UI.Panels.Impl;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private EPanelType startPanelType;

        [SerializeField] private CanvasView canvasView;
        [Header("Panels")]
        [SerializeField] private GamePanelView gamePanelView;
        [SerializeField] private LosePanelView losePanelView;
        [SerializeField] private WinPanelView winPanelView;
        
        public override void InstallBindings()
        {
            Container
                .Bind<CanvasView>()
                .FromInstance(canvasView)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<GamePanelView>()
                .FromInstance(gamePanelView)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<LosePanelView>()
                .FromInstance(losePanelView)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<WinPanelView>()
                .FromInstance(winPanelView)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<PanelsHandler>()
                .AsSingle()
                .WithArguments(startPanelType)
                .NonLazy();
        }
    }
}