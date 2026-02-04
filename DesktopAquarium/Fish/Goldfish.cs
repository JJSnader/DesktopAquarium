using DesktopAquarium.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopAquarium.Fish
{
    public partial class Goldfish: BaseFish
    {
        public Goldfish(GoldfishSettings settings)
            : base(settings)
        {
            InitializeComponent();

            SwimLGif = Properties.Resources.GoldfishSwimL;
            SwimRGif = Properties.Resources.GoldfishSwimR;
            DragLGif = Properties.Resources.GoldfishDragL;
            DragRGif = Properties.Resources.GoldfishDragR;
            DefaultIdleLGif = Properties.Resources.GoldfishDefaultIdleL;
            DefaultIdleRGif = Properties.Resources.GoldfishDefaultIdleR;
            IdleLGifs = [];
            IdleRGifs = [];
            Icon = ImageHelper.LoadIconFromBytes(Properties.Resources.GoldfishIcon);

            (int width, int height) = ImageHelper.GetImageDimensions(Properties.Resources.GoldfishDefaultIdleL);
            InitializeForm(width, height);
        }
    }
}
