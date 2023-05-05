using System.Windows.Controls;

namespace Cryptosystem.View.Asymmetric;

public partial class DiffieHellmanPage : Page
{
    private readonly TextBox _textBox;
    
    public DiffieHellmanPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        //_textBox.TextChanged += TextBoxOnTextChanged;
        
        InitializeComponent();
    }
    
    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}