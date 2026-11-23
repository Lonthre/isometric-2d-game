using UnityEngine;


namespace Isometric2DGame.Core {

    /// <summary>Helper for snapping vectors to 8 screen directions (45 degrees each).</summary>
    public static class Direction8 {
        private const int Count = 8;
        private const float StepDegrees = 45f;

        /// <summary>Returns a Vector2 in radians closest to one the 8 directions.</summary>
        public static Vector2 Snap(Vector2 vector) {
            if (vector.sqrMagnitude < 0.0001f) return Vector2.zero;

            var angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            var snapped = Mathf.Round(angle / StepDegrees) * StepDegrees * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(snapped), Mathf.Sin(snapped));
        }

        /// <summary>
        ///     Returns 0-7, with 0 = West direction to match the sprite anims sheet rows. Clockwise: W, NW, N, NE, E, SE, S,
        ///     SW.
        /// </summary>
        public static int ToIndex(Vector2 vector) {
            var angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            var clockwiseFromEast = Mathf.RoundToInt(angle / StepDegrees);
            return ((Count / 2 - clockwiseFromEast) % Count + Count) % Count;
        }
    }

}