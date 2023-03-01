using System.Windows;
using System.Windows.Controls;
using Lab1.Cryptography;

namespace Lab1.View;

public partial class Base64Page : Page
{
    private readonly TextBox _textBox;
 
    public Base64Page(MainWindow window)
    {
        _textBox = window.MainTextArea;
        _textBox.TextChanged += TextBoxOnTextChanged;
        InitializeComponent();
    }

    private void EncryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        _textBox.Text = new Base64().Encrypt(_textBox.Text);
    }

    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        _textBox.Text = new Base64().Decrypt(_textBox.Text);
    }
    
    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        EncryptButton.IsEnabled = !_textBox.Text.Equals("");
        DecryptButton.IsEnabled = !_textBox.Text.Equals("");
    }
}