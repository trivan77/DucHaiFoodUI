using CNPMNC.Models.ForOverview.ImportHistoryTable;
using CNPMNC.Utils;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CNPMNC.ViewModels
{
     class VMOverview : NotifyBase
     {
          public VMOverview()
          {
               ImportHistoryRows = new ObservableCollection<ImportHistoryRowModel>();
               for(int i = 1; i < 50; i++)
               {
                    AddRow(new ImportHistoryRowModel
                    {
                         MaPhieu = "MaPhieu_"+i.ToString(),
                         ThoiGian = "09/09/24",
                         Kho = "Kho Nam Từ Liêm",
                         ThanhTien = i.ToString()+"00.000.000",
                         NguoiTao = "Trần Bảo Trí"
                    });
               }
          }

          #region Biến

          public ObservableCollection<ImportHistoryRowModel> ImportHistoryRows { get; set; }
          public ISeries[] DoanhThuSeries { get; set; } = new ISeries[]
          {
               new ScatterSeries<int,RectangleGeometry>
               {
                    Fill = new SolidColorPaint(new SKColor(0x12, 0x55, 0x93, 0xFF)),
                    Stroke = null,
                    GeometrySize = 10,
                    Values = new ObservableCollection<int> { 200, 558, 458, 249 },
               }
          };


          public Axis[] XAxes { get; set; } = new Axis[]
          {
               new Axis
               {
                    // Use the labels property to define named labels.
                    Labels = new string[] { "JAN", "FEB", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUG", "SEP", "OCT", "NOV", "DEC" },
                    TextSize = 13
               }
          };
          public Axis[] YAxes { get; set; } = new Axis[]
          {
               new Axis
               {
                    TextSize = 13
               }
          };

          public List<double> Values = new List<double> { 100, 200, 110, 135 };
          public List<string> Legends = new List<string> { "Máy giặt", "Máy rửa bát", "Điện thoại", "Laptop" };
          #endregion

          #region Function
          public void AddRow(ImportHistoryRowModel row)
          {
               ImportHistoryRows.Add(row);
          }
          #endregion
     }
}
