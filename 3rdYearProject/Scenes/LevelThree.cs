using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;

namespace _3rdYearProject
{
    public class LevelThree : Scene
    {
        private Microsoft.Xna.Framework.Game _game;
        private Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService _service;
        SpriteBatch _spriteBatch;
        ContentManager _content;
        GraphicsDevice _graphicsDev;
        LevelBuilder _levelBuilder;
        SpriteFont _font;
        Player _player;
        Camera _camera;
        MouseState _mouseStateLastFrame;
        Rectangle _rightBounds, _leftBounds, _backgroundBounds;
        Texture2D _debugTex, _backgroundTex;
        Texture2D _textBox;
        SoundEffect _music;
        SoundEffectInstance _musicInstance;
        TimeSpan _levelTime;
        GamePadState _gamePadState;

        Vector2 _scorePos;


        public LevelThree(Microsoft.Xna.Framework.Game game)
        {
            this._game = game;
            _service = (Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService)game.Services.GetService(typeof(Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService));
            _content = new ContentManager(game.Services);
            _content.RootDirectory = "content";
            _graphicsDev = game.GraphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDev);
            _camera = new Camera(_graphicsDev.Viewport);
            _rightBounds = new Rectangle(_graphicsDev.Viewport.Width - 100, 0, 500, _graphicsDev.Viewport.Height);
            _leftBounds = new Rectangle(_graphicsDev.Viewport.X - 100, 0, 300, _graphicsDev.Viewport.Height);
            _backgroundBounds = new Rectangle(0, 0, _graphicsDev.Viewport.Width + 100, _graphicsDev.Viewport.Height + 80);
            _levelBuilder = new LevelBuilder(_content, 3);
            _scorePos = new Vector2(40, 20);
            _levelTime = TimeSpan.Zero;

            Initialize();
        }

        public void Initialize()
        {
            _player = new Player();
            _player.Initialize();
            _player.setPosition(new Vector2(100, 200));
            _levelBuilder.buildLevel();

            LoadContent();

            //Console.WriteLine("In Level Two");
            _musicInstance.Play();
        }

        public void LoadContent()
        {
            _player.LoadContent(_content);
            _font = _content.Load<SpriteFont>("Fonts\\Neuropolitical");
            _debugTex = _content.Load<Texture2D>("PlayerSprites\\debugRec");
            _backgroundTex = _content.Load<Texture2D>("Backgrounds\\night");
            _music = _content.Load<SoundEffect>("Music\\level3");
            _textBox = _content.Load<Texture2D>("Backgrounds\\textBox");

            _musicInstance = _music.CreateInstance();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                SceneManager.GetInstance(_game).Current = SceneManager.State.MENU;
            _gamePadState = GamePad.GetState(PlayerIndex.One);

            _player.Update(gameTime);
            _levelBuilder.Update(gameTime, _player);
            _camera.Update();

            if (_levelBuilder.getFinished())
            {

                _musicInstance.Stop();

                string key = "Name";
                string value = SceneManager.GetInstance(_game)._userName;
                int jumps = _player._noJumps;
                int minutes = _levelTime.Minutes;
                int seconds = _levelTime.Seconds;

                SceneManager.GetInstance(_game)._dao.Save(key, value, jumps, minutes, seconds,3);

                SceneManager.GetInstance(_game).Current = SceneManager.State.MENU;
            }

            _levelTime += gameTime.ElapsedGameTime;

            //If We move near the edge, move the camera
            if (_player._playerDefaultRectangle.Intersects(_rightBounds))
            {

                _camera.forward();
                if (Keyboard.GetState().IsKeyDown(Keys.X) || (_gamePadState.IsButtonDown(Buttons.X)))
                {
                    _rightBounds.X += 6;
                    _leftBounds.X += 6;
                    _scorePos.X += 6;
                }
                else
                {
                    _rightBounds.X += 4;
                    _leftBounds.X += 4;
                    _scorePos.X += 4f;
                }
            }
            if (_player._playerDefaultPosition.X > 500)
            {
                if (_player._playerDefaultRectangle.Intersects(_leftBounds))
                {
                    _camera.backward();
                    if (Keyboard.GetState().IsKeyDown(Keys.X) || (_gamePadState.IsButtonDown(Buttons.X)))
                    {
                        _rightBounds.X -= 6;
                        _leftBounds.X -= 6;
                        _scorePos.X -= 6;
                    }
                    else
                    {
                        _leftBounds.X -= 4;
                        _rightBounds.X -= 4;
                        _scorePos.X -= 4;
                    }
                }
            }

            if (_player._isDead)
            {
                _musicInstance.Stop();
            }
            if (_player._deadTimer >= 100)
            {
                _musicInstance.Stop();

                SceneManager.GetInstance(_game).Current = SceneManager.State.MENU;
            }

            _mouseStateLastFrame = Mouse.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                _player.setPosition(new Vector2(_player._playerDefaultPosition.X-50, _player._playerDefaultPosition.Y+50));
                _camera.reset();
            }

            //Console.WriteLine("Mouse: " + _mouseStateLastFrame.X + " " + _mouseStateLastFrame.Y);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, _camera.Transform);

            //_spriteBatch.Draw(_backgroundTex, _backgroundBounds, Color.White);

            for (int i = 0; i < 20; i++)
            {
                _spriteBatch.Draw(_backgroundTex, new Rectangle(_backgroundBounds.Width * i, 0, _backgroundBounds.Width, _backgroundBounds.Height), Color.White);
                _spriteBatch.Draw(_backgroundTex, new Rectangle(-_backgroundBounds.Width * i, 0, _backgroundBounds.Width, _backgroundBounds.Height), Color.White);
            }

            //_spriteBatch.Draw(_debugTex, _rightBounds, Color.White);
            //_spriteBatch.Draw(_debugTex, _leftBounds, Color.White);

            //_spriteBatch.DrawString(_font, "Welcome to Bethselamin", new Vector2(350, 50), Color.Black);

            _player.Draw(_spriteBatch);
            _levelBuilder.Draw(_spriteBatch);
            _spriteBatch.Draw(_textBox, new Rectangle((int)_scorePos.X - 20, 10, 210, 50), Color.White);
            _spriteBatch.DrawString(_font, "Time: " + _levelTime.Minutes + "." + _levelTime.Seconds, _scorePos, Color.Black);

            _spriteBatch.End();
        }
    }
}
