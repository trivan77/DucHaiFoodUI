using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace CNPMNC.Models.DataSync
{
     class HeThongKhoSyncModel
     {
          public static async Task<ObservableCollection<string>> DanhSachMaKhoTenKho()
          {
               var result = new ObservableCollection<string>();
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/kho/get";

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
                         var data = JsonSerializer.Deserialize<ApiResponse<HeThongKho>>(json,options);

                         foreach (var kho in data.Data.Data)
                         {
                              result.Add(kho.MaKho + ". " + kho.TenKho);
                         }
                    }

                    return result;
               }
          }
     }
}
