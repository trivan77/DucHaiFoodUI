using System;
using System.Collections;
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

namespace CNPMNC.Views.Component
{
     /// <summary>
     /// Interaction logic for FloatingLabelComboBox.xaml
     /// </summary>
     public partial class FloatingLabelComboBox : UserControl
     {
          public event EventHandler DropDownOpened;
          public FloatingLabelComboBox()
          {
               InitializeComponent();

               partComboBox.DropDownOpened += (s, e) =>
               {
                    DropDownOpened?.Invoke(this, EventArgs.Empty);
               };
          }

          public static readonly DependencyProperty ItemsSourceProperty =
               DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(FloatingLabelComboBox), new PropertyMetadata(null));

          public IEnumerable ItemsSource
          {
               get => (IEnumerable)GetValue(ItemsSourceProperty);
               set => SetValue(ItemsSourceProperty, value);
          }

          public static readonly DependencyProperty SelectedItemProperty =
               DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(FloatingLabelComboBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

          public object SelectedItem
          {
               get => GetValue(SelectedItemProperty);
               set => SetValue(SelectedItemProperty, value);
          }

          public static readonly DependencyProperty LabelTextProperty =
               DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(FloatingLabelComboBox), new PropertyMetadata(""));

          public string LabelText
          {
               get => (string)GetValue(LabelTextProperty);
               set => SetValue(LabelTextProperty, value);
          }
     }
}
