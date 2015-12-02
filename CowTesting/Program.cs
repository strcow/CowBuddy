namespace CowTesting
{
    using CowLibrary.Addons;

    public class Program
    {
        public static void Main(string[] args)
        {
            var addon = new Addon("Cowtesting")
                .Add(new Drawings());

            addon.MenuInitialized += menu =>
                {
                    menu.AddLabel("OLA BEBE");
                };
        }
    }
}
