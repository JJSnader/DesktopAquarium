namespace DesktopAquarium.Settings
{
    public class SharkSettings : BaseSettings
    {
        public bool CursorChompEnabled { get; set; }
        public bool PlayChaseSound { get; set; }

        public SharkSettings()
        {
            FishMoveSpeed = 60;
        }
    }
}
