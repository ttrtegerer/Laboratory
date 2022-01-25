using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MobileLaboratory
{
    public class Yslugi : INotifyPropertyChanged
    {
        private double prise;
        private string name;
        
        public double Prise
        {
            get { return prise; }
            set
            {
                if (prise != value)
                {
                    prise = value;
                    OnPropertyChanged("id");
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("dateTime");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}