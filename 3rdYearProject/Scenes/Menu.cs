using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace _3rdYearProject
{
    public class Menu:Scene
    {
        private Microsoft.Xna.Framework.Game                            _game;
        private Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService _service;
        SpriteBatch                                                     _spriteBatch;
        ContentManager                                                  _content;
        GraphicsDevice                                                  _graphicsDev;
        SpriteFont                                                      _font;
        InputName                                                       _inputName;
        KeyboardState                                                   _keyState, _previous;
        

        public Menu(Microsoft.Xna.Framework.Game game)
        {
            this._game = game;
            _service = (Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService)game.Services.GetService(typeof(Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService));
            _content = new ContentManager(game.Services);
            _content.RootDirectory = "content";
            _graphicsDev = game.GraphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDev);
            _inputName = new InputName(_content);

            Initialize();
        }

        public void Initialize()
        {
            LoadContent();
            Console.WriteLine("In Menu");
        }

        public void LoadContent()
        {
            _font = _content.Load<SpriteFont>("Fonts\\Squarefont");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _keyState = Keyboard.GetState();

            if (_keyState.IsKeyDown(Keys.Escape))
                _game.Exit();
            if (_keyState.IsKeyDown(Keys.Enter) && (_inputName.login()!=""))
            {
                string name = _inputName.login();
                Entity e = new Entity();
                e.Name = name;

                SceneManager.GetInstance(_game)._userName = name;
                SceneManager.GetInstance(_game)._dao.Insert(e);
                SceneManager.GetInstance(_game).Current = SceneManager.State.LEVEL1;
            }

            _inputName.constructMessage(_keyState, _previous);

            _previous = _keyState;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _spriteBatch.Begin();
            _inputName.Draw(_spriteBatch);
            _spriteBatch.DrawString(_font, "MENU", new Vector2(320, 50), Color.White);
            _spriteBatch.DrawString(_font, "Please enter your username", new Vector2(230, 150), Color.White);
            _spriteBatch.End();
        }
    }
}
