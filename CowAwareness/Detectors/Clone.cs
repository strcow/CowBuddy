namespace CowAwareness.Detectors
{
    using System.Collections.Generic;
    using System.Linq;

    using CowLibrary.Addons;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Rendering;

    using SharpDX;

    public class Clone : Feature, IToggleFeature
    {
        private readonly List<string> cloneHeroes = new List<string> { "shaco", "leblanc", "monkeyking", "yorick" };
        private readonly List<Obj_AI_Base> heroes = new List<Obj_AI_Base>();
        private ColorBGRA color;

        public override string Name
        {
            get { return "Clone Revealer"; }
        }

        public void Enable()
        {
            Drawing.OnEndScene += this.Drawing_OnEndScene;
        }

        public void Disable()
        {
            Drawing.OnEndScene -= this.Drawing_OnEndScene;
        }

        protected override void Initialize()
        {
            this.Menu.AddLabel("Detects clone champions real location with a circle, enabled for:");
            this.Menu.AddLabel("- Shaco");
            this.Menu.AddLabel("- LeBlanc");
            this.Menu.AddLabel("- Wukong");
            this.Menu.AddLabel("- Yorick");

            this.color = Color.Magenta;
            this.heroes.AddRange(EntityManager.Heroes.Enemies.Where(e => this.cloneHeroes.Contains(e.ChampionName.ToLower())));

            if (!this.heroes.Any())
            {
                this.Disable();
            }
        }

        private void Drawing_OnEndScene(System.EventArgs args)
        {
            foreach (var hero in this.heroes.Where(hero => !hero.IsDead && hero.IsVisible && hero.Position.IsOnScreen()))
            {
                Circle.Draw(this.color, hero.BoundingRadius, 2f, hero.Position);
            }
        }
    }
}