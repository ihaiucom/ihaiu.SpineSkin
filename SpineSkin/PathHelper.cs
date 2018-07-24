using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class PathHelper
{
    public static void CheckPath(string path, bool isFile = true)
    {
        if (isFile) path = path.Substring(0, path.LastIndexOf('/'));
        string[] dirs = path.Split('/');
        string target = "";

        bool first = true;
        foreach (string dir in dirs)
        {
            if (first)
            {
                first = false;
                target += dir;
                continue;
            }

            if (string.IsNullOrEmpty(dir)) continue;
            target += "/" + dir;
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }
        }
    }

    public static string ChangeExtension(string path, string ext)
    {
        string e = Path.GetExtension(path);
        if (string.IsNullOrEmpty(e))
        {
            return path + ext;
        }

        bool backDSC = path.IndexOf('\\') != -1;
        path = path.Replace('\\', '/');
        if (path.IndexOf('/') == -1)
        {
            return path.Substring(0, path.LastIndexOf('.')) + ext;
        }

        string dir = path.Substring(0, path.LastIndexOf('/'));
        string name = path.Substring(path.LastIndexOf('/'), path.Length - path.LastIndexOf('/'));
        name = name.Substring(0, name.LastIndexOf('.')) + ext;
        path = dir + name;

        if (backDSC)
        {
            path = path.Replace('/', '\\');
        }
        return path;
    }
}