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
    public class InputName
    {
        string                  _finalMessage;
        string                  _tempMessage;
        int                     _index;
        SpriteFont              _font;
        StringBuilder           _sb;

        public InputName(ContentManager c)
        {
            _tempMessage = "";
            _index = 0;
            _font = c.Load<SpriteFont>("Fonts\\Squarefont");
            _sb = new StringBuilder();
        }

        public void constructMessage(KeyboardState state, KeyboardState previous)
        {

            if (_index <= 14)
            {

                if (state.IsKeyDown(Keys.Q) && (!previous.IsKeyDown(Keys.Q)))
                {
                    if (_index == 0)
                        _sb.Append("Q");
                    else
                        _sb.Append("q");

                    _index++;
                }
                if (state.IsKeyDown(Keys.W) && (!previous.IsKeyDown(Keys.W)))
                {
                    if (_index == 0)
                        _sb.Append("W");
                    else
                        _sb.Append("w");

                    _index++;
                }
                if (state.IsKeyDown(Keys.E) && (!previous.IsKeyDown(Keys.E)))
                {
                    if (_index == 0)
                        _sb.Append("E");
                    else
                        _sb.Append("e");

                    _index++;
                }
                if (state.IsKeyDown(Keys.R) && (!previous.IsKeyDown(Keys.R)))
                {
                    if (_index == 0)
                        _sb.Append("R");
                    else
                        _sb.Append("r");

                    _index++;
                }
                if (state.IsKeyDown(Keys.T) && (!previous.IsKeyDown(Keys.T)))
                {
                    if (_index == 0)
                        _sb.Append("T");
                    else
                        _sb.Append("t");

                    _index++;
                }
                if (state.IsKeyDown(Keys.Y) && (!previous.IsKeyDown(Keys.Y)))
                {
                    if (_index == 0)
                        _sb.Append("Y");
                    else
                        _sb.Append("y");

                    _index++;
                }
                if (state.IsKeyDown(Keys.U) && (!previous.IsKeyDown(Keys.U)))
                {
                    if (_index == 0)
                        _sb.Append("U");
                    else
                        _sb.Append("u");

                    _index++;
                }
                if (state.IsKeyDown(Keys.I) && (!previous.IsKeyDown(Keys.I)))
                {
                    if (_index == 0)
                        _sb.Append("I");
                    else
                        _sb.Append("i");

                    _index++;
                }
                if (state.IsKeyDown(Keys.O) && (!previous.IsKeyDown(Keys.O)))
                {
                    if (_index == 0)
                        _sb.Append("O");
                    else
                        _sb.Append("o");

                    _index++;
                }
                if (state.IsKeyDown(Keys.P) && (!previous.IsKeyDown(Keys.P)))
                {
                    if (_index == 0)
                        _sb.Append("P");
                    else
                        _sb.Append("p");

                    _index++;
                }
                if (state.IsKeyDown(Keys.A) && (!previous.IsKeyDown(Keys.A)))
                {
                    if (_index == 0)
                        _sb.Append("A");
                    else
                        _sb.Append("a");

                    _index++;
                }
                if (state.IsKeyDown(Keys.S) && (!previous.IsKeyDown(Keys.S)))
                {
                    if (_index == 0)
                        _sb.Append("S");
                    else
                        _sb.Append("s");

                    _index++;
                }
                if (state.IsKeyDown(Keys.D) && (!previous.IsKeyDown(Keys.D)))
                {
                    if (_index == 0)
                        _sb.Append("D");
                    else
                        _sb.Append("d");

                    _index++;
                }
                if (state.IsKeyDown(Keys.F) && (!previous.IsKeyDown(Keys.F)))
                {
                    if (_index == 0)
                        _sb.Append("F");
                    else
                        _sb.Append("f");

                    _index++;
                }
                if (state.IsKeyDown(Keys.G) && (!previous.IsKeyDown(Keys.G)))
                {
                    if (_index == 0)
                        _sb.Append("G");
                    else
                        _sb.Append("g");

                    _index++;
                }
                if (state.IsKeyDown(Keys.H) && (!previous.IsKeyDown(Keys.H)))
                {
                    if (_index == 0)
                        _sb.Append("H");
                    else
                        _sb.Append("h");

                    _index++;
                }
                if (state.IsKeyDown(Keys.J) && (!previous.IsKeyDown(Keys.J)))
                {
                    if (_index == 0)
                        _sb.Append("J");
                    else
                        _sb.Append("j");

                    _index++;
                }
                if (state.IsKeyDown(Keys.K) && (!previous.IsKeyDown(Keys.K)))
                {
                    if (_index == 0)
                        _sb.Append("K");
                    else
                        _sb.Append("k");

                    _index++;
                }
                if (state.IsKeyDown(Keys.L) && (!previous.IsKeyDown(Keys.L)))
                {
                    if (_index == 0)
                        _sb.Append("L");
                    else
                        _sb.Append("l");

                    _index++;
                }
                if (state.IsKeyDown(Keys.Z) && (!previous.IsKeyDown(Keys.Z)))
                {
                    if (_index == 0)
                        _sb.Append("Z");
                    else
                        _sb.Append("z");

                    _index++;
                }
                if (state.IsKeyDown(Keys.X) && (!previous.IsKeyDown(Keys.X)))
                {
                    if (_index == 0)
                        _sb.Append("X");
                    else
                        _sb.Append("x");

                    _index++;
                }
                if (state.IsKeyDown(Keys.C) && (!previous.IsKeyDown(Keys.C)))
                {
                    if (_index == 0)
                        _sb.Append("C");
                    else
                        _sb.Append("c");

                    _index++;
                }
                if (state.IsKeyDown(Keys.V) && (!previous.IsKeyDown(Keys.V)))
                {
                    if (_index == 0)
                        _sb.Append("V");
                    else
                        _sb.Append("v");

                    _index++;
                }
                if (state.IsKeyDown(Keys.B) && (!previous.IsKeyDown(Keys.B)))
                {
                    if (_index == 0)
                        _sb.Append("B");
                    else
                        _sb.Append("b");

                    _index++;
                }
                if (state.IsKeyDown(Keys.N) && (!previous.IsKeyDown(Keys.N)))
                {
                    if (_index == 0)
                        _sb.Append("N");
                    else
                        _sb.Append("n");

                    _index++;
                }
                if (state.IsKeyDown(Keys.M) && (!previous.IsKeyDown(Keys.M)))
                {
                    if (_index == 0)
                        _sb.Append("M");
                    else
                        _sb.Append("m");

                    _index++;
                }
                if (state.IsKeyDown(Keys.D1) && (!previous.IsKeyDown(Keys.D1)))
                {
                    if (_index == 0)
                        _sb.Append("1");
                    else
                        _sb.Append("1");

                    _index++;
                }
                if (state.IsKeyDown(Keys.D2) && (!previous.IsKeyDown(Keys.D2)))
                {
                    if (_index == 0)
                        _sb.Append("2");
                    else
                        _sb.Append("2");

                    _index++;
                }
                if (state.IsKeyDown(Keys.D3) && (!previous.IsKeyDown(Keys.D3)))
                {
                    if (_index == 0)
                        _sb.Append("3");
                    else
                        _sb.Append("3");

                    _index++;
                }
                if (state.IsKeyDown(Keys.D4) && (!previous.IsKeyDown(Keys.D4)))
                {
                    if (_index == 0)
                        _sb.Append("4");
                    else
                        _sb.Append("4");

                    _index++;
                }
                if (state.IsKeyDown(Keys.D5) && (!previous.IsKeyDown(Keys.D5)))
                {
                    if (_index == 0)
                        _sb.Append("5");
                    else
                        _sb.Append("5");

                    _index++;
                }
                if (state.IsKeyDown(Keys.D6) && (!previous.IsKeyDown(Keys.D6)))
                {
                    if (_index == 0)
                        _sb.Append("6");
                    else
                        _sb.Append("6");

                    _index++;
                }
                if (state.IsKeyDown(Keys.D7) && (!previous.IsKeyDown(Keys.D7)))
                {
                    if (_index == 0)
                        _sb.Append("7");
                    else
                        _sb.Append("7");

                    _index++;
                }
                if (state.IsKeyDown(Keys.D8) && (!previous.IsKeyDown(Keys.D8)))
                {
                    if (_index == 0)
                        _sb.Append("8");
                    else
                        _sb.Append("8");

                    _index++;
                }
                if (state.IsKeyDown(Keys.D9) && (!previous.IsKeyDown(Keys.D9)))
                {
                    if (_index == 0)
                        _sb.Append("9");
                    else
                        _sb.Append("9");

                    _index++;
                }
                if (state.IsKeyDown(Keys.D0) && (!previous.IsKeyDown(Keys.D0)))
                {
                    if (_index == 0)
                        _sb.Append("0");
                    else
                        _sb.Append("0");

                    _index++;
                }
                if (state.IsKeyDown(Keys.Space) && (!previous.IsKeyDown(Keys.Space)))
                {
                    if (_index == 0)
                        _sb.Append(" ");
                    else
                        _sb.Append(" ");

                    _index++;
                }
                if (state.IsKeyDown(Keys.Decimal) && (!previous.IsKeyDown(Keys.Decimal)))
                {
                    if (_index == 0)
                        _sb.Append(".");
                    else
                        _sb.Append(".");

                    _index++;
                }
                if (state.IsKeyDown(Keys.OemMinus) && (!previous.IsKeyDown(Keys.OemMinus)))
                {
                    if (_index == 0)
                        _sb.Append("-");
                    else
                        _sb.Append("-");

                    _index++;
                }
            }

            _tempMessage = _sb.ToString();
        }

        public string login()
        {
            _finalMessage = _tempMessage;
            return _finalMessage;
        }

        public void Draw(SpriteBatch s)
        {
            s.DrawString(_font, _tempMessage,new Vector2(280,350),Color.White);
        }
    }
}
