using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Threading;
using Fortnite.Game;
using Fortnite.Sys;
using Fortnite.Sys.Structs;
using Fortnite.Utils;
using Application = System.Windows.Application;
using Point = System.Drawing.Point;

namespace Fortnite.Render
{
    public class WindowOverlay : TickThread
    {
        public WindowOverlay(GameProcess gameProcess) : base(nameof(WindowOverlay), 100)


        {
            GameProcess = gameProcess;

            Window = new Form
            {
                Name = "Overlay Window",
                Text = "Overlay Window",
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.None,
                TopMost = true,
                Width = 16,
                Height = 16,
                Left = -32000,
                Top = -32000,
                StartPosition = FormStartPosition.Manual
            };

            Window.Load += (sender, args) =>
            {
                var exStyle = User32.GetWindowLong(Window.Handle, User32.GWL_EXSTYLE);
                exStyle |= User32.WS_EX_LAYERED;
                exStyle |= User32.WS_EX_TRANSPARENT;

                User32.SetWindowLong(Window.Handle, User32.GWL_EXSTYLE, (IntPtr)exStyle);

                User32.SetLayeredWindowAttributes(Window.Handle, 0, 255, User32.LWA_ALPHA);
            };
            Window.SizeChanged += (sender, args) => ExtendFrameIntoClientArea();
            Window.LocationChanged += (sender, args) => ExtendFrameIntoClientArea();
            Window.Closed += (sender, args) => Application.Current.Shutdown();

            Window.Show();
        }

        private GameProcess GameProcess { get; set; }

        public Form Window { get; private set; }

        public override void Dispose()
        {
            base.Dispose();

            Window.Close();
            Window.Dispose();
            Window = default;

            GameProcess = default;
        }

        private void ExtendFrameIntoClientArea()
        {
            var margins = new Margins
            {
                Left = -1,
                Right = -1,
                Top = -1,
                Bottom = -1
            };
            Dwmapi.DwmExtendFrameIntoClientArea(Window.Handle, ref margins);
        }

        protected override void Tick()
        {
            Update(GameProcess.WindowRectangle);
        }

        private void Update(Rectangle windowRectangleClient)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Window.Location == windowRectangleClient.Location && Window.Size == windowRectangleClient.Size) return;
                if (windowRectangleClient.Width > 0 && windowRectangleClient.Height > 0)
                {
                    Window.Location = windowRectangleClient.Location;
                    Window.Size = windowRectangleClient.Size;
                }
                else
                {
                    Window.Location = new Point(-32000, -32000);
                    Window.Size = new Size(16, 16);
                }
            }, DispatcherPriority.Normal);
        }
    }
}