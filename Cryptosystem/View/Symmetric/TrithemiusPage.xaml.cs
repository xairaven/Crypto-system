using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cryptosystem.Cryptography.Symmetric;

namespace Cryptosystem.View.Symmetric;

public partial class TrithemiusPage : Page
{
    private readonly TextBox _textBox;
    
    public TrithemiusPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        _textBox.TextChanged += TextBoxOnTextChanged;
        InitializeComponent();
    }

    private void KeyTypeCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox) return;
        
        AreKeyBoxesEnabled();
        IsButtonsEnabled(ValidateAllBoxes());
    }

    private void AreKeyBoxesEnabled()
    {
        if (KeyTypeCombo.SelectedItem is not KeyType type) return;
        
        ATermBox.IsEnabled = type is KeyType.Linear or KeyType.NonLinear;
        BTermBox.IsEnabled = type is KeyType.Linear or KeyType.NonLinear;
        CTermBox.IsEnabled = type is KeyType.NonLinear;
        MottoBox.IsEnabled = type is KeyType.Motto;
    }
    
    private void TermBoxEnabled(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is not TextBox box) return;

        if ((bool)e.NewValue)
        {
            box.Text = "0";
            return;
        }
        
        ClearIfDisabled(sender, e);
    }
    
    private void ClearIfDisabled(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is not TextBox box) return;
        
        if ((bool)e.NewValue == false) box.Text = "";
    }
    
    private void TermBox_OnLostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is not TextBox box) return;

        if (box.Text.Trim().Equals("")) box.Text = "0";
        
        IsButtonsEnabled(ValidateAllBoxes());
    }
    
    private void MottoBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsButtonsEnabled(ValidateAllBoxes());
    }

    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsButtonsEnabled(ValidateAllBoxes());
    }

    private bool ValidateAllBoxes()
    {
        if (KeyTypeCombo.SelectedItem is not KeyType type) return false;

        if (type is KeyType.Motto && MottoBox.Text.Trim().Equals("")) return false;

        return !_textBox.Text.Trim().Equals("");
    }
    private void IsButtonsEnabled(bool value)
    {
        EncryptButton.IsEnabled = value;
        DecryptButton.IsEnabled = value;
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

    private void EncryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (KeyTypeCombo.SelectedItem is not KeyType type) 
            throw new ApplicationException("Choose type of key");

        if (type == KeyType.Motto)
        {
            _textBox.Text = new Trithemius().Encrypt(_textBox.Text, MottoBox.Text);
            return;
        }
        
        int.TryParse(ATermBox.Text, out var a);
        int.TryParse(BTermBox.Text, out var b);

        if (type == KeyType.Linear)
        {
            _textBox.Text = new Trithemius().Encrypt(_textBox.Text, a, b);
            return;
        }

        int.TryParse(CTermBox.Text, out var c);

        _textBox.Text = new Trithemius().Encrypt(_textBox.Text, a, b, c);
    }
    
    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (KeyTypeCombo.SelectedItem is not KeyType type) 
            throw new ApplicationException("Choose type of key");

        if (type == KeyType.Motto)
        {
            _textBox.Text = new Trithemius().Decrypt(_textBox.Text, MottoBox.Text);
            return;
        }
        
        int.TryParse(ATermBox.Text, out var a);
        int.TryParse(BTermBox.Text, out var b);

        if (type == KeyType.Linear)
        {
            _textBox.Text = new Trithemius().Decrypt(_textBox.Text, a, b);
            return;
        }

        int.TryParse(CTermBox.Text, out var c);

        _textBox.Text = new Trithemius().Decrypt(_textBox.Text, a, b, c);
    }
}

public enum KeyType
{
    Linear,
    NonLinear,
    Motto
}