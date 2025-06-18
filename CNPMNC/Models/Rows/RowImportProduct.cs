using CNPMNC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPMNC.Models.Rows
{
     class RowImportProduct : NotifyBase
     {
          public RowImportProduct() { }

          public RowImportProduct(int sTT, int maSp, string tenSp, int soLuong, string giaBan, string giaNhap)
          {
               this.sTT = sTT;
               MaSp = maSp;
               TenSp = tenSp;
               this.soLuong = soLuong;
               GiaBan = giaBan;
               GiaNhap = giaNhap;
          }

          private int sTT;
          public int STT
          {
               get => sTT;
               set
               {
                    if (sTT != value)
                    {
                         sTT = value;
                         OnPropertyChanged(nameof(STT));
                    }
               }
          }

          public int MaSp { get; set; }
          public string TenSp { get; set; }

          private int soLuong;

          public int SoLuong
          {
               get => soLuong;
               set
               {
                    if (soLuong != value)
                    {
                         soLuong = value;
                         OnPropertyChanged(nameof(SoLuong));
                    }
               }
          }

          public string GiaBan { get; set; }
          public string GiaNhap { get; set; }
     }
}
