using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;


namespace _3rdYearProject
{
    class Highscores:Scene
    {
        private Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService _service;
        SpriteBatch                                                     _spriteBatch;
        ContentManager                                                  _content;
        GraphicsDevice                                                  _graphicsDev;
        SpriteFont                                                      _font;

        public Highscores(Microsoft.Xna.Framework.Game game)
        {
            _service = (Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService)game.Services.GetService(typeof(Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService));
            _content = new ContentManager(game.Services);
            _content.RootDirectory = "content";
            _graphicsDev = game.GraphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDev);
            _font = _content.Load<SpriteFont>("Fonts\\Squarefont");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font,"Under Construction", new Vector2(200,200),Color.LightSteelBlue);
            _spriteBatch.End();
        }
    }
}
