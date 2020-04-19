using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gameEngine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run(); 
            }
        }
    }
}
