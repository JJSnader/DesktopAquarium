namespace DesktopAquarium.Settings
{
    public class BaseSettings
    {
        public int FishID { get; set; }
        public int FishMoveSpeed { get; set; }
        public int FishIdleTimeInMilliseconds { get; set; }
        public bool AlwaysOnTop { get; set; }
        public bool FollowCursor { get; set; }
        public bool PrimaryScreenOnly { get; set; }
        public FishType FishType { get; set; }
        public string? Name { get; set; }

        private const int DefaultMoveSpeed = 50;
        private const int DefaultIdleTimerInterval = 3000;

        public BaseSettings()
        {
            FishMoveSpeed = DefaultMoveSpeed;
            FishIdleTimeInMilliseconds = DefaultIdleTimerInterval;
        }
    }
}
