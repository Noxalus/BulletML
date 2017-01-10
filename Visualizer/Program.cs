using System;

namespace Visualizer
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new Visualizer())
                game.Run();
        }
    }
}