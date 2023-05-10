using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cryptosystem.View.Asymmetric;

public partial class RSAPage : Page
{
    private readonly TextBox _textBox;
    
    public RSAPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        //_textBox.TextChanged += TextBoxOnTextChanged;
        
        InitializeComponent();
    }
    
    private void NumericOnly(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !NumericTextRegex().IsMatch(e.Text);
    }

    [GeneratedRegex(@"^[0-9\-]+$")]
    private static partial Regex NumericTextRegex();

    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void EncryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void PaddingModeChanged(object sender, SelectionChangedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ClearButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void GenerateButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void OpenKeyButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void SaveKeyButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void HelpButton_OnClick(object sender, RoutedEventArgs e)
    {
        var message = "Keys must be in PEM format.\n" +
                      "Also, secret key have to comply with the standard PKCS#8.\n\n" +
                      "Key size: Number in bits, must be a power of two.\n" +
                      "Lower than 2048 - can be cracked by quantum computer.\n\n" +
                      "Padding: Check documentation.\n" +
                      "If you don't want, you can not select or select 'Default'";

        MessageBox.Show(messageBoxText: message,
            caption: "Info",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Information);
    }
}