using DesktopAquarium.Settings;
using System.Configuration;

namespace DesktopAquarium.Fish
{
    public partial class Pufferfish : BaseFish
    {
        private bool _isPuffed;

        private System.Windows.Forms.Timer _transitionTimer;
        private System.Windows.Forms.Timer _shrinkTimer;

        private const int PuffedTimerInterval = 15_000;

        public Pufferfish(PufferfishSettings settings) : base(settings)
        {
            InitializeComponent();
            _isPuffed = false;
            _transitionTimer = new System.Windows.Forms.Timer();
            var ih = new ImageHelper();
            _transitionTimer.Interval = ih.GetGifDuration(Properties.Resources.PufferPuffL) - 10;
            _transitionTimer.Tick += TransitionTimer_Tick;

            _shrinkTimer = new System.Windows.Forms.Timer();
            _shrinkTimer.Interval = PuffedTimerInterval;
            _shrinkTimer.Tick += ShrinkTimer_Tick;

            SwimLGif = Properties.Resources.PufferSwimL;
            SwimRGif = Properties.Resources.PufferSwimR;
            DragLGif = Properties.Resources.PufferDragL;
            DragRGif = Properties.Resources.PufferDragR;
            DefaultIdleLGif = Properties.Resources.PufferIdleL;
            DefaultIdleRGif = Properties.Resources.PufferIdleR;
            IdleLGifs = [];
            IdleRGifs = [];
            Icon = ImageHelper.LoadIconFromBytes(Properties.Resources.PufferIcon);

            (var width, var height) = ImageHelper.GetImageDimensions(Properties.Resources.PufferIdleL);
            InitializeForm(width, height);
        }

        private void FinishPuffTransition(bool isPuffed)
        {
            if (isPuffed)
            {
                SwimLGif = Properties.Resources.PufferPuffedSwimL;
                SwimRGif = Properties.Resources.PufferPuffedSwimR;
                DragLGif = Properties.Resources.PufferPuffedDragL;
                DragRGif = Properties.Resources.PufferPuffedDragR;
                DefaultIdleLGif = Properties.Resources.PufferPuffedIdleL;
                DefaultIdleRGif = Properties.Resources.PufferPuffedIdleR;
                IdleLGifs = [];
                IdleRGifs = [];
            }
            else
            {
                SwimLGif = Properties.Resources.PufferSwimL;
                SwimRGif = Properties.Resources.PufferSwimR;
                DragLGif = Properties.Resources.PufferDragL;
                DragRGif = Properties.Resources.PufferDragR;
                DefaultIdleLGif = Properties.Resources.PufferIdleL;
                DefaultIdleRGif = Properties.Resources.PufferIdleR;
                IdleLGifs = [];
                IdleRGifs = [];
            }

            SetIdleImage(false);
        }

        public override void MoveTimer_Elapsed(object? sender, EventArgs e)
        {
            base.MoveTimer_Elapsed(sender, e);
        }

        private void TransitionTimer_Tick(object? sender, EventArgs e)
        {
            FinishPuffTransition(_isPuffed);
            _transitionTimer.Stop();
            if (_isPuffed) 
                _shrinkTimer.Start();
            IdleTimer.Start();
        }

        private void ShrinkTimer_Tick(object? sender, EventArgs e)
        {
            if (IsDragging)
            {
                IsDragging = false;
                Position = Location;
            }
            _isPuffed = false;
            _shrinkTimer.Stop();
            MoveTimer.Stop();
            IdleTimer.Stop();
            if (IsFacingLeft)
            {
                PbMain.Image = ImageHelper.LoadImageFromBytes(Properties.Resources.PufferShrinkL);
            }
            else
            {
                PbMain.Image = ImageHelper.LoadImageFromBytes(Properties.Resources.PufferShrinkR);
            }
            _transitionTimer.Start();
        }

        public override void MouseDown_Raised(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !_isPuffed)
            {
                MoveTimer.Stop();
                IdleTimer.Stop();
                _isPuffed = true;
                if (IsFacingLeft)
                {
                    PbMain.Image = ImageHelper.LoadImageFromBytes(Properties.Resources.PufferPuffL);
                }
                else
                {
                    PbMain.Image = ImageHelper.LoadImageFromBytes(Properties.Resources.PufferPuffR);
                }
                _transitionTimer.Start();
            }
            else
                base.MouseDown_Raised(sender, e);
        }
    }
}
