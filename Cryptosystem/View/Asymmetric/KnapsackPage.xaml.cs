using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cryptosystem.Cryptography.Asymmetric;

namespace Cryptosystem.View.Asymmetric;

public partial class KnapsackPage
{
    private readonly TextBox _textBox;

    public KnapsackPage(MainWindow window)
    {
        _textBox = window.MainTextArea;
        _textBox.TextChanged += TextBoxOnTextChanged;

        InitializeComponent();
    }

    private void EncryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void GenerateButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var SVString = SecretSeqBox.Text.Trim();
            long[] SV;
            if (SVString.Equals(""))
            {
                SV = Knapsack.GenerateSV(new Random().Next(5, 11));
                SecretSeqBox.Text = string.Join(", ", SV);
            }
            else ValidateSequence(SVString, out SV);


            var modString = ModBox.Text.Trim();
            long mod;
            if (modString.Equals(""))
            {
                mod = Knapsack.GenerateMod(SV);
                ModBox.Text = mod.ToString();
            }
            else ValidateMod(modString, out mod);


            var TString = TBox.Text.Trim();
            long T;
            if (TString.Equals(""))
            {
                T = Knapsack.GenerateT(mod);
                TBox.Text = T.ToString();
            }
            else ValidateT(TString, out T);
            
            var PVString = PublicSeqBox.Text.Trim();
            long[] PV;
            if (PVString.Equals(""))
            {
                PV = Knapsack.GeneratePV(SV, T, mod);
                PublicSeqBox.Text = string.Join(", ", PV);
            }
            else ValidateSequence(PVString, out PV);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,
                "Error!",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void PublicSeqBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        AreButtonsEnabled();
    }

    private void SecretSeqBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        AreButtonsEnabled();
    }

    private void TBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        AreButtonsEnabled();
    }

    private void ModBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        AreButtonsEnabled();
    }

    private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        AreButtonsEnabled();
    }

    private void AreButtonsEnabled()
    {
        var isEnabled = !_textBox.Text.Trim().Equals("")
                        && !PublicSeqBox.Text.Trim().Equals("")
                        && !SecretSeqBox.Text.Trim().Equals("")
                        && !TBox.Text.Trim().Equals("")
                        && !ModBox.Text.Trim().Equals("");

        EncryptButton.IsEnabled = isEnabled;
        DecryptButton.IsEnabled = isEnabled;
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

    private static void ValidateSequence(string str, out long[] sequence)
    {
        var stringSequence = str.Split(", ");

        sequence = new long[stringSequence.Length];
        for (int i = 0; i < sequence.Length; i++)
        {
            if (!long.TryParse(stringSequence[i], out var num))
                throw new ArgumentException("An error occurred while reading the secret sequence.");
            
            sequence[i] = num;
        }
    }

    private static void ValidateMod(string modString, out long mod)
    {
        if (!long.TryParse(modString, out mod))
            throw new Exception("Variable Mod is not a number");
    }
    
    private static void ValidateT(string modString, out long t)
    {
        if (!long.TryParse(modString, out t))
            throw new Exception("Variable T is not a number");
    }

    private void ClearButton_OnClick(object sender, RoutedEventArgs e)
    {
        SecretSeqBox.Text = "";
        TBox.Text = "";
        ModBox.Text = "";
        PublicSeqBox.Text = "";
    }
}