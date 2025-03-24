using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CNPMNC.Views.Component
{
     /// <summary>
     /// Interaction logic for UCStatisticCard.xaml
     /// </summary>
     public partial class UCStatisticCard : UserControl
     {
          public UCStatisticCard()
          {
               InitializeComponent();
          }
          public static readonly DependencyProperty IconSourceProperty =
          DependencyProperty.Register("IconSource", typeof(string), typeof(UCStatisticCard), new PropertyMetadata(null));

          public static readonly DependencyProperty TitleProperty =
              DependencyProperty.Register("Title", typeof(string), typeof(UCStatisticCard), new PropertyMetadata(""));

          public static readonly DependencyProperty RevenueProperty =
              DependencyProperty.Register("Revenue", typeof(string), typeof(UCStatisticCard), new PropertyMetadata(""));

          public static readonly DependencyProperty PercentageProperty =
              DependencyProperty.Register("Percentage", typeof(int), typeof(UCStatisticCard),
                  new PropertyMetadata(0, OnPercentageChanged));

          public static readonly DependencyProperty ChartSourceProperty =
              DependencyProperty.Register("ChartSource", typeof(string), typeof(UCStatisticCard), new PropertyMetadata(null));

          public static readonly DependencyProperty PercentageColorProperty =
              DependencyProperty.Register("PercentageColor", typeof(Brush), typeof(UCStatisticCard), new PropertyMetadata(Brushes.Black));

          public string IconSource
          {
               get => (string)GetValue(IconSourceProperty);
               set => SetValue(IconSourceProperty, value);
          }

          public string Title
          {
               get => (string)GetValue(TitleProperty);
               set => SetValue(TitleProperty, value);
          }

          public string Revenue
          {
               get => (string)GetValue(RevenueProperty);
               set => SetValue(RevenueProperty, value);
          }

          public int Percentage
          {
               get => (int)GetValue(PercentageProperty);
               set => SetValue(PercentageProperty, value);
          }

          public string ChartSource
          {
               get => (string)GetValue(ChartSourceProperty);
               private set => SetValue(ChartSourceProperty, value);
          }

          public Brush PercentageColor
          {
               get => (Brush)GetValue(PercentageColorProperty);
               private set => SetValue(PercentageColorProperty, value);
          }

          private static void OnPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
          {
               if (d is UCStatisticCard card)
               {
                    card.UpdateChartAndColor();
               }
          }

          private void UpdateChartAndColor()
          {
               if (Percentage < 0) // Giảm doanh thu
               {
                    ChartSource = "Resources/Images/Down  Chart.png";
                    PercentageColor = Brushes.Red;
               }
               else // Tăng doanh thu hoặc bằng 0
               {
                    ChartSource = "Resources/Images/UpChart.png";
                    PercentageColor = new SolidColorBrush(Color.FromRgb(18, 85, 147));
               }
          }
     }
}
