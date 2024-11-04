namespace DesktopAquarium
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool hideOnStart = args.Any(a => a.Contains("--silent"));

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Form mainForm = new frmMain();
            if (hideOnStart) mainForm.Shown += (s, e) => mainForm.Hide();
            Application.Run(mainForm);
        }
    }
}
