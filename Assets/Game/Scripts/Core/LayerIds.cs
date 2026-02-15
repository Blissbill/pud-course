using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public static class LayerIds
    {
        public static readonly Lazy<int> Default = new(() => LayerMask.NameToLayer("Default"));
        public static readonly Lazy<int> PlayerBullet = new(() => LayerMask.NameToLayer("PlayerBullet"));
        public static readonly Lazy<int> EnemyBullet = new(() => LayerMask.NameToLayer("EnemyBullet"));
    }
}