using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab1.View;

public partial class CaesarPage : Page
{
    public CaesarPage()
    {
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
}