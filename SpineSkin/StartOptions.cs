using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommandLine;

public class StartOptions
{
    [Option("autoEnd", Required = false, Default = false)]
    public bool autoEnd { get; set; }


    [Option("cmd", Required = false, Default = "generate")]
    public string cmd { get; set; }

    [Option("setting", Required = false, Default = "./SpineSkinSetting.json")]
    public string setting { get; set; }

    [Option("rootEditor", Required = false, Default = "E:/zengfeng/GamePF/gamepf_art/Animation/SpineWorkspace/Editor")]
    public string rootEditor { get; set; }

    [Option("rootImages", Required = false, Default = "E:/zengfeng/GamePF/gamepf_art/Animation/SpineWorkspace/Images")]
    public string rootImages { get; set; }

    [Option("rootLib", Required = false, Default = "E:/zengfeng/GamePF/gamepf_art/Animation/SpineWorkspace/Lib")]
    public string rootLib { get; set; }

    [Option("rootGenerates", Required = false, Default = "E:/zengfeng/GamePF/gamepf_art/Animation/SpineWorkspace/Generates")]
    public string rootGenerates { get; set; }

    // 是否使用生成目录
    [Option("useGenerate", Required = false, Default = true)]
    public bool useGenerate { get; set; }

    // 导出spine脚本命令
    [Option("exportSpine", Required = false, Default = "E:/zengfeng/GamePF/gamepf_art/Animation/SpineWorkspace/exportspine.bat")]
    public string exportSpineBat { get; set; }

    // 导出spine目录
    [Option("rootExportSpine", Required = false, Default = "E:/zengfeng/GamePF/gamepf_art/Animation/SpineWorkspace/ExportSpine")]
    public string rootExportSpine { get; set; }

    // 导出spine设置
    [Option("exportSpineSettingJson", Required = false, Default = "E:/zengfeng/GamePF/gamepf_art/Animation/SpineWorkspace/laya.export.json")]
    public string exportSpineSettingJson { get; set; }

    // 导出sk目录
    [Option("rootExportSK", Required = false, Default = "E:/zengfeng/GamePF/gamepf_art/Animation/SpineWorkspace/ExportSK")]
    public string rootExportSK { get; set; }

    // 导出spine res目录
    [Option("rootExportSpineRes", Required = false, Default = "E:/zengfeng/GamePF/gamepf_art/Animation/SpineWorkspace/ExportSKRes/Unit")]
    public string rootExportSpineRes { get; set; }





    public void Save(string path = null)
    {
        if (string.IsNullOrEmpty(path))
            path = "./SpineSkinSetting.json";

        string json = JsonHelper.ToJsonType(this);
        File.WriteAllText(path, json);
    }

    public static StartOptions Load(string path = null)
    {
        if (string.IsNullOrEmpty(path))
            path = "./SpineSkinSetting.json";

        string json = File.ReadAllText(path);
        StartOptions options = JsonHelper.FromJson<StartOptions>(json);
        return options;
    }
}