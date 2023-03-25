using System.Windows.Controls;

namespace Cryptosystem.View.Symmetric;

public partial class XORCipherPage : Page
{
    private readonly TextBox _textBox;
    
    public XORCipherPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        
        InitializeComponent();
    }
}