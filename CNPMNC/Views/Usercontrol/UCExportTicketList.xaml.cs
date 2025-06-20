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
     /// Interaction logic for UCExportTicketList.xaml
     /// </summary>
     public partial class UCExportTicketList : UserControl
     {
          VMExportTicket vmExportTicket;
          public UCExportTicketList()
          {
               InitializeComponent();

               vmExportTicket = VMExportTicket.Instance;
               DataContext = vmExportTicket;

               // Gọi bất đồng bộ sau khi UI load xong
               this.Loaded += async (s, e) => await vmExportTicket.LoadExportTicketList();
          }
     }
}
