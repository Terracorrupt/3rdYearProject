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
        int                                                             _positionOffset1,_positionOffset2,_positionOffset3;
        Entity[]                                                        _entitiesL1;
        Entity[]                                                        _entitiesL2;
        Entity[]                                                        _entitiesL3;
        

        public Highscores(Microsoft.Xna.Framework.Game game)
        {
            this._game = game;
            _service = (Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService)game.Services.GetService(typeof(Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService));
            _content = new ContentManager(game.Services);
            _content.RootDirectory = "content";
            _graphicsDev = game.GraphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDev);
            _font = _content.Load<SpriteFont>("Fonts\\Neuropolitical");

            _entitiesL1 = SceneManager.GetInstance(_game)._dao.getHighScores(1);
            _entitiesL2 = SceneManager.GetInstance(_game)._dao.getHighScores(2);
            _entitiesL3 = SceneManager.GetInstance(_game)._dao.getHighScores(3);

            _positionOffset1 = 80;
            _positionOffset2 = 80;
            _positionOffset3 = 80;

            for (int i=0; i<_entitiesL2.Length;i++)
            {
                if (_entitiesL2[i].L2Minutes == 60)
                {
                    _entitiesL2[i].L2Minutes = 0;
                }
                if (_entitiesL2[i].L2Seconds == 60)
                {
                    _entitiesL2[i].L2Seconds = 0;
                }
            }
            for (int i = 0; i < _entitiesL1.Length; i++)
            {
                if (_entitiesL1[i].L1Minutes == 60)
                {
                    _entitiesL1[i].L1Minutes = 0;
                }
                if (_entitiesL1[i].L1Seconds == 60)
                {
                    _entitiesL1[i].L1Seconds = 0;
                }
            }
            for (int i = 0; i < _entitiesL3.Length; i++)
            {
                if (_entitiesL3[i].L3Minutes == 60)
                {
                    _entitiesL3[i].L3Minutes = 0;
                }
                if (_entitiesL3[i].L3Seconds == 60)
                {
                    _entitiesL3[i].L3Seconds = 0;
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

            _positionOffset1 = 80;
            _positionOffset2 = 80;
            _positionOffset3 = 80;


            if (_entitiesL1.Length >= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    _spriteBatch.DrawString(_font, "" + (i + 1) + ": " + _entitiesL1[i].Name + " --- Time: " + _entitiesL1[i].L1Minutes + "." + _entitiesL1[i].L1Seconds, new Vector2(50, _positionOffset1), Color.White);
                    _positionOffset1 += 40;
                }
            }
            else
            {
                for (int i = 0; i < _entitiesL1.Length; i++)
                {
                    _spriteBatch.DrawString(_font, "" + (i + 1) + ": " + _entitiesL1[i].Name + " --- Time: " + _entitiesL1[i].L1Minutes + "." + _entitiesL1[i].L1Seconds, new Vector2(50, _positionOffset1), Color.White);
                    _positionOffset1 += 40;
                }
            }

            if (_entitiesL2.Length >= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    _spriteBatch.DrawString(_font, "" + (i + 1) + ": " + _entitiesL2[i].Name + " --- Time: " + _entitiesL2[i].L2Minutes + "." + _entitiesL2[i].L2Seconds, new Vector2(500, _positionOffset2), Color.White);
                    _positionOffset2 += 40;
                }
            }
            else
            {
                for (int i = 0; i < _entitiesL2.Length; i++)
                {
                    _spriteBatch.DrawString(_font, "" + (i + 1) + ": " + _entitiesL2[i].Name + " --- Time: " + _entitiesL2[i].L2Minutes + "." + _entitiesL2[i].L2Seconds, new Vector2(500, _positionOffset2), Color.White);
                    _positionOffset2 += 40;
                }
            }

            if (_entitiesL3.Length >= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    _spriteBatch.DrawString(_font, "" + (i + 1) + ": " + _entitiesL3[i].Name + " --- Time: " + _entitiesL3[i].L3Minutes + "." + _entitiesL3[i].L3Seconds, new Vector2(950, _positionOffset3), Color.White);
                    _positionOffset3 += 40;
                }
            }
            else
            {
                for (int i = 0; i < _entitiesL2.Length; i++)
                {
                    _spriteBatch.DrawString(_font, "" + (i + 1) + ": " + _entitiesL3[i].Name + " --- Time: " + _entitiesL3[i].L3Minutes + "." + _entitiesL3[i].L3Seconds, new Vector2(950, _positionOffset3), Color.White);
                    _positionOffset3 += 40;
                }
            }

            _spriteBatch.DrawString(_font, "Level 1", new Vector2(120,30), Color.LawnGreen);
            _spriteBatch.DrawString(_font, "Level 2", new Vector2(630, 30), Color.LawnGreen);
            _spriteBatch.DrawString(_font, "Level 3", new Vector2(1060, 30), Color.LawnGreen);

            _spriteBatch.DrawString(_font,"Press B to return", new Vector2(200,700),Color.Yellow);
            _spriteBatch.End();
        }
    }
}
