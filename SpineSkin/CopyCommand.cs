using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public static class CopyCommand
{
    public static void Copy(string src, string dest, bool overwrite = true)
    {
        if(Directory.Exists(src))
        {
            CopyDirectory(src, dest, overwrite);
        }
        else if(File.Exists(src))
        {
            CopyFile(src, dest, overwrite);
        }
    }


    public static void CopyFile(string src, string dest, bool overwrite = true)
    {
        if (File.Exists(src))
        {
            if (overwrite)
            {
                if(File.Exists(dest))
                {
                    File.Delete(dest);
                }
            }
            else
            {
                if (File.Exists(dest))
                {
                    return;
                }
            }
            PathHelper.CheckPath(dest);
            File.Copy(src, dest, overwrite);
        }
    }

    public static void CopyDirectory(string srcPath, string destPath, bool overwrite = true)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)     //判断是否文件夹
                {
                    if (!Directory.Exists(destPath + "/" + i.Name))
                    {
                        Directory.CreateDirectory(destPath + "/" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                    }
                    CopyDirectory(i.FullName, destPath + "/" + i.Name, overwrite);    //递归调用复制子文件夹
                }
                else
                {
                    string dest = destPath + "/" + i.Name;
                    CopyFile(i.FullName, dest, overwrite); //不是文件夹即复制文件，true表示可以覆盖同名文件
                }
            }
        }
        catch (Exception e)
        {
           Console.WriteLine($"拷贝目录出错 srcPath={srcPath}, destPath={destPath}" + e.ToString());
        }
    }
}