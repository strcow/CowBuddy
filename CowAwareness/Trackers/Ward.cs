namespace CowAwareness.Trackers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using CowLibrary.Addons;

    using EloBuddy;
    using EloBuddy.SDK;
    using EloBuddy.SDK.Menu.Values;
    using EloBuddy.SDK.Rendering;

    using SharpDX;

    using Color = System.Drawing.Color;
    using Font = System.Drawing.Font;

    public class Ward : Feature, IToggleFeature
    {
        private static List<WardInfo> wards = new List<WardInfo>();

        private Text text;

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
            this.text = new Text(string.Empty, new Font(FontFamily.GenericSansSerif, 11f, FontStyle.Bold));
        }

        private void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var ward = sender as Obj_AI_Minion;

            if (ward == null || !sender.Name.ToLower().Contains("ward"))
            {
                return;
            }

            /*
            if (ward.IsAlly)
            {
                return;
            }*/

            switch (ward.BaseSkinName)
            {
                case "VisionWard":
                    wards.Add(new WardInfo(ward, true));
                    break;
                default:
                    wards.Add(new WardInfo(ward, false));
                    break;
            }
        }

        private void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            var ward = sender as Obj_AI_Minion;

            var wardInfo = wards.FirstOrDefault(w => w.Ward == ward);

            if (wardInfo != null)
            {
                wardInfo.Available = false;
            }
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            var removeList = new List<WardInfo>();

            foreach (var wardInfo in wards)
            {
                if (!wardInfo.Available)
                {
                    removeList.Add(wardInfo);
                    continue;
                }

                var buff = wardInfo.Ward.Buffs.FirstOrDefault();

                if (buff == null)
                {
                    removeList.Add(wardInfo);
                    continue;
                }

                var remaining = buff.EndTime - Game.Time;

                if (remaining > 0 || wardInfo.IsPink)
                {
                    int radius = this["range"].Cast<KeyBind>().CurrentValue ? 1100 : 60;
                    
                    new Circle { Color = wardInfo.Color, Radius = radius, BorderWidth = 1f }.Draw(wardInfo.Position);

                    if (this["timer"].Cast<CheckBox>().CurrentValue)
                    {
                        var location = Drawing.WorldToScreen(wardInfo.Position);

                        if (!wardInfo.IsPink)
                        {
                            this.text.Draw(
                                string.Format("{0:0}", remaining),
                                Color.White,
                                (int)location.X,
                                (int)location.Y);
                        }
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
            public WardInfo(Obj_AI_Minion ward, bool isPink)
            {
                this.Ward = ward;
                this.Color = isPink ? Color.Magenta : Color.Lime;
                this.Available = true;
                this.Position = ward.Position;
                this.IsPink = isPink;
            }

            public Obj_AI_Minion Ward { get; set; }

            public bool IsPink { get; set; }

            public Color Color { get; set; }

            public Vector3 Position { get; set; }

            public bool Available { get; set; }
        }
    }
}
