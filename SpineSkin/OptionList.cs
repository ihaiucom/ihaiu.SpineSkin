using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class OptionList
{
    public List<OptionItem> enableoverwrites = new List<OptionItem>();
    public List<OptionItem> disableoverwrites = new List<OptionItem>();
    public List<OptionCheckItem> checks = new List<OptionCheckItem>();


    public void Save(string path = null)
    {
        if (string.IsNullOrEmpty(path))
            path = "./setting.json";

        string json = JsonHelper.ToJsonType(this);
        File.WriteAllText(path, json);
    }

    public static OptionList Load(string path = null)
    {
        if (string.IsNullOrEmpty(path))
            path = "./setting.json";

        string json = File.ReadAllText(path);
        OptionList options = JsonHelper.FromJson<OptionList>(json);
        return options;
    }
}


public class OptionItem
{
    public string src;
    public string dst;
}


public class OptionCheckItem
{
    // 检测目录
    public string src;
    // 参考目录
    public string dst;
    // 检测文件后缀
    public string srcExt;
    // 参考文件后缀
    public string dstExt;
    // 检测目录备份
    public string srcBak;
    // 参考目录备份
    public string dstBak;
    // 日志文件
    public string log;
}
