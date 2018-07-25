using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class SpineResskManager
{

    public void Reads()
    {
        DirectoryInfo directory = new DirectoryInfo(Setting.Options.rootExportSK);
        DirectoryInfo[] infos = directory.GetDirectories();
        foreach (DirectoryInfo styleDir in infos)
        {
            SKStyleData exportData = new SKStyleData(styleDir.FullName);
        }
    }


    public class SKStyleData
    {

        // 文件夹路径
        public string folderPath;

        // 名称
        public string name;

        public bool enable = true;

        public SKStyleData(string folderPath)
        {
            this.folderPath = folderPath;
            name = Path.GetFileName(folderPath);

            string srcSK = null;
            string dstSK = Setting.Options.rootExportSpineRes + "/Bones/" + name + ".sk";
            List<string> srcImages = new List<string>();
            List<string> dstImages = new List<string>();


            DirectoryInfo directory = new DirectoryInfo(folderPath);
            DirectoryInfo[] infos = directory.GetDirectories();
            foreach (DirectoryInfo imgDirInfo in infos)
            {
                string imgName = imgDirInfo.Name;
                if(srcSK == null)
                    srcSK = imgDirInfo.FullName + "/" + imgName + ".sk";
                string srcPng = imgDirInfo.FullName + "/" + imgName + ".png";
                string dstPng = Setting.Options.rootExportSpineRes + "/" + name  + "/" + imgName + ".png";
                srcImages.Add(srcPng);
                dstImages.Add(dstPng);
            }

            if(File.Exists(srcSK))
            {
                CopyCommand.CopyFile(srcSK, dstSK);
            }

            for(int i = 0; i < srcImages.Count; i ++)
            {
                if (File.Exists(srcImages[i]))
                {
                    CopyCommand.CopyFile(srcImages[i], dstImages[i]);
                }

            }
        }
    }
}