﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;
using static AsciiEngine.Utility;

namespace AsciiEngine
{
    public class Input
    {
        public Dictionary<Key, bool> KeyDictionary = new Dictionary<Key, bool>
        {
            {Key.Down, false },
            {Key.Up, false },
            {Key.Left, false },
            {Key.Right, false },
            {Key.Z, false },
            {Key.Escape, false },
            {Key.F1, false },
            {Key.F12, false }
        };

        private void StartThread()
        {
            Thread InputThread = new Thread(() =>
            {
                double ticks = GetEngineTicks();
                while(!Core.EndProcesses)
                {
                    //ticks = GetEngineTicks();
                    KeyDictionary[Key.Down] = Keyboard.IsKeyDown(Key.Down);
                    KeyDictionary[Key.Up] = Keyboard.IsKeyDown(Key.Up);
                    KeyDictionary[Key.Right] = Keyboard.IsKeyDown(Key.Right);
                    KeyDictionary[Key.Left] = Keyboard.IsKeyDown(Key.Left);
                    KeyDictionary[Key.Z] = Keyboard.IsKeyDown(Key.Z);
                    KeyDictionary[Key.Escape] = Keyboard.IsKeyDown(Key.Escape);
                    KeyDictionary[Key.F1] = Keyboard.IsKeyDown(Key.F1);
                    KeyDictionary[Key.F12] = Keyboard.IsKeyDown(Key.F12);
                    UpdateSystemInput();
                    Thread.Sleep(8);
                }
 
                // do something with retVal
            });
            InputThread.SetApartmentState(ApartmentState.STA);
            InputThread.Start();
        }

        public Input()
        {
            StartThread();
        }

        private void UpdateSystemInput()
        {
            if (KeyDictionary[Key.F1])
            {
                switch (Core.Engine.GameUpdate.FrameRate)
                {
                    case 60:
                        Core.Engine.GameUpdate.FrameRate = 999;
                        break;
                    case 30:
                        Core.Engine.GameUpdate.FrameRate = 60;
                        break;
                    default:
                        Core.Engine.GameUpdate.FrameRate = 30;
                        break;
                }

            }
            if (KeyDictionary[Key.F12])
            {
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                //Core.Engine.Display.Update();
                Core.Engine.Camera.FitScreenSize();

            }
        }
    }
}
