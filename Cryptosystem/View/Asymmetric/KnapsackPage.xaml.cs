using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Cryptosystem.Cryptography.Asymmetric;
using Cryptosystem.Utils;

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
        try
        {
            var isASCII = ASCIICheckBox.IsChecked.GetValueOrDefault();
            
            ValidateSequence(PublicSeqBox.Text.Trim(), out var publicSequence);

            var message = _textBox.Text;
            _textBox.Text = new Knapsack().Encrypt(message, publicSequence, isASCII);
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
        throw new System.NotImplementedException();
    }

    private void GenerateButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            long[] secretSequence;
            if (SecretSeqBox.Text.Trim().Equals(""))
            {
                secretSequence = Knapsack.GenerateSV(new Random().Next(5, 11));
                SecretSeqBox.Text = string.Join(", ", secretSequence);
            }
            else ValidateSequence(SecretSeqBox.Text.Trim(), out secretSequence);
            
            long mod;
            if (ModBox.Text.Trim().Equals(""))
            {
                mod = Knapsack.GenerateMod(secretSequence);
                ModBox.Text = mod.ToString();
            }
            else ValidateNumber(ModBox.Text.Trim(), out mod);
            
            long T;
            if (TBox.Text.Trim().Equals(""))
            {
                T = Knapsack.GenerateT(mod);
                TBox.Text = T.ToString();
            }
            else ValidateNumber(TBox.Text.Trim(), out T);
            
            long[] publicSequence;
            if (PublicSeqBox.Text.Trim().Equals(""))
            {
                publicSequence = Knapsack.GeneratePV(secretSequence, T, mod);
                PublicSeqBox.Text = string.Join(", ", publicSequence);
            }
            else ValidateSequence(PublicSeqBox.Text.Trim(), out publicSequence);
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

    private static void ValidateNumber(string str, out long number)
    {
        if (!long.TryParse(str, out number))
            throw new Exception($"Variable {nameof(number)} is not a number");
    }

    private void ClearButton_OnClick(object sender, RoutedEventArgs e)
    {
        SecretSeqBox.Text = "";
        TBox.Text = "";
        ModBox.Text = "";
        PublicSeqBox.Text = "";
    }
}