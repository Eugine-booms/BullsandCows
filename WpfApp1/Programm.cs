using System;

using WpfApp1;

namespace BullsAndCowsWPF
{
    public static class Programm
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
