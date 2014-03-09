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
        Microsoft.Xna.Framework.Game                                    _game;
        private Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService _service;
        SpriteBatch                                                     _spriteBatch;
        ContentManager                                                  _content;
        GraphicsDevice                                                  _graphicsDev;
        SpriteFont                                                      _font;
        int                                                             _positionOffset;
        Entity[]                                                        _entities;

        public Highscores(Microsoft.Xna.Framework.Game game)
        {
            this._game = game;
            _service = (Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService)game.Services.GetService(typeof(Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService));
            _content = new ContentManager(game.Services);
            _content.RootDirectory = "content";
            _graphicsDev = game.GraphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDev);
            _font = _content.Load<SpriteFont>("Fonts\\Neuropolitical");

            _entities = SceneManager.GetInstance(_game)._dao.getHighScores();
            _positionOffset = 40;

            for (int i=0; i<_entities.Length;i++)
            {
                if (_entities[i].Minutes == 60)
                {
                    _entities[i].Minutes = 0;
                }
                if (_entities[i].Seconds == 60)
                {
                    _entities[i].Seconds = 0;
                }
            }
            
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
                SceneManager.GetInstance(_game).Current = SceneManager.State.MENU;          
            }
                
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _spriteBatch.Begin();

            _positionOffset = 40;


            if (_entities.Length >= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    _spriteBatch.DrawString(_font, "" + (i + 1) + ": " + _entities[i].Name + " --- Time: " + _entities[i].Minutes + "." + _entities[i].Seconds, new Vector2(200, _positionOffset), Color.White);
                    _positionOffset += 40;
                }
            }
            else
            {
                for (int i = 0; i < _entities.Length; i++)
                {
                    _spriteBatch.DrawString(_font, "" + (i + 1) + ": " + _entities[i].Name + " --- Time: " + _entities[i].Minutes + "." + _entities[i].Seconds, new Vector2(200, _positionOffset), Color.White);
                    _positionOffset += 40;
                }
            }
            _spriteBatch.DrawString(_font,"Press B to return", new Vector2(200,450),Color.Yellow);
            _spriteBatch.End();
        }
    }
}
