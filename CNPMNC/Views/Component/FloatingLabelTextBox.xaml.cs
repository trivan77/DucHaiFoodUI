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

namespace CNPMNC.Views.Component
{
     /// <summary>
     /// Interaction logic for FloatingLabelTextBox.xaml
     /// </summary>
     public partial class FloatingLabelTextBox : UserControl
     {
          public FloatingLabelTextBox()
          {
               InitializeComponent();
          }

          public static readonly DependencyProperty LabelTextProperty =
              DependencyProperty.Register("LabelText", typeof(string), typeof(FloatingLabelTextBox), new PropertyMetadata(""));

          public string LabelText
          {
               get => (string)GetValue(LabelTextProperty);
               set => SetValue(LabelTextProperty, value);
          }

          public static readonly DependencyProperty TextProperty =
              DependencyProperty.Register("Text", typeof(string), typeof(FloatingLabelTextBox), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

          public string Text
          {
               get => (string)GetValue(TextProperty);
               set => SetValue(TextProperty, value);
          }
     }
}
