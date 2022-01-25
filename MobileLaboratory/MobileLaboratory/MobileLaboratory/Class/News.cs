using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MobileLaboratory.Class
{
    public class News : INotifyPropertyChanged
    {
        private string title;
        private string text;
        private Nullable<System.DateTime> date;

      
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("title");
                }
            }
        }
        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged("text");
                }
            }
        }
        public Nullable<System.DateTime> Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("date");
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
