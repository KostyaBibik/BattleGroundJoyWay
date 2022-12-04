using Db;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = nameof(GameSettingsInstaller),
        menuName = "Installers/" + nameof(GameSettingsInstaller))]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private CardPrefabs cardPrefabs;
        
        public override void InstallBindings()
        {
            Container.BindInstance(cardPrefabs);
        }
    }
}