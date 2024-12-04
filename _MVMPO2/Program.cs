using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace _MVMPO2
{
    class Program
    {
        static void Main(string[] args) 
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new App());
        }
    }
}