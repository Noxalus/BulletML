using BulletML;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

#if ANDROID
using Visualizer_Android;
#endif

namespace Visualizer_Core
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    internal sealed class Visualizer : Game
    {
        public static GraphicsDeviceManager Graphics;
        private SpriteBatch _spriteBatch;

        private readonly FPSCounter _fpsCounter = new FPSCounter();

        private Texture2D _playerTexture;
        private SpriteFont _mainFont;

        private Texture2D _pixel;

        private Camera2D _camera;

        private Player _player;
        private MoverManager _moverManager;
        private Vector2 _moverManagerPosition;
        private Mover _mover;

        private float _rank = 0.5f;
        private bool _pause = false;

        private readonly List<BulletPattern> _patterns = new List<BulletPattern>();
        private readonly List<string> _patternNames = new List<string>();
        private readonly List<String> _currentPatternErrors = new List<string>();
        private int _currentPattern;
        private readonly List<FileInfo> _patternFileInfos = new List<FileInfo>();

        private FileSystemWatcher _watcher;
        private KeyboardState _previousKeyboardState;

        // Performance
        private Stopwatch _stopWatch;
        private TimeSpan _updateTime;
        private TimeSpan _drawTime;

#if ANDROID
        private VisualizerActivity _activity;

        public Visualizer(VisualizerActivity activity)
        {
            _activity = activity;
#else
        public Visualizer()
        {
            // Allow window resizing
            Window.AllowUserResizing = true;
#endif

            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
#if !ANDROID
            _previousKeyboardState = Keyboard.GetState();
#endif

            _player = new Player();
            _moverManager = new MoverManager(_player.GetPosition);
            _camera = new Camera2D(GraphicsDevice.Viewport);

            _stopWatch = new Stopwatch();
            _stopWatch.Start();

            _player.Initialize();

            _moverManagerPosition = new Vector2(Config.GameAeraSize.X / 2f, Config.GameAeraSize.Y / 2f);

            base.Initialize();
        }

        private float GetRank()
        {
            return _rank;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _pixel = new Texture2D(Graphics.GraphicsDevice, 1, 1) { Name = "pixel" };
            _pixel.SetData(new[] { Color.White });

            _playerTexture = Content.Load<Texture2D>("Sprites\\bullet1");
            var bulletTextures = new List<Texture2D>() {
                Content.Load<Texture2D>("Sprites\\bullet1"),
                Content.Load<Texture2D>("Sprites\\bullet2"),
                Content.Load<Texture2D>("Sprites\\bullet3"),
                Content.Load<Texture2D>("Sprites\\bullet4"),
                Content.Load<Texture2D>("Sprites\\bullet5"),
                Content.Load<Texture2D>("Sprites\\bullet6"),
                Content.Load<Texture2D>("Sprites\\bullet7")
            };

            // Set the list of usable textures to spawn bullets with
            _moverManager.BulletTextures = bulletTextures;

            _mainFont = Content.Load<SpriteFont>("Fonts\\main");

            // Get all the xml files in the samples directory
            var index = 0;

#if ANDROID
            foreach (var source in _activity.ListFile(@"Patterns"))
            {
                var filename = @"Patterns/" + source;
#else
            foreach (var filename in Directory.GetFiles("Assets\\Patterns", "*.xml"))
            {
#endif
                // Store the name
                _patternNames.Add(filename);

                _patternFileInfos.Add(new FileInfo(filename));

                // Load the pattern
                var pattern = new BulletPattern();
                _currentPatternErrors.Add(null);
                _patterns.Add(pattern);

                try
                {
#if ANDROID
                    var stream = _activity.ApplicationContext.Assets.Open(filename);
                    pattern.ParseStream(filename, stream);
#else
                    pattern.Parse(filename);
#endif
                }
                catch (Exception ex)
                {
                    _currentPatternErrors[index] = ex.Message;
                }

                index++;
            }

            GameManager.GameDifficulty = GetRank;
            AddBullet(true);
        }

        private void LoadPatternFile()
        {
            var pattern = new BulletPattern();

            try
            {
                pattern.Parse(_patternFileInfos[_currentPattern].FullName);
                _patterns[_currentPattern] = pattern;
                _currentPatternErrors[_currentPattern] = null;
            }
            catch (Exception ex)
            {
                _currentPatternErrors.Insert(_currentPattern, ex.Message);
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            _fpsCounter.Update(gameTime);

            _stopWatch.Reset();
            _stopWatch.Start();

            HandleInput(gameTime);

            if (!_pause)
                _moverManager.Update();

            _player.Update(gameTime);

            _updateTime = _stopWatch.Elapsed;

            if (_mover != null)
            {
                _mover.Position = _moverManagerPosition;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _stopWatch.Reset();
            _stopWatch.Start();

            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(), blendState: BlendState.AlphaBlend);

            _spriteBatch.Draw(_pixel, new Rectangle(0, 0, (int)Config.GameAeraSize.X, (int)Config.GameAeraSize.Y), Color.CornflowerBlue);

            foreach (var mover in _moverManager.Movers)
            {
                _spriteBatch.Draw(
                    mover.Texture,
                    mover.Position, null,
                    mover.Color,
                    mover.Direction,
                    new Vector2(mover.Texture.Width / 2f, mover.Texture.Height / 2f), mover.Scale, SpriteEffects.None, 0f
                );
            }

            _spriteBatch.Draw(
                _playerTexture,
                _moverManagerPosition, null,
                Color.White, 0f,
                new Vector2(_playerTexture.Width / 2f, _playerTexture.Height / 2f),
                new Vector2(1f), SpriteEffects.None, 0f
            );

            _spriteBatch.Draw(
                _playerTexture,
                _player.Position, null,
                Color.Red, 0f,
                new Vector2(_playerTexture.Width / 2f, _playerTexture.Height / 2f),
                new Vector2(1f), SpriteEffects.None, 0f
            );

            _spriteBatch.End();

            _drawTime = _stopWatch.Elapsed;

            DrawStrings(gameTime);

            base.Draw(gameTime);
        }

        private void DrawStrings(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _fpsCounter.Draw(gameTime);
            _spriteBatch.DrawString(_mainFont, $"FPS: {_fpsCounter.FramesPerSecond}", Vector2.Zero, Color.White);
            _spriteBatch.DrawString(_mainFont, $"Pattern: {_patternNames[_currentPattern]}", new Vector2(0, 20f), Color.White);
            _spriteBatch.DrawString(_mainFont, $"Update time: {_updateTime.TotalMilliseconds} ms", new Vector2(0, 40f), Color.White);
            _spriteBatch.DrawString(_mainFont, $"Draw time: { _drawTime.TotalMilliseconds } ms", new Vector2(0, 60f), Color.White);
            _spriteBatch.DrawString(_mainFont, $"Bullets: { _moverManager.Movers.Count }", new Vector2(0, 80f), Color.White);

            if (!string.IsNullOrEmpty(_currentPatternErrors[_currentPattern]))
            {
                var error = WrapString(_currentPatternErrors[_currentPattern], Graphics.PreferredBackBufferWidth, _mainFont);

                _spriteBatch.DrawString(_mainFont, error,
                    new Vector2(0, Graphics.PreferredBackBufferHeight - _mainFont.MeasureString(error).Y), Color.Red
                );
            }

            _spriteBatch.End();
        }

        private void HandleTouchInput(GameTime gameTime)
        {
            var touchState = TouchPanel.GetState();

            foreach (var touch in touchState)
            {
                var normalizedTouchPosition = new Vector2(
                    touch.Position.X / Graphics.GraphicsDevice.Viewport.Width,
                    touch.Position.Y / Graphics.GraphicsDevice.Viewport.Height
                );

                if (touch.State == TouchLocationState.Pressed)
                {
                    if (normalizedTouchPosition.X < 0.25f)
                        PreviousPattern();
                    else if (normalizedTouchPosition.X > 0.75f)
                        NextPattern();
                    else
                        AddBullet();
                }
                else
                {
                    if (normalizedTouchPosition.Y < 0.25f)
                        _moverManager.Clear();
                    else if (normalizedTouchPosition.Y > 0.75f)
                        AddBullet(false);
                }
            }
        }

        private void HandleInput(GameTime gameTime)
        {
#if ANDROID
            HandleTouchInput(gameTime);
#else
            HandleKeyboardInput(gameTime);
#endif
        }

        private void HandleKeyboardInput(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.P) && _previousKeyboardState.IsKeyUp(Keys.P))
                _pause = !_pause;

            if (keyboardState.IsKeyDown(Keys.PageUp) && _previousKeyboardState.IsKeyUp(Keys.PageUp))
            {
                NextPattern();
            }
            else if (keyboardState.IsKeyDown(Keys.PageDown) && _previousKeyboardState.IsKeyUp(Keys.PageDown))
            {
                PreviousPattern();
            }

            if (keyboardState.IsKeyDown(Keys.LeftControl) && _previousKeyboardState.IsKeyUp(Keys.LeftControl))
                AddBullet();

            if (keyboardState.IsKeyDown(Keys.Space))
                AddBullet();

            if (keyboardState.IsKeyDown(Keys.Delete))
                _moverManager.Clear();

            if (keyboardState.IsKeyDown(Keys.E) && _previousKeyboardState.IsKeyUp(Keys.E))
                EditCurrentPatternFile();

            // Mover manager position

            if (keyboardState.IsKeyDown(Keys.I))
                _moverManagerPosition -= new Vector2(0, 250) * dt;

            if (keyboardState.IsKeyDown(Keys.K))
                _moverManagerPosition += new Vector2(0, 250) * dt;

            if (keyboardState.IsKeyDown(Keys.J))
                _moverManagerPosition -= new Vector2(250, 0) * dt;

            if (keyboardState.IsKeyDown(Keys.L))
                _moverManagerPosition += new Vector2(250, 0) * dt;

            // Camera
            if (keyboardState.IsKeyDown(Keys.NumPad7))
                _camera.Zoom -= dt * 0.5f;

            if (keyboardState.IsKeyDown(Keys.NumPad9))
                _camera.Zoom += dt * 0.5f;

            _camera.Zoom = MathHelper.Clamp(_camera.Zoom, 0.1f, 10f);

            if (keyboardState.IsKeyDown(Keys.NumPad8))
                _camera.Position -= new Vector2(0, 250) * dt;

            if (keyboardState.IsKeyDown(Keys.NumPad5))
                _camera.Position += new Vector2(0, 250) * dt;

            if (keyboardState.IsKeyDown(Keys.NumPad4))
                _camera.Position -= new Vector2(250, 0) * dt;

            if (keyboardState.IsKeyDown(Keys.NumPad6))
                _camera.Position += new Vector2(250, 0) * dt;

            if (keyboardState.IsKeyDown(Keys.NumPad1))
                Config.GameAeraSize += Config.GameAeraSize * (-0.01f);

            if (keyboardState.IsKeyDown(Keys.NumPad3))
                Config.GameAeraSize += Config.GameAeraSize * 0.01f;

            _previousKeyboardState = Keyboard.GetState();
        }

        #region Pattern file edit
        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                _watcher.EnableRaisingEvents = false;

                // Wait until the file is not in used
                while (IsFileLocked(_patternFileInfos[_currentPattern]))
                {
                    Thread.Sleep(10);
                }

                LoadPatternFile();
                AddBullet(true);
            }
            finally
            {
                _watcher.EnableRaisingEvents = true;
            }
        }

        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                stream?.Close();
            }

            return false;
        }

        private void EditCurrentPatternFile()
        {
            // Remove the old watcher
            _watcher?.Dispose();

            // Watch the bullet pattern file
            _watcher = new FileSystemWatcher
            {
                Path = _patternFileInfos[_currentPattern].DirectoryName,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = _patternFileInfos[_currentPattern].Name
            };

            // Add event handler
            _watcher.Changed += OnChanged;

            // Begin watching
            _watcher.EnableRaisingEvents = true;

            Process.Start(_patternFileInfos[_currentPattern].FullName);
        }
        #endregion

        private void AddBullet(bool clear = false)
        {
            if (clear)
                _moverManager.Clear();

            if (!string.IsNullOrEmpty(_currentPatternErrors[_currentPattern]))
                return;

            // Add a new bullet in the center of the screen
            _mover = (Mover)_moverManager.CreateBullet(true);
            _mover.Position = _moverManagerPosition;
            _mover.InitTopNode(_patterns[_currentPattern].RootNode);
        }

        private void NextPattern()
        {
            _currentPattern = (_currentPattern + 1) % _patterns.Count;
            AddBullet(true);
        }

        private void PreviousPattern()
        {
            _currentPattern = (_currentPattern - 1) < 0 ? _patterns.Count - 1 : _currentPattern - 1;
            AddBullet(true);
        }

        private string WrapString(String text, float width, SpriteFont font)
        {
            var returnString = string.Empty;
            var currentLine = string.Empty;
            var newLine = Environment.NewLine;
            var wordArray = text.Split(' ');

            foreach (var word in wordArray)
            {
                if (font.MeasureString(currentLine + word).Length() > width)
                {
                    currentLine += newLine;
                    returnString += currentLine;
                    currentLine = string.Empty;
                }

                currentLine += word + ' ';
            }

            return returnString + currentLine;
        }
    }
}