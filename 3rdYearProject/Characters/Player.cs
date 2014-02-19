﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _3rdYearProject
{
    class Player:Character
    {

        public enum State                       { IDLE, RUNNING, JUMPING };
        public State                            _currentState;
        private State                           _previousState;
        private Texture2D                       _playerDefaultTexture;
        public  Vector2                         _playerDefaultPosition;
        private Vector2                         _velocity;
        private Vector2                         _acceleration;
        private Rectangle                       _playerDefaultRectangle;
        private float                           _topSpeed;
        private float                           _gravity;
        private float                           _jumpSpeed;
        private bool                            _canJump;

        public override void Initialize()
        {
            _playerDefaultPosition = new Vector2(300, 300);
            _gravity = 20f;
            _canJump = false;
        }

        public override void LoadContent(ContentManager content)
        {
            _playerDefaultTexture = content.Load<Texture2D>("PlayerSprites//TempPlayer");
        }

        public override void Update(GameTime gameTime)
        {
            

            Movement(gameTime);
            _playerDefaultRectangle = new Rectangle((int)_playerDefaultPosition.X, (int)_playerDefaultPosition.Y, _playerDefaultTexture.Width, _playerDefaultTexture.Height);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerDefaultTexture, _playerDefaultRectangle, Color.White);
        }

        public void Movement(GameTime gameTime)
        {

            //Press X to Speed Up!
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                _topSpeed = 6f;
            }
            else
            {
                _topSpeed = 3f;
            }

            //If We reach top speed, we can't increase velocity anymore
            if (_velocity.X < _topSpeed+1&&_velocity.X>-_topSpeed-1)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    
                        _acceleration.X -= .3f;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                   
                        _acceleration.X += .3f;
                }
                else
                    _acceleration.X = 4f * -_velocity.X;

                _velocity.X += _acceleration.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                //Accounting for top speed on + and - 
                if (_velocity.X > -_topSpeed -1)
                    _velocity.X = _topSpeed;
                else
                    _velocity.X = -_topSpeed;

                //Reset Acceleration or bad things happen
                _acceleration.X = 0;
            }
            

            //Now for that Y Position...
            if (Keyboard.GetState().IsKeyDown(Keys.Z) && _canJump)
            {
                _velocity.Y = -_jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _canJump = false;
                Console.WriteLine("Jumped");
            }

            //Gravity
            if (!_canJump)
            {
                _velocity.Y += _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _jumpSpeed = 500f;
            }
            else
                _velocity.Y = 0;

            _canJump = _playerDefaultPosition.Y >= 300;


            //Finally, Update your actual position
            _playerDefaultPosition+= _velocity;

            Console.WriteLine("Acceleration: " + _acceleration.X);
            Console.WriteLine("Velocity: " + _velocity.X);
            //Console.WriteLine("X: " + _playerDefaultPosition.X + " Y: " + _playerDefaultPosition.Y);
            
        }
    }
}
