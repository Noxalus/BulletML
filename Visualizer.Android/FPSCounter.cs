using System;
using Microsoft.Xna.Framework;

namespace Visualizer.Android
{
    public class FPSCounter
    {
        private static readonly TimeSpan OneSecondTimeSpan = TimeSpan.FromSeconds(1);
        private int _framesCounter;
        private TimeSpan _timer = OneSecondTimeSpan;

        public int FramesPerSecond { get; private set; }

        public void Update(GameTime gameTime)
        {
            _timer += gameTime.ElapsedGameTime;
            if (_timer <= OneSecondTimeSpan)
                return;

            FramesPerSecond = _framesCounter;
            _framesCounter = 0;
            _timer -= OneSecondTimeSpan;
        }

        public void Draw()
        {
            _framesCounter++;
        }
    }
}