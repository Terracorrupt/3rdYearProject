using System;
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

        public enum State                       { IDLELEFT,IDLERIGHT, RUNNINGLEFT,RUNNINGRIGHT, JUMPINGLEFT,JUMPINGRIGHT };
        public State                            _currentState;
        private State                           _previousState;
        private Texture2D                       _playerDefaultTexture, _playerIdleLeftTex, _playerIdleRightTex, _playerRunningLeftTex,
                                                _playerRunningRightTex, _jumpingTex;
        private Animation                       _playerIdleLeftAnim, _playerIdleRightAnim,_playerRunningLeftAnim, _playerRunningRightAnim,
                                                _jumpingAnim;
        public  Vector2                         _playerDefaultPosition;
        public  Vector2                         _velocity;
        public Vector2                          _acceleration;
        public Rectangle                        _playerDefaultRectangle;
        private float                           _topSpeed;
        private float                           _gravity;
        private float                           _jumpSpeed;
        private float                           _lastJump;
        public bool                             _canJump;
        private bool                            _noSpamJump;
        public bool                             _isColliding;
        public int                              _noJumps;
        public int                              _bruteCount;
        public int                              _jumpTimer;
        GamePadState                            _gamePadState;

        public override void Initialize()
        {
            _playerDefaultPosition = new Vector2(300, 300);
            _playerIdleLeftAnim = new Animation(100, new Vector2(30, 33), 8);
            _playerIdleRightAnim = new Animation(100, new Vector2(30, 33), 8);
            _playerRunningLeftAnim = new Animation(100, new Vector2(36, 36), 6);
            _playerRunningRightAnim = new Animation(100, new Vector2(36, 36), 6);
            _jumpingAnim = new Animation(2, new Vector2(36, 36), 4);

            _gravity = 15f;
            _canJump = false;
            _noJumps = 0;
            _noSpamJump = true;
        }

        public override void LoadContent(ContentManager content)
        {
            _playerDefaultTexture = content.Load<Texture2D>("PlayerSprites//debugRec");
            _playerIdleLeftTex = content.Load<Texture2D>("PlayerSprites//sonic_idle_left");
            _playerIdleRightTex = content.Load<Texture2D>("PlayerSprites//sonic_idle_right");
            _playerRunningLeftTex = content.Load<Texture2D>("PlayerSprites//sonic_running_left");
            _playerRunningRightTex = content.Load<Texture2D>("PlayerSprites//sonic_running_right");
            _jumpingTex = content.Load<Texture2D>("PlayerSprites//jumping");
        }

        public override void Update(GameTime gameTime)
        {
            if (_currentState == State.IDLELEFT)
                _playerIdleLeftAnim.Update();
            if (_currentState == State.IDLERIGHT)
                _playerIdleRightAnim.Update();
            if (_currentState == State.RUNNINGLEFT)
                _playerRunningLeftAnim.Update();
            if (_currentState == State.RUNNINGRIGHT)
                _playerRunningRightAnim.Update();
            if (_currentState == State.JUMPINGLEFT || _currentState == State.JUMPINGRIGHT)
                _jumpingAnim.Update();

            Movement(gameTime);
            _playerDefaultRectangle = new Rectangle((int)_playerDefaultPosition.X, (int)_playerDefaultPosition.Y, 55, 53);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_currentState == State.IDLELEFT)
                spriteBatch.Draw(_playerIdleLeftTex, new Rectangle((int)_playerDefaultPosition.X, (int)_playerDefaultPosition.Y, 50, 53), _playerIdleLeftAnim.GetCurrentSprite(), Color.White);
            else if (_currentState == State.IDLERIGHT)
                spriteBatch.Draw(_playerIdleRightTex, new Rectangle((int)_playerDefaultPosition.X, (int)_playerDefaultPosition.Y, 50, 53), _playerIdleRightAnim.GetCurrentSprite(), Color.White);
            else if (_currentState == State.RUNNINGLEFT)
                spriteBatch.Draw(_playerRunningLeftTex, new Rectangle((int)_playerDefaultPosition.X, (int)_playerDefaultPosition.Y, 55, 53), _playerRunningLeftAnim.GetCurrentSprite(), Color.White);
            else if (_currentState == State.RUNNINGRIGHT)
                spriteBatch.Draw(_playerRunningRightTex, new Rectangle((int)_playerDefaultPosition.X, (int)_playerDefaultPosition.Y, 55, 53), _playerRunningRightAnim.GetCurrentSprite(), Color.White);
            else if (_currentState == State.JUMPINGLEFT || _currentState == State.JUMPINGRIGHT)
                spriteBatch.Draw(_jumpingTex, new Rectangle((int)_playerDefaultPosition.X, (int)_playerDefaultPosition.Y, 55, 53), _jumpingAnim.GetCurrentSprite(), Color.White);
            else
                spriteBatch.Draw(_playerDefaultTexture, _playerDefaultRectangle, Color.Black);
        }

        public void Movement(GameTime gameTime)
        {
            _gamePadState = GamePad.GetState(PlayerIndex.One);

            //Press X to Speed Up!
            if (Keyboard.GetState().IsKeyDown(Keys.X) || (_gamePadState.IsButtonDown(Buttons.X)))
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
                if (Keyboard.GetState().IsKeyDown(Keys.Left) || (_gamePadState.IsButtonDown(Buttons.DPadLeft)))
                {
                    //Animation stuff
                    if (_canJump)
                    {
                        _currentState = State.RUNNINGLEFT;
                    }

                    _acceleration.X -= .3f;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right) || (_gamePadState.IsButtonDown(Buttons.DPadRight)))
                {
                    //Animation stuff
                    if (_canJump)
                    {
                        _currentState = State.RUNNINGRIGHT;
                    }

                    _acceleration.X += .3f;
                }
                else
                {
                    //Animation stuff
                    if (_canJump&&(_previousState==State.RUNNINGRIGHT||_previousState==State.JUMPINGRIGHT))
                    {
                        _currentState = State.IDLERIGHT;
                    }
                    if (_canJump && (_previousState == State.RUNNINGLEFT || _previousState == State.JUMPINGLEFT))
                    {
                        _currentState = State.IDLELEFT;
                    }

                    _acceleration.X = 4f * -_velocity.X;
                }

                //Update Velocity
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

            _lastJump = _velocity.Y;

            //Now for that Y Position...
            if ((Keyboard.GetState().IsKeyDown(Keys.Z)||(_gamePadState.IsButtonDown(Buttons.A))) && _canJump && _noSpamJump)
            {
                _velocity.Y = -_jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _canJump = false;
                //Console.WriteLine("Jumped");

                //For Counting Jumps and no spamming
                if (_bruteCount == 0)
                {
                    _noJumps++;
                }
                if (_bruteCount == 3)
                {
                    _noSpamJump = false;
                }
                
                _bruteCount++;
            }
            //NO SPAM JUMPING
            if (Keyboard.GetState().IsKeyUp(Keys.Z)&&(_gamePadState.IsButtonUp(Buttons.A)))
            {
                _noSpamJump = true;
                _bruteCount = 0;
            }
            //Pressure based jump
            if (Keyboard.GetState().IsKeyUp(Keys.Z) && (_gamePadState.IsButtonUp(Buttons.A))&&!_canJump)
            {
                _jumpTimer++;
                Console.WriteLine(_jumpTimer);
                if (_playerDefaultPosition.Y < 400&&_jumpTimer>5)
                {
                    _jumpSpeed = 300f;
                    _velocity.Y = _jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds+1;
                    _jumpTimer = 0;
                }
                    
            }

            //Gravity
            if (!_canJump)
            {
                _velocity.Y += _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _jumpSpeed = 500f;
                
            }
            else
                _velocity.Y = 0;

            //If we've been facing left, jump left, et cetera for right
            if(!_canJump&&(_previousState==State.IDLELEFT||_previousState==State.RUNNINGLEFT))
            {
                if(!_isColliding)
                    _currentState = State.JUMPINGLEFT;
            }
            if (!_canJump && (_previousState == State.IDLERIGHT || _previousState == State.RUNNINGRIGHT))
            {
                if (!_isColliding)
                    _currentState = State.JUMPINGRIGHT;
            }

            _canJump = _playerDefaultPosition.Y >= 400;
            
            //Finally, Update your actual position and state
            _playerDefaultPosition+= _velocity;
            _previousState = _currentState;

            //DEBUG

            //Console.WriteLine("Jumps: " + _noJumps);
            //Console.WriteLine("IsColliding: " + _isColliding);
            //Console.WriteLine("Position: " + _playerDefaultPosition);
            //Console.WriteLine("Acceleration: " + _acceleration.X);
            //Console.WriteLine("Velocity: " + _velocity.X);
            //Console.WriteLine("X: " + _playerDefaultPosition.X + " Y: " + _playerDefaultPosition.Y);
            
        }

        public void jumpHack(float f)
        {
            _canJump = _playerDefaultPosition.Y <= f;
            
        }
    }
}
