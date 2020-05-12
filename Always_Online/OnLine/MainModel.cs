using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OnLine
{
    public class MainModel : INotifyPropertyChanged
    {
        public event EventHandler ToTrayHandler;
        public event PropertyChangedEventHandler PropertyChanged;

        private AutoMoveController moveController = null;
        private bool autoMove;
        private string autoMoveStatus;

        public MainModel()
        {
            AutoMove = true;
            moveController = new AutoMoveController();
            moveController.StatusChanged += MoveController_StatusChanged;

            this.AutoMoveCommand = new RelayCommand<bool>(flag =>
            {
                AutoMove = flag;
                if (AutoMove)
                {
                    moveController.Start();
                }
                else
                {
                    moveController.Stop();
                }
            });

            this.ToTrayCommand = new RelayCommand<object>(obj =>
            {
                ToTrayHandler?.Invoke(this, new EventArgs());
            });

            moveController.Start();
        }

        private void MoveController_StatusChanged(object sender, string message)
        {
            AutoMoveStatus = message;
        }

        public bool AutoMove
        {
            get { return autoMove; }
            set
            {
                autoMove = value;
                OnPropertyChanged();
            }
        }

        public string AutoMoveStatus { get => autoMoveStatus; 
            set
            {
                autoMoveStatus = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool InTray { get; set; }

        public RelayCommand<bool> AutoMoveCommand { get; set; }
        public RelayCommand<object> ToTrayCommand { get; set; }
    }
}
