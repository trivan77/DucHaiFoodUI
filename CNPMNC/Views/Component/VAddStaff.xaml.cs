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
using System.Windows.Shapes;

namespace CNPMNC.Views.Component
{
     /// <summary>
     /// Interaction logic for VAddStaff.xaml
     /// </summary>
     public partial class VAddStaff : Window
     {
          VMStaffList vmStaffList;
          public VAddStaff()
          {
               InitializeComponent();

               vmStaffList = new VMStaffList();
               DataContext = vmStaffList;

               cbxGioiTinh.ItemsSource = new List<string> { "Nam", "Nữ" };
               cbxChucVu.ItemsSource = new List<string> { "Admin", "Phụ trách kho", "Nhân viên" };
          }

          private void cbxKho_DropDownOpened(object sender, EventArgs e)
          {
               vmStaffList.LoadStoreNames();
          }
     }
}
