﻿using System;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        StringBuilder header = new StringBuilder("***********************************");
        header.AppendLine();

        string rootPath = @"Path to project";
        var files = Directory.GetFiles(rootPath, "*.cs", SearchOption.AllDirectories);

        var result = files.Select(path => new { Name = Path.GetFileName(path), Contents = File.ReadAllText(path) })
                          .Where(info => !info.Contents.Contains("<auto-generated>") && !info.Contents.Contains("<autogenerated />")
                          && !info.Contents.Contains("<auto-generated/>"))
                          .Select(info =>
                              header.ToString()
                              + "Filename: " + info.Name + Environment.NewLine
                              + header.ToString()
                              + info.Contents);

        StringBuilder singleStr = new StringBuilder();
        singleStr.AppendLine(string.Join(Environment.NewLine, result));

        Console.WriteLine(singleStr.ToString());
        File.WriteAllText(Path.Combine(rootPath, "output.txt"), singleStr.ToString(), Encoding.UTF8);
    }
}