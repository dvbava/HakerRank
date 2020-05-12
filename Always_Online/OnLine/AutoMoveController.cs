using System;
using System.Threading.Tasks;

namespace OnLine
{
    public class AutoMoveController
    {
        public event EventHandler<string> StatusChanged;

        protected virtual void OnStatusChanged(string message)
        {
            EventHandler<string> handler = StatusChanged;
            handler?.Invoke(this, message);
        }

        public bool AutoMove { get; private set; }

        public void Stop()
        {
            AutoMove = false;
            OnStatusChanged("Auto move stopped.");
        }

        public void Start()
        {
            AutoMove = true;
            StartAutoMove();
            OnStatusChanged("Auto move started.");
        }

        private void StartAutoMove()
        {
            Task.Run(async () =>
            {
                var zig = 4;

                while (AutoMove)
                {
                    try
                    {
                        OnStatusChanged($"Auto move started. ({zig},{zig})");
                        Jiggler.Jiggle(zig, zig);
                    }
                    catch (Exception ex)
                    {
                        OnStatusChanged("Error: " + ex.Message);
                    }
                    zig = zig == 4 ? 0 : 4;

                    await Task.Delay(5000);
                }
            });
        }
    }
}
