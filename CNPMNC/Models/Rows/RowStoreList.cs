using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPMNC.Models.Rows
{
     class RowStoreList
     {
          public RowStoreList(int stt, string tenKho, string diaChi, string ngay_tao, string tenNv, string dienThoai)
          {
               STT = stt;
               TenKho = tenKho;
               DiaChi = diaChi;
               NgayTao = ngay_tao;
               TenPhuTrach = tenNv;
               SDTLienHe = dienThoai;
          }

          public int STT { get; set; }
          public string TenKho { get; set; }
          public string DiaChi { get; set; }
          public string NgayTao { get; set; }
          public string TenPhuTrach { get; set; }
          public string SDTLienHe { get; set; }
     }
}
