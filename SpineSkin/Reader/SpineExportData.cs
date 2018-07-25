using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class SpineExportData
{

    // 文件夹路径
    public string folderPath;

    // 名称
    public string name;

    public bool enable = true;

    public SpineExportData(string folderPath)
    {
        this.folderPath = folderPath;
        name = Path.GetFileName(folderPath);

        string fileAtlas = null;
        string fileJson = null;
        string filePng = null;

        string[] files = Directory.GetFiles(folderPath, "*.atlas");
        if (files.Length > 0)
        {
            fileAtlas = files[0];
        }
        else
        {
            enable = false;
            Console.WriteLine($"【警告】 不存在 {folderPath}/xxx.atlas");
        }

        files = Directory.GetFiles(folderPath, "*.atlas");
        if (files.Length > 0)
        {
            fileAtlas = files[0];
        }
        else
        {
            enable = false;
            Console.WriteLine($"【警告】 不存在 {folderPath}/xxx.atlas");
        }



        files = Directory.GetFiles(folderPath, "*.json");
        if (files.Length > 0)
        {
            fileJson = files[0];
        }
        else
        {
            enable = false;
            Console.WriteLine($"【警告】 不存在 {folderPath}/xxx.json");
        }



        files = Directory.GetFiles(folderPath, "*.png");
        if (files.Length > 0)
        {
            filePng = files[0];
        }
        else
        {
            enable = false;
            Console.WriteLine($"【警告】 不存在 {folderPath}/xxx.png");
        }

        if(enable)
        {
            string fileAtlasName = folderPath + "/" + name + ".atlas";
            string fileJsonName = folderPath + "/" + name + ".json";
            string filePngName = folderPath + "/" + name + ".png";
            
            if(!File.Exists(fileAtlasName))
            {
                File.Move(fileAtlas, fileAtlasName);
            }

            if (!File.Exists(fileJsonName))
            {
                File.Move(fileJson, fileJsonName);
            }

            if (!File.Exists(filePngName))
            {
                File.Move(filePng, filePngName);
            }

            //if (File.Exists(fileAtlas))
            //    File.Delete(fileAtlas);

            //if (File.Exists(fileJson))
            //    File.Delete(fileJson);

            //if (File.Exists(filePng))
            //    File.Delete(filePng);


            string conent = File.ReadAllText(fileAtlasName).Replace(Path.GetFileName(filePng), Path.GetFileName(filePngName));
            File.WriteAllText(fileAtlasName, conent);

        }

    }
}