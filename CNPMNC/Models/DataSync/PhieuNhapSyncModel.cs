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
     class PhieuNhapSyncModel
     {


          public static async Task ThemPhieuNhap(ObservableCollection<string> DanhSachSP)
          {
               HttpClient httpClient = new HttpClient();
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/chi-tiet-phieu-nhap/create";

                    var requestBody = new
                    {
                         values = new
                         {
                              maPn = 
                         }
                    };
               var json = JsonSerializer.Serialize(requestBody);
               var content = new StringContent(json, Encoding.UTF8, "application/json");

               var response = await httpClient.PostAsync(url, content);
               response.EnsureSuccessStatusCode();

               var responseJson = await response.Content.ReadAsStringAsync();

               var result = JsonSerializer.Deserialize<ApiResponseDetail<SanPham>>(responseJson, new JsonSerializerOptions
               {
                    PropertyNameCaseInsensitive = true
               });
          }

          public static async Task ThemChiTietPhieuNhap(ObservableCollection<string> DanhSachSP)
          {
               HttpClient httpClient = new HttpClient();
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/chi-tiet-phieu-nhap/create";

               for ()

                    var requestBody = new
                    {
                         values = new
                         {
                              maPn =
                         }
                    };
               var json = JsonSerializer.Serialize(requestBody);
               var content = new StringContent(json, Encoding.UTF8, "application/json");

               var response = await httpClient.PostAsync(url, content);
               response.EnsureSuccessStatusCode();

               var responseJson = await response.Content.ReadAsStringAsync();

               var result = JsonSerializer.Deserialize<ApiResponseDetail<SanPham>>(responseJson, new JsonSerializerOptions
               {
                    PropertyNameCaseInsensitive = true
               });
          }
     }
}
