using System.IO;

namespace Lab1.Model;

public interface IFileController
{
    bool New();
    FileInfo Open();
    bool Save();
    bool SaveAs();
    bool Print();
}