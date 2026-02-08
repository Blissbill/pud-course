using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public static class LayerIds
    {
        public static readonly int Default;
        public static readonly int PlayerBullet;
        public static readonly int EnemyBullet;

        static LayerIds()
        {
            Default = LayerMask.NameToLayer("Default");
            PlayerBullet = LayerMask.NameToLayer("PlayerBullet");
            EnemyBullet = LayerMask.NameToLayer("EnemyBullet");

#if UNITY_EDITOR
            List<string> missingLayers = new List<string>();
            if (Default < 0) missingLayers.Add("Default");
            if (PlayerBullet < 0) missingLayers.Add("PlayerBullet");
            if (EnemyBullet < 0) missingLayers.Add("EnemyBullet");
            if (missingLayers.Count > 0)
            {
                throw new InvalidOperationException($"Missing layers: {String.Join(", ", missingLayers)}");
            } 
#endif
        }
    }
}