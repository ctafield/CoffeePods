using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CoffeePods.Classes;

namespace CoffeePods.UserControls
{
    public partial class CoffeeSizeView : UserControl
    {

        public static readonly DependencyProperty BackgroundColourProperty = DependencyProperty.Register(
            "BackgroundColour", typeof(Color?), typeof(CoffeeSizeView), new PropertyMetadata(default(Color?), PropertiesChanged));

        public Color? BackgroundColour
        {
            get { return (Color?)GetValue(BackgroundColourProperty); }
            set { SetValue(BackgroundColourProperty, value); }
        }

        public static readonly DependencyProperty CupSizeEnumProperty = DependencyProperty.Register(
            "CupSizeEnum", typeof(CupSizeEnum?), typeof(CoffeeSizeView), new PropertyMetadata(default(CupSizeEnum?), PropertiesChanged));

        public CupSizeEnum? CupSizeEnum
        {
            get { return (CupSizeEnum?)GetValue(CupSizeEnumProperty); }
            set { SetValue(CupSizeEnumProperty, value); }
        }

        private static void PropertiesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {

            var control = dependencyObject as CoffeeSizeView;
            if (control == null)
                return;

            control.UpdateSizes();

        }

        private void UpdateSizes()
        {
            if (!CupSizeEnum.HasValue || !BackgroundColour.HasValue)
                return;

            stackScore.Children.Clear();

            var large = GetImage("large.png", 98, Classes.CupSizeEnum.Lungo, "110ml");
            var medium = GetImage("medium.png", 88, Classes.CupSizeEnum.Espresso, "40ml");
            var small = GetImage("small.png", 78, Classes.CupSizeEnum.Ristretto, "25ml");

            stackScore.Children.Add(small);

            stackScore.Children.Add(medium);

            stackScore.Children.Add(large);

        }

        private UIElement GetImage(string path, int dim, CupSizeEnum flag, string text)
        {
            var stackPanel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 12, 0),
            };

            var border = new Border
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                Width = dim,
                Height = dim,
                OpacityMask = new ImageBrush()
                {
                    ImageSource = new BitmapImage(new Uri("/Images/CupSizes/" + path, UriKind.Relative))
                }
            };

            var cupsize = CupSizeEnum.Value;

            var thisColour = cupsize.HasFlag(flag)
                ? new SolidColorBrush(BackgroundColour.GetValueOrDefault())
                : new SolidColorBrush(Colors.LightGray);

            border.Background = thisColour;

            stackPanel.Children.Add(border);

            var textBlock = new TextBlock
            {
                Text = text,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = thisColour,
                FontSize = 14
            };

            stackPanel.Children.Add(textBlock);

            return stackPanel;
        }

        public CoffeeSizeView()
        {
            InitializeComponent();
        }


    }
}
