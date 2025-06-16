using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPMNC.Models.Rows
{
     class RowImportTicket
     {
          public RowImportTicket(int sTT, int maPn, string ngayNhap, string tenNv, string tenKho, string tongTien)
          {
               STT = sTT;
               MaPhieu = maPn;
               NgayNhap = ngayNhap;
               TenNguoiTao = tenNv;
               TenKho = tenKho;
               ThanhTien = tongTien;
          }

          public int STT { get; set; }
          public int MaPhieu { get; set; }
          public string NgayNhap { get; set; }
          public string TenNguoiTao { get; set; }
          public string TenKho { get; set; }
          public string ThanhTien { get; set; }
     }
}
