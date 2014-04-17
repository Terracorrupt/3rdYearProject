using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace _3rdYearProject
{
    public class Animation
    {
        float                       _timeIncrement; 
        float                       _currentTime; 
        float                       _previousTime; 
        Vector2                     _sprite;  
        int                         _i; 
        int                         _numberOfFrames;

        public Animation(float _increment, Vector2 _spriteSize, int _numFrames)
        {
            _i = 0;
            _timeIncrement = _increment;
            _sprite = _spriteSize;
            _numberOfFrames = _numFrames;
            _previousTime = System.Environment.TickCount;
            _currentTime = _previousTime;
        }

        public void Update()
        {
            _currentTime = System.Environment.TickCount;
            
            //Change Frame based on time passed in
            if (_currentTime - _previousTime > _timeIncrement)
            {
                _previousTime = System.Environment.TickCount;
                _i++;
            }

            //Reset back to first frame if we're at end of spritesheet
            if (_i > _numberOfFrames - 1)
            {
                _i = 0;
            }
        }

        public Rectangle GetCurrentSprite()
        {
            return new Rectangle((int)(_i * _sprite.X), (int)0, (int)_sprite.X, (int)_sprite.Y);
        }

        public void SetRectangleY(int newY)
        {
            _sprite.Y = newY;        }

        public int GetCurrentFrame()
        {
            return _i;
        }

        public void SetCurrentFrame(int frameNo)
        {
            _i = frameNo;
        }
    }
}
