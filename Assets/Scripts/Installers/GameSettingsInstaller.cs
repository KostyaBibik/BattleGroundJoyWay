using Db.Actions.Impl;
using Db.Impl;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = nameof(GameSettingsInstaller),
        menuName = "Installers/" + nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private CardSettings cardSettings;
        [SerializeField] private ActionSettings actionSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(cardSettings);
            Container.BindInstance(actionSettings);
        }
    }
}