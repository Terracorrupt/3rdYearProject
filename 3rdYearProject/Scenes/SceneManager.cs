using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3rdYearProject
{
    class SceneManager
    {
        public enum State                           {LOADING, MENU, LEVEL1, EXIT};
        private static SceneManager                 _manager;
        public static State                         _last;
        private static State                         _current;
        public Scene                                _previous;
        private static Scene                        _active;
        private Microsoft.Xna.Framework.Game        _game;

        //Constructor
        private SceneManager(Microsoft.Xna.Framework.Game game)
        {
            this._game = game;
            _last = State.LOADING;
            _current = State.MENU;
        }

        //Singelton
        public static SceneManager GetInstance(Microsoft.Xna.Framework.Game game)
        {
            if (_manager == null)
                _manager = new SceneManager(game);
            return _manager;
        }

        public State Current
        {
            get { return _current; }
            set { _current = value; }
        }

        public Scene NextScene()
        {
            switch (Current)
            {
                case State.LOADING:
                    break;
                case State.MENU:
                    if (_last != State.MENU)
                    {
                        _active = new Menu(_game);
                        _last = State.MENU;
                        _previous = _active;
                    }
                    break;
                case State.LEVEL1:
                    if (_last != State.LEVEL1)
                    {
                        _active = new LevelOne(_game);
                        _last = State.LEVEL1;
                        _previous = _active;
                    }
                    break;
                case State.EXIT:
                    break;

            }
            return _active;
        }



    }
}
