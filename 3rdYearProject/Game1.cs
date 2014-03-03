#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace _3rdYearProject
{
    public class Game1 : Game
    {
        //Variables
        GraphicsDeviceManager                       _graphics;
        SpriteBatch                                 _spriteBatch;
        SceneManager                                _manager;

        public Game1() : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            _manager = SceneManager.GetInstance(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 700;

            //_graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            //_dao = new DAO();

            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            
        }

       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            //if (Keyboard.GetState().IsKeyDown(Keys.S))
            //{

            //    string[] key = new string[2];
            //    string[] value = new string[2]; ;

            //    key[0] = "_Name";
            //    value[0] = "James";

            //    _dao.Save(key, value);
                
            //}

            //Let the Scene Manager do it's thing
            _manager.NextScene().Update(gameTime);
           
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _manager.NextScene().Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
