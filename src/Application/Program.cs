using System;

namespace Application
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using var game = new SanctuaryGame();
            game.Run();
        }
    }
}
