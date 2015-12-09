namespace CowardVayne
{
    using System.Collections.Generic;
    using System.Linq;

    using CowardVayne.Template;

    using EloBuddy;
    using EloBuddy.SDK;

    using SharpDX;

    public class Condemn : Module
    {
        #region Static Fields

        private static readonly string[] MobNames =
            {
                "SRU_Red", "SRU_Blue", "SRU_Gromp", "SRU_Murkwolf",
                "SRU_Razorbeak", "SRU_Krug"
            };

        #endregion

        #region Public Methods and Operators

        public static Obj_AI_Base GetTarget(Vector3 fromPosition)
        {
            var targetList =
                EntityManager.Heroes.Enemies.Where(
                    h =>
                    h.IsValidTarget(SpellManager.E.Range) && !h.HasBuffOfType(BuffType.SpellShield)
                    && !h.HasBuffOfType(BuffType.SpellImmunity)
                    && h.Health > ObjectManager.Player.GetAutoAttackDamage(h, true) * 2).ToList();

            if (!targetList.Any())
            {
                return null;
            }

            foreach (var enemy in targetList)
            {
                var prediction = SpellManager.E.GetPrediction(enemy);

                var predictionsList = new List<Vector3>
                                          {
                                              enemy.ServerPosition,
                                              enemy.Position,
                                              prediction.CastPosition,
                                              prediction.UnitPosition
                                          };

                var wallsFound = 0;

                foreach (var position in predictionsList)
                {
                    var distance = fromPosition.Distance(position);

                    for (var i = 0; i < Config.Settings.Condemn.PushDistance; i += (int)enemy.BoundingRadius)
                    {
                        var finalPosition = fromPosition.Extend(position, distance + i).To3D();
                        if (NavMesh.GetCollisionFlags(finalPosition).HasFlag(CollisionFlags.Wall)
                            || NavMesh.GetCollisionFlags(finalPosition).HasFlag(CollisionFlags.Building))
                        {
                            wallsFound++;
                            break;
                        }
                    }
                }

                if (wallsFound >= Config.Settings.Condemn.Accuracy)
                {
                    return enemy;
                }
            }

            return null;
        }

        public override void OnPostAttack(AttackableUnit target)
        {
            if (!Config.Settings.Jungle.UseE || !ManaManager.CanCast())
            {
                return;
            }

            var ai = target as Obj_AI_Base;

            if (ai != null && MobNames.Contains(ai.BaseSkinName))
            {
                target.Condemn();
            }
        }

        public override void OnTick()
        {
            if (!Config.Settings.Combo.UseE && !Config.Settings.Harass.UseE && !Config.Settings.Condemn.PermaActive)
            {
                return;
            }

            if (!ManaManager.CanCast())
            {
                return;
            }

            var target = GetTarget(ObjectManager.Player.Position);
            target.Condemn();
        }

        #endregion
    }
}