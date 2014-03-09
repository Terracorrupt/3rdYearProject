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
        Texture2D                                                       _textBox, _backGround;
        ContentManager                                                  _content;
        GraphicsDevice                                                  _graphicsDev;
        SpriteFont                                                      _font;
        InputName                                                       _inputName;
        KeyboardState                                                   _keyState, _previous;
        GamePadState                                                    _gamePadState;
        private bool                                                    _play, _exit, _highscores,_isAllowed,_isAllowed2, _loggingIn;
        private int                                                     _menuIndex;
        Color                                                           _color1, _color2,_color3;
        

        public Menu(Microsoft.Xna.Framework.Game game)
        {
            this._game = game;
            _service = (Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService)game.Services.GetService(typeof(Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService));
            _content = new ContentManager(game.Services);
            _content.RootDirectory = "content";
            _graphicsDev = game.GraphicsDevice;
            _spriteBatch = new SpriteBatch(_graphicsDev);
            _inputName = new InputName(_content);
            _color1 = Color.White;
            _color2 = Color.White;
            _color3 = Color.White;

            Initialize();
        }

        public void Initialize()
        {
            _menuIndex = 1;
            LoadContent();
            Console.WriteLine("In Menu");
        }

        public void LoadContent()
        {
            _textBox = _content.Load<Texture2D>("Backgrounds\\textBox");
            _font = _content.Load<SpriteFont>("Fonts\\Neuropolitical");
            _backGround = _content.Load<Texture2D>("Backgrounds\\landscape");
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _keyState = Keyboard.GetState();
            _gamePadState = GamePad.GetState(PlayerIndex.One);

            //Setting Menu Index
            if (_menuIndex == 1)
            {
                _play = true;
                _highscores = false;
                _exit = false;

                _color1 = Color.Yellow;
                _color2 = Color.White;
                _color3 = Color.White;
            }
            if (_menuIndex == 2)
            {
                _play = false;
                _highscores = true;
                _exit = false;

                _color1 = Color.White;
                _color2 = Color.Yellow;
                _color3 = Color.White;
            }
            if (_menuIndex == 3)
            {
                _play = false;
                _highscores = false;
                _exit = true;

                _color1 = Color.White;
                _color2 = Color.White;
                _color3 = Color.Yellow;
            }

            //What is on
            if (_play)
            {
                if (_keyState.IsKeyDown(Keys.Down))
                {
                    _isAllowed = true;

                }
                if (_isAllowed && ((_keyState.IsKeyUp(Keys.Down))))
                {
                    _menuIndex++;
                    _isAllowed = false;
                }
            }

            if (_highscores)
            {
                if (_keyState.IsKeyDown(Keys.Up))
                {
                    _isAllowed = true;

                }
                if (_isAllowed && ((_keyState.IsKeyUp(Keys.Up))))
                {
                    _menuIndex--;
                    _isAllowed = false;
                }

                if (_keyState.IsKeyDown(Keys.Down))
                {
                    _isAllowed2 = true;

                }
                if (_isAllowed2 && ((_keyState.IsKeyDown(Keys.Down))))
                {
                    _menuIndex++;
                    _isAllowed2 = false;
                }
            }

            if (_exit)
            {
                if (_keyState.IsKeyDown(Keys.Up))
                {
                    _isAllowed = true;

                }
                if (_isAllowed && ((_keyState.IsKeyUp(Keys.Up))))
                {
                    _menuIndex--;
                    _isAllowed = false;
                }
            }


            //Play
            if (_play&&(_keyState.IsKeyDown(Keys.Enter))||(_gamePadState.IsButtonDown(Buttons.A)))
            {

                _loggingIn = true;
            }

            if (_loggingIn)
            {

                _inputName.constructMessage(_keyState, _previous);

                if (_inputName.login() != "" && ((_keyState.IsKeyDown(Keys.Enter)) || (_gamePadState.IsButtonDown(Buttons.A))))
                {
                    string name = _inputName.login();
                    SceneManager.GetInstance(_game)._userName = name;
                    SceneManager.GetInstance(_game)._dao.Save("Name", name, 0, 60, 60);
                    SceneManager.GetInstance(_game).Current = SceneManager.State.LEVEL1;
                }
            }


            //Highscores
            //Play
            if (_highscores && ((_keyState.IsKeyDown(Keys.Enter)) || (_gamePadState.IsButtonDown(Buttons.A))))
            {
                SceneManager.GetInstance(_game).Current = SceneManager.State.HIGHSCORE;
            }


            //Exit
            if (_exit && ((_keyState.IsKeyDown(Keys.Enter)) || (_gamePadState.IsButtonDown(Buttons.A))))
            {
                _game.Exit();
            }

            

            _previous = _keyState;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backGround, new Rectangle(0, 0, _graphicsDev.Viewport.Width + 100, _graphicsDev.Viewport.Height + 80), Color.White);
            _spriteBatch.Draw(_textBox, new Rectangle(150,30,440,70), Color.Red);
            _spriteBatch.Draw(_textBox, new Rectangle(220, 320, 250, 150), Color.White);
            _inputName.Draw(_spriteBatch);
            _spriteBatch.DrawString(_font, "Bethselamin Time Trial", new Vector2(180, 50), Color.White);

            if (_loggingIn)
                _spriteBatch.DrawString(_font, "Please enter your username", new Vector2(130, 150), Color.White);
            else
            {
                _spriteBatch.DrawString(_font, "Play", new Vector2(240, 350), _color1);
                _spriteBatch.DrawString(_font, "High scores", new Vector2(240, 380), _color2);
                _spriteBatch.DrawString(_font, "Exit", new Vector2(240, 410), _color3);
            }

            _spriteBatch.End();
        }
    }
}
