using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CNPMNC.Utils
{
     class SystemNotify
     {
          public static void ErrorNotify(string msg)
          {
               MessageBox.Show(msg, "Lỗi xảy ra", MessageBoxButton.OK, MessageBoxImage.Error);
          }

          public static void SuccessNotify(string msg)
          {
               MessageBox.Show(msg, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
          }
     }
}
