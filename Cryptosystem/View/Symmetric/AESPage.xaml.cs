using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Cryptosystem.Cryptography.Symmetric;
using Cryptosystem.Enum;

using SysCrypto = System.Security.Cryptography;

namespace Cryptosystem.View.Symmetric;

public partial class AESPage  : Page
{
    private readonly TextBox _textBox;
    
    public AESPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        _textBox.TextChanged += TextBoxOnTextChanged;
        
        InitializeComponent();
    }

    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsButtonsEnabled();
    }

    private void EncryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (DataModeCombo.SelectedItem is not DataMode dataMode)
            throw new Exception("Wrong data mode");

        if (CipherModeCombo.SelectedItem is not SysCrypto.CipherMode cipherMode)
            throw new Exception("Wrong cipher mode");
        
        if (IVBox.Text.Trim().Equals(""))
        {
            GenerateIV();
        }

        try
        {
            _textBox.Text = new AES().Encrypt(_textBox.Text, dataMode, cipherMode, 
                KeyBox.Text.Trim(), IVBox.Text.Trim());
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                messageBoxText: exception.Message,
                caption: "Error!",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error);
        }
    }

    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (DataModeCombo.SelectedItem is not DataMode dataMode)
            throw new Exception("Wrong data mode");

        if (CipherModeCombo.SelectedItem is not SysCrypto.CipherMode cipherMode)
            throw new Exception("Wrong cipher mode");

        if (IVBox.Text.Trim().Equals(""))
        {
            GenerateIV();
        }
        
        
        try
        {
            _textBox.Text = new AES().Decrypt(_textBox.Text, dataMode, cipherMode, 
                KeyBox.Text.Trim(), IVBox.Text.Trim());
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                messageBoxText: exception.Message,
                caption: "Error!",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error);
        }
    }

    private void CipherModeChanged(object sender, SelectionChangedEventArgs e)
    {
        IsButtonsEnabled();
    }

    private void DataModeChanged(object sender, SelectionChangedEventArgs e)
    {
        IsButtonsEnabled();
    }

    private void KeyBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsButtonsEnabled();
    }

    private void IsButtonsEnabled()
    {
        var isEnabled = !_textBox.Text.Trim().Equals("")
                        && !KeyBox.Text.Trim().Equals("")
                        && CipherModeCombo.SelectedItem is not null
                        && DataModeCombo.SelectedItem is not null;

        EncryptButton.IsEnabled = isEnabled;
        DecryptButton.IsEnabled = isEnabled;
    }
    
    private void GenerateKeyButton_OnClick(object sender, RoutedEventArgs e)
    {
        using var provider = SysCrypto.Aes.Create();
        provider.GenerateKey();
        var bytes = provider.Key;
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] %= 128;
        }
        KeyBox.Text = Encoding.ASCII.GetString(bytes);
    }

    private void GenerateIVButton_OnClick(object sender, RoutedEventArgs e)
    {
       GenerateIV();
    }

    private void GenerateIV()
    {
        using var provider = SysCrypto.Aes.Create();
        provider.GenerateIV();
        var bytes = provider.IV;
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] %= 128;
        }
        IVBox.Text = Encoding.ASCII.GetString(bytes);
    }
}