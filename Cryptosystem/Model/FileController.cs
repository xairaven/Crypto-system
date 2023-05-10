using System;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace Cryptosystem.Model;

public class FileController : IFileController
{
    public FileInfo Open(string filter = "Text files (*.txt)|*.txt;|All files (*.*)|*.*")
    {
        var openDialog = new OpenFileDialog()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
            Filter = filter
        };

        if (!openDialog.ShowDialog() == true)
        {
            throw new FileLoadException("Error occured! File was not open.");
        }
        
        return new FileInfo(openDialog.FileName);
    }

    public void Save(FileInfo fileInfo, string text)
    {
        if (text.Trim().Equals("")) return;

        var extension = fileInfo.Extension;
        
        if (extension.Equals(".txt"))
        {
            File.WriteAllText(fileInfo.FullName, text, Encoding.Unicode);
        }
        else if (extension.Equals(".pem"))
        {
            File.WriteAllBytes(fileInfo.FullName, Encoding.ASCII.GetBytes(text));
        }
        else
        {
            HexToFile(fileInfo, text);
        }
    }

    public FileInfo SaveAs(string text, 
        string filter = "Text files (*.txt)|*.txt;|All files (*.*)|*.*")
    {
        var saveDialog = new SaveFileDialog()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
            Filter = filter
        };
        
        if (!saveDialog.ShowDialog() == true)
        {
            throw new FileLoadException("Error occured! Dialog closed.");
        }

        var fileInfo = new FileInfo(saveDialog.FileName);

        var extension = fileInfo.Extension;
        
        if (extension.Equals(".txt"))
        {
            File.WriteAllText(fileInfo.FullName, text, Encoding.Unicode);
        }
        else if (extension.Equals(".pem"))
        {
            File.WriteAllBytes(fileInfo.FullName, Encoding.ASCII.GetBytes(text));
        }
        else
        {
           HexToFile(fileInfo, text);
        }
        
        return fileInfo;
    }

    private void HexToFile(FileInfo fileInfo, string text)
    {
        try
        {
            var bytes = Convert.FromHexString(text);
            File.WriteAllBytes(fileInfo.FullName, bytes);
        }
        catch (FormatException exception)
        {
            MessageBox.Show(
                messageBoxText: exception.Message,
                caption: "Denied",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error
            );   
        }
    }
}