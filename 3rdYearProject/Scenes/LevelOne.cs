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
        Camera                                                              _camera;
        MouseState                                                          _mouseStateLastFrame;
        Rectangle                                                           _rightBounds, _leftBounds;
        Texture2D                                                           _debugTex;

        public LevelOne(Microsoft.Xna.Framework.Game game)
        {
            this._game = game;
            _service = (Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService)game.Services.GetService(typeof(Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService));
            _content = new ContentManager(game.Services);
            _content.RootDirectory = "content";
            _graphicsDev = game.GraphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDev);
            _camera = new Camera(_graphicsDev.Viewport);
            _rightBounds = new Rectangle(_graphicsDev.Viewport.Width - 100, 0, 500, _graphicsDev.Viewport.Height);
            _leftBounds = new Rectangle(_graphicsDev.Viewport.X-100, 0, 300, _graphicsDev.Viewport.Height);
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
            _debugTex = _content.Load<Texture2D>("PlayerSprites\\debugRec");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
                SceneManager.GetInstance(_game).Current = SceneManager.State.MENU;

            _player.Update(gameTime);
            _camera.Update();

            //If We move near the edge, move the camera
            if (_player._playerDefaultRectangle.Intersects(_rightBounds))
            {
                _camera.forward();
                _rightBounds.X += 3;
                _leftBounds.X += 3;
            }
            if (_player._playerDefaultRectangle.Intersects(_leftBounds))
            {
                _camera.backward();
                _leftBounds.X -= 3;
                _rightBounds.X -= 3;
            }
            
            _mouseStateLastFrame = Mouse.GetState();

            //Console.WriteLine("Mouse: " + _mouseStateLastFrame.X + " " + _mouseStateLastFrame.Y);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, _camera.Transform);
            _player.Draw(_spriteBatch);
            _spriteBatch.Draw(_debugTex,_rightBounds,Color.White);
            _spriteBatch.Draw(_debugTex, _leftBounds, Color.White);
            _spriteBatch.DrawString(_font, "LEVEL 1", new Vector2(350, 50), Color.White);
            _spriteBatch.End();
        }
    }
}
