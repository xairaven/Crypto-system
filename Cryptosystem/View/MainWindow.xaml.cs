﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Cryptosystem.Model;
using Cryptosystem.View.Asymmetric;
using Cryptosystem.View.Bruteforce;
using Cryptosystem.View.Coding;
using Cryptosystem.View.Symmetric;

namespace Cryptosystem.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly List<Page> _pages;
    private FileInfo? _fileInfo;

    public MainWindow()
    {
        InitializeComponent();

        _pages = new List<Page>
        {
            new AESPage(this),
            new Base64Page(this),
            new CaesarPage(this),
            new CaesarBruteforcePage(this),
            new DESPage(this),
            new HexPage(this),
            new KnapsackPage(this),
            new LitFragPage(this),
            new OneTimePadPage(this),
            new RSAPage(this),
            new TripleDESPage(this),
            new TrithemiusPage(this),
            new TrithemiusBruteforcePage(this),
            new VernamPage(this),
            new XORCipherPage(this),
        };
    }

    private void About_OnClick(object sender, RoutedEventArgs e)
    {
        new AboutWindow().Show();
    }

    private void CipherChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox list)
        {
            CipherFrame.Content = _pages[(int)list.SelectedItem];
        }
    }

    private void UndoButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (MainTextArea.CanUndo)
        {
            MainTextArea.Undo();
        }
    }

    private void RedoButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (MainTextArea.CanRedo)
        {
            MainTextArea.Redo();
        }
    }
    
    private void NewFile_OnClick(object sender, RoutedEventArgs e)
    {
        _fileInfo = null;
        MainTextArea.Text = "";
        DisableStatusBar();
    }
    
    private void CloseFile_OnClick(object sender, RoutedEventArgs e)
    {
        NewFile_OnClick(sender, e);
    }

    private void OpenFile_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _fileInfo = new FileController().Open();
        }
        catch (Exception)
        {
            return;
        }

        if (_fileInfo.Extension.Equals(".txt"))
        {
            MainTextArea.Text = File.ReadAllText(_fileInfo.FullName);
        }
        else
        {
            var bytes = File.ReadAllBytes(_fileInfo.FullName);
            MainTextArea.Text = Convert.ToHexString(bytes);
        }
        
        EnableStatusBar(_fileInfo.FullName);
    }

    private void EnableStatusBar(string fileName)
    {
        StatusBar.Text = fileName;
        StatusBar.IsEnabled = true;
        StatusBarState.Source = new BitmapImage(
            new Uri("pack://application:,,,/Resources/Icons/CheckMark.png"));
    }

    private void DisableStatusBar()
    {
        StatusBar.Text = "";
        StatusBar.IsEnabled = false;
        StatusBarState.Source = new BitmapImage(
            new Uri("pack://application:,,,/Resources/Icons/CrossMark.png"));
    }
    
    private void MainTextArea_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        DetailsBar.Content = $"Symbols: {MainTextArea.Text.Length}";
    }

    private void SaveFile_OnClick(object sender, RoutedEventArgs e)
    {
        if (_fileInfo == null)
        {
            try
            {
               _fileInfo = new FileController().SaveAs(MainTextArea.Text);
            }
            catch (Exception)
            {
                return;
            }
            EnableStatusBar(_fileInfo.FullName);
            return;
        }

        new FileController().Save(_fileInfo, MainTextArea.Text);
    }

    private void SaveAsFile_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            new FileController().SaveAs(MainTextArea.Text);
        }
        catch (Exception)
        {
            return;
        }
    }

    private void Exit_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}