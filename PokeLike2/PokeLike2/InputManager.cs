using System;
using Microsoft.Xna.Framework.Input;

namespace PokeLike2
{
    static class InputManager
    {
        public delegate void InputEventHandler(Keys key);
        public static event InputEventHandler OnKeyPressed, OnKeyReleased, OnKeyDown;

        private static Keys[] lastFrameKeys = new Keys[0];
        private static Keys[] currentKeys = new Keys[0];

        public static void Update()
        {
            lastFrameKeys = currentKeys;

            KeyboardState keyState = Keyboard.GetState();
            currentKeys = keyState.GetPressedKeys();

            foreach (var key in currentKeys)
            {
                if (OnKeyDown != null)
                    OnKeyDown(key);

                bool isAlreadyPressed = false;
                foreach (var lastKey in lastFrameKeys)
                    if (key == lastKey)
                        isAlreadyPressed = true;

                if (!isAlreadyPressed)
                    if (OnKeyPressed != null)
                        OnKeyPressed(key);
            }

            foreach (var key in lastFrameKeys)
            {
                bool isReleased = keyState.IsKeyUp(key);
                if (isReleased)
                    if (OnKeyReleased != null)
                        OnKeyReleased(key);
            }
        }
    }
}
