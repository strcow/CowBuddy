namespace CowardVayne.Template
{
    using System.Diagnostics.CodeAnalysis;

    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    public static class Config
    {
        #region Static Fields

        private static readonly Menu Menu;

        #endregion

        #region Constructors and Destructors

        static Config()
        {
            Menu = MainMenu.AddMenu(AddonManager.AddonName, AddonManager.AddonName.ToLower());
            Menu.AddGroupLabel("Welcome to this AddonTemplate!");
            Menu.AddLabel("To change the menu, please have a look at the");
            Menu.AddLabel("Config.cs class inside the project, now have fun!");

            // Global settings
            Settings.Combo.Load();
            Settings.Harass.Load();
            Settings.LastHit.Load();
            Settings.Clear.Load();
            Settings.Jungle.Load();

            // TODO: Champion specific settings
            Settings.Tumble.Load();
            Settings.Condemn.Load();
        }

        #endregion

        #region Public Methods and Operators

        public static void Initialize()
        {
        }

        #endregion

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static class Settings
        {
            public static class Clear
            {
                #region Static Fields

                private static readonly CheckBox Menu_E;

                private static readonly Slider Menu_ManaManager;

                private static readonly CheckBox Menu_Q;

                private static readonly CheckBox Menu_R;

                private static readonly CheckBox Menu_W;

                #endregion

                #region Constructors and Destructors

                static Clear()
                {
                    var menu = Menu.AddSubMenu("LaneClear");

                    menu.AddGroupLabel("Skills");
                    Menu_Q = menu.Add("lc.q.enabled", new CheckBox("Use Q", false));
                    Menu_W = menu.Add("lc.w.enabled", new CheckBox("Use W", false));
                    Menu_E = menu.Add("lc.e.enabled", new CheckBox("Use E", false));
                    Menu_R = menu.Add("lc.r.enabled", new CheckBox("Use R", false));
                    Menu_ManaManager = menu.Add("lc.mana", new Slider("Mana Manager", 60));
                }

                #endregion

                #region Public Properties

                public static int Mana
                {
                    get
                    {
                        return Menu_ManaManager.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return Menu_E.CurrentValue
                               && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return Menu_Q.CurrentValue
                               && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
                    }
                }

                public static bool UseR
                {
                    get
                    {
                        return Menu_R.CurrentValue
                               && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return Menu_W.CurrentValue
                               && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
                    }
                }

                #endregion

                #region Public Methods and Operators

                public static void Load()
                {
                }

                #endregion
            }

            public static class Combo
            {
                #region Static Fields

                private static readonly CheckBox Menu_E;

                private static readonly Slider Menu_MinEnemiesR;

                private static readonly CheckBox Menu_Q;

                private static readonly CheckBox Menu_Q2Stacks;

                private static readonly CheckBox Menu_R;

                private static readonly CheckBox Menu_W;

                #endregion

                #region Constructors and Destructors

                static Combo()
                {
                    var menu = Menu.AddSubMenu("Combo");

                    menu.AddGroupLabel("Skills");
                    Menu_Q = menu.Add("combo.q.enabled", new CheckBox("Use Q"));
                    Menu_W = menu.Add("combo.w.enabled", new CheckBox("Use W"));
                    Menu_E = menu.Add("combo.e.enabled", new CheckBox("Use E"));
                    Menu_R = menu.Add("combo.r.enabled", new CheckBox("Use R", false));
                }

                #endregion

                #region Public Properties

                public static bool UseE
                {
                    get
                    {
                        return Menu_E.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return Menu_Q.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
                    }
                }

                public static bool UseR
                {
                    get
                    {
                        return Menu_R.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return Menu_W.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
                    }
                }

                #endregion

                #region Public Methods and Operators

                public static void Load()
                {
                }

                #endregion
            }

            public static class Condemn
            {
                #region Static Fields

                private static readonly Slider Menu_EAccuracy;

                private static readonly CheckBox Menu_EPermaActive;

                private static readonly Slider Menu_EPushDistance;

                private static readonly CheckBox Menu_ESmartKillsecure;

                private static readonly CheckBox Menu_ETrinket;

                #endregion

                #region Constructors and Destructors

                static Condemn()
                {
                    var menu = Menu.AddSubMenu("Condemn");

                    Menu_EPushDistance = menu.Add("condemn.distance", new Slider("E Push Distance", 420, 350, 470));
                    Menu_EAccuracy = menu.Add("condemn.accuracy", new Slider("Accuracy", 2, 1, 4));
                    menu.AddLabel("Lower the accuracy for more frequent E uses, but miss stuns more often");
                    Menu_ETrinket = menu.Add("condemn.trinket", new CheckBox("Trinket Bush on Condemn"));
                    Menu_ESmartKillsecure = menu.Add("condemn.smartks", new CheckBox("Smart E KS"));
                    Menu_EPermaActive = menu.Add(
                        "condemn.perma",
                        new CheckBox("Uses E whenever possible to stun", false));
                }

                #endregion

                #region Public Properties

                public static int Accuracy
                {
                    get
                    {
                        return Menu_EAccuracy.CurrentValue;
                    }
                }

                public static bool CondemnKillsecure
                {
                    get
                    {
                        return Menu_ESmartKillsecure.CurrentValue;
                    }
                }

                public static bool PermaActive
                {
                    get
                    {
                        return Menu_EPermaActive.CurrentValue;
                    }
                }

                public static int PushDistance
                {
                    get
                    {
                        return Menu_EPushDistance.CurrentValue;
                    }
                }

                #endregion

                #region Public Methods and Operators

                public static void Load()
                {
                }

                #endregion
            }

            public static class Harass
            {
                #region Static Fields

                private static readonly CheckBox Menu_E;

                private static readonly Slider Menu_ManaManager;

                private static readonly CheckBox Menu_Q;

                private static readonly CheckBox Menu_R;

                private static readonly CheckBox Menu_W;

                #endregion

                #region Constructors and Destructors

                static Harass()
                {
                    var menu = Menu.AddSubMenu("Harass");

                    menu.AddGroupLabel("Skills");
                    Menu_Q = menu.Add("harass.q.enabled", new CheckBox("Use Q"));
                    Menu_W = menu.Add("harass.w.enabled", new CheckBox("Use W"));
                    Menu_E = menu.Add("harass.e.enabled", new CheckBox("Use E"));
                    Menu_R = menu.Add("harass.r.enabled", new CheckBox("Use R", false));
                    Menu_ManaManager = menu.Add("harass.mana", new Slider("Mana Manager", 60));
                }

                #endregion

                #region Public Properties

                public static int Mana
                {
                    get
                    {
                        return Menu_ManaManager.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return Menu_E.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return Menu_Q.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
                    }
                }

                public static bool UseR
                {
                    get
                    {
                        return Menu_R.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return Menu_W.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
                    }
                }

                #endregion

                #region Public Methods and Operators

                public static void Load()
                {
                }

                #endregion
            }

            public static class Jungle
            {
                #region Static Fields

                private static readonly CheckBox Menu_E;

                private static readonly Slider Menu_ManaManager;

                private static readonly CheckBox Menu_Q;

                private static readonly CheckBox Menu_R;

                private static readonly CheckBox Menu_W;

                #endregion

                #region Constructors and Destructors

                static Jungle()
                {
                    var menu = Menu.AddSubMenu("Jungle");

                    menu.AddGroupLabel("Skills");
                    Menu_Q = menu.Add("jungle.q.jungle", new CheckBox("Q Jungle"));
                    Menu_W = menu.Add("jungle.w.jungle", new CheckBox("W Jungle"));
                    Menu_E = menu.Add("jungle.e.jungle", new CheckBox("E Jungle"));
                    Menu_R = menu.Add("jungle.r.jungle", new CheckBox("R Jungle"));
                    Menu_ManaManager = menu.Add("jungle.mana", new Slider("Mana Manager", 60));
                }

                #endregion

                #region Public Properties

                public static int Mana
                {
                    get
                    {
                        return Menu_ManaManager.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return Menu_E.CurrentValue
                               && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return Menu_Q.CurrentValue
                               && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
                    }
                }

                public static bool UseR
                {
                    get
                    {
                        return Menu_R.CurrentValue
                               && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return Menu_W.CurrentValue
                               && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
                    }
                }

                #endregion

                #region Public Methods and Operators

                public static void Load()
                {
                }

                #endregion
            }

            public static class LastHit
            {
                #region Static Fields

                private static readonly CheckBox Menu_E;

                private static readonly Slider Menu_ManaManager;

                private static readonly CheckBox Menu_Q;

                private static readonly CheckBox Menu_R;

                private static readonly CheckBox Menu_W;

                #endregion

                #region Constructors and Destructors

                static LastHit()
                {
                    var menu = Menu.AddSubMenu("LastHit");

                    menu.AddGroupLabel("Skills");
                    Menu_Q = menu.Add("lh.q.enabled", new CheckBox("Use Q", false));
                    Menu_W = menu.Add("lh.w.enabled", new CheckBox("Use W", false));
                    Menu_E = menu.Add("lh.e.enabled", new CheckBox("Use E", false));
                    Menu_R = menu.Add("lh.r.enabled", new CheckBox("Use R", false));
                    Menu_ManaManager = menu.Add("lh.mana", new Slider("Mana Manager", 60));
                }

                #endregion

                #region Public Properties

                public static int Mana
                {
                    get
                    {
                        return Menu_ManaManager.CurrentValue;
                    }
                }

                public static bool UseE
                {
                    get
                    {
                        return Menu_E.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
                    }
                }

                public static bool UseQ
                {
                    get
                    {
                        return Menu_Q.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
                    }
                }

                public static bool UseR
                {
                    get
                    {
                        return Menu_R.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
                    }
                }

                public static bool UseW
                {
                    get
                    {
                        return Menu_W.CurrentValue && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
                    }
                }

                #endregion

                #region Public Methods and Operators

                public static void Load()
                {
                }

                #endregion
            }

            public static class Tumble
            {
                #region Static Fields

                private static readonly CheckBox Menu_QKillsteal;

                private static readonly CheckBox Menu_QWall;

                private static readonly CheckBox Menu_TumbleAvoidTurrets;

                private static readonly CheckBox Menu_TumbleToCondemn;

                #endregion

                #region Constructors and Destructors

                static Tumble()
                {
                    var menu = Menu.AddSubMenu("Tumble");

                    Menu_QWall = menu.Add("tumble.wall", new CheckBox("Q to Wall when Possible (Mirin Mode)"));
                    Menu_TumbleToCondemn = menu.Add("tumble.smartq", new CheckBox("Try to QE when possible"));
                    Menu_QKillsteal = menu.Add("tumble.ks", new CheckBox("Q for KS"));
                    Menu_TumbleAvoidTurrets = menu.Add(
                        "tumble.avoidturrets",
                        new CheckBox("Avoid Tumbling to enemy turrets"));
                }

                #endregion

                #region Public Properties

                public static bool MirinMode
                {
                    get
                    {
                        return Menu_QWall.CurrentValue;
                    }
                }

                public static bool TumbleAvoidTurrets
                {
                    get
                    {
                        return Menu_TumbleAvoidTurrets.CurrentValue;
                    }
                }

                public static bool TumbleKillsteal
                {
                    get
                    {
                        return Menu_QKillsteal.CurrentValue;
                    }
                }

                public static bool TumbleToCondemn
                {
                    get
                    {
                        return Menu_TumbleToCondemn.CurrentValue;
                    }
                }

                #endregion

                #region Public Methods and Operators

                public static void Load()
                {
                }

                #endregion
            }
        }
    }
}