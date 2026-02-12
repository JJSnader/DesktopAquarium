namespace DesktopAquarium
{
    public class KillPlantEventArgs : EventArgs
    {
        public int PlantID { get; set; }
        public KillPlantEventArgs(int plantID)
        {
            PlantID = plantID;
        }
    }
}
