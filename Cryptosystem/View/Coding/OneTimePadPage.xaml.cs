using System.Windows.Controls;

namespace Cryptosystem.View.Coding;

public partial class OneTimePadPage : Page
{
    private readonly TextBox _textBox;
    
    public OneTimePadPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        
        InitializeComponent();
    }
}