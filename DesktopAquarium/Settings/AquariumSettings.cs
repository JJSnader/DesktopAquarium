using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAquarium.Settings
{
    public class AquariumSettings
    {
        public List<BaseFishSettings> FishList { get; set; }

        public List<BasePlantSettings> PlantList { get; set; }

        public AquariumSettings()
        {
            FishList = [];
            PlantList = [];
        }
    }
}
