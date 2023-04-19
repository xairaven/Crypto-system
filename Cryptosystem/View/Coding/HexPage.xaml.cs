using System.Windows;
using System.Windows.Controls;
using Cryptosystem.Cryptography.Coding;

namespace Cryptosystem.View.Coding;

public partial class HexPage : Page
{
    private readonly TextBox _textBox;
 
    public HexPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        _textBox.TextChanged += TextBoxOnTextChanged;
        InitializeComponent();
    }

    private void EncryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        _textBox.Text = new Hexadecimal().Encrypt(_textBox.Text);
    }

    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        _textBox.Text = new Hexadecimal().Decrypt(_textBox.Text);
    }
    
    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        EncryptButton.IsEnabled = !_textBox.Text.Equals("");
        DecryptButton.IsEnabled = !_textBox.Text.Equals("");
    }
}