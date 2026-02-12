using DesktopAquarium.Settings;

namespace DesktopAquarium
{
    public class PlantSettingsChangedEventArgs : EventArgs
    {
        public BasePlantSettings NewSettings { get; set; }
        public int PlantID { get; set; }

        public PlantSettingsChangedEventArgs(BasePlantSettings newSettings, int fishID)
        {
            NewSettings = newSettings;
            PlantID = fishID;
        }
    }
}
