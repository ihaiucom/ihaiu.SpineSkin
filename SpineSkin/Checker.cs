using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Checker
{
    public OptionCheckItem item;
    string time = DateTime.Now.ToString("yyyy-MM-dd-dddd-HH_mm_ss_fff");

    public Checker(OptionCheckItem item)
    {
        this.item = item;
    }

    StringWriter sw = new StringWriter();

    void log(string txt )
    {
        sw.WriteLine(txt);
        Console.WriteLine(txt);
    }

    // 运行
    public void Run()
    {
        // 备份
        if (!string.IsNullOrEmpty(item.srcBak))
        {
            item.srcBak = item.srcBak.Replace("XXXX", time);
            CopyCommand.Copy(item.src, item.srcBak, true);
        }

        if (!string.IsNullOrEmpty(item.dstBak))
        {
            item.dstBak = item.dstBak.Replace("XXXX", time);
            CopyCommand.Copy(item.dst, item.dstBak, true);
        }

        // 处理
        try
        {
            CheckDirectory(item.src, item.dst);
        }
        catch(Exception e)
        {
            log($"[Error] {e.ToString()}");
        }

        string logPath = item.log.Replace("XXXX", time);
        if (!string.IsNullOrEmpty(logPath))
        {
            PathHelper.CheckPath(logPath);
            File.WriteAllText(logPath, sw.ToString(), UTF8Encoding.Default);
        }
    }




    public void CheckDirectory(string srcPath, string destPath)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {


                if (i is DirectoryInfo)     //判断是否文件夹
                {
                    string src = srcPath + "/" + i.Name;
                    string dest = destPath + "/" + i.Name;
                    // 检测到目标目录不存在
                    if (!Directory.Exists(dest))
                    {
                        log($"[删除目录] {dest}");
                        Directory.Delete(src, true);
                        continue;
                    }

                    CheckDirectory(i.FullName, dest);    //递归调用复制子文件夹
                }
                else
                {
                    string dstname = string.IsNullOrEmpty(item.srcExt) ? i.Name : i.Name.Replace(item.srcExt, item.dstExt);
                    string src = srcPath + "/" + i.Name;
                    string dest = destPath + "/" + dstname;
                    // 检测到目录文件不存在
                    if(!File.Exists(dest))
                    {
                        log($"[删除文件] {dest}");
                        File.Delete(src);
                    }
                    else
                    {
                        dstname = Path.GetFileName(new FileInfo(dest).FullName);
                        if (Path.GetFileName(new FileInfo(dest).FullName) != i.Name)
                        {
                            log($"[警告][文件名大小不一致] srcname:{i.Name},  dstname:{dstname}");
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }


}