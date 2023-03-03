using System.Windows.Controls;

namespace Cryptosystem.View.Symmetric;

public partial class TrithemiusPage : Page
{
    private readonly TextBox _textBox;
    
    public TrithemiusPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        InitializeComponent();
    }
}