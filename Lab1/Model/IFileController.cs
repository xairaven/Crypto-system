using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Lab1.Model;

public interface IFileController
{
    FileInfo Open();
    void Save(FileInfo fileInfo, string textBox);
    FileInfo SaveAs(string text);
}