namespace CowAwareness.Trackers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using CowLibrary.Addons;

    using EloBuddy;
    using EloBuddy.SDK.Rendering;

    using Color = System.Drawing.Color;
    using Font = System.Drawing.Font;

    public class Teleport : Feature, IToggleFeature
    {
        private readonly HashSet<TeleportInfo> teleports = new HashSet<TeleportInfo>();
        private Text text;

        public override string Name
        {
            get
            {
                return "Recall Tracker";
            }
        }

        public void Enable()
        {
            Obj_AI_Base.OnTeleport += this.OnTeleport;
            Drawing.OnEndScene += this.Drawing_OnEndScene;
        }

        public void Disable()
        {
            Obj_AI_Base.OnTeleport += this.OnTeleport;
        }

        protected override void Initialize()
        {
            this.Menu.AddLabel("Tracks recalls and teleports");
            this.text = new Text(string.Empty, new Font(FontFamily.GenericSansSerif, 10f, FontStyle.Bold));
        }

        private void OnTeleport(Obj_AI_Base sender, GameObjectTeleportEventArgs args)
        {
            var hero = sender as AIHeroClient;

            if (hero == null)
            {
                return;
            }

            if (args.RecallName != string.Empty)
            {
                this.teleports.Add(new TeleportInfo(hero, Game.Time + GetRecallTime(args.RecallName)));
                return;
            }

            foreach (var tp in this.teleports)
            {
                if (Math.Abs(Game.Time - tp.EndTime) < 0.02)
                {
                    tp.Finished = true;
                }
                else
                {
                    tp.Aborted = true;
                }

                System.Threading.Timer timer = null;
                timer = new System.Threading.Timer(cb => 
                {
                    this.teleports.Remove(tp);
                    if (timer != null)
                    {
                        timer.Dispose();
                    }
                },
                null, 
                3000, 
                System.Threading.Timeout.Infinite);
            }
        }

        private void Drawing_OnEndScene(EventArgs args)
        {
            int y = 400;
            foreach (var tp in this.teleports)
            {
                var remaining = tp.EndTime - Game.Time;

                if (tp.Finished)
                {
                    this.text.Draw(string.Format("{0} finished recalling", tp.Hero.ChampionName), Color.Lime, 250, y);
                }
                else if (tp.Aborted)
                {
                    this.text.Draw(string.Format("{0} aborted recalling", tp.Hero.ChampionName), Color.Red, 250, y);
                }
                else
                {
                    this.text.Draw(string.Format("{0} recalling {1:0.00} ({2}%)", tp.Hero.ChampionName, remaining, tp.Hero.HealthPercent), Color.White, 250, y);
                }
                
                y += 20;
            }
        }

        public static int GetRecallTime(string recallName)
        {
            switch (recallName.ToLower())
            {
                case "odinrecall":
                    return 4500;
                case "odinrecallimproved":
                    return 4000;
                case "recall":
                    return 8000;
                case "recallimproved":
                    return 7000;
                case "superrecallimproved":
                    return 4000;
                case "superrecall":
                    return 4000;
                default:
                    return 4500;
            }
        }

        private class TeleportInfo
        {
            public TeleportInfo(AIHeroClient hero, float endTime)
            {
                this.Hero = hero;
                this.EndTime = endTime;
                this.Aborted = false;
                this.Finished = false;
            }

            public AIHeroClient Hero { get; private set; }

            public float EndTime { get; private set; }

            public bool Aborted { get; set; }

            public bool Finished { get; set; }
        }
    }
}
