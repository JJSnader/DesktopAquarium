using System.Drawing.Imaging;

using DesktopAquarium.Settings;

namespace DesktopAquarium.Fish
{
    public partial class BaseFish : Form
    {
        private bool _isFacingLeft;
        private bool _isDragging;
        private int _identifyFishCountdown;

        private Point _targetLocation;
        private Point _dragForm;
        private Point _dragCursor;

        private BaseSettings _settings;
        private ImageHelper _imageHelper;
        private MemoryStream? _memoryStream;
        private Random _rand;

        private System.Windows.Forms.Timer _moveTimer;
        private System.Windows.Forms.Timer _idleTimer;
        private System.Windows.Forms.Timer _idleGifStopTimer;
        private System.Windows.Forms.Timer _identifyFishTimer;

        public PictureBox PbMain
        {
            get => pbMain;
        }

        public Random Rand
        {
            get => _rand;
        }

        public bool IsFacingLeft
        {
            get => _isFacingLeft;
            set => _isFacingLeft = value;
        }

        public Point TargetLocation
        {
            get => _targetLocation;
            set => _targetLocation = value;
        }

        public System.Windows.Forms.Timer MoveTimer
        {
            get => _moveTimer;
            set => _moveTimer = value;
        }

        public System.Windows.Forms.Timer IdleTimer
        {
            get => _idleTimer;
            set => _idleTimer = value;
        }

        public System.Windows.Forms.Timer IdleGifStopTimer
        {
            get => _idleGifStopTimer;
            set => _idleGifStopTimer = value;
        }

        public Point FormCenter
        {
            get => new Point(Left + Width / 2, Top + Height / 2);
        }

        public byte[] SwimLGif { get; set; }
        public byte[] SwimRGif { get; set; }

        public byte[] DragLGif { get; set; }
        public byte[] DragRGif { get; set; }

        public byte[] DefaultIdleLGif { get; set; }
        public byte[] DefaultIdleRGif { get; set; }

        public List<byte[]> IdleLGifs { get; set; }
        public List<byte[]> IdleRGifs { get; set; }

        public BaseFish(BaseSettings settings)
        {
            InitializeComponent();

            SwimLGif = [];
            SwimRGif = [];
            DragLGif = [];
            DragRGif = [];
            DefaultIdleLGif = [];
            DefaultIdleRGif = [];
            IdleLGifs = [];
            IdleRGifs = [];

            ShowInTaskbar = false;

            _settings = settings;
            _imageHelper = new ImageHelper();
            _rand = new Random(DateTime.Now.GetHashCode());

            _moveTimer = new System.Windows.Forms.Timer();
            _moveTimer.Tick += MoveTimer_Elapsed;

            _idleTimer = new System.Windows.Forms.Timer();
            _idleTimer.Tick += IdleTimer_Elapsed;

            _idleGifStopTimer = new System.Windows.Forms.Timer();
            _idleGifStopTimer.Tick += IdleGifStopTimer_Elapsed;

            _identifyFishTimer = new System.Windows.Forms.Timer();
            _identifyFishTimer.Interval = 1000;
            _identifyFishTimer.Tick += IdentifyFishTimer_Elapsed;

            LoadSettings();

            if (_settings.FollowCursor)
                _moveTimer.Start();
            else 
                _idleTimer.Start();


            lbFishName.Text = _settings.Name ?? "[No name]";

            pbMain.MouseDown += frmMain_MouseDown;
            pbMain.MouseUp += frmMain_MouseUp;
            pbMain.MouseMove += frmMain_MouseMove;
            MouseDown += frmMain_MouseDown;
            MouseUp += frmMain_MouseUp;
            MouseMove += frmMain_MouseMove;
        }

