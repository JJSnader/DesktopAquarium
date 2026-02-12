using DesktopAquarium.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAquarium.Settings
{
    public class BasePlantSettings
    {
        public int PlantID { get; set; }

        public bool TopMost { get; set; } = false;

        public PlantType PlantType { get; set; }

        public Scale Scale { get; set; }

        public Point Location { get; set; }

        public BasePlantSettings()
        {
            Scale = Scale.Standard;
        }
    }
}
