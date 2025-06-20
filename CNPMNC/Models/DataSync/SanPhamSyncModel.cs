using CNPMNC.Models.Rows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CNPMNC.Models.DataSync
{
     class SanPhamSyncModel
     {

          Dictionary<int, string> LoaiSanPhamListInt = new Dictionary<int, string> { { 1, "Thực phẩm tươi sống" }, { 2, "Thực phẩm đông lạnh" }, { 3, "Bánh kẹo/Đồ ăn vặt" }, { 4, "Đồ uống" }, { 5, "Gia vị" } };

          public static async Task<ObservableCollection<RowProductList>> DanhSachSanPham()
          {
               var result = new ObservableCollection<RowProductList>();
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
                              result.Add(new RowProductList(sanPham.MaSp ,i + 1, sanPham.TenSp, (int)Convert.ToDouble(sanPham.GiaBan), (int)Convert.ToDouble(sanPham.GiaNhap), sanPham.DonViTinh, sanPham.SoLuong));
                         }
                    }

                    return result;
               }
          }

          public static async Task<ObservableCollection<string>> DanhSachMaSPTenSP(int maKho)
          {
               var result = new ObservableCollection<string>();
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/chi-tiet-san-pham/get";

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
                         var data = JsonSerializer.Deserialize<ApiResponse<ChiTietSanPham>>(json, options);

                         foreach (var chiTietSanPham in data.Data.Data)
                         {
                              if(chiTietSanPham.MaKho == maKho)
                              {
                                   SanPham sp = await GetSanPhamById(chiTietSanPham.MaSp);
                                   result.Add(chiTietSanPham.MaSp + ". " + sp.TenSp + " - " + chiTietSanPham.SoLuong);
                              }
                         }
                    }

                    return result;
               }
          }
          public static async Task<ObservableCollection<string>> DanhSachMaSPTenSP()
          {
               var result = new ObservableCollection<string>();
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

                         foreach (var sanPham in data.Data.Data)
                         {
                              result.Add(sanPham.MaSp + ". " + sanPham.TenSp);
                         }
                    }

                    return result;
               }
          }

          public static async Task<SanPham> GetSanPhamById(int id)
          {
               HttpClient httpClient = new HttpClient();
               var url = "http://cong-nghe-phan-mem.asuna.id.vn/api/cong-nghe-phan-mem/san-pham/get-detail";

               var requestBody = new { id = id };
               var json = JsonSerializer.Serialize(requestBody);
               var content = new StringContent(json, Encoding.UTF8, "application/json");

               var response = await httpClient.PostAsync(url, content);
               response.EnsureSuccessStatusCode();

               var responseJson = await response.Content.ReadAsStringAsync();

               var result = JsonSerializer.Deserialize<ApiResponseDetail<SanPham>>(responseJson, new JsonSerializerOptions
               {
                    PropertyNameCaseInsensitive = true
               });

               return result?.Data;
          }
     }
}
