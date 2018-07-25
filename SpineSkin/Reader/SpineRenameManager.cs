using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class SpineRenameManager
{

    public void Reads()
    {
        DirectoryInfo directory = new DirectoryInfo(Setting.Options.rootExportSpine);
        DirectoryInfo[] infos = directory.GetDirectories();
        foreach (DirectoryInfo styleDir in infos)
        {
            DirectoryInfo[] dirs = styleDir.GetDirectories();
            foreach (DirectoryInfo item in dirs)
            {
                SpineExportData exportData = new SpineExportData(item.FullName);
            }
        }
    }
}