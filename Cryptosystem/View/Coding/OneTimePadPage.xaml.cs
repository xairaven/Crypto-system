using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cryptosystem.Cryptography.Coding;

namespace Cryptosystem.View.Coding;

public partial class OneTimePadPage : Page
{
    private readonly TextBox _textBox;
    
    public OneTimePadPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        
        InitializeComponent();
    }
    
    private void NumericOnly(object sender, TextCompositionEventArgs e)
    {
        e.Handled = IsTextNumeric(e.Text);
    }
    
    private static bool IsTextNumeric(string str)
    {
        return !MyRegex().IsMatch(str);
    }

    [GeneratedRegex(@"^[0-9\-]+$")]
    private static partial Regex MyRegex();

    private void GenerateButton_OnClick(object sender, RoutedEventArgs e)
    {
        int.TryParse(LengthBox.Text, out var padLength);
        if (!Validate(padLength)) return;
        
        _textBox.Text = new OneTimePad().Create(padLength);
    }

    private bool Validate(int key)
    {
        if (key != 0) return true;
       
        MessageBox.Show(
            messageBoxText: "Key must be a number (or unequal to 0)",
            caption: "Error",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Error
        );
        return false;
    }

    private void LengthBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        GenerateButton.IsEnabled = !LengthBox.Text.Trim().Equals("");
    }
}