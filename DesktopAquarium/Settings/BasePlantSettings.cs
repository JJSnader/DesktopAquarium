using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAquarium.Settings
{
    public class BasePlantSettings
    {
        public decimal Scale { get; set; }

        public Point Location { get; set; }

        public BasePlantSettings()
        {
            Scale = 1;
        }
    }
}
