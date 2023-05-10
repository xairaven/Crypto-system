using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cryptosystem.Cryptography.Asymmetric;
using Cryptosystem.Cryptography.Generators;
using Cryptosystem.Enum;
using Cryptosystem.Model;

namespace Cryptosystem.View.Asymmetric;

public partial class RSAPage
{
    private readonly TextBox _textBox;
    private (string PublicKeyPEM, string PrivateKeyPEM) _keys;

    private const int DefaultKeySize = 2048;
    private const int DefaultHashLength = 10;
    private readonly RSAEncryptionPadding DefaultPaddingMode = RSAEncryptionPadding.OaepSHA256;
    
    
    public RSAPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        _textBox.TextChanged += TextBoxOnTextChanged;

        InitializeComponent();
    }

    private void NumericOnly(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !NumericTextRegex().IsMatch(e.Text);
    }

    [GeneratedRegex(@"^[0-9\-]+$")]
    private static partial Regex NumericTextRegex();

    private void EncryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var message = _textBox.Text;
            
            var paddingModeLocal = (RSAPaddingMode) PaddingCombo.SelectedIndex;
            var padding = Padding(paddingModeLocal);

            _textBox.Text = new LocalRSA(padding).Encrypt(message, _keys.PublicKeyPEM);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,
                "Error!",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var message = _textBox.Text;

            var paddingModeLocal = (RSAPaddingMode) PaddingCombo.SelectedIndex;
            var padding = Padding(paddingModeLocal);

            _textBox.Text = new LocalRSA(padding).Decrypt(message, _keys.PrivateKeyPEM);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,
                "Error!",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void ClearButton_OnClick(object sender, RoutedEventArgs e)
    {
        _keys.PublicKeyPEM = string.Empty;
        _keys.PrivateKeyPEM = string.Empty;
    
        PublicKeyHash.Text = string.Empty;
        PrivateKeyHash.Text = string.Empty;

        KeySizeBox.Text = string.Empty;
        PaddingCombo.SelectedIndex = 0;

        AreButtonsEnabled();
    }

    private void GenerateButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var stringKeySize = KeySizeBox.Text.Trim();

            var keySize = !stringKeySize.Trim().Equals("") ? 
                int.Parse(stringKeySize) 
                : DefaultKeySize;

            _keys = GenerateRSA.PEMKeys(keySize);

            UpdatePublicKeyHash();
            UpdatePrivateKeyHash();
            AreButtonsEnabled();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,
                "Error!",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void AreButtonsEnabled()
    {
        var isMessageExists = !_textBox.Text.Trim().Equals("");
        var isPaddingSelected = PaddingCombo.SelectedIndex != -1;

        var isPublicKeyLoaded = _keys.PublicKeyPEM != string.Empty;
        var isPrivateKeyLoaded = _keys.PrivateKeyPEM != string.Empty;

        EncryptButton.IsEnabled = isMessageExists && isPaddingSelected && isPublicKeyLoaded;
        DecryptButton.IsEnabled = isMessageExists && isPaddingSelected && isPrivateKeyLoaded;
    }

    private string GenerateHash(string key)
    {
        var hash = GenerateSHA1.HexHash(key);
        if (hash.Length > DefaultHashLength) hash = hash[..DefaultHashLength];

        return hash;
    }
    
    private void UpdatePublicKeyHash()
    {
        PublicKeyHash.Text = GenerateHash(_keys.PublicKeyPEM);
    }

    private void UpdatePrivateKeyHash()
    {
        PrivateKeyHash.Text = GenerateHash(_keys.PrivateKeyPEM);
    }

    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        AreButtonsEnabled();
    }
    
    private void PaddingCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        AreButtonsEnabled();
    }

    private void KeyHash_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var keyHashBox = sender as TextBox;

        keyHashBox!.IsEnabled = !keyHashBox.Text.Equals("");
    }

    private string OpenKey()
    {
        var key = string.Empty;

        try
        {
            var fileInfo = new FileController().Open(
                filter: "PEM files (*.pem)|*.pem");


            var bytes = File.ReadAllBytes(fileInfo.FullName);

            key = Encoding.ASCII.GetString(bytes);
        }
        catch (IOException) { return ""; }
        catch (Exception e)
        {
            MessageBox.Show(e.Message,
                "Error!",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        return key;
    }

    private void SaveKey(string key)
    {
        try
        {
            new FileController().SaveAs(
                text: key,
                filter: "PEM files (*.pem)|*.pem");
        }
        catch (IOException) {}
        catch (Exception e)
        {
            MessageBox.Show(e.Message,
                "Error!",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void OpenPublicKeyButton_OnClick(object sender, RoutedEventArgs e)
    {
        var key = OpenKey();
        if (key.Equals("")) return;

        _keys.PublicKeyPEM = key;

        UpdatePublicKeyHash();
        AreButtonsEnabled();
    }

    private void OpenSecretKeyButton_OnClick(object sender, RoutedEventArgs e)
    {
        var key = OpenKey();
        if (key.Equals("")) return;

        _keys.PrivateKeyPEM = key;
        UpdatePrivateKeyHash();
        AreButtonsEnabled();
    }

    private void SavePublicKeyButton_OnClick(object sender, RoutedEventArgs e)
    {
        SaveKey(_keys.PublicKeyPEM);
    }
    
    private void SaveSecretKeyButton_OnClick(object sender, RoutedEventArgs e)
    {
        SaveKey(_keys.PrivateKeyPEM);
    }

    private RSAEncryptionPadding Padding(RSAPaddingMode mode) => mode switch
    {
        RSAPaddingMode.Default => DefaultPaddingMode,
        RSAPaddingMode.Pkcs1 => RSAEncryptionPadding.Pkcs1,
        RSAPaddingMode.SHA1 => RSAEncryptionPadding.OaepSHA1,
        RSAPaddingMode.SHA256 => RSAEncryptionPadding.OaepSHA256,
        RSAPaddingMode.SHA384 => RSAEncryptionPadding.OaepSHA384,
        RSAPaddingMode.SHA512 => RSAEncryptionPadding.OaepSHA512,
        _ => throw new ArgumentOutOfRangeException(nameof(mode), $"Not expected padding value: {mode}"),
    };
    
    private void HelpButton_OnClick(object sender, RoutedEventArgs e)
    {
        const string message =
            "Keys must be in PEM format.\n" +
            "Also, secret key have to comply with the standard PKCS#8.\n\n" +
            "Key size: Number in bits. Lower than 2048 - can be cracked by quantum computer.\n\n" +
            "Padding: Check documentation.\n" +
            "If you don't want, you can select 'Default'";

        MessageBox.Show(messageBoxText: message,
            caption: "Info",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Information);
    }
}