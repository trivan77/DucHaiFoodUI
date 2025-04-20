using CNPMNC.ViewModels;
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
          VMMainWindow vmMainWindow;
          public VMainWindow()
          {
               InitializeComponent();

               vmMainWindow = new VMMainWindow();
               DataContext = vmMainWindow;

               vmMainWindow.ShowOverviewCommand.Execute(null);
               btnOverview.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
          }

          private void NavbarButton_Click(object sender, RoutedEventArgs e)
          {
               vmMainWindow.IsTicketChildVisible = Visibility.Hidden;

               btnOverview.IsChecked = false;
               btnStaffList.IsChecked = false;
               btnStore.IsChecked = false;
               btnTicket.IsChecked = false;

               btnImportTicket.IsChecked = false;
               btnExportTicket.IsChecked = false;
               btnTransTicket.IsChecked = false;

               ToggleButton clickedButton = sender as ToggleButton;
               if (clickedButton != null)
               {
                    clickedButton.IsChecked = true;
               }
          }

          private void TicketChildButton_Click(object sender, RoutedEventArgs e)
          {
               vmMainWindow.IsTicketChildVisible = Visibility.Hidden;

               btnImportTicket.IsChecked = false;
               btnExportTicket.IsChecked = false;
               btnTransTicket.IsChecked = false;

               btnTicket.IsChecked = true;
               btnOverview.IsChecked = false;
               btnStaffList.IsChecked = false;
               btnStore.IsChecked = false;

               ToggleButton clickedButton = sender as ToggleButton;
               if (clickedButton != null)
               {
                    clickedButton.IsChecked = true;
               }
          }

          private void btnTicket_Click(object sender, RoutedEventArgs e)
          {
               if (btnTicket.IsChecked == false && vmMainWindow.IsTicketChildVisible == Visibility.Hidden) btnTicket.IsChecked = true;
          }
     }
}
