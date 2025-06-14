using CNPMNC.Models.DataSync;
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
     class VMStoreList : NotifyBase
     {
          public VMStoreList()
          {

               StoreListRows = new ObservableCollection<RowStoreList>();
          }

          #region Biến
          public ObservableCollection<RowStoreList> StoreListRows { get; set; }
          #endregion

          #region Function

          public async Task LoadStoreList()
          {
               var danhSach = await HeThongKhoSyncModel.DanhSachKho();
               StoreListRows.Clear();
               foreach (var item in danhSach) 
               {
                    StoreListRows.Add(item);
               }
          }
          #endregion
     }
}
