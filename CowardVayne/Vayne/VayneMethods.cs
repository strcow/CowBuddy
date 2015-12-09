namespace CowardVayne
{
    using System.Linq;

    using EloBuddy;
    using EloBuddy.SDK;

    using SharpDX;

    public static class VayneMethods
    {
        #region Public Methods and Operators

        public static void Condemn(this AttackableUnit target)
        {
            if (target != null)
            {
                Player.CastSpell(SpellSlot.E, target);
            }
        }

        public static bool HasWStacks(this Obj_AI_Base target, int count)
        {
            // why 1? cuz we are onafterattack
            return target.Buffs.Any(bu => bu.Name == "vaynesilvereddebuff" && bu.Count == count);
        }

        public static void Tumble(this Vector3 to, AttackableUnit afterTumbleTarget)
        {
            Player.CastSpell(SpellSlot.Q, to);

            if (afterTumbleTarget != null && afterTumbleTarget.IsValidTarget(Player.Instance.GetAutoAttackRange()))
            {
                Player.IssueOrder(GameObjectOrder.AttackUnit, afterTumbleTarget);
            }
        }

        #endregion
    }
}