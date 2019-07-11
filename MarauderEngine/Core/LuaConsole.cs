using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MarauderEngine.Systems;
using Microsoft.Xna.Framework.Input;
using NLua;

namespace MarauderEngine.Core
{
    public class LuaConsole : SystemManager<LuaConsole>
    {

        private Keys[] lastPressedKeys;
        public string text;
        public string lastText;
        public bool showDebug;

        private KeyboardState kbState = Keyboard.GetState();
        private KeyboardState oldKeyboardState;
        
        private Lua lua;
        
        public LuaConsole()
        {
            lastPressedKeys = new Keys[0];
        }
        public struct Function
        {
            public string Path;
            public object Target;
            public MethodBase Method;
        }
        private List<Function> _luaFunctions = new List<Function>();
        private void LoadLuaFunctions()
        {
            lua = new Lua();
            lua.LoadCLRPackage();
            lua.RegisterFunction("SetVerbosityLevel",this, GetType().GetMethod("SetVerbosityLevel"));
            lua.RegisterFunction("ClearConsole", this, GetType().GetMethod("ClearConsole"));
            lua.RegisterFunction("SetParticleMax", this, GetType().GetMethod("SetParticleMax"));
            lua.RegisterFunction("SpawnVessel", this, GetType().GetMethod("SpawnVessel"));
            lua.RegisterFunction("ToggleCollider", this, GetType().GetMethod("ToggleCollider"));
            
            for (int i = 0; i < _luaFunctions.Count; i++)
            {
                lua.RegisterFunction(_luaFunctions[i].Path, _luaFunctions[i].Target, _luaFunctions[i].Method);
            }
        }

        public void AddCommand(string path, object target, MethodBase method)
        {
            //if (lua == null)
            //{
            //    LoadLuaFunctions();
            //}
            //lua.RegisterFunction(path, target, method);

            Function tempFunction = new Function()
            {
                Path = path,
                Target = target,
                Method = method
            };

            _luaFunctions.Add(tempFunction);
        }

        public void ToggleCollider()
        {

        }

        public void SpawnVessel()
        {
             //VesselManager.GetInstance().AddVessel(new Vessel(Game1.worldPosition));
        }

        public void SetParticleMax(int max)
        {
            ParticleSystem.Init(max);
        }

        public void SetVerbosityLevel(int level)
        {
            Debug.VerbosityLevel = level;
            Debug.Log($"Set verbosity level to {level}", Debug.LogType.Warning, 1);
        }

        public void ClearConsole()
        {
            Debug.Clear();
        }

        public void Update()
        {
            kbState = Keyboard.GetState(); 
            Keys[] pressedKeys = kbState.GetPressedKeys();
            //check if any of the previous update's keys are no longer pressed
            if (kbState.IsKeyDown(Keys.RightShift) == false && kbState.IsKeyDown(Keys.LeftShift) == false)
            {

                foreach (Keys key in pressedKeys)
                {
                    if (!lastPressedKeys.Contains(key))
                        OnKeyDown(key);
                }
            }
            else
            {
                foreach (Keys key in pressedKeys)
                {
                    if (!lastPressedKeys.Contains(key))
                        OnKeyDownShift(key);
                }
            }

            //save the currently pressed keys so we can compare on the next update
            lastPressedKeys = pressedKeys;
            oldKeyboardState = kbState;

        }

