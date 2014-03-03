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
        public DAO                                  _dao;
        public string                               _userName;
        string                                      _connectionString = "mongodb://localhost";          //Home PC is 192.168.1.16
        string                                      _databaseString = "project";
        string                                      _collectionString = "test1";
        private Microsoft.Xna.Framework.Game        _game;

        //Constructor
        private SceneManager(Microsoft.Xna.Framework.Game game)
        {
            this._game = game;
            _last = State.LOADING;
            _current = State.MENU;

            _dao = new DAO();

            if (_dao.setup(_connectionString, _databaseString, _collectionString))
            {
                Console.WriteLine("Connected to " + _databaseString + " using the " + _collectionString + " collection on " + _connectionString);
            }
            else
            {
                Console.WriteLine("Connection to " + _databaseString + " failed");
            }

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
