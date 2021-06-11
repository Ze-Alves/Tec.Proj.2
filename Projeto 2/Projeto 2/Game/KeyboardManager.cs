using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Linq;

namespace IPCA.Monogame
{
    public enum KeysState { Up, Down, GoingUp, GoingDown }



    public class KeyboardManager : GameComponent
    {
        private static KeyboardManager instance;

        private Dictionary<Keys, KeysState> _keyboardState;
        private Dictionary<Keys, Dictionary<KeysState, List<Action>>> func;


        public KeyboardManager(Game game) : base(game)
        {
            if (instance != null) throw new Exception("KeybosrdManager constructor called twice");
            instance = this;

            _keyboardState = new Dictionary<Keys, KeysState>();
            func = new Dictionary<Keys, Dictionary<KeysState, List<Action>>>();
            game.Components.Add(this);
        }


        public static bool IsKeyUp(Keys k)
        {
            return instance._keyboardState.ContainsKey(k) &&
                instance._keyboardState[k] == KeysState.Up;
        }
        public static bool IsKeyDown(Keys k) => instance._keyboardState.ContainsKey(k) &&
            instance._keyboardState[k] == KeysState.Down;
        public static bool IsKeyGoingDown(Keys k) => instance._keyboardState.ContainsKey(k) &&
            instance._keyboardState[k] == KeysState.GoingDown;
        public static bool IsKeyGoingUp(Keys k) => instance._keyboardState.ContainsKey(k) &&
            instance._keyboardState[k] == KeysState.GoingUp;



        public static void Register(Keys key, KeysState state, Action code)
        {
            if (!instance.func.ContainsKey(key))
                instance.func[key] = new Dictionary<KeysState, List<Action>>();
            if (!instance.func[key].ContainsKey(state))
                instance.func[key][state] = new List<Action>();

            instance.func[key][state].Add(code);
            instance._keyboardState[key] = KeysState.Up;

        }



        public override void Update(GameTime gametime)
        {
            KeyboardState state = Keyboard.GetState();
            List<Keys> pressedKeys = state.GetPressedKeys().ToList();
            foreach (Keys k in pressedKeys)
            {
                if (!_keyboardState.ContainsKey(k)) _keyboardState[k] = KeysState.Up;

                switch (_keyboardState[k])
                {
                    case KeysState.Down:
                    case KeysState.GoingDown:
                        _keyboardState[k] = KeysState.Down;
                        break;
                    case KeysState.Up:
                    case KeysState.GoingUp:
                        _keyboardState[k] = KeysState.GoingDown;
                        break;
                }
            }
            foreach (Keys k in _keyboardState.Keys.Except(pressedKeys).ToArray())
            {
                switch (_keyboardState[k])
                {
                    case KeysState.Down:
                    case KeysState.GoingDown:
                        _keyboardState[k] = KeysState.GoingUp;
                        break;
                    case KeysState.Up:
                    case KeysState.GoingUp:
                        _keyboardState[k] = KeysState.Up;
                        break;


                }


            }


            foreach (Keys k in func.Keys)
            {
                    KeysState kstate = _keyboardState[k];
                if (func[k].ContainsKey(kstate))
                {
                    foreach(Action act in func[k][kstate])
                    {
                        act();
                    }
                }
            }


        }
    }
}
