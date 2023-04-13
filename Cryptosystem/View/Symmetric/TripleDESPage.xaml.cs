using System.IO;
using System.Windows.Controls;

namespace Cryptosystem.View.Symmetric;

public partial class TripleDESPage : Page
{
    private readonly TextBox _textBox;
    private FileInfo? _fileInfo;
    
    public TripleDESPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        _textBox.TextChanged += TextBoxOnTextChanged;
        
        InitializeComponent();
    }

    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}