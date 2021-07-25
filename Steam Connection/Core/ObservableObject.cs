using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Steam_Connection.Core
{
    class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static PropertyChangedEventHandler StaticPropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        static protected void OnStaticPropertyChanged(string pname)
        {
            System.ComponentModel.PropertyChangedEventArgs e = new System.ComponentModel.PropertyChangedEventArgs(pname);
            System.ComponentModel.PropertyChangedEventHandler h = StaticPropertyChanged;
            if (h != null)
                h(null, e);

        }
    }
}
