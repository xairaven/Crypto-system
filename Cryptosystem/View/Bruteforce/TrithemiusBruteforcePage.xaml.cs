using System.Windows.Controls;

namespace Cryptosystem.View.Bruteforce;

public partial class TrithemiusBruteforcePage : Page
{
    private readonly TextBox _textBox;
    
    public TrithemiusBruteforcePage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        
        InitializeComponent();
    }
}