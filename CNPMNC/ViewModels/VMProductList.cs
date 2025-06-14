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
     class VMProductList : NotifyBase
     {
          Dictionary<string,int> LoaiSanPhamListString = new Dictionary<string, int>  { { "Thực phẩm tươi sống", 1 },  { "Thực phẩm đông lạnh", 2 }, { "Bánh kẹo/Đồ ăn vặt", 3 }, { "Đồ uống", 4 }, { "Gia vị", 5 } };

          private static VMProductList _instance;
          public static VMProductList Instance
          {
               get
               {
                    if (_instance == null)
                         _instance = new VMProductList();
                    return _instance;
               }
          }

          public VMProductList()
          {
               ProductListRows = new ObservableCollection<RowProductList>();
          }

          #region Biến
          public ObservableCollection<RowProductList> ProductListRows { get; set; }

          VAddProduct vAddProduct;

          private SanPham _sanPhamAdded = new SanPham();
          private string loaiSPSelected;
          public SanPham SanPhamAdded
          {
               get => _sanPhamAdded;
               set
               {
                    _sanPhamAdded = value;
                    OnPropertyChanged(nameof(SanPhamAdded));
               }
          }

          public string LoaiSPSelected
          {
               get => loaiSPSelected;
               set
               {
                    loaiSPSelected = value;
                    OnPropertyChanged(nameof(LoaiSPSelected));
               }
          }

          #endregion

          #region Function
          public void AddRow(RowProductList row)
          {
               ProductListRows.Add(row);
          }

          public void ShowAddProduct()
          {
               // Kiểm tra xem cửa sổ đã được mở chưa
               var existingWindow = Application.Current.Windows
                   .OfType<VAddProduct>()
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
                    vAddProduct = new VAddProduct();

                    // Gắn sự kiện đóng cửa sổ
                    vAddProduct.Closed += async (s, e) =>
                    {
                         await LoadProductList();  // Gọi lại khi cửa sổ đóng
                    };

                    vAddProduct.Show();
               }
          }

          public async Task LoadProductList()
          {
               var danhSach = await SanPhamSyncModel.DanhSachSanPham();
               ProductListRows.Clear();
               foreach (var item in danhSach)
               {
                    ProductListRows.Add(item);
               }
          }

          public async Task AddProduct()
          {
               var url = "http://cong-nghe-phan-mem.asuna.id.vn/api/cong-nghe-phan-mem/san-pham/create";

               var payload = new
               {
                    values = new
                    {
                         tenSp = SanPhamAdded.TenSp,
                         donViTinh = SanPhamAdded.DonViTinh,
                         loaiSp = LoaiSanPhamListString[LoaiSPSelected],
                         giaNhap = SanPhamAdded.GiaNhap,
                         giaBan = SanPhamAdded.GiaBan
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

                         var responseObj = JsonConvert.DeserializeObject<ApiResponse<object>>(result);

                         if (responseObj != null && responseObj.Status == 1)
                         {
                              SystemNotify.SuccessNotify("Thêm sản phẩm thành công !!!");
                              await LoadProductList();  // Gọi để load lại ds sản phẩm
                         }
                         else
                         {
                              SystemNotify.ErrorNotify("Thêm sản phẩm thất bại!!!");
                         }

                    }
                    else
                    {
                         SystemNotify.ErrorNotify("Lỗi kết nối với máy chủ !!!");
                    }
               }
          }
          #endregion

          #region ICommand
          private ICommand mShowAddProduct;
          private ICommand mAddProduct;
          private ICommand mGetStoreName;
          #endregion

          #region Get/Set Biến ICommand
          public ICommand ShowAddProductCommand
          {
               get
               {
                    if (mShowAddProduct == null)
                    {
                         mShowAddProduct = new RelayCommand(ShowAddProduct);
                    }
                    return mShowAddProduct;
               }
          }

          public ICommand AddProductCommand
          {
               get
               {
                    if (mAddProduct == null)
                    {
                         mAddProduct = new AsyncRelayCommand(AddProduct);
                    }
                    return mAddProduct;
               }
          }
          #endregion
     }
}
