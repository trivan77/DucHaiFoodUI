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
     class NhanVienSyncModel
     {
          class NhanVienDto
          {
               public int maNv { get; set; }
               public int maKho { get; set; }
               public string tenNv { get; set; }
               public string username { get; set; }
               public string gioiTinh { get; set; }
               public string dienThoai { get; set; }
               public string diaChi { get; set; }
               public string soCmnd { get; set; }
               public string chucVu { get; set; }
               public string matKhau { get; set; }
               public string ngayTuyenDung { get; set; }
          }

          public static async Task<ObservableCollection<RowStaffList>> DanhSachNhanVien()
          {
               var result = new ObservableCollection<RowStaffList>();
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/nhan-vien/get";

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
                         var data = JsonSerializer.Deserialize<ApiResponse<NhanVienDto>>(json, options);

                         for (int i = 0; i < data.Data.Data.Count; i++)
                         {
                              var nv = data.Data.Data[i];
                              result.Add(new RowStaffList(i + 1, nv.tenNv, nv.dienThoai, nv.maKho.ToString(), nv.ngayTuyenDung, nv.chucVu));
                         }
                    }

                    return result;
               }
          }

          public static async Task<bool> DangNhap(string username, string password)
          {
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/nhan-vien/get";

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
                         var data = JsonSerializer.Deserialize<ApiResponse<NhanVienDto>>(json, options);

                         var matched = data?.Data?.Data.FirstOrDefault(nv =>
                            nv.username == username && nv.matKhau == password);

                         if (matched != null)
                         {
                              // Gán vào UserSession.CurrentUser
                              UserSession.CurrentUser = new NhanVien
                              {
                                   MaNv = matched.maNv,
                                   MaKho = matched.maKho.ToString(),
                                   TenNv = matched.tenNv,
                                   Username = matched.username,
                                   GioiTinh = matched.gioiTinh,
                                   DienThoai = matched.dienThoai,
                                   DiaChi = matched.diaChi,
                                   SoCmnd = matched.soCmnd,
                                   ChucVu = matched.chucVu,
                                   MatKhau = matched.matKhau,
                                   NgayTuyenDung = matched.ngayTuyenDung
                              };

                              return true; // Đăng nhập thành công
                         }
                    }

                    return false;
               }

               return false; // Sai tài khoản hoặc lỗi server
          }

          public static async Task GetNhanVienById(string maNV)
          {
               var httpClient = new HttpClient();

               var url = "http://cong-nghe-phan-mem.asuna.id.vn/api/cong-nghe-phan-mem/nhan-vien/get-detail";
               var requestBody = new { id = maNV };

               var json = JsonSerializer.Serialize(requestBody);
               var content = new StringContent(json, Encoding.UTF8, "application/json");

               var response = await httpClient.PostAsync(url, content);
               var responseString = await response.Content.ReadAsStringAsync();

               // Deserialize JSON -> đối tượng C#
               var result = JsonSerializer.Deserialize<ApiResponseDetail<NhanVien>>(responseString, new JsonSerializerOptions
               {
                    PropertyNameCaseInsensitive = true
               });

               // In ra thử
               Console.WriteLine($"Tên nhân viên: {result.Data.TenNv}");
          }
     }
}
