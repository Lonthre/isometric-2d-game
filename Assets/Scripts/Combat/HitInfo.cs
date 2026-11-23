using UnityEngine;


namespace Isometric2DGame.Combat {

    /// <summary>Describes a single hit: damage amount, direction, and source.</summary>
    public readonly struct HitInfo {
        public int Damage { get; }
        public Vector2 Direction { get; }
        public GameObject Source { get; }

        public HitInfo(int damage, Vector2 direction, GameObject source = null) {
            Damage = damage;
            Direction = direction;
            Source = source;
        }
    }

}