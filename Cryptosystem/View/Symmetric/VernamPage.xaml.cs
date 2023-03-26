using System.IO;
using System.Windows.Controls;

namespace Cryptosystem.View.Symmetric;

public partial class VernamPage : Page
{
    private readonly TextBox _textBox;
    private FileInfo? _dictFileInfo;
    
    public VernamPage(MainWindow window)
    {
        _textBox = window.MainTextArea;

        InitializeComponent();
    }
}