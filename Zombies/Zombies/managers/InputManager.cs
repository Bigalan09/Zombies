using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zombies.managers
{
    class InputManager
    {
        private KeyboardState lastKeyState;
        private MouseState lastMouseState;

        public Vector2 MousePosition()
        {
            return new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }
        public bool MouseLeftClick()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public bool MouseLeftPressed()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public bool MouseLeftRelease()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        public bool MouseRightClick()
        {
            if (Mouse.GetState().RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public bool MouseRightPressed()
        {
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public bool MouseRightRelease()
        {
            if (Mouse.GetState().RightButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        public void SetLastState()
        {
            lastKeyState = Keyboard.GetState();
            lastMouseState = Mouse.GetState();
        }

        public bool ScrollWheelInc()
        {
            if (Mouse.GetState().ScrollWheelValue > lastMouseState.ScrollWheelValue)
                return true;
            else
                return false;
        }

        public bool ScrollWheelDec()
        {
            if (Mouse.GetState().ScrollWheelValue < lastMouseState.ScrollWheelValue)
                return true;
            else
                return false;
        }

        public bool KeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        public bool KeyClicked(Keys key)
        {
            return lastKeyState.IsKeyUp(key) && Keyboard.GetState().IsKeyDown(key);
        }

        public List<Keys> GetPressedKeys()
        {
            List<Keys> l = new List<Keys>();

            foreach (Keys key in Keyboard.GetState().GetPressedKeys())
            {
                if (lastKeyState.IsKeyUp(key))
                    l.Add(key);
            }

            return l;
        }

        public String TranslateKey(Keys key)
        {
            bool shift = Game1.Instance.InputManager.KeyDown(Keys.LeftShift) ||
                         Game1.Instance.InputManager.KeyDown(Keys.RightShift);
            bool alt = Game1.Instance.InputManager.KeyDown(Keys.LeftAlt) ||
                       Game1.Instance.InputManager.KeyDown(Keys.RightAlt);

            int len = key.ToString().Length;

            if (alt)
            {
                if (key == Keys.D2)
                    return "@";
                if (key == Keys.D3)
                    return "";
                if (key == Keys.D7)
                    return "{";
                if (key == Keys.D8)
                    return "[";
                if (key == Keys.D9)
                    return "]";
                if (key == Keys.D0)
                    return "}";
                if (key == Keys.OemSemicolon)
                    return "~";
                if (key == Keys.OemBackslash)
                    return "|";
            }
            else
            {
            }

            if (shift)
            {
                if (key == Keys.D0)
                    return "=";
                if (key == Keys.D1)
                    return "!";
                if (key == Keys.D2)
                    return "\"";
                if (key == Keys.D3)
                    return "#";
                if (key == Keys.D4)
                    return "";
                if (key == Keys.D5)
                    return "%";
                if (key == Keys.D6)
                    return "&";
                if (key == Keys.D7)
                    return "/";
                if (key == Keys.D8)
                    return "(";
                if (key == Keys.D9)
                    return ")";
                if (key == Keys.OemPeriod)
                    return ":";
                if (key == Keys.OemComma)
                    return ";";
                if (key == Keys.OemMinus || key == Keys.Subtract)
                    return "_";
                if (key == Keys.OemQuestion)
                    return "*";
                if (key == Keys.OemSemicolon)
                    return "^";
                if (key == Keys.OemBackslash)
                    return ">";

                if (len == 1 && (int)key.ToString()[0] >= 65 && (int)key.ToString()[0] <= 90)
                    return key.ToString();
            }
            else
            {
                if (key == Keys.D0 || key == Keys.NumPad0)
                    return "0";
                if (key == Keys.D1 || key == Keys.NumPad1)
                    return "1";
                if (key == Keys.D2 || key == Keys.NumPad2)
                    return "2";
                if (key == Keys.D3 || key == Keys.NumPad3)
                    return "3";
                if (key == Keys.D4 || key == Keys.NumPad4)
                    return "4";
                if (key == Keys.D5 || key == Keys.NumPad5)
                    return "5";
                if (key == Keys.D6 || key == Keys.NumPad6)
                    return "6";
                if (key == Keys.D7 || key == Keys.NumPad7)
                    return "7";
                if (key == Keys.D8 || key == Keys.NumPad8)
                    return "8";
                if (key == Keys.D9 || key == Keys.NumPad9)
                    return "9";
                if (key == Keys.OemPeriod)
                    return ".";
                if (key == Keys.OemComma)
                    return ",";
                if (key == Keys.OemPlus || key == Keys.Add)
                    return "+";
                if (key == Keys.OemMinus || key == Keys.Subtract)
                    return "-";
                if (key == Keys.Multiply)
                    return "*";
                if (key == Keys.OemQuestion)
                    return "'";
                if (key == Keys.OemSemicolon)
                    return "";
                if (key == Keys.OemBackslash)
                    return "<";

                if (len == 1 && (int)key.ToString()[0] >= 65 && (int)key.ToString()[0] <= 90)
                    return key.ToString().ToLower();
            }

            if (key == Keys.Space)
                return " ";

            return "";
        }

        public bool KeyReleased(Keys key)
        {
            return lastKeyState.IsKeyDown(key) && Keyboard.GetState().IsKeyUp(key);
        }
    }
}
