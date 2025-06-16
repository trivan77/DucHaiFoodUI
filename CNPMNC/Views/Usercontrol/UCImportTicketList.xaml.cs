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
     /// Interaction logic for UCTicketList.xaml
     /// </summary>
     public partial class UCImportTicketList : UserControl
     {
          VMImportTicket vmImportTicket;
          public UCImportTicketList()
          {
               InitializeComponent();

               vmImportTicket = VMImportTicket.Instance;
               DataContext = vmImportTicket;

               // Gọi bất đồng bộ sau khi UI load xong
               this.Loaded += async (s, e) => await vmImportTicket.LoadImportTicketList();
          }
     }
}
