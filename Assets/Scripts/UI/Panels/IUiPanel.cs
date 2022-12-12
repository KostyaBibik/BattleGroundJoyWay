using System;
using Db.Enums;

namespace UI.Panels
{
    public interface IUiPanel
    {
        public event Action<EPanelType> onPanelOpen;
        
        void NavigateTo(EPanelType menuPanelEnum);

        void EnablePanel(bool isEnable);
    }
}