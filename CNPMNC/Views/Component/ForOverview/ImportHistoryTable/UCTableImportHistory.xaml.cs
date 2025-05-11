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

namespace CNPMNC.Views.Component.ForOverview.ImportHistoryTable
{
     /// <summary>
     /// Interaction logic for UCTableImportHistory.xaml
     /// </summary>
     public partial class UCTableImportHistory : UserControl
     {
          public UCTableImportHistory()
          {
               InitializeComponent();
          }

          public event EventHandler<int> ChiTietClicked;

          private void ItemsControl_Loaded(object sender, RoutedEventArgs e)
          {
               var itemsControl = sender as ItemsControl;
               if (itemsControl == null) return;

               foreach (var item in itemsControl.Items)
               {
                    var container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as FrameworkElement;
                    if (container != null)
                    {
                         var rowControl = FindVisualChild<UCRowImportHistory>(container);
                         if (rowControl != null)
                         {
                              rowControl.ChiTietClicked += (s, maPhieu) =>
                              {
                                   ChiTietClicked?.Invoke(this, maPhieu);
                              };
                         }
                    }
               }
          }

          private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
          {
               int count = VisualTreeHelper.GetChildrenCount(parent);
               for (int i = 0; i < count; i++)
               {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    if (child is T tChild)
                         return tChild;
                    var result = FindVisualChild<T>(child);
                    if (result != null)
                         return result;
               }
               return null;
          }
     }
}
