using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl
{
    class KeyCommands:INotifyPropertyChanged
    {
        private string stop = 00.ToString("D2");
        private string up = 01.ToString("D2");
        private string down = 02.ToString("D2");
        private string right = 03.ToString("D2");
        private string left = 06.ToString("D2");
        private string rightup = 04.ToString("D2");
        private string rightdown = 05.ToString("D2");
        private string leftup = 07.ToString("D2");
        private string leftdown = 08.ToString("D2");

        public string Leftdown
        {
            get { return leftdown; }
            set 
            { 
                leftdown = value;
                OnPropertyChanged("Leftdown");
            }
        }

        public string Leftup
        {
            get { return leftup; }
            set 
            { 
                leftup = value;
                OnPropertyChanged("Leftup");
            }
        }

        public string Rightdown
        {
            get { return rightdown; }
            set 
            { 
                rightdown = value;
                OnPropertyChanged("Rightdown");
            }
        }

        public string Rightup
        {
            get { return rightup; }
            set
            { 
                rightup = value;
                OnPropertyChanged("Rightup");
            }
        }


        public string Left
        {
            get { return left; }
            set 
            { 
                left = value;
                OnPropertyChanged("Left");
            }
        }


        public string Right
        {
            get { return right; }
            set 
            { 
                right = value;
                OnPropertyChanged("Right");
            }
        }

        public string Down
        {
            get { return down; }
            set 
            { 
                down = value;
                OnPropertyChanged("Down");
            }
        }

        public string Up
        {
            get { return up; }
            set 
            { 
                up = value;
                OnPropertyChanged("Up");
            }
        }

        public string Stop
        {
            get { return stop; }
            set 
            { 
                stop = value;
                OnPropertyChanged("Stop");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            var temphandler = PropertyChanged;

            if (temphandler != null)
            {
                temphandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