        /// <summary>
        /// This must always be called from fish implementing this class.
        /// This sets the width and height to the proper dimensions 
        /// (they should match the dimensions of the fish GIFs),
        /// and sets the inital GIF.
        /// </summary>
        /// <param name="width">The width of the form.</param>
        /// <param name="height">The height of the form.</param>
        public void InitializeForm(int width, int height)
        {
            Width = width;
            Height = height;
            lbFishName.Location = new Point((Width / 2) - (lbFishName.Width / 2), (Height / 2) - (lbFishName.Height / 2));
            pbMain.Width = width;
            pbMain.Height = height;
            _isFacingLeft = true;
            if ( _settings.FollowCursor )
            {
                pbMain.Image = ImageHelper.LoadImageFromBytes(SwimLGif);
            }
            else
            {
                SetIdleImage(false);
            }
        }

        public void LoadSettings()
        {
            Text = _settings.Name ?? _settings.FishType.ToString();
            _moveTimer.Interval = (int)(_settings.FishMoveInterval / 2.5);
            _idleTimer.Interval = Math.Max((int)(_settings.FishIdleTimeInMilliseconds / 2.5), 1);
            TopMost = _settings.AlwaysOnTop;
        }

        private Rectangle GetDestinationScreen()
        {
            var screens = Screen.AllScreens;

            Screen currentScreen = Screen.FromControl(this);

            var destScreen = _rand.Next(screens.Length + (screens.Length / 2));
            // ~33% chance to stay on the same screen
            if (destScreen < screens.Length)
            {
                return screens[destScreen].WorkingArea;
            }
            else
            {
                return currentScreen.WorkingArea;
            }
        }

        #region Public Methods

        public void MoveToRandomLocation()
        {
            int newX, newY;

            Rectangle screen;
            if (_settings.PrimaryScreenOnly)
                screen = Screen.PrimaryScreen?.Bounds ?? SystemInformation.VirtualScreen;
            else
                screen = GetDestinationScreen(); 
            do
            {
                newX = _rand.Next(screen.Left, screen.Right - Width);
                newY = _rand.Next(screen.Top, screen.Bottom - Height);
            }
            while ((Math.Abs(newX - Location.X) < 100 || Math.Abs(newY - Location.Y) < 100)
            && (Math.Abs(newX - Location.X) > 200 || Math.Abs(newY - Location.Y) > 200));

            _targetLocation = new Point(newX, newY);
            if (newX > Location.X)
            {
                if (_isFacingLeft)
                    _isFacingLeft = false;
                pbMain.Image = ImageHelper.LoadImageFromBytes(SwimRGif);
            }
            else
            {
                if (!_isFacingLeft)
                    _isFacingLeft = true;
                pbMain.Image = ImageHelper.LoadImageFromBytes(SwimLGif);
            }

            _idleTimer.Stop();
            _moveTimer.Start();
        }

        public void SetIdleImage(bool onlyDefault)
        {
            if (_settings.FollowCursor)
            {
                if (IsFacingLeft)
                {
                    pbMain.Image = ImageHelper.LoadImageFromBytes(SwimLGif);
                }
                else
                {
                    pbMain.Image = ImageHelper.LoadImageFromBytes(SwimRGif);
                }
                return;
            }
            var defaultIdle = DefaultIdleLGif;
            var idleList = IdleLGifs;
            if (!_isFacingLeft)
            {
                defaultIdle = DefaultIdleRGif;
                idleList = IdleRGifs;
            }

            if (onlyDefault || idleList.Count == 0)
            {
                pbMain.Image = ImageHelper.LoadImageFromBytes(defaultIdle);
            }
            else
            {
                // don't let this run if there are no other idle animations
                // or else a divide by zero will happen
                var defaultOrSpecial = _rand.Next(0, 2);
                if (defaultOrSpecial == 0)
                    pbMain.Image = ImageHelper.LoadImageFromBytes(defaultIdle);
                else
                {
                    var chance = 100 / idleList.Count;
                    var img = _rand.Next(0, 100);
                    for (int i = 0; i < idleList.Count; ++i)
                    {
                        if (img <= chance * (i + 1))
                        {
                            var gifLength = _imageHelper.GetGifDuration(idleList[i]);
                            _idleTimer.Stop();
                            _idleGifStopTimer.Interval = gifLength;
                            pbMain.Image = ImageHelper.LoadImageFromBytes(idleList[i]);
                            _idleGifStopTimer.Start();
                            break;
                        }
                    }
                }
            }
        }

