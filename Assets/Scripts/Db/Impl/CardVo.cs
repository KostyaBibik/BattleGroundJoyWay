using System;
using Db.Enums;
using UnityEngine;

namespace Db.Impl
{
    [Serializable]
    public struct CardVo
    {
        public ETeam Team;
        
        [Space]
        public int health;
        public string name;
    }
}