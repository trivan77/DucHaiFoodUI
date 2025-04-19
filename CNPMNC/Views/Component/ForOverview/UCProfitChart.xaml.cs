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

namespace CNPMNC.Views.Component.ForOverview
{
     /// <summary>
     /// Interaction logic for UCProfitChart.xaml
     /// </summary>
     public partial class UCProfitChart : UserControl
     {
          public UCProfitChart()
          {
               InitializeComponent();
               this.Loaded += UCProfitChart_Loaded;
          }

          private void UCProfitChart_Loaded(object sender, RoutedEventArgs e)
          {
               UpdateChart();
          }

          public List<double> Values
          {
               get { return (List<double>)GetValue(ValuesProperty); }
               set { SetValue(ValuesProperty, value); }
          }

          public static readonly DependencyProperty ValuesProperty =
              DependencyProperty.Register("Values", typeof(List<double>), typeof(UCProfitChart), new PropertyMetadata(new List<double>(), OnDataChanged));

          public List<string> Legends
          {
               get { return (List<string>)GetValue(LegendsProperty); }
               set { SetValue(LegendsProperty, value); }
          }

          public static readonly DependencyProperty LegendsProperty =
              DependencyProperty.Register("Legends", typeof(List<string>), typeof(UCProfitChart), new PropertyMetadata(new List<string>(), OnDataChanged));

          private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
          {
               var control = d as UCProfitChart;
               control?.UpdateChart();
          }

          private void UpdateChart()
          {
               if (Values.Count < 4 || Legends.Count < 4)
                    return;

               double max = Math.Max(Math.Max(Values[0], Values[1]), Math.Max(Values[2], Values[3]));
               if (max == 0) max = 1;

               Column1.Height = new GridLength(Values[0] * 330 / max, GridUnitType.Pixel);
               Column2.Height = new GridLength(Values[1] * 330 / max, GridUnitType.Pixel);
               Column3.Height = new GridLength(Values[2] * 330 / max, GridUnitType.Pixel);
               Column4.Height = new GridLength(Values[3] * 330 / max, GridUnitType.Pixel);

               num1.Text = Values[0].ToString();
               num2.Text = Values[1].ToString();
               num3.Text = Values[2].ToString();
               num4.Text = Values[3].ToString();

               legend1.Text = Legends[0];
               legend2.Text = Legends[1];
               legend3.Text = Legends[2];
               legend4.Text = Legends[3];
          }
     }
}
