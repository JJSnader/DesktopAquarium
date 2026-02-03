using DesktopAquarium.Settings;
using System.Runtime;

namespace DesktopAquarium.Plants
{
    public partial class BasePlant : Form
    {
        public PointF Position { get; set; }

        private bool _isDragging;
        private Point _targetLocation;
        private Point _dragForm;
        private Point _dragCursor;

        public byte[] IdleGif { get; set; }

        public decimal Scale { get; set; }

        public BasePlant(BasePlantSettings settings)
        {
            InitializeComponent();
            Scale = settings.Scale;
            if (Scale <= 0)
                Scale = 1;
            Location = settings.Location;

            pbMain.MouseDown += MouseDown_Raised;
            pbMain.MouseUp += frmMain_MouseUp;
            pbMain.MouseMove += frmMain_MouseMove;
            MouseDown += MouseDown_Raised;
            MouseUp += frmMain_MouseUp;
            MouseMove += frmMain_MouseMove;
        }

        public void InitializeForm(int width, int height)
        {
            Width = (int)(width * Scale);
            Height = (int)(height * Scale);
            pbMain.Width = (int)(width * Scale);
            pbMain.Height = (int)(height * Scale);
            pbMain.Image = ImageHelper.LoadImageFromBytes(IdleGif);

            OnMove(null);
        }

        public void OnExit(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            var screen = Screen.FromControl(this).WorkingArea;

            int bottomY = screen.Bottom - this.Height;

            if (this.Top < bottomY)
            {
                this.Top = bottomY;
            }
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
                Position = Location;
            }
        }
    }
}
