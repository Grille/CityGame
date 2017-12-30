using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace CityGame
{
    static public class Program
    {

        static public MainWindow MainWindow;
        static public MenuWindow MenuWindow;

        static public MenuOverlay MenuOverlay;

        [STAThread]
        static void Main(string[] args)
        {
            MainWindow = new MainWindow();
            MenuWindow = new MenuWindow();

            MenuOverlay = new MenuOverlay();
            //Console.WriteLine();
            Application.Run(MainWindow);
                //new MainWindow().Run(60);
        }
    }
}