        #endregion
        #region Timers

        public virtual void IdleTimer_Elapsed(object? sender, EventArgs e)
        {
            MoveToRandomLocation();
        }

        public virtual void MoveTimer_Elapsed(object? sender, EventArgs e)
        {
            var formCenter = FormCenter;
            if (_settings.FollowCursor)
            {
                _targetLocation = Cursor.Position;
                if (_targetLocation.X > formCenter.X && IsFacingLeft)
                {
                    IsFacingLeft = false;
                    PbMain.Image = ImageHelper.LoadImageFromBytes(SwimRGif);
                }
                else if (TargetLocation.X < formCenter.X && !IsFacingLeft)
                {
                    IsFacingLeft = true;
                    PbMain.Image = ImageHelper.LoadImageFromBytes(SwimLGif);
                }
            }
            int deltaX = _targetLocation.X - formCenter.X;
            int deltaY = _targetLocation.Y - formCenter.Y;

            // Move the form 2 pixels closer to the target
            if (Math.Abs(deltaX) > 2 || Math.Abs(deltaY) > 2)
            {
                int moveX = Math.Sign(deltaX) * Math.Min(2, Math.Abs(deltaX));
                int moveY = Math.Sign(deltaY) * Math.Min(2, Math.Abs(deltaY));

                Location = new Point(Location.X + moveX, Location.Y + moveY);
            }
            else
            {
                if (!_settings.FollowCursor)
                {
                    _moveTimer.Stop();
                    _idleTimer.Start();
                    SetIdleImage(false);
                }
            }
        }

        private void IdleGifStopTimer_Elapsed(object? sender, EventArgs e)
        {
            SetIdleImage(true);

            _idleGifStopTimer.Stop();
            if (_idleGifStopTimer.Interval <= _settings.FishIdleTimeInMilliseconds)
            {
                _idleTimer.Interval = _settings.FishIdleTimeInMilliseconds - _idleGifStopTimer.Interval;
                _idleTimer.Start();
            }
            else
            {
                if (!_settings.FollowCursor)
                    MoveToRandomLocation();
                else
                    _moveTimer.Start();
            }
        }

        private void IdentifyFishTimer_Elapsed(object? sender, EventArgs e)
        {
            if (_identifyFishCountdown > 0)
            {
                _identifyFishCountdown--;
                return;
            }

            _identifyFishCountdown = 4;
            _identifyFishTimer.Stop();
            lbFishName.Visible = false;
        }

        #endregion
        #region Events

        public virtual void IdentifyFish_Raised(object? sender, EventArgs e)
        {
            lbFishName.Visible = true;
            _identifyFishCountdown = 4;
            _identifyFishTimer.Start();
        }

        public virtual void KillFish_Raised(object? sender, KillFishEventArgs e)
        {
            if (e.FishID != _settings.FishID)
                return;

            _moveTimer.Stop();
            _idleGifStopTimer.Stop();
            _idleTimer.Stop();
            Close();
            Dispose();
        }

        public virtual void SettingsChanged_Raised(object? sender, SettingsChangedEventArgs e)
        {
            if (e.FishID != _settings.FishID)
                return;

            _settings = e.NewSettings;
            lbFishName.Text = _settings.Name ?? "[No name]";
            LoadSettings();
        }

        private void frmMain_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _moveTimer.Stop();
                _idleTimer.Stop();
                if (_isFacingLeft)
                    pbMain.Image = ImageHelper.LoadImageFromBytes(DragLGif);
                else
                    pbMain.Image = ImageHelper.LoadImageFromBytes(DragRGif);
                
                _isDragging = true;
                _dragCursor = Cursor.Position;
                _dragForm = Location;
            }
        }

        private void frmMain_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(_dragCursor));
                Location = Point.Add(_dragForm, new Size(dif));
            }
        }

        private void frmMain_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = false;
                SetIdleImage(false);
                if (_settings?.FollowCursor ?? false)
                    _moveTimer.Start();
                else
                    _idleTimer.Start();
            }
        }

        #endregion
    }
}
