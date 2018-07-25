using CommandLine;
using System;
using System.IO;

public class CmdType
{
    public const string generate = "generate";
    public const string rename = "rename";
    public const string ressk = "ressk";
}

public class Setting
{
    public static StartOptions Options { get; set; }
    public static string cmd = CmdType.generate;

    public static void Init(string[] args)
    {
        bool useSetting = args.Length == 0;
        foreach (string op in args)
        {
            if (op.StartsWith("--setting"))
            {
                useSetting = true;
                break;
            }
        }

        Parse(args);

        if (!File.Exists(Options.setting))
        {
            Options.Save(Options.setting);
        }

        cmd = Options.cmd;
        if(string.IsNullOrEmpty(cmd))
        {
            cmd = CmdType.generate;
        }

        if (useSetting)
        {
            Options = StartOptions.Load(Options.setting);
        }
    }


    public static void Parse(string[] args)
    {
        Parser.Default.ParseArguments<StartOptions>(args)
            .WithNotParsed(error => throw new Exception($"命令行格式错误!"))
            .WithParsed(options =>
            {
                Options = options;
            });
    }


}