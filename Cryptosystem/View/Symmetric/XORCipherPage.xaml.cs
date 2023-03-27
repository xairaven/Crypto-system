using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cryptosystem.Cryptography.Symmetric;
using Cryptosystem.Model;

namespace Cryptosystem.View.Symmetric;

public partial class XORCipherPage : Page
{
    private readonly TextBox _textBox;
    private FileInfo? _fileInfo;
    
    public XORCipherPage(MainWindow window)
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
        if (KeyBox.IsEnabled)
        {
            int.TryParse(KeyBox.Text, out var key);
            if (!Validate(key)) return;
        
            _textBox.Text = new XORCipher().Encrypt(_textBox.Text, key);
        }
        else if (_fileInfo is not null)
        {
            var gamma = File.ReadAllLines(_fileInfo.FullName, Encoding.Unicode);

            try
            {
                _textBox.Text = new XORCipher().Encrypt(_textBox.Text, gamma);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    messageBoxText: ex.Message,
                    button: MessageBoxButton.OK,
                    caption: "Error",
                    icon: MessageBoxImage.Error
                );
            }
        } else return;
    }

    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (KeyBox.IsEnabled)
        {
            int.TryParse(KeyBox.Text, out var key);
            if (!Validate(key)) return;
        
            _textBox.Text = new XORCipher().Decrypt(_textBox.Text, key);
        }
        else if (_fileInfo is not null)
        {
            var gamma = File.ReadAllLines(_fileInfo.FullName, Encoding.Unicode);
        
            try
            {
                _textBox.Text = new XORCipher().Encrypt(_textBox.Text, gamma);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    messageBoxText: ex.Message,
                    button: MessageBoxButton.OK,
                    caption: "Error",
                    icon: MessageBoxImage.Error
                );
            }
        } else return;
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
        OpenPad.IsEnabled = KeyBox.Text.Trim().Equals("");
        ClosePad.IsEnabled = KeyBox.Text.Trim().Equals("");
            
        IsButtonsEnabled();
    }
    
    private void FilePath_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        KeyBox.IsEnabled = _fileInfo is null;
    }
    
    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsButtonsEnabled();
    }
    
    private void OpenPad_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _fileInfo = new FileController().Open();
        }
        catch (Exception)
        {
            return;
        }

        if (!_fileInfo.Extension.Equals(".txt"))
        {
            MessageBox.Show(
                messageBoxText: "Error! Filetype of dictionary must be txt.",
                caption: "Wrong filetype!",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error);
            return;
        }

        FilePath.Text = _fileInfo.Name;
        FilePath.IsEnabled = true;
        
        IsButtonsEnabled();
    }

    private void ClosePad_OnClick(object sender, RoutedEventArgs e)
    {
        _fileInfo = null;
        FilePath.Text = "";
        FilePath.IsEnabled = false;
        
        IsButtonsEnabled();
    }
    
    private void IsButtonsEnabled()
    {
        var isEnabled = !_textBox.Text.Equals("")
                        && (_fileInfo is not null
                        ^ !KeyBox.Text.Trim().Equals(""));

        EncryptButton.IsEnabled = isEnabled;
        DecryptButton.IsEnabled = isEnabled;
    }
}