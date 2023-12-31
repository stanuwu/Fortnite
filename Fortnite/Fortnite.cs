﻿using System;
using System.Diagnostics;
using System.Windows;
using Fortnite.Features;
using Fortnite.Game;
using Fortnite.Render;

namespace Fortnite
{
    // IDisposable
    // Disposes when closed to avoid memory leaks
    // All IDisposable classes used by this program should be stored and disposed in here
    public class Fortnite : Application, IDisposable
    {
        // Client Version
        public const int Version = 1;

        public Fortnite()
        {
            Startup += (sender, args) => OnStartup();
            Exit += (sender, args) => Dispose();
        }

        private GameProcess GameProcess { get; set; }
        private GameData GameData { get; set; }
        private WindowOverlay WindowOverlay { get; set; }
        private Graphics Graphics { get; set; }
        private AimBot AimBot { get; set; }
        private TriggerBot TriggerBot { get; set; }

        public void Dispose()
        {
            GameData.Dispose();
            GameData = default;

            GameProcess.Dispose();
            GameProcess = default;

            WindowOverlay.Dispose();
            WindowOverlay = default;

            Graphics.Dispose();
            Graphics = default;

            AimBot.Dispose();
            AimBot = default;

            TriggerBot.Dispose();
            TriggerBot = default;
        }

        private void OnStartup()
        {
            // set process priority
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            GameProcess = new GameProcess();
            GameData = new GameData(GameProcess);
            WindowOverlay = new WindowOverlay(GameProcess);
            Graphics = new Graphics(GameProcess, GameData, WindowOverlay);
            AimBot = new AimBot(GameProcess, GameData);
            TriggerBot = new TriggerBot(GameProcess, GameData);

            GameProcess.Start();
            GameData.Start();
            WindowOverlay.Start();
            Graphics.Start();
            AimBot.Start();
            TriggerBot.Start();
        }
    }
}