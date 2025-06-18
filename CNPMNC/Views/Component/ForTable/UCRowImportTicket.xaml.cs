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

namespace CNPMNC.Views.Component.ForTable
{
     /// <summary>
     /// Interaction logic for UCRowImportTicket.xaml
     /// </summary>
     public partial class UCRowImportTicket : UserControl
     {
          public UCRowImportTicket()
          {
               InitializeComponent();
          }

          private async void Edit_Click(object sender, RoutedEventArgs e)
          {
               // Nếu chưa mở, tạo mới và hiển thị
               VAddImportTicket vAddImportTicket = new VAddImportTicket(Convert.ToInt32(txtMaPN.Text), txtTenNV.Text, txtTenKho.Text, txtNgayNhap.Text, txtThanhTien.Text);

               //// Gắn sự kiện đóng cửa sổ
               //vAddProduct.Closed += async (s, args) =>
               //{
               //     await vmProductList.LoadProductList();  // Gọi lại khi cửa sổ đóng
               //};

               vAddImportTicket.Show();
          }

          private void Delete_Click(object sender, RoutedEventArgs e)
          {

          }
     }
}
