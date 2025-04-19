using CNPMNC.ViewModels;
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

namespace CNPMNC.Views.Usercontrol
{
     /// <summary>
     /// Interaction logic for UCOverview.xaml
     /// </summary>
     public partial class UCOverview : UserControl
     {
          VMOverview vm;
          public UCOverview()
          {
               InitializeComponent();
               vm = new VMOverview();
               this.DataContext = vm;

               profitChart.Values = vm.Values;
               profitChart.Legends = vm.Legends;
          }
     }
}
