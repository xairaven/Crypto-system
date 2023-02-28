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

namespace Lab1.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<Page> _pages;
        
        public MainWindow()
        {
            InitializeComponent();

            _pages = new List<Page>()
            {
                new CaesarPage(),
                new Base64Page()
            };
        }

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
            new AboutWindow().Show();
        }

        private void CipherChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox list)
            {
                CipherFrame.Content = _pages[(int) list.SelectedItem];
            }
        }
    }
}