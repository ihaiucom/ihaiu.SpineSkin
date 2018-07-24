using CommandLine;
using System;
using System.IO;


public class Setting
{
    public static StartOptions Options { get; set; }

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