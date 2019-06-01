using System.Collections.Generic;
using MarauderEngine.Graphics;
using MarauderEngine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarauderEngine.Core
{

    /// <summary>
    /// an in engine debug method
    /// press F5 to toggle 
    /// </summary>
    public static class Debug
    {
        
        public enum LogType
        {
            Info,
            Warning,
            Error
        }
        public struct LogInformation
        {
            public string Text;
            public LogType LogType;
            public int VerbosityLevel; 
        }
        private static List<LogInformation> Logs = new List<LogInformation>();
        public static int VerbosityLevel = 1;
        public static bool debug;
        public static bool ShowBoundariesAndMesh;
        public static bool SaveLogs = false; 

        /// <summary>
        /// press F5 to toggle debug
        /// </summary>
        public static bool ShowTextualDebug;
        public  static LuaConsole Console = new LuaConsole();

        private static KeyboardState _keyboardState;
        private static KeyboardState _oldKeyboardState;
        private static int savedNum = 1;
        private static string _debugFontName;
        public static void Initialize(string debugFontName)
        {
            _debugFontName = debugFontName; 
        } 

        public static void Update()
        {
            _keyboardState = Keyboard.GetState();

            if (_keyboardState.IsKeyDown(Keys.F5) && _oldKeyboardState.IsKeyUp(Keys.F5))
            {
                ShowTextualDebug = !ShowTextualDebug;
                if (!ShowTextualDebug)
                {
                    Console.showDebug = false; 
                }
            }

            if (_keyboardState.IsKeyDown(Keys.F6) && _oldKeyboardState.IsKeyUp(Keys.F6))
            {
                debug = !debug;

            }

            if (_keyboardState.IsKeyDown(Keys.F7) && _oldKeyboardState.IsKeyUp(Keys.F7))
            {
                ShowBoundariesAndMesh = !ShowBoundariesAndMesh;

            }

            if (ShowTextualDebug)
            {
                if (_keyboardState.IsKeyDown(Keys.OemTilde) && _oldKeyboardState.IsKeyUp(Keys.OemTilde))
                {
                    Console.showDebug = !Console.showDebug;
                }
            }

            if (Console.showDebug)
            {
                Console.Update();
                if (_keyboardState.IsKeyDown(Keys.Enter) && _oldKeyboardState.IsKeyUp(Keys.Enter))
                {

                }
            }

            _oldKeyboardState = _keyboardState;
        }

        public static void Log(object obj, LogType logType = LogType.Info, int verbosity = 1)
        {
            Log(obj.ToString(), logType, verbosity);
        }

        public static void Log(string text, LogType logType = LogType.Info, int verbosityLevel = 1)
        {
            if (verbosityLevel <= VerbosityLevel)
            {
                Logs.Insert(0, new LogInformation()
                {
                    Text = text,
                    LogType = logType,
                    VerbosityLevel = verbosityLevel
                });

                if (Logs.Count > 200)
                {
                    if (SaveLogs)
                    {
                        SaveLog();
                    }
                    Logs.Clear();
                    //Debug.Log("Saved Log", LogType.Warning, 1);
                }
            }
            
        }

        public static void Log(string text, int verbosityLevel)
        {
            if (verbosityLevel <= VerbosityLevel)
            {
                Logs.Insert(0, new LogInformation()
                {
                    Text = text,
                    LogType = LogType.Info,
                    VerbosityLevel = verbosityLevel
                });

                if (Logs.Count > 200)
                {
                    if (SaveLogs)
                    {
                        SaveLog();
                    }
                    Logs.Clear();
                    //Debug.Log("Saved Log", LogType.Warning, 1);
                }
            }

        }

        public static void SaveLog()
        {
            string[] stringLogs = new string[Logs.Count];
            for (int i = 0; i < stringLogs.Length; i++)
            {
                stringLogs[i] =
                    $"{Logs[i].Text} - LogType: {Logs[i].LogType.ToString()} - Verbosity Level:{Logs[i].VerbosityLevel}";
            }
            GameData<string> logData = new GameData<string>();
            logData.folderPath += @"Logs\"; 

            logData.SaveData(stringLogs, $"DebugLog-{savedNum}");
            savedNum++; 
            Logs.Clear();
        }

        public static void Clear()
        {
            Logs.Clear();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (ShowTextualDebug)
            {
                if (Console.showDebug)
                {
                    GUI.GUI.DrawBox(
                        new Rectangle(GUI.GUI.ScreenBounds.X, GUI.GUI.ScreenBounds.Height - 20, Game1.width, 26),
                        Color.Black * .8f);
                    GUI.GUI.DrawString(Console.text,
                        new Vector2(GUI.GUI.ScreenBounds.X, GUI.GUI.ScreenBounds.Height - 20), _debugFontName, 1,
                        Color.White);

                }

                int width = 0;

                for (int i = 0; i < 20; i++)
                {
                    if (i < Logs.Count)
                    {
                        int x = (int) TextureManager.GetContent<SpriteFont>(_debugFontName).MeasureString(Logs[i].Text)
                            .X;
                        if (x > width)
                        {
                            width = x;
                        }
                    }
                }

                GUI.GUI.DrawBox(GUI.GUI.ScreenBounds.X, GUI.GUI.ScreenBounds.Height - 430, width + 10, 400,
                    Color.Black * .9f);
                for (int i = 0; i < 20; i++)
                {
                    if (i < Logs.Count)
                    {
                        if (Logs[i].VerbosityLevel <= VerbosityLevel)
                        {

                            switch (Logs[i].LogType)
                            {
                                case LogType.Error:
                                {
                                    GUI.GUI.DrawString(Logs[i].Text,
                                        new Vector2(GUI.GUI.ScreenBounds.X,
                                            GUI.GUI.ScreenBounds.Height - 50 - (i * 20)), _debugFontName,
                                        1, Color.Red);
                                    break;
                                }
                                case LogType.Warning:
                                {
                                    GUI.GUI.DrawString(Logs[i].Text,
                                        new Vector2(GUI.GUI.ScreenBounds.X,
                                            GUI.GUI.ScreenBounds.Height - 50 - (i * 20)), _debugFontName,
                                        1, Color.Yellow);
                                    break;

                                }
                                case LogType.Info:
                                {
                                    GUI.GUI.DrawString(Logs[i].Text,
                                        new Vector2(GUI.GUI.ScreenBounds.X,
                                            GUI.GUI.ScreenBounds.Height - 50 - (i * 20)), _debugFontName,
                                        1, Color.White);
                                    break;
                                }
                            }



                        }
                    }

                }
            }
        }

        
    }
}
