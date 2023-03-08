using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Cryptosystem.Cryptography.Bruteforce;
using Cryptosystem.Model;

namespace Cryptosystem.View.Bruteforce;

public partial class TrithemiusBruteforcePage : Page
{
    private readonly TextBox _textBox;
    private FileInfo? _dictFileInfo;
    
    public TrithemiusBruteforcePage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        window.CloseButton.Click += CloseButtonOnClick;
        
        InitializeComponent();
    }
    
    private void InitialTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsDecryptButtonEnabled();
    }

    private void EncryptedTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsDecryptButtonEnabled();
    }

    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (_dictFileInfo is null) return;
        
        var dict = File.ReadAllLines(_dictFileInfo.FullName).ToHashSet().ToList();

        var start = 1_000_000_000.0m * Stopwatch.GetTimestamp() / Stopwatch.Frequency;

        try
        {
            _textBox.Text = new TrithemiusBruteforce(dict).
                Decrypt(InitialTextBox.Text, EncryptedTextBox.Text);
        }
        catch (Exception exception)
        {
            MessageBox.Show(
                messageBoxText: exception.Message,
                caption: "Error",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error
            );
            return;
        }
        
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
            _dictFileInfo = new FileController().Open();
        }
        catch (Exception)
        {
            return;
        }

        if (!_dictFileInfo.Extension.Equals(".txt"))
        {
            MessageBox.Show(
                messageBoxText: "Error! Filetype of dictionary must be txt.",
                caption: "Wrong filetype!",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error);
            return;
        }

        DictPath.Text = _dictFileInfo.Name;

        DictPath.IsEnabled = true;
        IsDecryptButtonEnabled();
    }

    private void CloseDict_OnClick(object sender, RoutedEventArgs e)
    {
        _dictFileInfo = null;
        DictPath.Text = "";
        DictPath.IsEnabled = false;
        IsDecryptButtonEnabled();
    }
    
    private void IsDecryptButtonEnabled()
    {
        DecryptButton.IsEnabled = !InitialTextBox.Text.Trim().Equals("")
                                  && !EncryptedTextBox.Text.Trim().Equals("")
                                  && _dictFileInfo is not null;
    }
    
    private void CloseButtonOnClick(object sender, RoutedEventArgs e)
    {
        InitialTextBox.Text = "";
        EncryptedTextBox.Text = "";
    }
}