using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        //注册EncodeProvider
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        Setting.Init(args);

        SpineSkinManager manager = new SpineSkinManager();
        manager.ReadStyles();
        manager.ReadImages();
        manager.Generate();
        manager.GenerateExportSpineBat();



        Console.WriteLine("完成");

        if (!Setting.Options.autoEnd)
            Console.Read();
    }
}