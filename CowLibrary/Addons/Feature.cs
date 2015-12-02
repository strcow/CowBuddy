namespace CowLibrary.Addons
{
    using EloBuddy.SDK.Menu;
    using EloBuddy.SDK.Menu.Values;

    public abstract class Feature
    {
        public abstract string Name { get; }

        public Menu Menu { get; private set; }

        protected ValueBase this[string menuItem]
        {
            get
            {
                return this.Menu[menuItem];
            }
        }

        public virtual void Load(Addon owner)
        {
            this.Menu = owner.Menu.AddSubMenu(this.Name, this.Name);
            this.Menu.AddGroupLabel("Settings");

            var toggleFeature = this as IToggleFeature;

            if (toggleFeature != null)
            {
                this.ToggleFeatureLoad(toggleFeature);
            }

            this.Initialize();
        }

        protected abstract void Initialize();

        private void ToggleFeatureLoad(IToggleFeature toggleFeature)
        {
            this.Menu.Add(this.Name + "enabled", new CheckBox("Enabled")).OnValueChange += (sender, args) =>
            {
                if (args.NewValue)
                {
                    toggleFeature.Enable();
                }
                else
                {
                    toggleFeature.Disable();
                }
            };

            if (this[this.Name + "enabled"].Cast<CheckBox>().CurrentValue)
            {
                toggleFeature.Enable();
            }
        }
    }
}
