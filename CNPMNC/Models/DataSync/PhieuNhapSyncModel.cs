using CNPMNC.Models.Rows;
using CNPMNC.Utils;
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
          public static async Task<int> MaPhieuNhapNew()
          {
               var result = 0;
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/phieu-nhap/get";

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
                         var data = JsonSerializer.Deserialize<ApiResponse<PhieuNhap>>(json, options);

                         var danhSach = data?.Data?.Data;
                         if (danhSach != null && danhSach.Any())
                         {
                              // Tìm MaPn lớn nhất trong danh sách
                              return danhSach.Max(p => p.MaPn);
                         }
                    }

                    throw new Exception("Lỗi lấy mã phiếu nhập");
               }
          }

          public static async Task ThemPhieuNhap(int _maNv, string _ngayNhap, int _maKho, string tongTien)
          {
               HttpClient httpClient = new HttpClient();
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/phieu-nhap/create";

               var requestBody = new
               {
                    values = new
                    {
                         maNv = _maNv,
                         ngayNhap = _ngayNhap,
                         maKho = _maKho,
                         tongTien = tongTien
                    }
               };
               var json = JsonSerializer.Serialize(requestBody);
               var content = new StringContent(json, Encoding.UTF8, "application/json");

               var response = await httpClient.PostAsync(url, content);
               response.EnsureSuccessStatusCode();

               SystemNotify.SuccessNotify("Thêm phiếu nhập thành công");
          }

          public static async Task ThemChiTietPhieuNhap(ObservableCollection<RowImportProduct> DanhSachSP)
          {
               int maPN = await MaPhieuNhapNew();

               HttpClient httpClient = new HttpClient();
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/chi-tiet-phieu-nhap/create";

               foreach (RowImportProduct rowImportProduct in DanhSachSP)
               {
                    SanPham sanPham = await SanPhamSyncModel.GetSanPhamById(rowImportProduct.MaSp);
                    var requestBody = new
                    {
                         values = new
                         {
                              maPn = maPN,
                              maSp = sanPham.MaSp,
                              donGia = sanPham.GiaBan,
                              donViTinh = sanPham.DonViTinh,
                              soLuong = sanPham.SoLuong,
                         }
                    };
                    var json = JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();
               }
          }
     }
}
