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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CNPMNC.Views.Component
{
     /// <summary>
     /// Interaction logic for VAddImportTicket.xaml
     /// </summary>
     public partial class VAddImportTicket : Window
     {
          VMImportTicket vmImportTicket;
          public VAddImportTicket()
          {
               InitializeComponent();

               vmImportTicket = VMImportTicket.Instance;
               DataContext = vmImportTicket;

               LoadCurrentStore();
               txtNguoiTaoPhieu.Text = UserSession.CurrentUser.TenNv;
               cbxTenSP.DropDownOpened += DanhSachTenSP_DropDownOpened;

               btnThem.Visibility = Visibility.Visible;
               btnUpdate.Visibility = Visibility.Hidden;
          }

          public VAddImportTicket(int maPn, string tenNV, string maKho, string ngayNhap, string tongTien)
          {
               InitializeComponent();

               vmImportTicket = VMImportTicket.Instance;
               DataContext = vmImportTicket;

               txtNgayTaoPhieu.Text = ngayNhap;
               txtCurrentStore.Text = maKho;
               txtNguoiTaoPhieu.Text = tenNV;
               vmImportTicket.TotalPrice = tongTien;
               cbxTenSP.DropDownOpened += DanhSachTenSP_DropDownOpened;
               LoadChiTietPhieuNhap(maPn);

               btnThem.Visibility = Visibility.Hidden;
               btnUpdate.Visibility = Visibility.Visible;
          }

          async void LoadChiTietPhieuNhap(int maPn)
          {
               var danhSachCTPN = await PhieuNhapSyncModel.DanhSachChiTietPhieuNhap(maPn);
               vmImportTicket.ProductImportRows.Clear();
               
               foreach(var ctpn in danhSachCTPN)
               {
                    vmImportTicket.ProductImportRows.Add(ctpn);
               }
          }

          private void DanhSachTenSP_DropDownOpened(object sender, EventArgs e)
          {
               vmImportTicket.LoadProductNames();
          }

          async void LoadCurrentStore()
          {
               txtCurrentStore.Text = await HeThongKhoSyncModel.KhoHienTai(Convert.ToInt32(UserSession.CurrentUser.MaKho));
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

                    cbxTenSP.SelectedItem = maSP.ToString() + ". " + tenSP;
                    txtSoLuong.Text = soLuong.ToString();
               }
          }
     }
}
