using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using BulletML;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Visualizer.Android
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    internal class Game1 : Game
    {
        public static GraphicsDeviceManager Graphics;
        private SpriteBatch _spriteBatch;

        private Activity1 _activity;

        private Texture2D _playerTexture;
        private Texture2D _bulletTexture;
        private SpriteFont _mainFont;

        private Camera2D _camera;

        private Player _player;
        private MoverManager _moverManager;
        private Mover _mover;

        private float _rank = 0.0f;
        private bool _pause = false;

        private readonly List<BulletPattern> _patterns = new List<BulletPattern>();
        private readonly List<string> _patternNames = new List<string>();
        private readonly List<string> _currentPatternErrors = new List<string>();
        private int _currentPattern;

        private readonly List<FileInfo> _patternFileInfos = new List<FileInfo>();

        private KeyboardState _previousKeyboardState;

        public Game1(Activity1 activity)
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            // Allow window resizing
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            Content.RootDirectory = "Content";

            _activity = activity;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            Graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            _previousKeyboardState = Keyboard.GetState();
            _player = new Player();
            _moverManager = new MoverManager(_player.GetPosition);
            _camera = new Camera2D(GraphicsDevice.Viewport);

            _player.Initialize();

            base.Initialize();
        }

        private float GetRank()
        {
            return _rank;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _playerTexture = Content.Load<Texture2D>("Sprites\\player");
            _bulletTexture = Content.Load<Texture2D>("Sprites\\player");

            _mainFont = Content.Load<SpriteFont>("Fonts\\main");

            // Get all the xml files in the samples directory
            var index = 0;

            foreach (var source in _activity.ListFile(@"Content/Patterns"))
            {
                var filename = @"Content/Patterns/" + source;
                // Store the name
                _patternNames.Add(filename);

                _patternFileInfos.Add(new FileInfo(filename));

                // Load the pattern
                var pattern = new BulletPattern();
                _currentPatternErrors.Add(null);
                _patterns.Add(pattern);

                try
                {
                    var stream = _activity.ApplicationContext.Assets.Open(filename);

                    pattern.ParseStream(filename, stream);
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
            HandleInput(gameTime);

            if (!_pause)
                _moverManager.Update();

            _player.Update(gameTime);

            _previousKeyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());

            foreach (var mover in _moverManager.Movers)
            {
                _spriteBatch.Draw(_bulletTexture,
                    mover.Position, null,
                    Color.White, mover.Direction,
                    new Vector2(_bulletTexture.Width / 2f, _bulletTexture.Height / 2f), 1f, SpriteEffects.None, 0f
                );
            }

            _spriteBatch.Draw(_playerTexture,
                _player.Position, null,
                Color.Red, 0f,
                new Vector2(_playerTexture.Width / 2f, _playerTexture.Height / 2f), new Vector2(1.5f), SpriteEffects.None, 0f
            );

            _spriteBatch.End();

            DrawStrings();

            base.Draw(gameTime);
        }

        private void DrawStrings()
        {
            _spriteBatch.Begin();

            if (_patternNames.Count > 0)
                _spriteBatch.DrawString(_mainFont, "Pattern: " + _patternNames[_currentPattern], Vector2.Zero, Color.White);
            _spriteBatch.DrawString(_mainFont, "Bullets: " + _moverManager.Movers.Count, new Vector2(0, 40f), Color.White);

            if (_currentPatternErrors.Count > 0 && !string.IsNullOrEmpty(_currentPatternErrors[_currentPattern]))
            {
                var error = WrapString(_currentPatternErrors[_currentPattern], Graphics.PreferredBackBufferWidth, _mainFont);

                _spriteBatch.DrawString(_mainFont, error,
                    new Vector2(0, Graphics.PreferredBackBufferHeight - _mainFont.MeasureString(error).Y), Color.Red
                );
            }

            _spriteBatch.End();
        }

        private void HandleInput(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            var touchState = TouchPanel.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            foreach (var touch in touchState)
            {
                if (touch.State == TouchLocationState.Pressed)
                    AddBullet();
            }

            if (keyboardState.IsKeyDown(Keys.P) && _previousKeyboardState.IsKeyUp(Keys.P))
                _pause = !_pause;

            if (keyboardState.IsKeyDown(Keys.PageUp) && _previousKeyboardState.IsKeyUp(Keys.PageUp))
            {
                _currentPattern = (_currentPattern + 1) % _patterns.Count;
                AddBullet(true);
            }
            else if (keyboardState.IsKeyDown(Keys.PageDown) && _previousKeyboardState.IsKeyUp(Keys.PageDown))
            {
                _currentPattern = (_currentPattern - 1) < 0 ? _patterns.Count - 1 : _currentPattern - 1;
                AddBullet(true);
            }

            if (keyboardState.IsKeyDown(Keys.LeftControl) && _previousKeyboardState.IsKeyUp(Keys.LeftControl))
                AddBullet();

            if (keyboardState.IsKeyDown(Keys.Space))
                AddBullet();

            if (keyboardState.IsKeyDown(Keys.Delete))
                _moverManager.Clear();

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
        }

        private void AddBullet(bool clear = false)
        {
            if (clear)
                _moverManager.Clear();

            if (_patterns.Count == 0 || !string.IsNullOrEmpty(_currentPatternErrors[_currentPattern]))
                return;

            // Add a new bullet in the center of the screen
            _mover = (Mover)_moverManager.CreateBullet(true);
            _mover.Position = new Vector2(Config.GameAeraSize.X / 2f, Config.GameAeraSize.Y / 2f);
            _mover.InitTopNode(_patterns[_currentPattern].RootNode);
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
