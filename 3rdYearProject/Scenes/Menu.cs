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


        public Menu(Microsoft.Xna.Framework.Game game)
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
            LoadContent();
            Console.WriteLine("In Menu");
        }

        public void LoadContent()
        {
            _font = _content.Load<SpriteFont>("Fonts\\Squarefont");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                SceneManager.GetInstance(_game).Current = SceneManager.State.LEVEL1;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "MENU", new Vector2(350, 50), Color.White);
            _spriteBatch.End();
        }
    }
}
