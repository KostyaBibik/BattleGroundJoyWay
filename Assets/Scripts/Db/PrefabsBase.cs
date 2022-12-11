using Game;
using UnityEngine;

namespace Db
{
    [CreateAssetMenu(menuName = "Prefabs/" + nameof(PrefabsBase),
        fileName = nameof(PrefabsBase))]
    public class PrefabsBase : ScriptableObject
    {
        [SerializeField] private GameObject cardView;
        [SerializeField] private IndicatorView indicatorView;
        public GameObject CardView => cardView;
        public IndicatorView IndicatorView => indicatorView;
    }
}