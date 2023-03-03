using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cryptosystem.Cryptography.Bruteforce;
using Cryptosystem.Model;

namespace Cryptosystem.View.Bruteforce;

public partial class CaesarBruteForcePage : Page
{
    private readonly TextBox _textBox;
    private FileInfo? _fileInfo;

    public CaesarBruteForcePage(MainWindow window)
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

    [GeneratedRegex(@"^[0-9]+$")]
    private static partial Regex MyRegex();

    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsDecryptButtonEnabled();
    }
    
    private void KeyBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsDecryptButtonEnabled();
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

    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        int.TryParse(KeyBox.Text, out var key);
        
        if (!Validate(key)) return;
        if (_fileInfo is null) return;
        
        var dict = File.ReadAllLines(_fileInfo.FullName).ToList();

        var start = 1_000_000_000.0m * Stopwatch.GetTimestamp() / Stopwatch.Frequency;
        
        _textBox.Text = new CaesarBruteForce(dict)
            .Decrypt(_textBox.Text, key);

        var time = ((1_000_000_000.0m * Stopwatch.GetTimestamp() / Stopwatch.Frequency) - start)
            / 1_000_000m;
        
        MessageBox.Show(
            messageBoxText: $"Bruteforce attack took {time} ms.",
            caption: "Elapsed time",
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Information);
    }

    private void OpenDict_OnClick(object sender, RoutedEventArgs e)
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

        DictPath.Text = _fileInfo.Name;

        DictPath.IsEnabled = true;
        IsDecryptButtonEnabled();
    }

    private void IsDecryptButtonEnabled()
    {
        DecryptButton.IsEnabled = !_textBox.Text.Equals("") 
                                  && !KeyBox.Text.Trim().Equals("")
                                  && _fileInfo is not null;
    }
}