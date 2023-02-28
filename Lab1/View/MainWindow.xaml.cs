﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Lab1.Model;

namespace Lab1.View;

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

        _pages = new List<Page>()
        {
            new CaesarPage(),
            new Base64Page(),
            new TrithemiusPage()
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

        MainTextArea.Text = File.ReadAllText(_fileInfo.FullName);
        
        if (!StatusBar.IsEnabled)
        {
            EnableStatusBar(_fileInfo.FullName);
        }
    }

    private void EnableStatusBar(string fileName)
    {
        StatusBar.Text = fileName;
        StatusBar.IsEnabled = true;
        StatusBarState.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/CheckMark.png"));
    }

    private void DisableStatusBar()
    {
        StatusBar.Text = "";
        StatusBar.IsEnabled = false;
        StatusBarState.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/CrossMark.png"));
    }

    private void SaveFile_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void SaveAsFile_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
    
    private void Exit_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}