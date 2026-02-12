using DesktopAquarium.Settings;
using System.Runtime;

namespace DesktopAquarium.Plants
{
    public partial class BasePlant : Form
    {
        private bool _isDragging;
        private int _identifyPlantCountdown;
        private Point _targetLocation;
        private Point _dragForm;
        private Point _dragCursor;
        private BasePlantSettings _settings;

        private System.Windows.Forms.Timer _identifyPlantTimer;

        public byte[] IdleGif { get; set; }


        public event EventHandler<int> LocationChanged;

        private bool _initialLoad = true;

        public decimal ScaleTiny { get; set; } = 0.2m;
        public decimal ScaleSmall { get; set; } = 0.6m;
        public decimal ScaleStandard { get; set; } = 1;
        public decimal ScaleLarge { get; set; } = 2;
        public decimal ScaleGiant { get; set; } = 3;

        public BasePlant(BasePlantSettings settings)
        {
            InitializeComponent();
            _settings = settings;
            TopMost = _settings.TopMost;
            Location = settings.Location;
            lbPlantName.Text = settings.PlantID.ToString();

            _identifyPlantTimer = new();
            _identifyPlantTimer.Interval = 1000;
            _identifyPlantTimer.Tick += IdentifyPlantTimer_Elapsed;

            pbMain.MouseDown += MouseDown_Raised;
            pbMain.MouseUp += frmMain_MouseUp;
            pbMain.MouseMove += frmMain_MouseMove;
            MouseDown += MouseDown_Raised;
            MouseUp += frmMain_MouseUp;
            MouseMove += frmMain_MouseMove;
        }

        public void InitializeForm(int width, int height)
        {
            decimal scale;
            switch (_settings.Scale)
            {
                case Enums.Scale.Tiny:
                    scale = ScaleTiny;
                    break;
                case Enums.Scale.Small:
                    scale = ScaleSmall;
                    break;
                case Enums.Scale.Large:
                    scale = ScaleLarge;
                    break;
                case Enums.Scale.Giant:
                    scale = ScaleGiant;
                    break;
                case Enums.Scale.Standard:
                default:
                    scale = 1m;
                    break;
            }

            Width = (int)(width * scale);
            Height = (int)(height * scale);
            pbMain.Width = (int)(width * scale);
            pbMain.Height = (int)(height * scale);
            pbMain.Image = ImageHelper.LoadImageFromBytes(IdleGif);

            lbPlantName.Location = new Point((Width / 2) - (lbPlantName.Width / 2), (Height / 2) - (lbPlantName.Height / 2));

            _initialLoad = false;
        }

        public void OnExit(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            if (!_initialLoad)
            {
                var screen = Screen.FromControl(this).WorkingArea;

                int bottomY = screen.Bottom - this.Height;

                if (this.Top < bottomY)
                {
                    this.Top = bottomY;
                }
            }
        }

        public virtual void IdentifyPlant_Raised(object? sender, EventArgs e)
        {
            lbPlantName.Visible = true;
            _identifyPlantCountdown = 4;
            _identifyPlantTimer.Start();
        }

        private void IdentifyPlantTimer_Elapsed(object? sender, EventArgs e)
        {
            if (_identifyPlantCountdown > 0)
            {
                _identifyPlantCountdown--;
                return;
            }

            _identifyPlantCountdown = 4;
            _identifyPlantTimer.Stop();
            lbPlantName.Visible = false;
        }

        public virtual void KillPlant_Raised(object? sender, KillPlantEventArgs e)
        {
            if (e.PlantID != _settings.PlantID)
                return;

            Close();
            Dispose();
        }

        public virtual void SettingsChanged_Raised(object? sender, PlantSettingsChangedEventArgs e)
        {
            if (e.PlantID != _settings.PlantID)
                return;

            _settings = e.NewSettings;
            lbPlantName.Text = _settings.PlantID.ToString();
        }

        public virtual void MouseDown_Raised(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
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
                _settings.Location = Location;
                LocationChanged?.Invoke(this, _settings.PlantID);
            }
        }
    }
}
