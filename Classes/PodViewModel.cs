using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using CoffeePods.Annotations;

namespace CoffeePods.Classes
{
    public class PodViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public Uri ListImageUri { get; set; }
        public Uri DetailImageUri { get; set; }

        public string GroupName { get; set; }

        public bool IsDecaf { get; set; }

        public int Strength { get; set; }
        public string Description { get; set; }

        public Color BackgroundColour { get; set; }

        public CupSizeEnum CupSizes { get; set; }

        public PodViewModel Clone()
        {
            return (PodViewModel)MemberwiseClone();
        }

        public bool IsFavourite
        {
            get
            {
                return App.ViewModel.FavouritePods.Any(x => x.Name == this.Name);
            }
            set
            {
                // do nowt
                OnPropertyChanged();
                OnPropertyChanged("FavouriteColour");
            }
        }

        public Color FavouriteColour
        {
            get { return IsFavourite ? BackgroundColour : Colors.Transparent; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
