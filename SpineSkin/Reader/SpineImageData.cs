using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


/// <summary>
/// 应用数据
/// </summary>
public class SpineImageData
{
    // 样式数据
    public SpineStyleData style;

    // 文件夹路径
    public string folderPath;

    // 名称
    public string name;

    // images文件夹路径
    public string folderImages;

    // 是否有效的
    public bool enable;

    // 原生图片列表
    public List<string> images = new List<string>();
    public Dictionary<string, string> imageDict = new Dictionary<string, string>();





    // 生成 文件夹路径
    public string genergateFolderPath;

    // 生成 spine文件路径
    public string genergateFileSpine;


    // 生成 images文件夹路径
    public string genergateFolderImages;

    // 生成 原生图片列表
    public List<string> generateImages = new List<string>();

    // 导出目录
    public string folderExportSpine;



    public SpineImageData(string folderPath, SpineStyleData style)
    {
        enable = true;
        this.folderPath = folderPath;
        this.style = style;
        name = Path.GetFileName(folderPath);
        folderImages = folderPath + "/images";



        if (!Directory.Exists(folderImages))
        {
            Console.WriteLine($"【警告】 不存在 {folderImages}");
            enable = false;
        }


        genergateFolderPath = folderPath;
        if (Setting.Options.useGenerate)
        {
            genergateFolderPath = Setting.Options.rootGenerates + "/" + style.name + "/" + name;
        }

        genergateFolderImages = genergateFolderPath + "/images";
        genergateFileSpine = genergateFolderPath + "/" + style.spineFileName;
        folderExportSpine = Setting.Options.rootExportSpine + "/" + style.name + "/" + name;


        if (enable)
            readImages();
    }

    void readImages()
    {
        string[] files = Directory.GetFiles(folderImages, "*.png");
        images = new List<string>(files);
        foreach (string f in files)
        {
            imageDict.Add(Path.GetFileName(f), f);
        }
    }



    public void Generate()
    {
        if(Setting.Options.useGenerate)
        {
            // 清理之前的生成目录
            if(Directory.Exists(genergateFolderPath))
                Directory.Delete(genergateFolderPath, true);
        }


        // 拷贝spine文件
        CopyCommand.Copy(style.fileSpine, genergateFileSpine);


        // 拷贝image
        if (Setting.Options.useGenerate)
        {
            foreach(string img in images)
            {
                string dst = genergateFolderImages + "/" + Path.GetFileName(img);
                CopyCommand.Copy(img, dst);
            }
        }

        // 检测缺失image
        foreach(var kvp in style.imageDict)
        {
            if(!imageDict.ContainsKey(kvp.Key))
            {
                string dst = genergateFolderImages + "/" + kvp.Key;
                string lib = Setting.Options.rootLib + "/" + kvp.Key;
                if (File.Exists(lib))
                    CopyCommand.Copy(lib, dst);
                else
                    Console.WriteLine($"【警告】 不存在文件 {lib}");
            }
        }


    }
}