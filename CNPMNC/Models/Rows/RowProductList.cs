using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPMNC.Models.Rows
{
     class RowProductList
     {
          public RowProductList(int maSP, int sTT, string tenSp, int giaBan, int giaNhap, string donViTinh, int soLuong)
          {
               MaSP = maSP;
               STT = sTT;
               TenSp = tenSp;
               GiaBan = giaBan;
               GiaNhap = giaNhap;
               DonViTinh = donViTinh;
               SoLuong = soLuong;
          }

          public int MaSP { get; set; }
          public int STT { get; set; }
          public string TenSp { get; set; }
          public int GiaBan { get; set; }
          public int GiaNhap { get; set; }
          public string DonViTinh { get; set; }
          public int SoLuong { get; set; }
     }
}
