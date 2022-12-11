using System;
using System.Collections.Generic;
using UnityEngine;

namespace Db.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(CardSettings),
        fileName = nameof(CardSettings))]
    public class CardSettings : ScriptableObject, ICardSettings
    {
        [SerializeField] private GameObject defaultCard;
        [SerializeField] private List<CardVo> cards;
        
        public GameObject DefaultCard => defaultCard;
        public List<CardVo> AllCards => cards;
        public CardVo GetCard(string cardName)
        {
            foreach (var cardVo in cards)
            {
                if (cardVo.name == cardName)
                    return cardVo;
            }
            
            throw new Exception($"[{nameof(CardSettings)}] Cannot find CardVo with name: {cardName}");
        }
    }
}