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
          public int STT { get; set; }
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
