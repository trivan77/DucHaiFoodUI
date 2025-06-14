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

namespace CNPMNC.Views.Component.ForTable
{
     /// <summary>
     /// Interaction logic for UCRowProductImport.xaml
     /// </summary>
     public partial class UCRowProductImport : UserControl
     {
          public UCRowProductImport()
          {
               InitializeComponent();
               this.MouseLeftButtonUp += UCRowProductImport_MouseLeftButtonUp;
          }

          public static readonly RoutedEvent RowClickedEvent = EventManager.RegisterRoutedEvent(
              "RowClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UCRowProductImport));

          public event RoutedEventHandler RowClicked
          {
               add { AddHandler(RowClickedEvent, value); }
               remove { RemoveHandler(RowClickedEvent, value); }
          }

          private void UCRowProductImport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
          {
               RaiseEvent(new RoutedEventArgs(RowClickedEvent, this));
          }
     }
}
