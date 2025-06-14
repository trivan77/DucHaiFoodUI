using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPMNC.Models.Rows
{
     class RowStaffList
     {
          public int STT { get; set; }
          public string TenNv { get; set; }
          public string DienThoai { get; set; }
          public string TenKho { get; set; }
          public string NgayTuyenDung { get; set; }
          public string ChucVu { get; set; }

          public RowStaffList(int sTT, string tenNv, string dienThoai, string tenKho, string ngayTuyenDung, string chucVu)
          {
               STT = sTT;
               TenNv = tenNv;
               DienThoai = dienThoai;
               TenKho = tenKho;
               NgayTuyenDung = ngayTuyenDung;
               ChucVu = chucVu;
          }
     }
}
