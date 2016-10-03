using BulletML;
using System;
using System.IO;
using System.Reflection;

namespace Tests.Utils
{
    public static class TestUtils
    {
        public static MoverManager Manager;
        public static Player Player;
        public static BulletPattern Pattern;

        public static void Initialize()
        {
            Player = new Player();
            Manager = new MoverManager(Player.GetPosition);
            Pattern = new BulletPattern();
        }

        public static string GetFilePath(string file)
        {
            var dllPathUri = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var projectPathUri = new Uri(dllPathUri, "../..");
            var projectPath = projectPathUri.LocalPath;

            return Path.Combine(projectPath, file);
        }
    }
}