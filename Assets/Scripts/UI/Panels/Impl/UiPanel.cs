using System;
using Db.Enums;
using UnityEngine;

namespace UI.Panels.Impl
{
    public abstract class UiPanel : MonoBehaviour, IUiPanel
    {
        public EPanelType panelType;

        public event Action<EPanelType> onPanelOpen;

        public virtual void NavigateTo(EPanelType menuPanelEnum)
        {
            onPanelOpen?.Invoke(menuPanelEnum);
        }

        public virtual void EnablePanel(bool isEnable)
        {
            gameObject.SetActive(isEnable);
        }
    }
}