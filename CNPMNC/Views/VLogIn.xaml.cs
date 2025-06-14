using CNPMNC.Models;
using CNPMNC.Models.DataSync;
using CNPMNC.Utils;
using CNPMNC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CNPMNC
{
     /// <summary>
     /// Interaction logic for MainWindow.xaml
     /// </summary>
     public partial class VLogIn : Window
     {
          #region Var
          bool isPWVisible = false;
          public static string userName = "";
          #endregion

          public VLogIn()
          {
               InitializeComponent();
          }

          private void txtUserName_TextChanged(object sender, TextChangedEventArgs e)
          {
               txtPlaceHolderUserName.Visibility = string.IsNullOrEmpty(txtUserName.Text) ? Visibility.Visible : Visibility.Collapsed;
          }

          private void pwBox_PasswordChanged(object sender, RoutedEventArgs e)
          {
               txtPlaceHolderPW.Visibility = string.IsNullOrEmpty(pwBox.Password) ? Visibility.Visible : Visibility.Collapsed;
               txtPW.Text = pwBox.Password;
          }

          private void txtPW_TextChanged(object sender, TextChangedEventArgs e)
          {
               txtPlaceHolderPW.Visibility = string.IsNullOrEmpty(txtPW.Text) ? Visibility.Visible : Visibility.Collapsed;
          }

          private void btnHidePW_MouseDown(object sender, MouseButtonEventArgs e)
          {
               if (isPWVisible)
               {
                    // Ẩn mật khẩu: Chuyển về PasswordBox
                    txtPW.Visibility = Visibility.Collapsed;
                    pwBox.Visibility = Visibility.Visible;
                    pwBox.Password = txtPW.Text;
               }
               else
               {
                    // Hiển thị mật khẩu: Chuyển sang TextBox
                    txtPW.Visibility = Visibility.Visible;
                    pwBox.Visibility = Visibility.Collapsed;
                    txtPW.Text = pwBox.Password;
               }

               isPWVisible = !isPWVisible;
          }

          private void btnLogin_MouseEnter(object sender, MouseEventArgs e)
          {
               btnLogin.Background = new SolidColorBrush(Color.FromRgb(27, 70, 110));
          }

          private void btnLogin_MouseLeave(object sender, MouseEventArgs e)
          {
               btnLogin.Background = new SolidColorBrush(Color.FromRgb(18, 85, 147));
          }

          private async void btnLogin_MouseDown(object sender, MouseButtonEventArgs e)
          {
               string username = txtUserName.Text;
               string password = pwBox.Password;

               bool success = await NhanVienSyncModel.DangNhap(username, password);

               if (success)
               {
                    // Mở cửa sổ chính hoặc chuyển sang giao diện chính
                    VMainWindow main = new VMainWindow();
                    main.Show();
                    this.Close();
               }
               else
               {
                    SystemNotify.ErrorNotify("Tài khoản hoặc mật khẩu không đúng!!!");
               }
          }
     }
}
