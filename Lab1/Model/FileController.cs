using System;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace Lab1.Model;

public class FileController : IFileController
{
    public FileInfo Open()
    {
        var openDialog = new OpenFileDialog()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
            Filter = "Text files (*.txt)|*.txt;|All files (*.*)|*.*"
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

        var textInFile = File.ReadAllText(fileInfo.FullName);
        if (!text.Equals(textInFile))
        {
            File.WriteAllText(fileInfo.FullName, text, Encoding.Unicode);
        }
    }

    public FileInfo SaveAs(string text)
    {
        var saveDialog = new SaveFileDialog()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
            Filter = "Text files (*.txt)|*.txt;|All files (*.*)|*.*"
        };
        
        if (!saveDialog.ShowDialog() == true)
        {
            throw new FileLoadException("Error occured! Dialog closed.");
        }
        
        File.WriteAllText(saveDialog.FileName, text, Encoding.Unicode);

        return new FileInfo(saveDialog.FileName);
    }
}