using UnityEngine;

namespace Db
{
    [CreateAssetMenu(menuName = "Prefabs/" + nameof(CardPrefabs),
        fileName = nameof(CardPrefabs))]
    public class CardPrefabs : ScriptableObject
    {
        [SerializeField] private GameObject cardView;
        public GameObject CardView => cardView;
    }
}