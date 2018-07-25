using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //注册EncodeProvider
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        Setting.Init(args);

        switch(Setting.cmd)
        {
            case CmdType.rename:
                new SpineRenameManager().Reads();
                break;
            case CmdType.ressk:
                new SpineResskManager().Reads();
                break;
            case CmdType.generate:
            default:
                SpineSkinManager manager = new SpineSkinManager();
                manager.ReadStyles();
                manager.ReadImages();
                manager.Generate();
                manager.GenerateExportSpineBat();
                break;
        }



        Console.WriteLine("完成");

        if (!Setting.Options.autoEnd)
            Console.Read();
    }
}