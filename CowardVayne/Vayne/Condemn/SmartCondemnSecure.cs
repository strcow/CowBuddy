namespace CowardVayne
{
    using System;

    using CowardVayne.Template;

    using EloBuddy;
    using EloBuddy.SDK;

    public class SmartCondemnSecure : Module
    {
        #region Public Methods and Operators

        public override void OnPostAttack(AttackableUnit target)
        {
            if (!Config.Settings.Condemn.CondemnKillsecure)
            {
                return;
            }

            var ai = target as AIHeroClient;

            if (ai == null)
            {
                return;
            }

            var dmg = ObjectManager.Player.GetSpellDamage(ai, SpellSlot.E)
                      + ai.MaxHealth * (0.06 + 0.015 * SpellManager.W.Level);

            if (ai.HasWStacks(1) && dmg >= ai.Health + ai.AllShield + 30)
            {
                ai.Condemn();
            }
        }

        #endregion
    }
}