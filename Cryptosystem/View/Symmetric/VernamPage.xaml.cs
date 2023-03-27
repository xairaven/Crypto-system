using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Cryptosystem.Cryptography.Symmetric;
using Cryptosystem.Model;

namespace Cryptosystem.View.Symmetric;

public partial class VernamPage : Page
{
    private readonly TextBox _textBox;
    private FileInfo? _fileInfo;
    
    public VernamPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        _textBox.TextChanged += TextBoxOnTextChanged;
        
        InitializeComponent();
    }

    private void EncryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (_fileInfo is null) return;
        var gamma = File.ReadAllLines(_fileInfo.FullName, Encoding.Unicode);

        try
        {
            _textBox.Text = new Vernam().Encrypt(_textBox.Text, gamma);
        }
        catch (Exception exc)
        {
            MessageBox.Show(
                messageBoxText: exc.Message,
                button: MessageBoxButton.OK,
                caption: "Error",
                icon: MessageBoxImage.Error
            );
        }
    }

    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (_fileInfo is null) return;
        var gamma = File.ReadAllLines(_fileInfo.FullName, Encoding.Unicode);

        try
        {
            _textBox.Text = new Vernam().Decrypt(_textBox.Text, gamma);
        }
        catch (Exception exc)
        {
            MessageBox.Show(
                messageBoxText: exc.Message,
                button: MessageBoxButton.OK,
                caption: "Error",
                icon: MessageBoxImage.Error
            );
        }
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
    
    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsButtonsEnabled();
    }

    private void IsButtonsEnabled()
    {
        var isEnabled = !_textBox.Text.Equals("")
                         && _fileInfo is not null;

        EncryptButton.IsEnabled = isEnabled;
        DecryptButton.IsEnabled = isEnabled;
    }
}