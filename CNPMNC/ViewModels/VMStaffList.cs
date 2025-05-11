using CNPMNC.Models;
using CNPMNC.Models.DataSync;
using CNPMNC.Models.Rows;
using CNPMNC.Utils;
using CNPMNC.Views.Component;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CNPMNC.ViewModels
{
     class VMStaffList : NotifyBase
     {
          public VMStaffList()
          {
               StaffListRows = new ObservableCollection<RowStaffList>();
               for (int i = 1; i < 13; i++)
               {
                    if (i % 3 == 0)
                    {
                         AddRow(new RowStaffList
                         {
                              STT = i,
                              TenNv = "HovaTen_" + (i + 1).ToString(),
                              DienThoai = "086899" + i.ToString() + i.ToString() + i.ToString() + i.ToString(),
                              TenKho = "Kho Nam Từ Liêm",
                              ThanhTien = (i + 7) * 1000000,
                              NgayTuyenDung = "30/4/2025",
                              ChucVu = "Phụ trách kho"
                         }) ;
                    }
                    else if (i == 5)
                    {
                         AddRow(new RowStaffList
                         {
                              STT = i,
                              TenNv = "HovaTen_" + (i + 1).ToString(),
                              DienThoai = "086899" + i.ToString() + i.ToString() + i.ToString() + i.ToString(),
                              TenKho = "Kho Nam Từ Liêm",
                              ThanhTien = (i + 7) * 1000000,
                              NgayTuyenDung = "30/4/2025",
                              ChucVu = "Admin"
                         }) ;
                    }
                    else
                    {
                         AddRow(new RowStaffList
                         {
                              STT = i,
                              TenNv = "HovaTen_" + (i + 1).ToString(),
                              DienThoai = "086899" + i.ToString() + i.ToString() + i.ToString() + i.ToString(),
                              TenKho = "Kho Nam Từ Liêm",
                              ThanhTien = (i + 7) * 1000000,
                              NgayTuyenDung = "30/4/2025",
                              ChucVu = "Nhân viên"
                         });
                    }
               }

               DanhSachTenKho = new ObservableCollection<string>();
          }

          #region Biến

          public ObservableCollection<RowStaffList> StaffListRows { get; set; }
          public ObservableCollection<string> DanhSachTenKho { get; set; }

          VAddStaff vAddStaff;

          public string  KhoSelected { get; set; }

          private NhanVien _nhanVienAdded = new NhanVien();
          public NhanVien NhanVienAdded
          {
               get => _nhanVienAdded;
               set
               {
                    _nhanVienAdded = value;
                    OnPropertyChanged(nameof(NhanVienAdded));
               }
          }
          #endregion

          #region Function
          public void AddRow(RowStaffList row)
          {
               StaffListRows.Add(row);
          }

          public void ShowAddStaff()
          {
               // Kiểm tra xem cửa sổ đã được mở chưa
               var existingWindow = Application.Current.Windows
                   .OfType<VAddStaff>()
                   .FirstOrDefault();

               if (existingWindow != null)
               {
                    // Nếu đã mở, đưa cửa sổ lên trước
                    if (existingWindow.WindowState == WindowState.Minimized)
                         existingWindow.WindowState = WindowState.Normal;

                    existingWindow.Activate();
               }
               else
               {
                    // Nếu chưa mở, tạo mới và hiển thị
                    vAddStaff = new VAddStaff();
                    vAddStaff.Show();
               }
          }

          public async void LoadStoreNames()
          {
               var danhSach = await HeThongKhoSyncModel.DanhSachMaKhoTenKho();
               DanhSachTenKho.Clear();
               foreach (var item in danhSach) DanhSachTenKho.Add(item);
          }

          public void AddStaff()
          {
               var url = "http://cong-nghe-phan-mem.asuna.id.vn/api/cong-nghe-phan-mem/nhan-vien/create";

               var payload = new
               {
                    values = new
                    {
                         tenNv = NhanVienAdded.TenNv,
                         gioiTinh = NhanVienAdded.GioiTinh,
                         dienThoai = NhanVienAdded.DienThoai,
                         diaChi = NhanVienAdded.DiaChi,
                         soCmnd = NhanVienAdded.SoCmnd,
                         chucVu = NhanVienAdded.ChucVu,
                         username= NhanVienAdded.Username,
                         matKhau = NhanVienAdded.MatKhau,
                         ngayTuyenDung = NhanVienAdded.NgayTuyenDung,
                         maKho = Convert.ToInt32(KhoSelected.Split('.')[0].Trim())
                    }
               };

               var json = JsonConvert.SerializeObject(payload);
               var content = new StringContent(json, Encoding.UTF8, "application/json");

               using (HttpClient client = new HttpClient())
               {
                    HttpResponseMessage response = client.PostAsync(url, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                         string result = response.Content.ReadAsStringAsync().Result;

                         SystemNotify.SuccessNotify("Thêm người dùng thành công !!!");
                    }
                    else
                    {
                         string error = response.Content.ReadAsStringAsync().Result;
                         Console.WriteLine("Lỗi: " + error);
                    }
               }
          }
          #endregion

          #region
          private ICommand mShowAddStaff;
          private ICommand mAddStaff;
          private ICommand mGetStoreName;
          #endregion

          #region Get/Set Biến ICommand
          public ICommand ShowAddStaffCommand
          {
               get
               {
                    if (mShowAddStaff == null)
                    {
                         mShowAddStaff = new RelayCommand(ShowAddStaff);
                    }
                    return mShowAddStaff;
               }
          }

          public ICommand AddStaffCommand
          {
               get
               {
                    if (mShowAddStaff == null)
                    {
                         mShowAddStaff = new RelayCommand(AddStaff);
                    }
                    return mShowAddStaff;
               }
          }
          #endregion
     }
}
