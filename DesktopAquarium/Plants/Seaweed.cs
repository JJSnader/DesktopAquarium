using DesktopAquarium.Settings;

namespace DesktopAquarium.Plants
{
    public partial class Seaweed : BasePlant
    {
        public Seaweed(BasePlantSettings settings) : base(settings) 
        {
            InitializeComponent();

            IdleGif = Properties.Resources.Seaweed;

            (var width, var height) = ImageHelper.GetImageDimensions(Properties.Resources.Seaweed);
            InitializeForm(width, height);
        }
    }
}
