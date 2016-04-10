using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using CoffeePods.Annotations;

namespace CoffeePods.Classes
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public bool IsDataLoaded;
        private Visibility _noFavouritesVisibility;

        public List<PodViewModel> Pods { get; set; }

        public ObservableCollection<PodViewModel> FavouritePods { get; set; }

        public Visibility NoFavouritesVisibility
        {
            get { return _noFavouritesVisibility; }
            private set
            {
                if (value == _noFavouritesVisibility) return;
                _noFavouritesVisibility = value;
                OnPropertyChanged();
            }
        }

        public void LoadData()
        {
            if (IsDataLoaded)
                return;

            Pods = new List<PodViewModel>();

            AddPod(12, "Kazaar", "Intenso", "kazaar.png", false, Color.FromArgb(255, 0, 0, 44), CupSizeEnum.Ristretto | CupSizeEnum.Espresso, "A daring blend of two Robustas from Brazil and Guatemala, specially prepared for Nespresso, and a separately roasted Arabica from South America, Kazaar is a coffee of exceptional intensity. Its powerful bitterness and notes of pepper are balanced by a full and creamy texture.");
            AddPod(11, "Dharkan", "Intenso", "dharkan.png", false, Color.FromArgb(255, 27, 58, 65), CupSizeEnum.Ristretto | CupSizeEnum.Espresso, "This blend of Arabicas from Latin America and Asia fully unveils its character thanks to the technique of long roasting at a low temperature. Its powerful personality reveals intense roasted notes together with hints of bitter cocoa powder and toasted cereals that express themselves in a silky and velvety texture.");
            AddPod(10, "Ristretto", "Intenso", "ristretto.png", false, Color.FromArgb(255, 0, 0, 1), CupSizeEnum.Ristretto | CupSizeEnum.Espresso, "A blend of South American and East African Arabicas, with a touch of Robusta, roasted separately to create the subtle fruity note of this full-bodied, intense espresso.");
            AddPod(9, "Arpeggio", "Intenso", "arpeggio.png", false, Color.FromArgb(255, 87, 46, 135), CupSizeEnum.Ristretto | CupSizeEnum.Espresso, "A dark roast of pure South and Central American Arabicas, Arpeggio has a strong character and intense body, enhanced by cocoa notes.");
            AddPod(8, "Roma", "Intenso", "roma.png", false, Color.FromArgb(255, 45, 41, 40), CupSizeEnum.Ristretto | CupSizeEnum.Espresso, "The balance of lightly roasted South and Central American Arabicas with Robusta, gives Roma sweet and woody notes and a full, lasting taste on the palate.");

            AddPod(6, "Livanto", "Espresso", "livanto.png", false, Color.FromArgb(255, 187, 103, 36), CupSizeEnum.Espresso, "A pure Arabica from South and Central America, Livanto is a well-balanced espresso characterised by roasted caramalised notes.");
            AddPod(5, "Capprico", "Espresso", "capriccio.png", false, Color.FromArgb(255, 51, 100, 89), CupSizeEnum.Espresso, "Blending South American Arabicas with a touch of Robusta, Capriccio is an espresso with a rich aroma and a strong typical cereal note.");
            AddPod(4, "Volluto", "Espresso", "volluto.png", false, Color.FromArgb(255, 219, 165, 99), CupSizeEnum.Espresso, "A pure and lightly roasted Arabica from South America, Volluto reveals sweet and biscuity flavours, reinforced by a little acidity and a fruity note.");
            AddPod(3, "Cosi", "Espresso", "cosi.png", false, Color.FromArgb(255, 59, 20, 0), CupSizeEnum.Espresso, "Pure, lightly roasted East African, Central and South American Arabicas make Cosi a light-bodied espresso with refreshing citrus notes.");

            AddPod(10, "Indriya From India", "Pure Origin", "indirya.png", false, Color.FromArgb(255, 123, 124, 114), CupSizeEnum.Ristretto | CupSizeEnum.Espresso, "Indriya from India is the noble marriage of Arabicas with a hint of Robusta from southern India. It is a full-bodied espresso, which has a distinct personality with notes of spices.");
            AddPod(6, "Rosabaya De Columbia", "Pure Origin", "rosabaya.png", false, Color.FromArgb(255, 187, 156, 154), CupSizeEnum.Espresso, "This blend of fine, individually roasted Colombian Arabicas, develops a subtle acidity with typical red fruit and winey notes.");
            AddPod(4, "Dulsão  do Brasil", "Pure Origin", "dulsao.png", false, Color.FromArgb(255, 237, 218, 189), CupSizeEnum.Espresso, "A pure Arabica coffee, Dulsão do Brasil is a delicate blend of red and yellow Bourbon beans with a distinctive note of toasted grain. The beans are treated semi-dry to allow the mucillage sugars to infuse the bean with a mellow flavour, then separately roasted to reveal an elegantly balanced satiny sweet flavour with a note of toasted grain.");
            AddPod(3, "Bukeela Ka Ethiopia", "Pure Origin", "bukeela.png", false, Color.FromArgb(255, 198, 140, 117), CupSizeEnum.Lungo, "In the heart of Ethiopia, Nespresso selected two delicate Arabicas to create the new Pure Origin Grand Cru: Bukeela ka Ethiopia.  This refreshingly floral Pure Origin coffee gradually reveals surprisingly wild musky, woody notes.");

            AddPod(7, "Fortissio Lungo", "Lungo", "fortissio.png", false, Color.FromArgb(255, 3, 71, 72), CupSizeEnum.Lungo, "Made from Central and South American Arabicas with just a hint of Robusta, Fortissio Lungo is an intense full-bodied blend with bitterness, which develops notes of dark roasted beans.");
            AddPod(4, "Vivalto Lungo", "Lungo", "vivalto.png", false, Color.FromArgb(255, 30, 95, 158), CupSizeEnum.Lungo, "Vivalto Lungo is a balanced coffee made from a complex blend of separately roasted South American and East African Arabicas, combining roasted and subtle floral notes.");
            AddPod(4, "Linizio Lungo", "Lungo", "linizio.png", false, Color.FromArgb(255, 209, 114, 45), CupSizeEnum.Lungo, "Pure Arabica from South America, Linizio Lungo is a well-rounded blend made of Brazilian and Colombian coffee.  The split-roasting gives a cereal, malty note typical for the Bourbon variety, while maintaining its mild and smooth character.");

            AddPod(7, "Decaffeinato Intenso", "Decaffeinato", "Decaffeinato_intenso.png", true, Color.FromArgb(255, 113, 26, 41), CupSizeEnum.Espresso, "Dark roasted South American Arabicas with a touch of Robusta bring out the subtle cocoa and roasted cereal notes of this full-bodied decaffeinated espresso.");
            AddPod(3, "Decaffeinato Lungo", "Decaffeinato", "Decaffeinato_lungo.png", true, Color.FromArgb(255, 159, 57, 61), CupSizeEnum.Lungo, "The slow roasting of this blend of South American Arabicas with a touch of Robusta gives Decaffeinato Lungo a smooth, creamy body and roasted cereal flavour.");
            AddPod(2, "Decaffeinato", "Decaffeinato", "Decaffeinato.png", true, Color.FromArgb(255, 237, 25, 63), CupSizeEnum.Espresso, "A blend of South American Arabicas reinforced with just a touch of Robusta is lightly roasted to reveal an aroma of red fruit.");

            AddPod(6, "Ciocattino", "Variations", "Ciocattino.png", false, Color.FromArgb(255, 31, 24, 21), CupSizeEnum.Espresso, "Dark and bitter chocolate notes meet the caramelised roast of the Livanto Grand Cru. A rich combination reminiscent of a square of dark chocolate.");
            AddPod(6, "Vanilio", "Variations", "vanilio.png", false, Color.FromArgb(255, 229, 225, 206), CupSizeEnum.Espresso, "A balanced harmony between the rich and the velvety aromas of vanilla and the mellow flavour of the Livanto Grand Cru. A blend distinguished by its full flavour, infinitely smooth and silky on the palate. ");
            AddPod(6, "Caramelito", "Variations", "Caramelito.png", false, Color.FromArgb(255, 197, 109, 58), CupSizeEnum.Espresso, "The sweet flavour of caramel softens the roasted notes of the Livanto Grand Cru. This delicate gourmet marriage evokes the creaminess of soft toffee.");
            AddPod(6, "Cioccorosso", "Variations Club Favourite", "cioccorosso.png", false, Color.FromArgb(255, 189, 210, 162), CupSizeEnum.Espresso, "The exquisite marriage of fragrant roasted Livanto Grand Cru coffee with delectable dark chocolate and refreshing red fruit notes.");

            LoadFavourites();
        }


        public void RemoveFavourite(PodViewModel model)
        {
            var removingModel = FavouritePods.FirstOrDefault(x => x.Name == model.Name);

            if (removingModel == null)
                return;

            FavouritePods.Remove(removingModel);

            SaveFavourites();

            if (!FavouritePods.Any())
                NoFavouritesVisibility = Visibility.Visible;

        }

        public void AddFavourite(PodViewModel model)
        {
            FavouritePods.Add(model);

            SaveFavourites();

            NoFavouritesVisibility = Visibility.Collapsed;
        }

        private void LoadFavourites()
        {
            NoFavouritesVisibility = Visibility.Visible;
            FavouritePods = new ObservableCollection<PodViewModel>();

            if (!IsolatedStorageSettings.ApplicationSettings.Contains("Favourites"))
                IsolatedStorageSettings.ApplicationSettings.Add("Favourites", new List<string>());

            var favourites = IsolatedStorageSettings.ApplicationSettings["Favourites"] as List<string>;
            if (favourites == null)
                return;

            foreach (var favourite in favourites)
            {
                var pod = Pods.FirstOrDefault(x => x.Name == favourite);
                if (pod != default(PodViewModel))
                {
                    AddFavourite(pod.Clone());
                }
            }

        }

        private void SaveFavourites()
        {
            IsolatedStorageSettings.ApplicationSettings["Favourites"] = FavouritePods.Select(x => x.Name).ToList();
        }

        private void AddPod(int strength, string name, string groupName, string uri, bool isDecaf, Color backgroundColour, CupSizeEnum cupSizes, string description)
        {

            var newModel = new PodViewModel()
            {
                Name = name,
                Strength = strength,
                GroupName = groupName,
                ListImageUri = new Uri("/Images/Small/" + uri, UriKind.Relative),
                DetailImageUri = new Uri("/Images/Large/" + uri, UriKind.Relative),
                IsDecaf = isDecaf,
                Description = description,
                BackgroundColour = backgroundColour,
                CupSizes = cupSizes
            };

            Pods.Add(newModel);
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
