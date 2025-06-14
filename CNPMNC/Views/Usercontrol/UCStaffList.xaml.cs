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
     /// Interaction logic for UCStaffList.xaml
     /// </summary>
     public partial class UCStaffList : UserControl
     {
          VMStaffList vmStaffList;
          public UCStaffList()
          {
               InitializeComponent();

               vmStaffList = new VMStaffList();
               DataContext = vmStaffList;

               // Gọi bất đồng bộ sau khi UI load xong
               this.Loaded += async (s, e) => await vmStaffList.LoadStaffList();
          }
     }
}
