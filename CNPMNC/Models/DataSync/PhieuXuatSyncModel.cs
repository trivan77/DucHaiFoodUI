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
     class PhieuXuatSyncModel
     {
          public static async Task<int> MaPhieuXuatNew()
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

          public static async Task<ObservableCollection<RowImportTicket>> DanhSachPhieuXuat()
          {
               var result = new ObservableCollection<RowImportTicket>();
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
                         var data = JsonSerializer.Deserialize<ApiResponse<PhieuXuat>>(json, options);

                         if (data.Status == 1)
                         {
                              for (int i = 0; i < data.Data.Data.Count; i++)
                              {
                                   var phieuXuat = data.Data.Data[i];

                                   NhanVien nv = await NhanVienSyncModel.GetNhanVienById(phieuXuat.MaNv);
                                   string tenKho = await HeThongKhoSyncModel.KhoHienTai(phieuXuat.MaKho);

                                   result.Add(new RowImportTicket(i + 1, phieuXuat.MaPx, phieuXuat.NgayXuat, nv.TenNv, tenKho, FormatTien(phieuXuat.TongTien)));
                              }
                         }
                         else
                         {
                              throw new Exception(data.Message + "!!!");
                         }
                    }
                    else
                    {
                         throw new Exception("Không có kết nối tới server!!!");
                    }

                    return result;
               }
          }

          public static async Task ThemPhieuXuat(int _maNv, string _ngayXuat, int _maKho, string tongTien, string loiNhuan)
          {
               HttpClient httpClient = new HttpClient();
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/phieu-nhap/create";

               var requestBody = new
               {
                    values = new
                    {
                         maNv = _maNv,
                         ngayXuat = _ngayXuat,
                         maKho = _maKho,
                         tongTien = tongTien,
                         loiNhuan = loiNhuan
                    }
               };
               var json = JsonSerializer.Serialize(requestBody);
               var content = new StringContent(json, Encoding.UTF8, "application/json");

               var response = await httpClient.PostAsync(url, content);
               response.EnsureSuccessStatusCode();

               SystemNotify.SuccessNotify("Thêm phiếu nhập thành công");
          }

          public static async Task ThemChiTietPhieuXuat(ObservableCollection<RowImportProduct> DanhSachSP, int _maKho)
          {
               int maPX = await MaPhieuXuatNew();

               HttpClient httpClient = new HttpClient();
               var url = "http://cong-nghe-phan-mem.asuna.id.vn//api/cong-nghe-phan-mem/chi-tiet-phieu-nhap/create";

               foreach (RowImportProduct rowImportProduct in DanhSachSP)
               {
                    SanPham sanPham = await SanPhamSyncModel.GetSanPhamById(rowImportProduct.MaSp);
                    var requestBody = new
                    {
                         values = new
                         {
                              maPn = maPX,
                              maKho = _maKho,
                              maSp = sanPham.MaSp,
                              donGia = sanPham.GiaBan,
                              donViTinh = sanPham.DonViTinh,
                              soLuong = rowImportProduct.SoLuong,
                         }
                    };
                    var json = JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync(url, content);
                    response.EnsureSuccessStatusCode();
               }
          }

          public static string FormatTien(string soTienStr)
          {
               if (decimal.TryParse(soTienStr, out decimal soTien))
               {
                    return string.Format("{0:N0}", soTien).Replace(",", ".");
               }

               throw new FormatException("Số tiền không hợp lệ.");
          }
     }
}
