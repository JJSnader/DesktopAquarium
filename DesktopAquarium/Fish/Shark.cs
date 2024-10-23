using DesktopAquarium.Settings;
using System.Media;

namespace DesktopAquarium.Fish
{
    public partial class Shark : BaseFish
    {
        private bool _isChasing;
        private DateTime _chaseStartTime;
        private SoundPlayer? _player;
        private SharkSettings _settings;
        private ImageHelper _imageHelper;
        private bool _defaultFollowCursorSetting;

        private const int ChaseDuration = 15;

        public Shark(SharkSettings baseSettings)
            : base(baseSettings)
        {
            _settings = baseSettings;
            SwimLGif = Properties.Resources.SharkSwimL;
            SwimRGif = Properties.Resources.SharkSwimR;
            DragLGif = Properties.Resources.SharkDragL;
            DragRGif = Properties.Resources.SharkDragR;
            DefaultIdleLGif = Properties.Resources.SharkDefaultIdleL;
            DefaultIdleRGif = Properties.Resources.SharkDefaultIdleR;
            IdleLGifs = [Properties.Resources.SharkIdle1L];
            IdleRGifs = [Properties.Resources.SharkIdle1R];
            Icon = ImageHelper.LoadIconFromBytes(Properties.Resources.SharkIcon);

            _defaultFollowCursorSetting = _settings.FollowCursor;
            _imageHelper = new ImageHelper();

            (var width, var height) = ImageHelper.GetImageDimensions(Properties.Resources.SharkIdle1L);
            SetFormDimensions(width, height);
        }

        public override void KillFish_Raised(object? sender, KillFishEventArgs e)
        { 
            if (e.FishID != _settings.FishID)
                return;

            _player?.Stop();
            base.KillFish_Raised(sender, e);
        }

        public override void SettingsChanged_Raised(object? sender, SettingsChangedEventArgs e)
        {
            if (e.FishID != _settings.FishID)
                return;
                
            base.SettingsChanged_Raised(sender, e);
            if (e.NewSettings.GetType() == typeof(SharkSettings))
            {
                _settings = (SharkSettings)e.NewSettings;
                _defaultFollowCursorSetting = _settings.FollowCursor;
                if (!_settings.CursorChompEnabled)
                {
                    _isChasing = false;
                    _player?.Stop();
                }
            }
        }

        public override void MoveTimer_Elapsed(object? sender, EventArgs e)
        {
            if (!_settings.CursorChompEnabled)
            {
                base.MoveTimer_Elapsed(sender, e); 
                return;
            }

            Cursor.Show();
            var doBaseMove = true;
            // 1 in 500 chance that a chase starts on next move
            // This sounds like a tiny chance but the move timer ticks A LOT
            var chance = Rand.Next(0, 500);
            if (chance == 17 && !_isChasing)
            {
                _isChasing = true;
                _chaseStartTime = DateTime.Now;
                _settings.FollowCursor = true;
                IdleTimer.Stop();
                IdleGifStopTimer.Stop();
                TargetLocation = Cursor.Position;
                if (IsFacingLeft)
                    PbMain.Image = ImageHelper.LoadImageFromBytes(Properties.Resources.SharkChaseL);
                else
                    PbMain.Image = ImageHelper.LoadImageFromBytes(Properties.Resources.SharkChaseR);
                if (_settings.PlayChaseSound)
                {
                    _player = new SoundPlayer(new MemoryStream(Properties.Resources.SharkChase));
                    _player.PlayLooping();
                }
                base.MoveTimer_Elapsed(sender, e);
            }
            else
            {
                if (_isChasing)
                {
                    TargetLocation = Cursor.Position;
                    var formCenter = FormCenter;
                    int deltaX = TargetLocation.X - formCenter.X;
                    int deltaY = TargetLocation.Y - formCenter.Y;
                    TimeSpan chaseTime = DateTime.Now - _chaseStartTime;
                    if ((Math.Abs(deltaX) < 5 && Math.Abs(deltaY) < 5) || chaseTime.Seconds >= ChaseDuration)
                    {
                        _player?.Stop();
                        MoveTimer.Stop();
                        _isChasing = false;
                        _settings.FollowCursor = _defaultFollowCursorSetting;
                        doBaseMove = false;

                        if (chaseTime.Seconds < ChaseDuration)
                        {
                            Cursor.Position = FormCenter;
                            Cursor.Hide();
                            _player = new SoundPlayer(new MemoryStream(Properties.Resources.SharkChaseEnd));
                            _player.Play();
                            var gifLength = _imageHelper.GetGifDuration(Properties.Resources.SharkIdle1L);
                            IdleGifStopTimer.Interval = gifLength;
                            if (IsFacingLeft)
                                PbMain.Image = ImageHelper.LoadImageFromBytes(Properties.Resources.SharkIdle1L);
                            else
                                PbMain.Image = ImageHelper.LoadImageFromBytes(Properties.Resources.SharkIdle1R);
                            IdleGifStopTimer.Start();
                        }
                        else
                        {
                            IdleTimer.Start();
                            SetIdleImage(false);
                        }
                    }
                    else if (TargetLocation.X > formCenter.X && IsFacingLeft)
                    {
                        IsFacingLeft = false;
                        PbMain.Image = ImageHelper.LoadImageFromBytes(Properties.Resources.SharkChaseR);
                    }
                    else if (TargetLocation.X < formCenter.X && !IsFacingLeft)
                    {
                        IsFacingLeft = true;
                        PbMain.Image = ImageHelper.LoadImageFromBytes(Properties.Resources.SharkChaseL);
                    }                    
                }
                if (doBaseMove)
                    base.MoveTimer_Elapsed(sender, e);
            }
        }
    }
}
