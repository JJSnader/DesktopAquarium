using DesktopAquarium.Enums;
using DesktopAquarium.Settings;
using System.Media;

namespace DesktopAquarium.Fish
{
    public partial class SpermWhale : BaseFish
    {
        SpermWhaleSettings _settings;
        System.Windows.Forms.Timer _soundTimer;
        Random _random;

        public SpermWhale(SpermWhaleSettings settings) : base(settings)
        {
            InitializeComponent();

            _settings = settings;
            _random = new Random(DateTime.Now.GetHashCode());
            _soundTimer = new System.Windows.Forms.Timer();
            switch (_settings.WhaleNoiseFrequency)
            {
                case Frequency.Rare:
                    _soundTimer.Interval = 120000;
                    break;
                case Frequency.Often:
                    _soundTimer.Interval = 20000;
                    break;
                case Frequency.Constant:
                    _soundTimer.Interval = 10000;
                    break;
                case Frequency.Normal:
                default:
                    _soundTimer.Interval = 30000;
                    break;
                    
            }
            _soundTimer.Tick += SoundTimer_Tick;

            SetImages();

            (var width, var height) = ImageHelper.GetImageDimensions(Properties.Resources.SpermWhaleIdleL);
            InitializeForm(width, height);

            if (_settings.DoWhaleNoises)
                _soundTimer.Start();
        }

        private void SetImages()
        {
            if (!string.IsNullOrEmpty(_settings.Name) && _settings.Name.ToUpper().Trim() == "MOBY DICK")
            {
                SwimLGif = Properties.Resources.MobyDickSwimL;
                SwimRGif = Properties.Resources.MobyDickSwimR;
                DragLGif = Properties.Resources.MobyDickDragL;
                DragRGif = Properties.Resources.MobyDickDragR;
                DefaultIdleLGif = Properties.Resources.MobyDickIdleL;
                DefaultIdleRGif = Properties.Resources.MobyDickIdleR;
                IdleLGifs = [];
                IdleRGifs = [];
            }
            else 
            { 
                SwimLGif = Properties.Resources.SpermWhaleSwimL;
                SwimRGif = Properties.Resources.SpermWhaleSwimR;
                DragLGif = Properties.Resources.SpermWhaleDragL;
                DragRGif = Properties.Resources.SpermWhaleDragR;
                DefaultIdleLGif = Properties.Resources.SpermWhaleIdleL;
                DefaultIdleRGif = Properties.Resources.SpermWhaleIdleR;
                IdleLGifs = [Properties.Resources.SpermWhaleIdle1L];
                IdleRGifs = [Properties.Resources.SpermWhaleIdle1R];
            }

            
        }

        public override void SettingsChanged_Raised(object? sender, SettingsChangedEventArgs e)
        {
            base.SettingsChanged_Raised(sender, e);

            SetImages();

            if (!_settings.DoWhaleNoises)
                _soundTimer.Stop();
            else if (!_soundTimer.Enabled)
                _soundTimer.Start();
        }

        private void SoundTimer_Tick(object? sender, EventArgs e)
        {
            var r = _random.Next(0, 10);

            byte[] sound;

            if (r == 0)
            {
                sound = Properties.Resources.whale_goofy;
            }
            else if (r < 4)
            {
                sound = Properties.Resources.whale2;
            }
            else if (r < 7)
            {
                sound = Properties.Resources.whale3;
            }
            else
            {
                sound = Properties.Resources.whale1;
            }

            if (r != 10)
            {
                var player = new SoundPlayer(new MemoryStream(sound));
                player.Play();
            }
        }
    }
}
