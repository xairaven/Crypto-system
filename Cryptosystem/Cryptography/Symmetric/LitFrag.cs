using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Cryptosystem.Cryptography.Symmetric;

public class LitFrag : SymmetricCipher
{
    private readonly List<char[]> _matrix;
    private readonly Dictionary<char, HashSet<string>> _dict;
    
    public LitFrag(string text)
    {
        _matrix = new List<char[]>();
        _dict = new Dictionary<char, HashSet<string>>();
        
        InitializeMatrix(text, _matrix);
        InitializeDictSet(_matrix, _dict);
    }

    public override string Encrypt(string message, params object[] keys)
    {
        ValidateMessage(message.ToLower());

        var list = new List<string>();
        foreach (var c in message.ToLower())
        {
            var count = _dict[c].Count;
            list.Add(_dict[c].ElementAt(new Random().Next(count)));
        }
        
        return string.Join(", ", list);
    }

    public override string Decrypt(string message, params object[] keys)
    {
        ValidateEncryptedMessage(message);

        var sb = new StringBuilder();
        var list = message.Split(", ");

        foreach (var str in list)
        {
            var position = str.Split("/");
            var l = int.Parse(position[0]);
            var p = int.Parse(position[1]);

            sb.Append(_matrix[l - 1][p - 1]);
        }
        
        return sb.ToString();
    }
    
    private void InitializeDictSet(List<char[]> matrix, Dictionary<char, HashSet<string>> dict)
    {
        for (var l = 0; l < matrix.Count; l++)
        {
            for (var p = 0; p < matrix[l].Length; p++)
            {
                var symbol = char.ToLower(matrix[l][p]);

                if (!dict.ContainsKey(symbol))
                {
                    dict.Add(symbol, new HashSet<string>());
                }

                // symbol position, format: line/position
                dict[symbol].Add($"{l + 1}/{p + 1}");
            }
        }
    }

    private void InitializeMatrix(string text, List<char[]> matrix)
    { 
        var reader = new StringReader(text);

        while (reader.Peek() > -1)
        {
            var line = reader.ReadLine()! + "\r\n";
            matrix.Add(line.ToCharArray());
        }
    }

    private void ValidateMessage(string message)
    {
        var missingLetters = message.Where(symbol => !_dict.ContainsKey(symbol)).ToList();
        if (missingLetters.Count == 0) return;

        var isMissingOk = MessageBoxHandling(missingLetters);
        if (!isMissingOk) throw new Exception("Complete the key with the missing letters.");
        
        foreach (var c in missingLetters)
        {
            if (!_dict.ContainsKey(c))
            {
                _dict.Add(c, new HashSet<string>());
            }

            var l = new Random().Next(0, _matrix.Count);
            var p = new Random().Next(0, _matrix[l].Length);
            
            // symbol position, format: line/position
            _dict[c].Add($"{l + 1}/{p + 1}");
        }
    }

    private bool MessageBoxHandling(List<char> missingLettersList)
    {
        var set = missingLettersList.ToHashSet();
        var missingLetters = string.Join(", ", set);

        var message = $"The key is missing the following letters:\n" +
                      $"{missingLetters}.\n" +
                      $"Do you want to replace it by random symbols?"; 
        
        var result = MessageBox.Show(
            messageBoxText: message,
            button: MessageBoxButton.YesNo,
            caption: "Error",
            icon: MessageBoxImage.Error
        );

        return result == MessageBoxResult.Yes;
    }

    private void ValidateEncryptedMessage(string message)
    {
        var list = message.Split(", ");
        foreach (var str in list)
        {
            var position = str.Split("/");
            if (!int.TryParse(position[0], out var l) || !int.TryParse(position[1], out var p))
            {
                throw new ArgumentException("This text is not encrypted by LitFrag method.");
            }

            if ((l < 1 || l > _matrix.Count) 
                || (p < 1 || p > _matrix[l - 1].Length))
            {
                throw new IndexOutOfRangeException("There are non-existent positions in the ciphertext.");
            }
        }
    }
} 