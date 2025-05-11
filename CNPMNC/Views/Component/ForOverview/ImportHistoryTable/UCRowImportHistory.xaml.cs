using CNPMNC.Models.Rows;
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
     /// Interaction logic for UCRowImportHistory.xaml
     /// </summary>
     public partial class UCRowImportHistory : UserControl
     {
          public UCRowImportHistory()
          {
               InitializeComponent();
          }

          public event EventHandler<int> ChiTietClicked;

          private void ChiTiet_Click(object sender, RoutedEventArgs e)
          {
               if (DataContext is RowImportHistory model)
               {
                    ChiTietClicked?.Invoke(this, model.MaPhieu);
               }
          }
     }
}
