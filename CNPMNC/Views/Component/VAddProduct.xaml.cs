using CNPMNC.Models;
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
     /// Interaction logic for VAddProduct.xaml
     /// </summary>
     public partial class VAddProduct : Window
     {
          VMProductList vmProductList;
          public VAddProduct()
          {
               InitializeComponent();
               cbxLoaiSanPham.ItemsSource = new List<string> { "Thực phẩm tươi sống", "Thực phẩm đông lạnh", "Bánh kẹo/Đồ ăn vặt", "Đồ uống", "Gia vị" };

               vmProductList = VMProductList.Instance;
               DataContext = vmProductList;

               btnThemMoi.Visibility = Visibility.Visible;
               btnCapNhat.Visibility = Visibility.Hidden;
          }

          public VAddProduct(SanPham sanPham, string loaiSPSelected)
          {
               InitializeComponent();
               cbxLoaiSanPham.ItemsSource = new List<string> { "Thực phẩm tươi sống", "Thực phẩm đông lạnh", "Bánh kẹo/Đồ ăn vặt", "Đồ uống", "Gia vị" };

               vmProductList = VMProductList.Instance;
               DataContext = vmProductList;

               vmProductList.SanPhamAdded = sanPham;
               vmProductList.LoaiSPSelected = loaiSPSelected;

               btnThemMoi.Visibility = Visibility.Hidden;
               btnCapNhat.Visibility = Visibility.Visible;
          }
     }
}
