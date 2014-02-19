using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace _3rdYearProject
{
    public class LevelOne : Scene
    {
        private Microsoft.Xna.Framework.Game                                _game;
        private Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService     _service;
        SpriteBatch                                                         _spriteBatch;
        ContentManager                                                      _content;
        GraphicsDevice                                                      _graphicsDev;
        SpriteFont                                                          _font;
        Player                                                              _player;


        public LevelOne(Microsoft.Xna.Framework.Game game)
        {
            this._game = game;
            _service = (Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService)game.Services.GetService(typeof(Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService));
            _content = new ContentManager(game.Services);
            _content.RootDirectory = "content";
            _graphicsDev = game.GraphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDev);

            Initialize();
        }

        public void Initialize()
        {
            _player = new Player();
            _player.Initialize();
            LoadContent();
            Console.WriteLine("In Level One");
        }

        public void LoadContent()
        {
            _player.LoadContent(_content);
            _font = _content.Load<SpriteFont>("Fonts\\Squarefont");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                SceneManager.GetInstance(_game).Current = SceneManager.State.MENU;

            _player.Update(gameTime);

            if (_player._playerDefaultPosition.X > 668)
                _player._playerDefaultPosition.X = 668;
            if (_player._playerDefaultPosition.X < 0)
                _player._playerDefaultPosition.X = 0;

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _spriteBatch.Begin();
            _player.Draw(_spriteBatch);
            _spriteBatch.DrawString(_font, "LEVEL 1", new Vector2(350, 50), Color.White);
            _spriteBatch.End();
        }
    }
}
