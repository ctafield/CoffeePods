using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Clarity.Phone.Extensions;
using CoffeePods.Classes;
using CoffeePods.UserControls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CoffeePods.Resources;

namespace CoffeePods
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);


            if (App.ViewModel == null)
                App.ViewModel = new MainViewModel();

            App.ViewModel.LoadData();

            DataContext = App.ViewModel;

            SetGroupedDataContext();
            llsFavouritePods.DataContext = App.ViewModel.FavouritePods;
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        private void LlsPods_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var listbox = sender as LongListSelector;

            if (listbox == null || listbox.SelectedItem as PodViewModel == null)
                return;

            var dh = new DialogService();

            var detailsView = new DetailsView();

            var selectedItem = (listbox.SelectedItem as PodViewModel).Clone();

            detailsView.SetContext(selectedItem);

            dh.Child = detailsView;

            dh.Opened += delegate {
                ApplicationBar.IsVisible = false;
            };

            dh.Closed += delegate {
                ApplicationBar.IsVisible = true;
            };

            dh.AnimationType = DialogService.AnimationTypes.Slide;

            ThreadPool.QueueUserWorkItem(delegate
            {
                Dispatcher.BeginInvoke(dh.Show);
            });

            listbox.SelectedItem = null;
        }

        private void mnuSortGroup_Click(object sender, EventArgs e)
        {
            SetGroupedDataContext();
        }

        private void SetGroupedDataContext()
        {

            var pods = from pod in App.ViewModel.Pods
                       group pod by pod.GroupName
                           into groupedPods
                           select new KeyedList<string, PodViewModel>(groupedPods);

            llsPods.IsGroupingEnabled = true;
            llsPods.DataContext = new List<KeyedList<string, PodViewModel>>(pods);
        }

        private void mnuSortName_Click(object sender, EventArgs e)
        {
            var button = sender as ApplicationBarIconButton;

            if (button == null)
                return;

            llsPods.IsGroupingEnabled = false;

            if (button.IconUri.ToString().EndsWith("appbar.sort.alphabetical.ascending.png"))
            {
                llsPods.DataContext = App.ViewModel.Pods.OrderByDescending(x => x.Name).ToList();
                button.IconUri = new Uri("/Icons/light/appbar.sort.alphabetical.descending.png", UriKind.Relative);
            }
            else
            {
                llsPods.DataContext = App.ViewModel.Pods.OrderBy(x => x.Name).ToList();
                button.IconUri = new Uri("/Icons/light/appbar.sort.alphabetical.ascending.png", UriKind.Relative);
            }
        }

        private void mnuSortIntesity_Click(object sender, EventArgs e)
        {
            var button = sender as ApplicationBarIconButton;

            if (button == null)
                return;

            llsPods.IsGroupingEnabled = false;

            if (button.IconUri.ToString().EndsWith("appbar.sort.numeric.ascending.png"))
            {
                llsPods.DataContext = App.ViewModel.Pods.OrderByDescending(x => x.Strength).ToList();
                button.IconUri = new Uri("/Icons/light/appbar.sort.numeric.descending.png", UriKind.Relative);
            }
            else
            {
                llsPods.DataContext = App.ViewModel.Pods.OrderBy(x => x.Strength).ToList();
                button.IconUri = new Uri("/Icons/light/appbar.sort.numeric.ascending.png", UriKind.Relative);
            }
        }

    }

}