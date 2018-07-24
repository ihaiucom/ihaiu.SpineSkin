using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/// <summary>
/// 样式数据
/// </summary>
public class SpineStyleData
{
    // 文件夹路径
    public string folderPath;

    // 名称
    public string name;

    // spine文件路径
    public string fileSpine;

    // spine文件名
    public string spineFileName;

    // images文件夹路径
    public string folderImages;

    // 是否有效的
    public bool enable;

    // 图片文件列表
    public List<string> images = new List<string>();
    public Dictionary<string, string> imageDict = new Dictionary<string, string>();



    public List<SpineImageData> imageDatas = new List<SpineImageData>();


    public SpineStyleData(string folderPath)
    {
        enable = true;
        this.folderPath = folderPath;
        name = Path.GetFileName(folderPath);
        fileSpine = folderPath + "/Main.spine";
        folderImages = folderPath + "/images";

        if (!File.Exists(fileSpine))
        {
            string[] files = Directory.GetFiles(folderPath, "*.spine");
            if (files.Length > 0)
                fileSpine = files[0];
        }

        spineFileName = Path.GetFileName(fileSpine);

        if (!File.Exists(fileSpine))
        {
            Console.WriteLine($"【警告】 不存在 {fileSpine}");
            enable = false;
        }

        if(!Directory.Exists(folderImages))
        {
            Console.WriteLine($"【警告】 不存在 {folderImages}");
            enable = false;
        }

        if(enable)
            readImages();
    }

    void readImages()
    {
        string[] files = Directory.GetFiles(folderImages, "*.png");
        images = new List<string>(files);
        foreach(string f in files)
        {
            imageDict.Add(Path.GetFileName(f), f);
        }

    }




}