namespace CowardVayne.Template
{
    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Enumerations;

    public static class SpellManager
    {
        #region Constructors and Destructors

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Active(SpellSlot.Q, 300);
            W = new Spell.Active(SpellSlot.W);
            // https://www.reddit.com/r/leagueoflegends/comments/1g7zg4/vayne_condemn_range_still_not_matching_aa_range/cahm5ws
            E = new Spell.Skillshot(
                SpellSlot.E,
                (uint)(590 + ObjectManager.Player.BoundingRadius),
                SkillShotType.Linear,
                250,
                1200);
            R = new Spell.Active(SpellSlot.R);
        }

        #endregion

        #region Public Properties

        public static Spell.Skillshot E { get; private set; }

        // You will need to edit the types of spells you have for each champ as they
        // don't have the same type for each champ, for example Xerath Q is chargeable,
        // right now it's  set to Active.
        public static Spell.Active Q { get; private set; }

        public static Spell.Active R { get; private set; }

        public static Spell.Active W { get; private set; }

        #endregion

        #region Public Methods and Operators

        public static void Initialize()
        {
        }

        #endregion
    }
}