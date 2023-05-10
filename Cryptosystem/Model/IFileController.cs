using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Cryptosystem.Model;

public interface IFileController
{
    FileInfo Open(string filter = "Text files (*.txt)|*.txt;|All files (*.*)|*.*");
    void Save(FileInfo fileInfo, string textBox);
    FileInfo SaveAs(string text, 
        string filter = "Text files (*.txt)|*.txt;|All files (*.*)|*.*");
}