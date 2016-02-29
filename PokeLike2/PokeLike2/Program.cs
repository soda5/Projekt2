// Copyright (c) 2016 Mischa Ahi
using System;

namespace PokeLike2
{
#if WINDOWS || LINUX

    public static class Program
    {

        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
