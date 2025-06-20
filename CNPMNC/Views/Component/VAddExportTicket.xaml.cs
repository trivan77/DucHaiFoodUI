using CNPMNC.Models;
using CNPMNC.Models.DataSync;
using CNPMNC.Models.Rows;
using CNPMNC.ViewModels;
using CNPMNC.Views.Component.ForTable;
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
     /// Interaction logic for VAddExportTicket.xaml
     /// </summary>
     public partial class VAddExportTicket : Window
     {
          VMExportTicket vmExportTicket;
          public VAddExportTicket()
          {
               InitializeComponent();

               vmExportTicket = VMExportTicket.Instance;
               DataContext = vmExportTicket;

               LoadCurrentStore();
               txtNguoiTaoPhieu.Text = UserSession.CurrentUser.TenNv;
               cbxTenSP.DropDownOpened += DanhSachTenSP_DropDownOpened;

               btnThem.Visibility = Visibility.Visible;
               btnUpdate.Visibility = Visibility.Hidden;
          }

          async void LoadCurrentStore()
          {
               txtCurrentStore.Text = await HeThongKhoSyncModel.KhoHienTai(Convert.ToInt32(UserSession.CurrentUser.MaKho));
          }

          private void DanhSachTenSP_DropDownOpened(object sender, EventArgs e)
          {
               vmExportTicket.LoadProductNames();
          }

          private void UCRowProductImport_RowClicked(object sender, RoutedEventArgs e)
          {
               var rowUC = sender as UCRowProductImport;
               var data = rowUC?.DataContext as RowImportProduct;

               if (data != null)
               {
                    int maSP = data.MaSp;
                    int soLuong = data.SoLuong;
                    string tenSP = data.TenSp;

                    cbxTenSP.SelectedItem = vmExportTicket.DanhSachTenSP.FirstOrDefault(sp => sp.Contains(maSP.ToString() + ". " + tenSP));
                    txtSoLuong.Text = soLuong.ToString();
               }
          }
     }
}
