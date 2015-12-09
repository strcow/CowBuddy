namespace CowardVayne
{
    using System;

    using CowardVayne.Template;

    using EloBuddy;

    public class CondemnHarass : Module
    {
        #region Public Methods and Operators

        public override void OnPostAttack(AttackableUnit target)
        {
            if (!Config.Settings.Harass.UseE || !ManaManager.CanCast())
            {
                return;
            }

            var ai = target as AIHeroClient;

            if (ai == null)
            {
                return;
            }

            if (ai.HasWStacks(1))
            {
                ai.Condemn();
            }
        }

        #endregion
    }
}