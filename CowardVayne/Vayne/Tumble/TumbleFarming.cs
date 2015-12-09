namespace CowardVayne
{
    using System;
    using System.Linq;

    using CowardVayne.Imports;
    using CowardVayne.Template;

    using EloBuddy;
    using EloBuddy.SDK;

    public class TumbleFarming : Module
    {
        public override void OnTick()
        {
            if (Orbwalker.CanAutoAttack || !SpellManager.Q.IsReady() || Player.Instance.IsRecalling() || !ManaManager.CanCast())
            {
                return;
            }

            if (!Config.Settings.LastHit.UseQ && !Config.Settings.Clear.UseQ)
            {
                return;
            }

            foreach (var minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                            Player.Instance.ServerPosition, ObjectManager.Player.GetAutoAttackRange()))
            {
                var dmg = Player.Instance.GetSpellDamage(minion, SpellSlot.Q) + Player.Instance.GetAutoAttackDamage(minion);
                if (Prediction.Health.GetPrediction(minion, (int)(Player.Instance.AttackDelay * 1000)) <= dmg / 2 && (Orbwalker.LastTarget == null || Orbwalker.LastTarget.NetworkId != minion.NetworkId))
                {

                    Tumble.CastTumble(minion);
                }
            }
        }
    }
}
