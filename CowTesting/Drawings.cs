namespace CowTesting
{
    using CowLibrary.Addons;

    using EloBuddy;

    public class Drawings : Feature, IToggleFeature
    {
        public override string Name
        {
            get { return "Drawings"; }
        }

        protected override void Initialize()
        {
            Chat.Print(" Drawings initialized ");
        }

        public void Disable()
        {
            Chat.Print(" Drawings disabled ");
        }

        public void Enable()
        {
            Chat.Print(" Drawings Enabled ");
        }
    }
}
