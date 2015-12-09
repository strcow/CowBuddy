namespace CowardVayne.Template
{
    using EloBuddy;
    using EloBuddy.SDK;

    public static class ManaManager
    {
        #region Public Methods and Operators

        public static bool CanCast()
        {
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                return true;
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
            {
                return ObjectManager.Player.ManaPercent >= Config.Settings.Jungle.Mana;
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                return ObjectManager.Player.ManaPercent >= Config.Settings.Harass.Mana;
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
            {
                return ObjectManager.Player.ManaPercent >= Config.Settings.Clear.Mana;
            }

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit))
            {
                return ObjectManager.Player.ManaPercent >= Config.Settings.LastHit.Mana;
            }

            return true;
        }

        #endregion
    }
}