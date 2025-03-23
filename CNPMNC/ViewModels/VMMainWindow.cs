using CNPMNC.Utils;
using CNPMNC.Views.Usercontrol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CNPMNC.ViewModels
{
     class VMMainWindow : NotifyBase
     {
          public void VMainWindow()
          {
               // Khởi tạo giá trị mặc định
               CurrentView = new UCOverview(); // Hoặc để null nếu muốn ẩn ban đầu
          }

          #region Biến
          private UserControl currentView;
          private Visibility isTicketChildVisible = Visibility.Hidden;
          #endregion

          #region Get/Set Biến 
          public UserControl CurrentView
          {
               get => currentView;
               set
               {
                    currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
               }
          }

          public Visibility IsTicketChildVisible
          {
               get => isTicketChildVisible;
               set
               {
                    isTicketChildVisible = value;
                    OnPropertyChanged(nameof(IsTicketChildVisible));
               }
          } 
          #endregion

          #region Biến ICommand
          private ICommand mShowOverview;
          private ICommand mShowStaffList;
          private ICommand mShowStoreList;
          private ICommand mShowTicketChild;
          private ICommand mShowImportTicket;
          private ICommand mShowExportTicket;
          private ICommand mShowTransTicket;
          #endregion

          #region Get/Set Biến ICommand
          public ICommand ShowOverviewCommand
          {
               get
               {
                    if (mShowOverview == null)
                    {
                         mShowOverview = new RelayCommand(ShowOverview);
                    }
                    return mShowOverview;
               }
          }

          public ICommand ShowStaffListCommand
          {
               get
               {
                    if (mShowStaffList == null)
                    {
                         mShowStaffList = new RelayCommand(ShowStaffList);
                    }
                    return mShowStaffList;
               }
          }

          public ICommand ShowStoreListCommand
          {
               get
               {
                    if (mShowStoreList == null)
                    {
                         mShowStoreList = new RelayCommand(ShowStoreList);
                    }
                    return mShowStoreList;
               }
          }

          public ICommand ShowTicketChildCommand
          {
               get
               {
                    if (mShowTicketChild == null)
                    {
                         mShowTicketChild = new RelayCommand(ShowTicketChild);
                    }
                    return mShowTicketChild;
               }
          }

          public ICommand ShowImportTicketCommand
          {
               get
               {
                    if (mShowImportTicket == null)
                    {
                         mShowImportTicket = new RelayCommand(ShowImportTicket);
                    }
                    return mShowImportTicket;
               }
          }

          public ICommand ShowExportTicketCommand
          {
               get
               {
                    if (mShowExportTicket == null)
                    {
                         mShowExportTicket = new RelayCommand(ShowExportTicket);
                    }
                    return mShowExportTicket;
               }
          }

          public ICommand ShowTransTicketCommand
          {
               get
               {
                    if (mShowTransTicket == null)
                    {
                         mShowTransTicket = new RelayCommand(ShowTransTicket);
                    }
                    return mShowTransTicket;
               }
          }
          #endregion

          private void ShowOverview()
          {
               if (CurrentView is UCOverview) return;
               CurrentView = new UCOverview();
          }

          private void ShowStaffList()
          {
               if (CurrentView is UCStaffList) return;
               CurrentView = new UCStaffList();
          }

          private void ShowStoreList()
          {
               if (CurrentView is UCStoreList) return;
               CurrentView = new UCStoreList();
          }

          private void ShowTicketChild()
          {
               if(IsTicketChildVisible == Visibility.Hidden)
               {
                    IsTicketChildVisible = Visibility.Visible;
               }
               else if (IsTicketChildVisible == Visibility.Visible)
               {
                    IsTicketChildVisible = Visibility.Hidden;
               }
          }

          private void ShowImportTicket()
          {
               if (CurrentView is UCImportTicketList) return;
               CurrentView = new UCImportTicketList();
          }

          private void ShowExportTicket()
          {
               if (CurrentView is UCExportTicketList) return;
               CurrentView = new UCExportTicketList();
          }

          private void ShowTransTicket()
          {
               if (CurrentView is UCTransTicketList) return;
               CurrentView = new UCTransTicketList();
          }
     }
}
