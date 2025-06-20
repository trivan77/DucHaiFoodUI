using CNPMNC.Models.Rows;
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
               ImportHistoryRows = new ObservableCollection<RowImportHistory>();
               for(int i = 1; i < 50; i++)
               {
                    AddRow(new RowImportHistory
                    {
                         MaPhieu = i * 100,
                         NgayNhap = "09/09/24",
                         TenKho = "Kho" + i.ToString(),
                         ThanhTien = (i + 7) * 1000000,
                         TenNguoiTao = "Trần Bảo Trí"
                    });
               }
          }

          #region Biến

          public ObservableCollection<RowImportHistory> ImportHistoryRows { get; set; }
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
          public List<string> Legends = new List<string> { "Đùi gà lóc xương", "Nước mắm tỏi ớt", "Tương ớt Chinsu", "Bánh bông lan" };
          #endregion

          #region Function
          public void AddRow(RowImportHistory row)
          {
               ImportHistoryRows.Add(row);
          }
          #endregion
     }
}
