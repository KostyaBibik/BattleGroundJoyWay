using System.Collections.Generic;
using Db.Impl;

namespace Db
{
    public interface ICardSettings
    {
        List<CardVo> AllCards { get; }
        CardVo GetPlant(string name);
    }
}