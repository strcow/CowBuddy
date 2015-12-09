namespace CowardVayne
{
    using System.Collections.Generic;
    using System.Linq;

    using CowardVayne.Imports;
    using CowardVayne.Template;

    using EloBuddy;
    using EloBuddy.SDK;

    using SharpDX;

    public class Tumble : Module
    {
        #region Static Fields

        private static readonly string[] MobNames =
            {
                "SRU_Red", "SRU_Blue", "SRU_Gromp", "SRU_Murkwolf",
                "SRU_Razorbeak", "SRU_Krug", "Sru_Crab"
            };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the best position to Q
        /// </summary>
        /// <returns></returns>
        public static TumblePosition GetBestPosition(AttackableUnit target)
        {
            // we want positions with less melee enemies in range
            var positions = GetPossiblePositions().Where(p => p.AlliesInRange >= p.EnemiesInRange).ToList();

            if (target != null)
            {
                // remove all posible positions where my current target is not in range
                positions.RemoveAll(p => !target.IsInRange(p.Position, ObjectManager.Player.GetAutoAttackRange()));
            }

            if (Config.Settings.Tumble.TumbleAvoidTurrets)
            {
                positions.RemoveAll(p => p.UnderEnemyTurret);
            }

            // can Q to E?
            if (Config.Settings.Tumble.TumbleToCondemn)
            {
                // check positions by condemn
                var condemn = positions.FirstOrDefault(p => p.CanCondemn);
                if (condemn != null)
                {
                    return condemn;
                }
            }

            var best =
                positions.OrderBy(p => p.AttackableEnemies)
                    .ThenBy(p => p.Position.Distance(Game.CursorPos, true))
                    .FirstOrDefault();

            return best != null ? best : new TumblePosition(Game.CursorPos);
        }

        /// <summary>
        /// Gets all possible tumble positions
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TumblePosition> GetPossiblePositions()
        {
            var list = new List<TumblePosition>();

            var player2D = ObjectManager.Player.Position.To2D();
            var direction = ObjectManager.Player.Direction.To2D();

            for (var i = 0; i < 360; i += 30)
            {
                var rotatedPosition = player2D + direction.Rotated(Geometry.DegreeToRadian(i)) * SpellManager.Q.Range;
                list.Add(new TumblePosition(rotatedPosition.To3DWorld()));
            }

            return list;
        }

        /// <summary>
        /// Handles Q on farming and jungle modes
        /// </summary>
        /// <param name="target"></param>
        public void JungleTumble(AttackableUnit target)
        {
            var ai = target as Obj_AI_Base;

            if (ai != null && MobNames.Contains(ai.BaseSkinName) && Config.Settings.Jungle.UseQ)
            {
                // Jungle
                CastTumble(target);
            }
        }

        /// <summary>
        /// Casts Q after auto attack
        /// </summary>
        /// <param name="target"></param>
        public override void OnPostAttack(AttackableUnit target)
        {
            if (!SpellManager.Q.IsReady())
            {
                return;
            }
            if (Config.Settings.Combo.UseQ || Config.Settings.Harass.UseQ)
            {
                CastTumble(target);
            }
            else if (Config.Settings.Jungle.UseQ)
            {
                this.JungleTumble(target);
            }
        }

        /// <summary>
        /// Resets auto attack after Q
        /// </summary>
        public override void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe || args.Slot != SpellSlot.Q)
            {
                return;
            }

            Orbwalker.DisableMovement = true;
            Orbwalker.ResetAutoAttack();
            Core.DelayAction(() => { Orbwalker.DisableMovement = false; }, 500);
        }

        #endregion

        #region Methods

        public static void CastTumble(AttackableUnit target)
        {
            if (!ManaManager.CanCast())
            {
                return;
            }

            var position = GetBestPosition(target);

            if (Config.Settings.Tumble.TumbleAvoidTurrets && position.UnderEnemyTurret
                && !ObjectManager.Player.UnderTurret(true))
            {
                return;
            }

            if (position.EnemiesInRange == 1 || position.AlliesInRange >= position.EnemiesInRange)
            {
                position.Position.Tumble(target);
            }
        }

        #endregion

        public class TumblePosition
        {
            #region Constructors and Destructors

            public TumblePosition(Vector3 position)
            {
                var me = ObjectManager.Player;
                this.Position = position;

                this.NearbyMelees =
                    EntityManager.Heroes.Enemies.Count(
                        e => e.IsMelee && !e.IsDead && e.IsValidTargetEx(380, true, position));
                this.EnemiesInRange =
                    EntityManager.Heroes.Enemies.Count(e => e.Position.IsInRange(position, 600f) && !e.IsDead);
                this.AlliesInRange =
                    EntityManager.Heroes.Allies.Count(e => e.Position.IsInRange(position, 600f) && !e.IsDead);
                this.AttackableEnemies = EntityManager.Heroes.Enemies.Count(e => e.IsInAutoAttackRange(me) && !e.IsDead);
                this.CanCondemn = SpellManager.E.IsReady() && Condemn.GetTarget(position) != null;
                this.UnderEnemyTurret = position.UnderTurret(true);
                this.IsWall = false;
                // TODO: Mirin mode?
            }

            #endregion

            #region Public Properties

            public int AlliesInRange { get; private set; }

            public int AttackableEnemies { get; private set; }

            public bool CanCondemn { get; private set; }

            public int EnemiesInRange { get; private set; }

            public bool IsWall { get; private set; }

            public int NearbyMelees { get; private set; }

            public Vector3 Position { get; private set; }

            public bool UnderEnemyTurret { get; private set; }

            #endregion
        }
    }
}