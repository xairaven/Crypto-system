using System.Windows.Controls;

namespace Cryptosystem.View.Symmetric;

public partial class LitFragPage : Page
{
    private readonly TextBox _textBox;
    
    public LitFragPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        
        InitializeComponent();
    }
}