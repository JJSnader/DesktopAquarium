namespace DesktopAquarium
{
    public partial class ColorTabControl : TabControl
    {
        public ColorTabControl()
        {
            InitializeComponent();
        }

        public Color HeaderBackColor { get; set; } = Color.FromArgb(0, 79, 111);

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (Alignment != TabAlignment.Top)
                return;

            Rectangle headerRect = new Rectangle(
                0,
                0,
                Width,
                ItemSize.Height + 4
            );

            using var brush = new SolidBrush(HeaderBackColor);
            e.Graphics.FillRectangle(brush, headerRect);
        }
    }
}
