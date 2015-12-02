namespace CowAwareness.Drawings
{
    using CowLibrary.Addons;

    public class WatermarkDisabler : Feature, IToggleFeature
    {
        public override string Name
        {
            get { return "Disable EB Watermark"; }
        }

        public void Enable()
        {
            EloBuddy.Hacks.RenderWatermark = false;
        }

        public void Disable()
        {
            EloBuddy.Hacks.RenderWatermark = true;
        }

        protected override void Initialize()
        {
        }
    }
}
