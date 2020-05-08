using System;
using System.Threading.Tasks;

namespace OnLine
{
    public class MainModel
    {
        public event EventHandler ToTrayHandler;

        public MainModel()
        {
            AutoMove = true;

            this.AutoMoveCommand = new RelayCommand<bool>(flag =>
            {
                AutoMove = flag;
                if (AutoMove) StartAutoMove();
            });

            this.ToTrayCommand = new RelayCommand<object>(obj =>
            {
                ToTrayHandler?.Invoke(this, new EventArgs());
            });

            StartAutoMove();
        }

        private void StartAutoMove()
        {
            Task.Run(async () =>
            {
                var zig = 1;

                while (AutoMove)
                {
                    Jiggler.Jiggle(4 * zig, 4 * zig);
                    zig = zig == 1 ? -1 : 1;

                    await Task.Delay(10000);
                }
            });
        }

        public bool AutoMove { get; set; }
        public bool InTray { get; set; }

        public RelayCommand<bool> AutoMoveCommand { get; set; }
        public RelayCommand<object> ToTrayCommand { get; set; }
    }
}
