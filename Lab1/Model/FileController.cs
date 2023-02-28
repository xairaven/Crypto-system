using System;
using System.IO;
using Microsoft.Win32;

namespace Lab1.Model;

public class FileController : IFileController
{
    public bool New()
    {
        
        return true;
    }

    public FileInfo Open()
    {
        var file = new OpenFileDialog()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
            Filter = "Text files (*.txt)|*.txt;|All files (*.*)|*.*"
        };

        if (!file.ShowDialog() == true)
        {
            throw new FileLoadException("Error occured! File was not open.");
        }
        
        return new FileInfo(file.FileName);
    }

    public bool Save()
    {
        throw new System.NotImplementedException();
    }

    public bool SaveAs()
    {
        throw new System.NotImplementedException();
    }

    public bool Print()
    {
        throw new System.NotImplementedException();
    }
}