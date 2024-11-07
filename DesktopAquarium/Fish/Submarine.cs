using DesktopAquarium.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopAquarium.Fish
{
    public partial class Submarine : BaseFish
    {
        public Submarine(SubmarineSettings settings)
            : base(settings)
        {
            InitializeComponent();

            SwimLGif = Properties.Resources.SubSwimL;
            SwimRGif = Properties.Resources.SubSwimR;
            DragLGif = Properties.Resources.SubDragL;
            DragRGif = Properties.Resources.SubDragR;
            DefaultIdleLGif = Properties.Resources.SubIdleL;
            DefaultIdleRGif = Properties.Resources.SubIdleR;
            IdleLGifs = [];
            IdleRGifs = [];
            Icon = ImageHelper.LoadIconFromBytes(Properties.Resources.SubmarineIcon);

            (var width, var height) = ImageHelper.GetImageDimensions(Properties.Resources.SubIdleL);
            InitializeForm(width, height);
        }
    }
}
