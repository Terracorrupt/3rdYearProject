using System;
using System.IO;
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
        Texture2D                       _tile, _spike, _spring;
        Texture2D                       _debugTex;
        Rectangle[,]                    _tileRectangles, _spikeRectangles, _springRectangles;
        Rectangle[,]                    _bottomTileBounds, _topTileBounds, _leftTileBounds, _rightTileBounds,
                                        _spikeTops, _springTops;
        int[,] _tilePositions;
        const int                       _tileRows = 12;
        const int                       _tileColumns= 150;
        float                           _tempY;
        

        public LevelBuilder(ContentManager c)
        {
            _tile = c.Load<Texture2D>("Tiles\\tile");
            _spike = c.Load<Texture2D>("Tiles\\spike");
            _spring = c.Load<Texture2D>("Tiles\\spring");
            _debugTex = c.Load<Texture2D>("PlayerSprites\\debugRec");

            _tileRectangles = new Rectangle[_tileRows, _tileColumns];
            _bottomTileBounds = new Rectangle[_tileRows, _tileColumns];
            _topTileBounds = new Rectangle[_tileRows, _tileColumns];
            _leftTileBounds = new Rectangle[_tileRows, _tileColumns];
            _rightTileBounds = new Rectangle[_tileRows, _tileColumns];

            _spikeRectangles = new Rectangle[_tileRows, _tileColumns];
            _spikeTops = new Rectangle[_tileRows, _tileColumns];

            _springRectangles = new Rectangle[_tileRows, _tileColumns];
            _springTops = new Rectangle[_tileRows, _tileColumns];
            
            _tilePositions = new int[_tileRows, _tileColumns]
            {

            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,1,1,1,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,1,1,0,1,1,1,1,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,1,3,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,3,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,1,1,0,0,0,1,1,1,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},            
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,1,1,1,1,0,0,1,1,1,0,0,1,0,0,0,0,0,0,1,1,1,1,0,0,3,0,0,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,1,1,1,2,2,2,2,2,1,1,2,2,2,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,2,0,0,0,2,0,0,1,2,2,2,2,0,3,0,2,2,0,3,0,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
            };

            
        }

        public void Update(GameTime gametime, Player p)
        {
            for (int i = 0; i < _tileRows; i++)
            {
                for (int j = 0; j < _tileColumns; j++)
                {


                    //Top Collision (This was a pain...)
                    if ((p._playerDefaultRectangle.Intersects(_topTileBounds[i, j]) && p._isColliding == false)) 
                    {
                        p._playerDefaultPosition = new Vector2(p._playerDefaultPosition.X, _tileRectangles[i, j].Y - p._playerDefaultRectangle.Height);
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
                    else if ((Keyboard.GetState().IsKeyDown(Keys.Z)) || (p._gamePadState.IsButtonDown(Buttons.A)))
                    {
                        p._isColliding = false;
                    }

                    _tempY = p._playerDefaultPosition.Y;

                    //Bottom Collision
                    if (p._playerDefaultRectangle.Intersects(_bottomTileBounds[i, j]))
                    {
                        p._playerDefaultPosition = new Vector2(p._playerDefaultPosition.X, _topTileBounds[i, j].Y + _topTileBounds[i, j].Height+41);
                        p._acceleration.Y = 0;
                        p._velocity.Y = 0;
                    }

                    //Left Collision
                    if (p._playerDefaultRectangle.Intersects(_leftTileBounds[i, j]) && (!p._playerDefaultRectangle.Intersects(_topTileBounds[i, j])) && (!p._playerDefaultRectangle.Intersects(_bottomTileBounds[i, j])))
                    {
                        p._playerDefaultPosition = new Vector2(_leftTileBounds[i, j].X-p._playerDefaultRectangle.Width-1, p._playerDefaultPosition.Y);
                        p._acceleration.X = 0;
                        p._velocity.X = 0;
                    }
                    //Right Collision
                    if (p._playerDefaultRectangle.Intersects(_rightTileBounds[i, j]) && (!p._playerDefaultRectangle.Intersects(_topTileBounds[i, j])) && (!p._playerDefaultRectangle.Intersects(_bottomTileBounds[i, j])))
                    {
                        p._playerDefaultPosition = new Vector2(_rightTileBounds[i, j].X+5, p._playerDefaultPosition.Y);
                        p._acceleration.X = 0;
                        p._velocity.X = 0;
                    }

                    //Spikes
                    if (p._playerDefaultRectangle.Intersects(_spikeTops[i, j]))
                    {
                        p._isDead = true;
                    }

                    //Springs
                    if (p._playerDefaultRectangle.Intersects(_springTops[i, j]))
                    {
                        p.manualJump();
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

                        _bottomTileBounds[i, j] = new Rectangle((int)j * 40 + 5, (int)i * 40 + 35, _tile.Width - 8, _tile.Height - 32);
                        _topTileBounds[i, j] = new Rectangle((int)j * 40 + 5, (int)i * 40, _tile.Width - 8, 5);
                        _leftTileBounds[i, j] = new Rectangle((int)j * 40, (int)i * 40 + 7, 4, _tile.Height- 10);
                        _rightTileBounds[i, j] = new Rectangle((int)j * 40 + 37, (int)i * 40 + 7, 4, _tile.Height- 10);
                    }
                    if (_tilePositions[i, j] == 2)
                    {
                        _spikeRectangles[i, j] = new Rectangle(j * 40, i * 40, 40, 40);
                        _spikeTops[i, j] = new Rectangle(j * 40, i * 40, 40, 20);
                    }
                    if (_tilePositions[i, j] == 3)
                    {
                        _springRectangles[i, j] = new Rectangle(j * 40, i * 40, 40, 40);
                        _springTops[i, j] = new Rectangle(j * 40, i * 40+20, 40, 20);
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
                    if (_tilePositions[i, j] == 2)
                    {
                        s.Draw(_spike, _spikeRectangles[i, j], Color.White);
                        //s.Draw(_debugTex, _spikeTops[i, j], Color.Yellow);
                    }
                    if (_tilePositions[i, j] == 3)
                    {
                        s.Draw(_spring, _springRectangles[i, j], Color.White);
                        //s.Draw(_debugTex, _springTops[i, j], Color.Yellow);
                    }
                }
            }
        }
    }
}
