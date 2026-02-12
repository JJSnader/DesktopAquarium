using DesktopAquarium.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAquarium
{
    public class FishSettingsChangedEventArgs : EventArgs
    {
        public BaseFishSettings NewSettings { get; set; }
        public int FishID { get; set; }

        public FishSettingsChangedEventArgs(BaseFishSettings newSettings, int fishID)
        {
            NewSettings = newSettings;
            FishID = fishID;
        }
    }
}
