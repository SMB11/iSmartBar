using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Security.Internal.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        public string ErrorText
        {
            set
            {
                txtError.Text = value;
            }
        }

        public string Username
        {
            get
            {
                return txtUsername.Text;
            }
        }
        
        public string Password
        {
            get
            {
                return txtPassword.Text;
            }
        }
    }
}
