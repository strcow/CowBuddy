namespace CowardVayne
{
    using System;
    using System.Linq;

    using CowardVayne.Template;

    using EloBuddy;
    using EloBuddy.SDK;

    public class TumbleKillsecure : Module
    {
        #region Public Methods and Operators

        public override void OnTick()
        {
            if (!Config.Settings.Tumble.TumbleKillsteal || !SpellManager.Q.IsReady())
            {
                return;
            }

            foreach (var hero in EntityManager.Heroes.Enemies.Where(e => !e.IsDead && e.IsInRange(Player.Instance.Position, Player.Instance.GetAutoAttackRange() + SpellManager.Q.Range)))
            {
                float aadmg = Player.Instance.GetAutoAttackDamage(hero) + 0.5f * Player.Instance.TotalAttackDamage;

                var threshold = hero.Health + hero.AllShield;

                if (aadmg > threshold)
                {
                    Tumble.CastTumble(hero);
                    break;
                }
            }
        }

        #endregion
    }
}