﻿using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cryptosystem.Cryptography.Symmetric;

namespace Cryptosystem.View.Symmetric;

public partial class CaesarPage : Page
{
    private readonly TextBox _textBox;

    public CaesarPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        _textBox.TextChanged += TextBoxOnTextChanged;
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

    private void EncryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        int.TryParse(KeyBox.Text, out var key);
        if (!Validate(key)) return;
        
        _textBox.Text = new Caesar().Encrypt(_textBox.Text, key);
    }

    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        int.TryParse(KeyBox.Text, out var key);
        if (!Validate(key)) return;
        
        _textBox.Text = new Caesar().Decrypt(_textBox.Text, key);
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

    private void KeyBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        EncryptButton.IsEnabled = !KeyBox.Text.Trim().Equals("") && !_textBox.Text.Equals("");
        DecryptButton.IsEnabled = !KeyBox.Text.Trim().Equals("") && !_textBox.Text.Equals("");
    }
    
    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        EncryptButton.IsEnabled = !KeyBox.Text.Trim().Equals("") && !_textBox.Text.Equals("");
        DecryptButton.IsEnabled = !KeyBox.Text.Trim().Equals("") && !_textBox.Text.Equals("");
    }
}