        private void OnKeyDown(Keys key)
        {
            
            switch (key)
            {
                case Keys.Q:
                    {
                        text += "q";
                        break;
                    }
                case Keys.W:
                    {
                        text += "w";
                        break;
                    }
                case Keys.E:
                    {
                        text += "e";
                        break;
                    }
                case Keys.R:
                    {
                        text += "r";
                        break;
                    }
                case Keys.T:
                    {
                        text += "t";
                        break;
                    }
                case Keys.Y:
                    {
                        text += "y";
                        break;
                    }
                case Keys.U:
                    {
                        text += "u";
                        break;
                    }
                case Keys.I:
                    {
                        text += "i";
                        break;
                    }
                case Keys.O:
                    {
                        text += "o";
                        break;
                    }
                case Keys.P:
                    {
                        text += "p";
                        break;
                    }
                case Keys.A:
                    {
                        text += "a";
                        break;
                    }
                case Keys.S:
                    {
                        text += "s";
                        break;
                    }
                case Keys.D:
                    {
                        text += "d";
                        break;
                    }
                case Keys.F:
                    {
                        text += "f";
                        break;
                    }
                case Keys.G:
                    {
                        text += "g";
                        break;
                    }
                case Keys.H:
                    {
                        text += "h";
                        break;
                    }
                case Keys.J:
                    {
                        text += "j";
                        break;
                    }
                case Keys.K:
                    {
                        text += "k";
                        break;
                    }
                case Keys.L:
                    {
                        text += "l";
                        break;
                    }
                case Keys.Z:
                    {
                        text += "z";
                        break;
                    }
                case Keys.X:
                    {
                        text += "x";
                        break;
                    }
                case Keys.C:
                    {
                        text += "c";
                        break;
                    }
                case Keys.V:
                    {
                        text += "v";
                        break;
                    }
                case Keys.B:
                    {
                        text += "b";
                        break;
                    }
                case Keys.N:
                    {
                        text += "n";
                        break;
                    }
                case Keys.M:
                    {
                        text += "m";
                        break;
                    }
                case Keys.Space:
                    {
                        text += " ";
                        break;
                    }
                case Keys.D1:
                    {
                        text += "1";
                        break;
                    }
                case Keys.D2:
                    {
                        text += "2";
                        break;
                    }
                case Keys.D3:
                    {
                        text += "3";
                        break;
                    }
                case Keys.D4:
                    {
                        text += "4";
                        break;
                    }
                case Keys.D5:
                    {
                        text += "5";
                        break;
                    }
                case Keys.D6:
                    {
                        text += "6";
                        break;
                    }
                case Keys.D7:
                    {
                        text += "7";
                        break;
                    }
                case Keys.D8:
                    {
                        text += "8";
                        break;
                    }
                case Keys.D9:
                    {
                        text += "9";
                        break;
                    }
                case Keys.D0:
                    {
                        text += "0";
                        break;
                    }
                case Keys.OemComma:
                    {
                        text += ",";
                        break;
                    }
                case Keys.OemPeriod:
                {
                    text += ".";
                    break;
                }
                case Keys.Enter:
                    {
                        EnterCommand(text);
                        break;
                    }
                case Keys.Back:
                    {
                        // checks if text doesnt equal null and that it can back space

                        if (text != null)
                        {
                            if (String.Compare(text, " ") < 0)
                            {
                                text = text.Substring(0, text.Length - 1 + 1);
                            }
                            else
                            {
                                text = text.Substring(0, text.Length - 1);
                            }
                        }

                        break;
                    }

            }
        }

        private void OnKeyDownShift(Keys key)
        {
            switch (key)
            {
                case Keys.Q:
                    {
                        text += "Q";
                        break;
                    }
                case Keys.W:
                    {
                        text += "W";
                        break;
                    }
                case Keys.E:
                    {
                        text += "E";
                        break;
                    }
                case Keys.R:
                    {
                        text += "R";
                        break;
                    }
                case Keys.T:
                    {
                        text += "T";
                        break;
                    }
                case Keys.Y:
                    {
                        text += "Y";
                        break;
                    }
                case Keys.U:
                    {
                        text += "U";
                        break;
                    }
                case Keys.I:
                    {
                        text += "I";
                        break;
                    }
                case Keys.O:
                    {
                        text += "O";
                        break;
                    }
                case Keys.P:
                    {
                        text += "P";
                        break;
                    }
                case Keys.A:
                    {
                        text += "A";
                        break;
                    }
                case Keys.S:
                    {
                        text += "S";
                        break;
                    }
                case Keys.D:
                    {
                        text += "D";
                        break;
                    }
                case Keys.F:
                    {
                        text += "F";
                        break;
                    }
                case Keys.G:
                    {
                        text += "G";
                        break;
                    }
                case Keys.H:
                    {
                        text += "H";
                        break;
                    }
                case Keys.J:
                    {
                        text += "J";
                        break;
                    }
                case Keys.K:
                    {
                        text += "K";
                        break;
                    }
                case Keys.L:
                    {
                        text += "L";
                        break;
                    }
                case Keys.Z:
                    {
                        text += "Z";
                        break;
                    }
                case Keys.X:
                    {
                        text += "X";
                        break;
                    }
                case Keys.C:
                    {
                        text += "C";
                        break;
                    }
                case Keys.V:
                    {
                        text += "V";
                        break;
                    }
                case Keys.B:
                    {
                        text += "B";
                        break;
                    }
                case Keys.N:
                    {
                        text += "N";
                        break;
                    }
                case Keys.M:
                    {
                        text += "M";
                        break;
                    }
                case Keys.Space:
                    {
                        text += " ";
                        break;
                    }
                case Keys.Enter:
                    {
                        EnterCommand(text);
                        break;
                    }
                case Keys.D9:
                    {
                        text += "(";
                        break;
                    }
                case Keys.D0:
                    {
                        text += ")";
                        break;
                    }
                case Keys.OemSemicolon:
                    {
                        text += ";";
                        break;
                    }
                case Keys.OemQuotes:
                    {
                        text += "\"";
                        break;
                    }
                case Keys.Back:
                    {
                        if (String.Compare(text, " ") < 0)
                        {
                            text = text.Substring(0, text.Length - 1 + 1);
                        }
                        else
                        {
                            text = text.Substring(0, text.Length - 1);
                        }
                        break;
                    }

            }
        }

        public void EnterCommand(string com)
        {
            //Game1.system1.ExecuteLuaCommand(com);

            LoadLuaFunctions();
            try
            {
                lua.DoString(text);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString(), Debug.LogType.Error, 1);
            }

            lastText = text;
            text = ""; 

        }
        private void OnKeyUp(Keys key)
        {

        }

        public override void Initialize()
        {
            
        }
    }
}
