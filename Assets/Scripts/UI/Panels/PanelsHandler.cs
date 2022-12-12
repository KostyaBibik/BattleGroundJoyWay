using System;
using System.Collections.Generic;
using Db.Enums;
using Signals;
using UI.Panels.Impl;
using Zenject;

namespace UI.Panels
{
    public class PanelsHandler : IInitializable, IDisposable
    {
        private readonly List<UiPanel> _uiPanels = new List<UiPanel>();
        private readonly SignalBus _signalBus;

        public PanelsHandler(
            SignalBus signalBus,
            LosePanelView mainPanelView,
            GamePanelView gamePanelView,
            WinPanelView winPanelView,
            EPanelType startPanel
        )
        {
            _signalBus = signalBus;
            
            _uiPanels.Add(mainPanelView);
            _uiPanels.Add(gamePanelView);
            _uiPanels.Add(winPanelView);

            SubscribeToPanels();
            EnablePanel(startPanel);
        }

        private void SubscribeToPanels()
        {
            foreach (var panel in _uiPanels)
            {
                panel.onPanelOpen += EnablePanel;
            }
        }
        
        private void EnablePanel(EPanelType startPanel)
        {
            foreach (var panel in _uiPanels)
            {
                panel.EnablePanel(panel.panelType == startPanel);
            }
        }

        public void Initialize()
        {
            _signalBus.Subscribe<WinSignal>(OnWinSignal);
            _signalBus.Subscribe<LoseSignal>(OnLoseSignal);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<WinSignal>(OnWinSignal);
            _signalBus.Unsubscribe<LoseSignal>(OnLoseSignal);
        }

        private void OnWinSignal(WinSignal winSignal)
        {
            EnablePanel(EPanelType.Win);
        }
        
        private void OnLoseSignal(LoseSignal loseSignal)
        {
            EnablePanel(EPanelType.Lose);
        }
    }
}