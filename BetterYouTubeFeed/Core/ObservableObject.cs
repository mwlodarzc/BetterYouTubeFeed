using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BetterYouTubeFeed.Core
{
    internal class ObservableObject : INotifyPropertyChanged // klasa używana do handlowania zmian interfejsu

    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string callMemberName = null)
        {
            //invoke if its not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callMemberName));
        }
    }
};
