using CNPMNC.Models.Rows;
using CNPMNC.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPMNC.ViewModels
{
     class VMStaffList : NotifyBase
     {
          public VMStaffList()
          {
               StaffListRows = new ObservableCollection<RowStaffList>();
               for (int i = 0; i < 9; i++)
               {
                    AddRow(new RowStaffList
                    {
                         STT = i,
                         TenNv = "HovaTen_"+(i+1).ToString(),
                         DienThoai = "086899"+i.ToString() + i.ToString() + i.ToString() + i.ToString(),
                         TenKho = "Kho" + i.ToString(),
                         ThanhTien = (i + 7) * 1000000,
                         NgayTuyenDung = "Trần Bảo Trí",
                         ChucVu = "Admin"
                    });
               }
          }

          #region Biến

          public ObservableCollection<RowStaffList> StaffListRows { get; set; }
          #endregion

          #region Function
          public void AddRow(RowStaffList row)
          {
               StaffListRows.Add(row);
          }
          #endregion
     }
}
