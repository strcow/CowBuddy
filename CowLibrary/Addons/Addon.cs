namespace CowLibrary.Addons
{
    using System;
    using System.Collections.Generic;

    using EloBuddy.SDK.Events;
    using EloBuddy.SDK.Menu;

    public class Addon
    {
        private readonly HashSet<Feature> features = new HashSet<Feature>();
        private readonly string addonName;

        public Addon(string name)
        {
            this.addonName = name;
            Loading.OnLoadingComplete += this.Loading_OnLoadingComplete;
        }

        public delegate void MenuInitializedEventHandler(Menu menu);

        public event MenuInitializedEventHandler MenuInitialized;

        public Menu Menu { get; private set; }

        public Addon Add(Feature feat)
        {
            this.features.Add(feat);
            return this;
        }

        private void Loading_OnLoadingComplete(EventArgs args)
        {
            this.Menu = MainMenu.AddMenu(this.addonName, this.addonName);
            this.OnMenuInitialized(this.Menu);

            foreach (var feat in this.features)
            {
                feat.Load(this);
            }
        }

        private void OnMenuInitialized(Menu menu)
        {
            if (this.MenuInitialized != null)
            {
                this.MenuInitialized(menu);
            }
        }
    }
}
