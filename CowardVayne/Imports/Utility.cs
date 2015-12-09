namespace CowardVayne.Imports
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    using EloBuddy;
    using EloBuddy.SDK;

    using SharpDX;

    public static class Utility
    {
        #region Public Methods and Operators

        public static long Benchmark(Action subject)
        {
            var watch = new Stopwatch();
            watch.Reset();
            watch.Start();
            subject();
            watch.Stop();
            return watch.ElapsedTicks;
        }

        public static bool IsValidTargetEx(
            this AttackableUnit unit,
            float range,
            bool checkTeam = true,
            Vector3 from = default(Vector3))
        {
            var ai = unit as Obj_AI_Base;

            if ((ai != null && ai.HasBuff("kindredrnodeathbuff") && ai.HealthPercent <= 10.0)
                || checkTeam && unit.Team == ObjectManager.Player.Team)
            {
                return false;
            }

            var targetPosition = ai != null ? ai.ServerPosition : unit.Position;
            var fromPosition = from.To2D().IsValid() ? from.To2D() : ObjectManager.Player.ServerPosition.To2D();

            var distance2 = Vector2.DistanceSquared(fromPosition, targetPosition.To2D());
            return distance2 <= range * range;
        }

        public static bool IsWall(this Vector3 position)
        {
            return NavMesh.GetCollisionFlags(position).HasFlag(CollisionFlags.Wall) || NavMesh.GetCollisionFlags(position).HasFlag(CollisionFlags.Building);
        }

        public static bool UnderTurret(this Obj_AI_Base unit)
        {
            return unit.Position.UnderTurret(true);
        }

        public static bool UnderTurret(this Obj_AI_Base unit, bool enemyTurretsOnly)
        {
            return unit.Position.UnderTurret(enemyTurretsOnly);
        }

        public static bool UnderTurret(this Vector3 position, bool enemyTurretsOnly)
        {
            var turretList = enemyTurretsOnly ? EntityManager.Turrets.Enemies : EntityManager.Turrets.AllTurrets;
            return turretList.Any(t => t.IsInRange(position, 950f) && !t.IsDead);
        }

        #endregion
    }
}