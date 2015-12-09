namespace CowardVayne.Template
{
    using System;
    using System.Collections.Generic;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Enumerations;
    using EloBuddy.SDK.Utils;

    public static class ModuleManager
    {
        #region Constructors and Destructors

        static ModuleManager()
        {
            // TODO: Change your modules here
            Modules = new List<Module>(
                new Module[]
                {
                    new SmartCondemnSecure(), 
                    new CondemnHarass(), 
                    new Condemn(),
                    new TumbleKillsecure(),
                    new Tumble(),
                    new TumbleFarming()
                });

            // Listen to events we need
            Game.OnTick += Game_OnTick;

            //Orbwalker.OnPreAttack += Orbwalker_OnPreAttack;
            Orbwalker.OnPostAttack += Orbwalker_OnPostAttack;
            //Orbwalker.OnAttack += Orbwalker_OnAttack;
            //Orbwalker.OnUnkillableMinion += Orbwalker_OnUnkillableMinion;

            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            //Obj_AI_Base.OnSpellCast += Obj_AI_Base_OnSpellCast;

            //Drawing.OnDraw += Drawing_OnDraw;
            //Drawing.OnEndScene += Drawing_OnEndScene;
        }

        #endregion

        #region Public Properties

        public static List<Module> Modules { get; private set; }

        #endregion

        #region Public Methods and Operators

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }

        #endregion

        #region Methods

        private static void Game_OnTick(EventArgs args)
        {
            foreach (var module in Modules)
            {
                try
                {
                    module.OnTick();
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, "Error at module '{0}' OnTick\n{1}", module.GetType().Name, e);
                }
            }
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            foreach (var module in Modules)
            {
                try
                {
                    module.OnProcessSpellCast(sender, args);
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, "Error at module '{0}' OnPostAttack\n{1}", module.GetType().Name, e);
                }
            }
        }

        private static void Obj_AI_Base_OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            foreach (var module in Modules)
            {
                try
                {
                    module.OnSpellCast(sender, args);
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, "Error at module '{0}' OnPostAttack\n{1}", module.GetType().Name, e);
                }
            }
        }

        private static void Orbwalker_OnAttack(AttackableUnit target, EventArgs args)
        {
            foreach (var module in Modules)
            {
                try
                {
                    module.OnAttack(target);
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, "Error at module '{0}' OnAttack\n{1}", module.GetType().Name, e);
                }
            }
        }

        private static void Orbwalker_OnPostAttack(AttackableUnit target, EventArgs args)
        {
            foreach (var module in Modules)
            {
                try
                {
                    module.OnPostAttack(target);
                }
                catch (Exception e)
                {
                    Logger.Log(LogLevel.Error, "Error at module '{0}' OnPostAttack\n{1}", module.GetType().Name, e);
                }
            }
        }

        #endregion
    }
}