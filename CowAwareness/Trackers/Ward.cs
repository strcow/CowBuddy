namespace CowAwareness.Trackers
{
    using System.Collections.Generic;
    using System.Linq;

    using CowLibrary.Addons;

    using EloBuddy;
    using EloBuddy.SDK.Menu.Values;
    using EloBuddy.SDK.Rendering;

    using SharpDX;

    using Color = System.Drawing.Color;

    public class Ward : Feature, IToggleFeature
    {
        private static List<WardInfo> wards = new List<WardInfo>();

        public override string Name
        {
            get
            {
                return "Ward Tracker";
            }
        }

        public void Disable()
        {
            Drawing.OnDraw -= this.Drawing_OnDraw;
            GameObject.OnCreate -= this.GameObject_OnCreate;
            GameObject.OnDelete -= this.GameObject_OnDelete;
        }

        public void Enable()
        {
            Drawing.OnDraw += this.Drawing_OnDraw;
            GameObject.OnCreate += this.GameObject_OnCreate;
            GameObject.OnDelete += this.GameObject_OnDelete;
        }

        protected override void Initialize()
        {
            this.Menu.Add("timer", new CheckBox("Draw remaining time"));
            this.Menu.Add("range", new KeyBind("Draw wards range", false, KeyBind.BindTypes.HoldActive, 'Z'));
        }

        private void GameObject_OnCreate(GameObject sender, System.EventArgs args)
        {
            if (!(sender is Obj_AI_Minion) || !sender.Name.Contains("Ward"))
            {
                return;
            }

            var ward = (Obj_AI_Minion)sender;

            if (ward.IsAlly)
            {
                return;
            }

            switch (ward.BaseSkinName)
            {
                case "YellowTrinket":
                    wards.Add(new WardInfo(ward.Name, false, true, Game.Time + 60, ward.Position, Color.Lime));
                    break;
                case "YellowTrinketUpgrade":
                    wards.Add(new WardInfo(ward.Name, false, true, Game.Time + 120, ward.Position, Color.Lime));
                    break;
                case "VisionWard":
                    wards.Add(new WardInfo(ward.Name, true, true, 0, ward.Position, Color.DeepPink));
                    break;
                case "SightWard":
                    wards.Add(new WardInfo(ward.Name, false, true, Game.Time + 180, ward.Position, Color.Lime));
                    break;
            }
        }

        private void GameObject_OnDelete(GameObject sender, System.EventArgs args)
        {
            if (!(sender is Obj_AI_Minion) || !sender.Name.Contains("Ward"))
            {
                return;
            }

            var ward = (Obj_AI_Minion)sender;

            if (ward.IsAlly)
            {
                return;
            }

            var wardInfo = wards.Where(w => w.Available).FirstOrDefault(w => w.Position == ward.Position);
            if (wardInfo != null)
            {
                wardInfo.Available = false;
            }
        }

        private void Drawing_OnDraw(System.EventArgs args)
        {
            var removeList = new List<WardInfo>();

            foreach (var wardInfo in wards)
            {
                if (!wardInfo.Available)
                {
                    removeList.Add(wardInfo);
                    continue;
                }

                var remaining = wardInfo.DeleteTimer - Game.Time;

                if (remaining > 0 || wardInfo.IsPink)
                {
                    var fancytimer = string.Format("{0:0}", remaining);
                    int radius = this["range"].Cast<KeyBind>().CurrentValue ? 1100 : 60;
                    
                    new Circle { Color = wardInfo.Color, Radius = radius, BorderWidth = 1f }.Draw(wardInfo.Position);

                    if (this["timer"].Cast<CheckBox>().CurrentValue && !wardInfo.IsPink)
                    {
                        Drawing.DrawText(Drawing.WorldToScreen(wardInfo.Position), Color.White, fancytimer, 50);
                    }
                }
                else
                {
                    removeList.Add(wardInfo);
                }
            }

            if (removeList.Any())
            {
                wards = wards.Except(removeList).ToList();
            }
        }

        internal class WardInfo
        {
            public WardInfo(string name, bool isPink, bool available, float deleteTimer, Vector3 position, Color color)
            {
                this.Name = name;
                this.IsPink = isPink;
                this.DeleteTimer = deleteTimer;
                this.Position = position;
                this.Color = color;
                this.Available = available;
            }

            public string Name { get; set; }

            public bool IsPink { get; set; }

            public Color Color { get; set; }

            public float DeleteTimer { get; set; }

            public Vector3 Position { get; set; }

            public bool Available { get; set; }
        }
    }
}
