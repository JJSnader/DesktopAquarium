using DesktopAquarium.Settings;

namespace DesktopAquarium.Fish
{
    internal class Jellyfish : BaseFish
    {
        public Jellyfish(JellyfishSettings settings) : base(settings)
        {
            SwimLGif = Properties.Resources.JellyfishIdle;
            SwimRGif = Properties.Resources.JellyfishIdle;
            DragLGif = Properties.Resources.JellyfishDrag;
            DragRGif = Properties.Resources.JellyfishDrag;
            DefaultIdleLGif = Properties.Resources.JellyfishIdle;
            DefaultIdleRGif = Properties.Resources.JellyfishIdle;
            IdleLGifs = [];
            IdleRGifs = [];
            Icon = ImageHelper.LoadIconFromBytes(Properties.Resources.JellyfishIcon);

            (var width, var height) = ImageHelper.GetImageDimensions(Properties.Resources.JellyfishIdle);
            InitializeForm(width, height);
        }
    }
}
