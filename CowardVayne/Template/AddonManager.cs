namespace CowardVayne.Template
{
    using System;

    using EloBuddy;
    using EloBuddy.SDK.Events;

    public static class AddonManager
    {
        #region Public Properties

        public static string AddonName { get; private set; }

        public static string ChampionName { get; private set; }

        #endregion

        #region Public Methods and Operators

        public static void Initialize(string addonName, string championName)
        {
            AddonName = addonName;
            ChampionName = championName;

            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        #endregion

        #region Methods

        private static void OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != ChampionName)
            {
                // Champion is not the one we made this addon for,
                // therefore we return
                return;
            }

            Chat.Print(
                string.Format("<font color='#ffffff'><font color='#ff0000'><b>{0}</b></font> Loaded!</font>", AddonName));
            Chat.Print(
                "<font color='#ffffff'>Check out <font color='#66FF33'><b>CowAwareness</b></font> for a gamebreaking experience!</font>");

            Config.Initialize();
            SpellManager.Initialize();
            ModuleManager.Initialize();
        }

        #endregion
    }
}