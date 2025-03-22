using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CNPMNC.Views
{
     /// <summary>
     /// Interaction logic for VMainWindow.xaml
     /// </summary>
     public partial class VMainWindow : Window
     {
          public VMainWindow()
          {
               InitializeComponent();
          }

          private void NavbarButton_Click(object sender, RoutedEventArgs e)
          {
               btnOverview.IsChecked = false;
               btnHumanResource.IsChecked = false;
               btnStore.IsChecked = false;
               btnTicket.IsChecked = false;

               ToggleButton clickedButton = sender as ToggleButton;
               if (clickedButton != null)
               {
                    clickedButton.IsChecked = true;
               }
          }
     }
}
