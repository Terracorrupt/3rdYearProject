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
    class LevelBuilder
    {
        Texture2D                       _tile;
        Texture2D                       _debugTex;
        Rectangle[,]                  _tileRectangles;
        Rectangle[,]                _bottomTileBounds, _topTileBounds, _leftTileBounds, _rightTileBounds;
        int[,]                    _tilePositions;
        const int _tileRows = 12;
        const int _tileColumns= 50;
        float _tempY;
        

        public LevelBuilder(ContentManager c)
        {
            _tile = c.Load<Texture2D>("Tiles\\tile");
            _debugTex = c.Load<Texture2D>("PlayerSprites\\debugRec");

            _tileRectangles = new Rectangle[_tileRows, _tileColumns];
            _bottomTileBounds = new Rectangle[_tileRows, _tileColumns];
            _topTileBounds = new Rectangle[_tileRows, _tileColumns];
            _leftTileBounds = new Rectangle[_tileRows, _tileColumns];
            _rightTileBounds = new Rectangle[_tileRows, _tileColumns];

            _tilePositions = new int[_tileRows, _tileColumns]
            {

            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,1,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}

            };
        }

        public void Update(GameTime gametime, Player p)
        {
            for (int i = 0; i < _tileRows; i++)
            {
                for (int j = 0; j < _tileColumns; j++)
                {


                    //Top Collision (This was a pain...)
                    if (p._playerDefaultRectangle.Intersects(_topTileBounds[i, j]) && p._isColliding == false)
                    {
                        p._playerDefaultPosition = new Vector2(p._playerDefaultPosition.X, _topTileBounds[i, j].Y - p._playerDefaultRectangle.Height);
                        p._canJump = true;
                        p.jumpHack(_topTileBounds[i, j].Y - p._playerDefaultRectangle.Height);
                        p._isColliding = true;
                        p._acceleration.Y = 0;
                        p._velocity.Y = 0;
                    }
                    if (p._playerDefaultPosition.Y > _tempY + 0.89)
                    {
                        //Console.WriteLine("Position: " + p._playerDefaultPosition);

                        p._isColliding = false;
                    }
                    else if ((Keyboard.GetState().IsKeyDown(Keys.Z)) || (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A)))
                    {
                        p._isColliding = false;
                    }

                    _tempY = p._playerDefaultPosition.Y;

                    //Bottom Collision
                    if (p._playerDefaultRectangle.Intersects(_bottomTileBounds[i, j])&&(!p._playerDefaultRectangle.Intersects(_leftTileBounds[i, j])))
                    {
                        p._playerDefaultPosition = new Vector2(p._playerDefaultPosition.X, _topTileBounds[i, j].Y + _topTileBounds[i, j].Height+41);
                        p._acceleration.Y = 0;
                        p._velocity.Y = 0;
                    }

                    //Left Collision
                    if (p._playerDefaultRectangle.Intersects(_leftTileBounds[i, j]) && (!p._playerDefaultRectangle.Intersects(_topTileBounds[i, j])) && (!p._playerDefaultRectangle.Intersects(_bottomTileBounds[i, j])))
                    {
                        p._playerDefaultPosition = new Vector2(_leftTileBounds[i, j].X-p._playerDefaultRectangle.Width, p._playerDefaultPosition.Y);
                        p._acceleration.X = 0;
                        p._velocity.X = 0;
                    }
                    //Left Collision
                    if (p._playerDefaultRectangle.Intersects(_rightTileBounds[i, j]) && (!p._playerDefaultRectangle.Intersects(_topTileBounds[i, j])) && (!p._playerDefaultRectangle.Intersects(_bottomTileBounds[i, j])))
                    {
                        p._playerDefaultPosition = new Vector2(_rightTileBounds[i, j].X+5, p._playerDefaultPosition.Y);
                        p._acceleration.X = 0;
                        p._velocity.X = 0;
                    }


                }
            }
        }

        public void buildLevel()
        {
            for (int i = 0; i < _tileRows; i++)
            {
                for (int j = 0; j < _tileColumns; j++)
                {

                    if (_tilePositions[i,j] == 1)
                    {
                        _tileRectangles[i, j] = new Rectangle(j*40, i*40, 40, 40);

                        _bottomTileBounds[i, j] = new Rectangle((j*40)+5,(i*40)+35,30,5);
                        _topTileBounds[i, j] = new Rectangle((j * 40) + 5,(i * 40), 32, 5);
                        _leftTileBounds[i, j] = new Rectangle((j * 40), (i * 40)+5, 5, 30);
                        _rightTileBounds[i, j] = new Rectangle((j * 40)+35, (i * 40) + 5, 5, 30);
                    }
                }
            }
        }



        public void Draw(SpriteBatch s)
        {
            for (int i = 0; i < _tileRows; i++)
            {
                for (int j = 0; j < _tileColumns; j++)
                {

                    if (_tilePositions[i, j] == 1)
                    {
                        s.Draw(_tile,_tileRectangles[i,j],Color.White);
                        //s.Draw(_debugTex, _bottomTileBounds[i, j], Color.White);
                        //s.Draw(_debugTex, _topTileBounds[i, j], Color.Green);
                        //s.Draw(_debugTex, _leftTileBounds[i, j], Color.Purple);
                        //s.Draw(_debugTex, _rightTileBounds[i, j], Color.Yellow);
                    }
                }
            }
        }



    }
}
