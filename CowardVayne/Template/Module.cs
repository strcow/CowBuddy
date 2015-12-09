namespace CowardVayne.Template
{
    using EloBuddy;

    public abstract class Module
    {
        #region Public Methods and Operators

        public virtual void OnAttack(AttackableUnit target)
        {
        }

        public virtual void OnPostAttack(AttackableUnit target)
        {
            // nothing by default
        }

        public virtual void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
        }

        public virtual void OnSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
        }

        public virtual void OnTick()
        {
            // nothing by default
        }

        #endregion
    }
}