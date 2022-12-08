using Db.Impl;

namespace PlayableItems.Logic
{
    public interface ICardFactory
    {
        CardView CreateCard(CardVo cardVo);
    }
}