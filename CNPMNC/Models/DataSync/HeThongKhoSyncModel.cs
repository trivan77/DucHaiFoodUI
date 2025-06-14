using CNPMNC.Models.Rows;
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
     class KhoDto
     {
          public int maKho { get; set; }
          public string tenKho { get; set; }
          public string diaChi { get; set; }
          public int? maQuanLy { get; set; }
          public string ngay_tao { get; set; }
          public List<NhanVienDto> NhanVien { get; set; }
     }

     class NhanVienDto
     {
          public string dienThoai { get; set; }
          public string tenNv { get; set; }
          public int maNv { get; set; }
     }

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
                         var data = JsonSerializer.Deserialize<ApiResponse<HeThongKho>>(json, options);

                         foreach (var kho in data.Data.Data)
                         {
                              result.Add(kho.MaKho + ". " + kho.TenKho);
                         }
                    }

                    return result;
               }
          }

          public static async Task<ObservableCollection<RowStoreList>> DanhSachKho()
          {
               var result = new ObservableCollection<RowStoreList>();
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
                         var data = JsonSerializer.Deserialize<ApiResponse<KhoDto>>(json, options);

                         for (int i = 0; i < data.Data.Data.Count; i++)
                         {
                              var kho = data.Data.Data[i];
                              NhanVienDto nhanVien = null;

                              if (kho.maQuanLy.HasValue && kho.NhanVien != null)
                              {
                                   nhanVien = kho.NhanVien.FirstOrDefault(nv => nv.maNv == kho.maQuanLy.Value);
                              }
                              result.Add(new RowStoreList(i + 1, kho.tenKho, kho.diaChi, kho.ngay_tao, nhanVien?.tenNv, nhanVien?.dienThoai));
                         }
                    }

                    return result;
               }
          }

          public static async Task<string> KhoHienTai(int maKhoHienTai)
          {
               string result = "";
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/kho/get-detail";

               using (var client = new HttpClient())
               {
                    var body = new
                    {
                         id = maKhoHienTai
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

                         var data = JsonSerializer.Deserialize<ApiResponseDetail<KhoDto>>(json, options);

                         if (data?.Status == 1 && data.Data != null)
                         {
                              result = data.Data.tenKho;
                         }
                    }

                    return result;
               }
          }
     }
}
