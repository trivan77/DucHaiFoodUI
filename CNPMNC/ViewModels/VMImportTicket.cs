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
     class VMImportTicket : NotifyBase
     {
          private static VMImportTicket _instance;
          public static VMImportTicket Instance
          {
               get
               {
                    if (_instance == null)
                         _instance = new VMImportTicket();
                    return _instance;
               }
          }

          public VMImportTicket()
          {
               ProductImportRows = new ObservableCollection<RowImportProduct>();

               DanhSachTenKho = new ObservableCollection<string>();

               DanhSachTenSP = new ObservableCollection<string>();
          }

          #region Biến

          public ObservableCollection<RowImportProduct> ProductImportRows { get; set; }
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

          private int numImported;
          public int NumImported
          {
               get => numImported;
               set
               {
                    numImported = value;
                    OnPropertyChanged(nameof(NumImported));
               }
          }

          VAddImportTicket vAddImportTicket;
          #endregion

          #region Biến ICommand
          private ICommand mShowAddImportTicket;
          private ICommand mAddProduct;
          private ICommand mUpdateProduct;
          private ICommand mDeleteProduct;
          #endregion

          #region Get/Set Biến ICommand
          public ICommand ShowAddImportTicketCommand
          {
               get
               {
                    if (mShowAddImportTicket == null)
                    {
                         mShowAddImportTicket = new RelayCommand(ShowAddImportTicket);
                    }
                    return mShowAddImportTicket;
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
          #endregion

          #region function
          public void ShowAddImportTicket()
          {
               try
               {
                    // Kiểm tra xem cửa sổ đã được mở chưa
                    var existingWindow = Application.Current.Windows
                        .OfType<VAddImportTicket>()
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
                         vAddImportTicket = new VAddImportTicket();
                         vAddImportTicket.Show();
                    }
               }
               catch (Exception ex)
               {
                    SystemNotify.ErrorNotify("Lỗi: " + ex.Message);
               }
          }

          public async void LoadStoreNames()
          {
               try
               {
                    var danhSach = await HeThongKhoSyncModel.DanhSachMaKhoTenKho();
                    DanhSachTenKho.Clear();
                    foreach (var item in danhSach) DanhSachTenKho.Add(item);
               }
               catch (Exception ex)
               {
                    SystemNotify.ErrorNotify("Lỗi lấy danh sách kho: " + ex.Message);
               }
          }

          public async void LoadProductNames()
          {
               try
               {
                    var danhSach = await SanPhamSyncModel.DanhSachMaSPTenSP();
                    DanhSachTenSP.Clear();
                    foreach (var item in danhSach) DanhSachTenSP.Add(item);
               }
               catch (Exception ex)
               {
                    SystemNotify.ErrorNotify("Lỗi lấy danh sách sản phẩm: " + ex.Message);
               }
          }

          public async void AddProduct()
          {
               try
               {
                    if (NumImported == 0)
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

                         bool checkExist = ProductImportRows.Any(p => p.MaSp == maSP);
                         if (checkExist)
                         {
                              SystemNotify.ErrorNotify("Sản phẩm đã được thêm trong danh sách!");
                         }
                         else
                         {
                              SanPham spSelected = await SanPhamSyncModel.GetSanPhamById(maSP);

                              RowImportProduct importProduct = new RowImportProduct();
                              importProduct.STT = ProductImportRows.Count + 1;
                              importProduct.MaSp = maSP;
                              importProduct.TenSp = tenSp;
                              importProduct.SoLuong = NumImported;
                              importProduct.GiaBan = spSelected.GiaBan;
                              importProduct.GiaNhap = spSelected.GiaNhap;

                              ProductImportRows.Add(importProduct);

                              TotalPrice = FormatTienVND(ParseTienVND(TotalPrice) + (((uint)Convert.ToDouble(spSelected.GiaBan)) * (uint)NumImported));

                              NumImported = 0;
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
                    if (NumImported == 0)
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

                         bool checkExist = ProductImportRows.Any(p => p.MaSp == maSP);
                         if (!checkExist)
                         {
                              //SystemNotify.ErrorNotify("Sản phẩm đã được thêm trong danh sách!");
                         }
                         else
                         {
                              var updatedProduct = ProductImportRows.FirstOrDefault(p => p.MaSp == maSP);

                              TotalPrice = FormatTienVND(ParseTienVND(TotalPrice) - Convert.ToUInt32(updatedProduct.SoLuong) * (uint)Convert.ToDouble(updatedProduct.GiaBan) + (uint)NumImported * (uint)Convert.ToDouble(updatedProduct.GiaBan));

                              updatedProduct.SoLuong = NumImported;
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
                    if (NumImported == 0)
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

                         bool checkExist = ProductImportRows.Any(p => p.MaSp == maSP);
                         if (!checkExist)
                         {
                              //SystemNotify.ErrorNotify("Sản phẩm đã được thêm trong danh sách!");
                         }
                         else
                         {
                              var deletedProduct = ProductImportRows.FirstOrDefault(p => p.MaSp == maSP);

                              for (int i = deletedProduct.STT; i < ProductImportRows.Count; i++)
                              {
                                   ProductImportRows[i].STT -= 1;
                              }

                              ProductImportRows.Remove(deletedProduct);

                              TotalPrice = FormatTienVND(ParseTienVND(TotalPrice) - Convert.ToUInt32(deletedProduct.SoLuong) * (uint)Convert.ToDouble(deletedProduct.GiaBan));
                         }
                    }
               }
               catch (Exception ex)
               {
                    SystemNotify.ErrorNotify("Lỗi xóa sản phẩm: " + ex.Message);
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
