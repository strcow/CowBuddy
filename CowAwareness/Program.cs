﻿namespace CowAwareness
{
    using CowAwareness.Detectors;
    using CowAwareness.Drawings;
    using CowAwareness.Trackers;

    using CowLibrary.Addons;

    public class Program
    {
        public static void Main(string[] args)
        {
            var addon = new Addon("CowAwareness")
                .Add(new Clock())
                .Add(new Clone())
                .Add(new Gank())
                .Add(new Cooldown())
                .Add(new Ward())
                .Add(new WatermarkDisabler());

            addon.MenuInitialized += menu =>
                 {
                     menu.AddGroupLabel("Version");
                     menu.AddLabel("release 1.0.0");

                     menu.AddSeparator();
                     menu.AddGroupLabel("Todo List");
                     menu.AddLabel("- try to fix cooldown for some special abilities");

                     menu.AddSeparator();
                     menu.AddGroupLabel("Credits");
                     menu.AddLabel("This project comes from lots of different sources");
                     menu.AddLabel(" if you think I should credit you, message me on EB");
                     menu.AddLabel("- Lizzaran for SFXUtility: got a lots of nice things from him");
                     menu.AddLabel("- Addon by strcow from elobuddy.net");
                 };
        }
    }
}