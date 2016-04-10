using System.Windows;
using System.Windows.Controls;
using CoffeePods.Classes;

namespace CoffeePods.UserControls
{
    public partial class DetailsView : UserControl
    {
        public DetailsView()
        {
            InitializeComponent();
        }

        public void SetContext(PodViewModel podViewModel)
        {
            DataContext = podViewModel;
        }

        private void btnLike_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as PodViewModel;
            if (model == null)
                return;

            if (model.IsFavourite)
            {
                App.ViewModel.RemoveFavourite(model);
                model.IsFavourite = false;
            }
            else
            {
                App.ViewModel.AddFavourite(model);
                model.IsFavourite = true;
            }
        }

    }
}
