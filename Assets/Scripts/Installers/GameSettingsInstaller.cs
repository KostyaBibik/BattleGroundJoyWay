using Db;
using Db.Impl;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = nameof(GameSettingsInstaller),
        menuName = "Installers/" + nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private CardPrefabs cardPrefabs;
        [SerializeField] private CardSettings cardSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(cardPrefabs);
            Container.BindInstance(cardSettings);
        }
    }
}