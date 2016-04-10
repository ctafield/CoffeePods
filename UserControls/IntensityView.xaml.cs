using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CoffeePods.UserControls
{
    public partial class IntensityView : UserControl
    {

        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            "BackgroundColor", typeof (Color?), typeof (IntensityView), new PropertyMetadata(default(Color?), PropertiesChanged));

        public static readonly DependencyProperty IntensityProperty = DependencyProperty.Register(
            "Intensity", typeof(int?), typeof(IntensityView), new PropertyMetadata(default(int), PropertiesChanged));

        public Color? BackgroundColor
        {
            get { return (Color?) GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public int? Intensity
        {
            get { return (int?)GetValue(IntensityProperty); }
            set { SetValue(IntensityProperty, value); }
        }

        private static void PropertiesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {

            var control = dependencyObject as IntensityView;
            if (control == null)
                return;

            control.UpdateScore();

        }

        private void UpdateScore()
        {

            // need both of these
            if (!BackgroundColor.HasValue || !Intensity.HasValue)
                return;

            stackScore.Children.Clear();

            for (int i = 1; i <= 12; i ++)
            {

                var border = new Border()
                {
                    Width = 33,
                    Height = 33,
                    BorderBrush = new SolidColorBrush(BackgroundColor.GetValueOrDefault()),
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(0, 0, 5, 0)
                };

                if (i <= Intensity)
                {
                    border.Background = new SolidColorBrush(BackgroundColor.GetValueOrDefault());
                }

                stackScore.Children.Add(border);
            }

        }

        public IntensityView()
        {
            InitializeComponent();

        }
    }
}
