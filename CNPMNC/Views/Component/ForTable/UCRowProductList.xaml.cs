using CNPMNC.Models;
using CNPMNC.Models.DataSync;
using CNPMNC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
     /// Interaction logic for UCRowProductList.xaml
     /// </summary>
     public partial class UCRowProductList : UserControl
     {
          VMProductList vmProductList;
          Dictionary<int, string> LoaiSanPhamListInt = new Dictionary<int, string> { { 1, "Thực phẩm tươi sống" }, { 2, "Thực phẩm đông lạnh" }, { 3, "Bánh kẹo/Đồ ăn vặt" }, { 4, "Đồ uống" }, { 5, "Gia vị" } };
          public UCRowProductList()
          {
               InitializeComponent();

               vmProductList = VMProductList.Instance;
          }

          async Task<SanPham> GetSanPhamByID(int maSP)
          {
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/san-pham/get";

               using (var client = new HttpClient())
               {
                    var body = new
                    {
                         current = 1,
                         pageSize = 20
                    };

                    var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                         var options = new JsonSerializerOptions
                         {
                              PropertyNameCaseInsensitive = true
                         };

                         var json = await response.Content.ReadAsStringAsync();
                         var data = JsonSerializer.Deserialize<ApiResponse<SanPham>>(json, options);

                         for (int i = 0; i < data.Data.Data.Count; i++)
                         {
                              var sanPham = data.Data.Data[i];
                              if(sanPham.MaSp == maSP)
                              {
                                   return sanPham;
                              }
                         }
                    }
               }

               return null;
          }

          private async void Edit_Click(object sender, RoutedEventArgs e)
          {
               var sanPham = await GetSanPhamByID(Convert.ToInt32(txtMaSP.Text));

               // Nếu chưa mở, tạo mới và hiển thị
               VAddProduct vAddProduct = new VAddProduct(sanPham, LoaiSanPhamListInt[sanPham.LoaiSp]);

               // Gắn sự kiện đóng cửa sổ
               vAddProduct.Closed += async (s, args) =>
               {
                    await vmProductList.LoadProductList();  // Gọi lại khi cửa sổ đóng
               };

               vAddProduct.Show();
          }

          private void Delete_Click(object sender, RoutedEventArgs e)
          {

          }
     }
}
