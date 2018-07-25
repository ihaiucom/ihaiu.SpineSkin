using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


public class SpineSkinManager
{
    public List<SpineStyleData> styleList = new List<SpineStyleData>();

    // 读取样式
    public void ReadStyles()
    {
        DirectoryInfo directory = new DirectoryInfo(Setting.Options.rootEditor);
        DirectoryInfo[]  infos = directory.GetDirectories();
        foreach(DirectoryInfo item in infos)
        {
            SpineStyleData style = new SpineStyleData(item.FullName);
            if(style.enable)
            {
                styleList.Add(style);
            }
        }
    }

    // 读取对应样式需要应用的文件
    public void ReadImages()
    {
        foreach(SpineStyleData style in styleList)
        {
            string root = Setting.Options.rootImages + "/" + style.name;
            DirectoryInfo directory = new DirectoryInfo(root);
            DirectoryInfo[] infos = directory.GetDirectories();
            foreach (DirectoryInfo item in infos)
            {
                SpineImageData img = new SpineImageData(item.FullName, style);
                if(img.enable)
                {
                    style.imageDatas.Add(img);
                }
            }
        }
    }

    // 生成
    public void Generate()
    {
        foreach (SpineStyleData style in styleList)
        {
            foreach(SpineImageData img in style.imageDatas)
            {
                img.Generate();
            }
        }
    }

    // 生成导出命令
    public void GenerateExportSpineBat()
    {
        StringWriter sw = new StringWriter();
        sw.WriteLine("@ECHO OFF");
        sw.WriteLine($"if exist \"{ Setting.Options.rootExportSpine}\" rmdir /s /q \"{ Setting.Options.rootExportSpine}\"");

        foreach (SpineStyleData style in styleList)
        {
            foreach (SpineImageData img in style.imageDatas)
            {
                sw.WriteLine($"Spine -i {img.genergateFileSpine} -o {img.folderExportSpine} -e {Setting.Options.exportSpineSettingJson}");
            }
        }


        PathHelper.CheckPath(Setting.Options.exportSpineBat);
        File.WriteAllText(Setting.Options.exportSpineBat, sw.ToString());
    }
}