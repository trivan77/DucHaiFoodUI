using CNPMNC.Models;
using CNPMNC.Models.DataSync;
using CNPMNC.Models.Rows;
using CNPMNC.Utils;
using CNPMNC.Views.Component;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CNPMNC.ViewModels
{
     class VMExportTicket : NotifyBase
     {
          private static VMExportTicket _instance;
          public static VMExportTicket Instance
          {
               get
               {
                    if (_instance == null)
                         _instance = new VMExportTicket();
                    return _instance;
               }
          }

          public VMExportTicket()
          {
               ProductExportRows = new ObservableCollection<RowImportProduct>();
               ExportTicketListRows = new ObservableCollection<RowImportTicket>();

               DanhSachTenKho = new ObservableCollection<string>();

               DanhSachTenSP = new ObservableCollection<string>();

               ExportDate = DateTime.Now.ToString("yyyy-MM-dd");
          }

          #region Biến

          public ObservableCollection<RowImportProduct> ProductExportRows { get; set; }
          public ObservableCollection<RowImportTicket> ExportTicketListRows { get; set; }
          public ObservableCollection<string> DanhSachTenKho { get; set; }
          public ObservableCollection<string> DanhSachTenSP { get; set; }

          public string productSelected;
          public string ProductSelected
          {
               get => productSelected;
               set
               {
                    productSelected = value;
                    OnPropertyChanged(nameof(ProductSelected));

                    if (value != null)
                    {
                         numStored = Convert.ToInt32(productSelected.Split('-')[1].Trim());
                    }
               }
          }

          private string exportDate;
          public string ExportDate
          {
               get => exportDate;
               set
               {
                    exportDate = value;
                    OnPropertyChanged(nameof(ExportDate));
               }
          }

          private string totalPrice;
          public string TotalPrice
          {
               get => totalPrice;
               set
               {
                    totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
               }
          }

          private int numStored;
          
          private int numExported;
          public int NumExported
          {
               get => numExported;
               set
               {
                    numExported = value;
                    OnPropertyChanged(nameof(NumExported));

                    if (value > numStored) NumExported = numStored;
               }
          }

          VAddExportTicket vAddExportTicket;
          int loiNhuan = 0;
          #endregion

          #region Biến ICommand
          private ICommand mShowAddExportTicket;
          private ICommand mAddProduct;
          private ICommand mUpdateProduct;
          private ICommand mDeleteProduct;
          private ICommand mAddExportTicket;
          #endregion

          #region Get/Set biến ICommand
          public ICommand ShowAddExportTicketCommand
          {
               get
               {
                    if (mShowAddExportTicket == null)
                    {
                         mShowAddExportTicket = new RelayCommand(ShowAddExportTicket);
                    }
                    return mShowAddExportTicket;
               }
          }

          public ICommand AddProductCommand
          {
               get
               {
                    if (mAddProduct == null)
                    {
                         mAddProduct = new RelayCommand(AddProduct);
                    }
                    return mAddProduct;
               }
          }
          public ICommand UpdateProductCommand
          {
               get
               {
                    if (mUpdateProduct == null)
                    {
                         mUpdateProduct = new RelayCommand(UpdateProduct);
                    }
                    return mUpdateProduct;
               }
          }
          public ICommand DeleteProductCommand
          {
               get
               {
                    if (mDeleteProduct == null)
                    {
                         mDeleteProduct = new RelayCommand(DeleteProduct);
                    }
                    return mDeleteProduct;
               }
          }
          public ICommand AddExportTicketCommand
          {
               get
               {
                    if (mAddExportTicket == null)
                    {
                         mAddExportTicket = new RelayCommand(AddExportTicket);
                    }
                    return mAddExportTicket;
               }
          }
          #endregion

          #region Function
          public async void LoadProductNames()
          {
               try
               {
                    var danhSach = await SanPhamSyncModel.DanhSachMaSPTenSP(Convert.ToInt32(UserSession.CurrentUser.MaKho));
                    DanhSachTenSP.Clear();
                    foreach (var item in danhSach) DanhSachTenSP.Add(item);
               }
               catch (Exception ex)
               {
                    SystemNotify.ErrorNotify("Lỗi lấy danh sách sản phẩm: " + ex.Message);
               }
          }

          public void ShowAddExportTicket()
          {
               try
               {
                    // Kiểm tra xem cửa sổ đã được mở chưa
                    var existingWindow = Application.Current.Windows
                        .OfType<VAddExportTicket>()
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
                         vAddExportTicket = new VAddExportTicket();
                         vAddExportTicket.Show();
                    }
               }
               catch (Exception ex)
               {
                    SystemNotify.ErrorNotify("Lỗi: " + ex.Message);
               }
          }

          public async Task LoadExportTicketList()
          {
               var danhSach = await PhieuXuatSyncModel.DanhSachPhieuXuat();
               ExportTicketListRows.Clear();
               foreach (var item in danhSach)
               {
                    ExportTicketListRows.Add(item);
               }
          }

          public async void AddProduct()
          {
               try
               {
                    if (NumExported == 0)
                    {
                         SystemNotify.ErrorNotify("Bạn chưa nhập số lượng sản phẩm!!!");
                    }
                    else if (ProductSelected == "" || ProductSelected == null)
                    {
                         SystemNotify.ErrorNotify("Bạn chưa chọn sản phẩm!!!");
                    }
                    else
                    {
                         int maSP = Convert.ToInt32(ProductSelected.Split('.','-')[0].ToString().Trim());
                         string tenSp = ProductSelected.Split('.', '-')[1].ToString().Trim();

                         bool checkExist = ProductExportRows.Any(p => p.MaSp == maSP);
                         if (checkExist)
                         {
                              SystemNotify.ErrorNotify("Sản phẩm đã được thêm trong danh sách!");
                         }
                         else
                         {
                              SanPham spSelected = await SanPhamSyncModel.GetSanPhamById(maSP);

                              RowImportProduct importProduct = new RowImportProduct();
                              importProduct.STT = ProductExportRows.Count + 1;
                              importProduct.MaSp = maSP;
                              importProduct.TenSp = tenSp;
                              importProduct.SoLuong = NumExported;
                              importProduct.GiaBan = spSelected.GiaBan;
                              importProduct.GiaNhap = spSelected.GiaNhap;

                              ProductExportRows.Add(importProduct);

                              TotalPrice = FormatTienVND(ParseTienVND(TotalPrice) + (((uint)Convert.ToDouble(spSelected.GiaBan)) * (uint)NumExported));
                              loiNhuan += (int)((Convert.ToDouble(spSelected.GiaBan) - Convert.ToDouble(spSelected.GiaNhap)) * NumExported);

                              NumExported = 0;
                              ProductSelected = null;
                         }
                    }
               }
               catch (Exception ex)
               {
                    SystemNotify.ErrorNotify("Lỗi thêm sản phẩm: " + ex.Message);
               }
          }
          public void UpdateProduct()
          {
               try
               {
                    if (NumExported == 0)
                    {
                         SystemNotify.ErrorNotify("Bạn chưa nhập số lượng sản phẩm!!!");
                    }
                    else if (ProductSelected == "" || ProductSelected == null)
                    {
                         SystemNotify.ErrorNotify("Bạn chưa chọn sản phẩm!!!");
                    }
                    else
                    {
                         int maSP = Convert.ToInt32(ProductSelected.Split('.')[0].ToString().Trim());
                         string tenSp = ProductSelected.Split('.')[1].ToString().Trim();

                         bool checkExist = ProductExportRows.Any(p => p.MaSp == maSP);
                         if (!checkExist)
                         {
                              //SystemNotify.ErrorNotify("Sản phẩm đã được thêm trong danh sách!");
                         }
                         else
                         {
                              var updatedProduct = ProductExportRows.FirstOrDefault(p => p.MaSp == maSP);

                              TotalPrice = FormatTienVND(ParseTienVND(TotalPrice) - Convert.ToUInt32(updatedProduct.SoLuong) * (uint)Convert.ToDouble(updatedProduct.GiaBan) + (uint)NumExported * (uint)Convert.ToDouble(updatedProduct.GiaBan));
                              loiNhuan = loiNhuan - (int)((Convert.ToDouble(updatedProduct.GiaBan) - Convert.ToDouble(updatedProduct.GiaNhap)) * (updatedProduct.SoLuong - NumExported));

                              updatedProduct.SoLuong = NumExported;

                              NumExported = 0;
                              ProductSelected = null;
                         }
                    }
               }
               catch (Exception ex)
               {
                    SystemNotify.ErrorNotify("Lỗi cập nhật sản phẩm: " + ex.Message);
               }
          }

          public void DeleteProduct()
          {
               try
               {
                    if (NumExported == 0)
                    {
                         SystemNotify.ErrorNotify("Bạn chưa nhập số lượng sản phẩm!!!");
                    }
                    else if (ProductSelected == "" || ProductSelected == null)
                    {
                         SystemNotify.ErrorNotify("Bạn chưa nhập chọn sản phẩm!!!");
                    }
                    else
                    {
                         int maSP = Convert.ToInt32(ProductSelected.Split('.')[0].ToString().Trim());
                         string tenSp = ProductSelected.Split('.')[1].ToString().Trim();

                         bool checkExist = ProductExportRows.Any(p => p.MaSp == maSP);
                         if (!checkExist)
                         {
                              //SystemNotify.ErrorNotify("Sản phẩm đã được thêm trong danh sách!");
                         }
                         else
                         {
                              var deletedProduct = ProductExportRows.FirstOrDefault(p => p.MaSp == maSP);

                              for (int i = deletedProduct.STT; i < ProductExportRows.Count; i++)
                              {
                                   ProductExportRows[i].STT -= 1;
                              }

                              ProductExportRows.Remove(deletedProduct);

                              TotalPrice = FormatTienVND(ParseTienVND(TotalPrice) - Convert.ToUInt32(deletedProduct.SoLuong) * (uint)Convert.ToDouble(deletedProduct.GiaBan));
                              loiNhuan = loiNhuan - (int)((Convert.ToDouble(deletedProduct.GiaBan) - Convert.ToDouble(deletedProduct.GiaNhap)) * deletedProduct.SoLuong);

                              NumExported = 0;
                              ProductSelected = null;
                         }
                    }
               }
               catch (Exception ex)
               {
                    SystemNotify.ErrorNotify("Lỗi xóa sản phẩm: " + ex.Message);
               }
          }

          public async void AddExportTicket()
          {
               try
               {
                    await PhieuXuatSyncModel.ThemPhieuXuat(UserSession.CurrentUser.MaNv, ExportDate, Convert.ToInt32(UserSession.CurrentUser.MaKho), ParseTienVND(TotalPrice).ToString() + ".00", loiNhuan.ToString() + ".00");

                    await PhieuXuatSyncModel.ThemChiTietPhieuXuat(ProductExportRows, UserSession.CurrentUser.MaKho);

                    ProductExportRows.Clear();

                    ExportDate = DateTime.Now.ToString("yyyy-MM-dd");

                    TotalPrice = "0";
                    loiNhuan = 0;
               }
               catch (Exception ex)
               {
                    SystemNotify.ErrorNotify("Lỗi: " + ex.Message);
               }
          }

          public string FormatTienVND(decimal soTien)
          {
               return string.Format("{0:N0}", soTien).Replace(",", ".");
          }

          public static uint ParseTienVND(string soTienStr)
          {
               if (soTienStr == null || soTienStr == "") soTienStr = "0";
               // Bỏ dấu chấm và khoảng trắng
               var cleaned = soTienStr.Replace(".", "").Trim();

               // Chuyển về số
               if (uint.TryParse(cleaned, out uint result))
                    return result;

               throw new FormatException("Số tiền không hợp lệ.");
          }
          #endregion
     }
}
