using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3rdYearProject
{
    public class Camera
    {
        public Viewport                     _viewport;
        private float                       _zoom;
        private float                       _rotation;
        private Matrix                      _transform;
        private Matrix                      _inverseTransform;
        private Vector2                     _pos;
        private MouseState                  _mState;
        private KeyboardState               _keyState;
        private Int32                       _scroll;


        public Matrix Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }
        

        public Camera(Viewport viewport)
        {
            _viewport = viewport;
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
            _scroll = 1;
        }

        public void Update()
        {

            Input();
            //Clamp zoom value
            _zoom = MathHelper.Clamp(_zoom, 0.0f, 10.0f);
            //Clamp rotation value
            _rotation = ClampAngle(_rotation);
            //Create view matrix
            _transform = Matrix.CreateRotationZ(_rotation) *
                            Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) *
                            Matrix.CreateTranslation(_pos.X, _pos.Y, 0);
            //Update inverse matrix
            _inverseTransform = Matrix.Invert(_transform);
        }

        public void forward()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                _pos.X -= 5.4f;
            }
            else
            {
                _pos.X -= 3.6f;
            }
        }

        public void backward()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                _pos.X += 5.4f;
            }
            else
            {
                _pos.X += 3.6f;
            }
        }

        protected virtual void Input()
        {
            _mState = Mouse.GetState();
            _keyState = Keyboard.GetState();
            //Check zoom
            if (_mState.ScrollWheelValue > _scroll)
            {
                _zoom += 0.1f;
                _scroll = _mState.ScrollWheelValue;
            }
            else if (_mState.ScrollWheelValue < _scroll)
            {
                _zoom -= 0.1f;
                _scroll = _mState.ScrollWheelValue;
            }
        }

        
        //Clamps a radian value between -pi and pi
        protected float ClampAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }
    }
}


