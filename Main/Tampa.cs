using System;
using Tampa.UI;

namespace Tampa
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TampaController.GetInstance().Run();
        }
    }
}
