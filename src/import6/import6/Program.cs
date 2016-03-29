namespace import6
{
    using CommandLine;
    using System;
    using System.Collections.Generic;
    using System.Management;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            var result = CommandLine.Parser.Default.ParseArguments(args, options);

            if (!result)
            {
                Console.WriteLine("Invalid parameters");
                System.Environment.Exit(1);
            }

            var mng = new ImportManager(options.Host, options.Plan, options.Port, options.APIKey);
            var script = mng.GetAllDomainsAsScript(addwebsite: options.Create);

            Console.WriteLine(script);

            //TestPrint();
        }

        static void TestPrint()
        {
            var mng = new ImportManager();
            var list = mng.GetAllDomains("ServerComment ='idealgayrimenkul.com.tr'");

            foreach (var item in list)
            {
                System.Console.WriteLine("Name: {0}", item.Name);
                System.Console.WriteLine("Meta: {0}", item.MetaName);
                System.Console.WriteLine("Path: {0}", item.Path);
                System.Console.WriteLine("Dir Browsing: {0}", item.EnableDirBrowsing.ToString());
                System.Console.WriteLine("SSL: {0}", item.EnableSSL.ToString());

                System.Console.WriteLine("Custom Headers");

                foreach (var head in item.Headers)
                    System.Console.WriteLine("{0}: {1}", head.Name, head.Value);

                System.Console.WriteLine("Http Errors");

                foreach (var error in item.HttpErrors)
                    System.Console.WriteLine("{0}, {1}, {2}", error.HandlerLocation, error.HandlerType, error.HttpErrorCode);

                System.Console.WriteLine("Mime Types");

                foreach (var mime in item.MimeTypes)
                    System.Console.WriteLine("{0}: {1}", mime.Extension, mime.MType);
            }
        }

    }

    public class Options
    {
        [Option('c', "create", HelpText = "Add create web site script", DefaultValue=false)]
        public bool Create { get; set; }

        [Option('k', "key", HelpText = "MaestroPanel API Key")]
        public string APIKey { get; set; }

        [Option('h', "host", HelpText = "MaestroPanel Host")]
        public string Host { get; set; }

        [Option('p', "port", DefaultValue = "9715", HelpText = "MaestroPanel Port")]
        public string Port { get; set; }

        [Option('s', "ssl", DefaultValue = false, HelpText = "MaestroPanel Enable SSL")]
        public bool SSL { get; set; }

        [Option('d', "plan", HelpText = "Domain Plan Name", DefaultValue="default")]
        public string Plan { get; set; }

        [HelpOption]
        public string Usage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("MaestroPanel IIS 6 Import Tool");
            usage.AppendLine("");
            usage.AppendLine("Parameters:");
            usage.AppendLine("");
            usage.AppendLine("\tkey: MaestroPanel API Key");
            usage.AppendLine("\thost: MaestroPanel Web Management Service Host. IP or Hostname");
            usage.AppendLine("\tport: MaestroPanel Web Management Service Port. Default 9715");
            usage.AppendLine("\tssl: Use SSL protocols access to MaestroPanel. Default false");
            usage.AppendLine("\tplan: MaestroPanel domain plan");
            usage.AppendLine("\tcreate: Generate curl script for create web site");
            usage.AppendLine("");
            usage.AppendLine("Usage:");
            usage.AppendLine("");
            usage.AppendLine("import6 --create --key 1_885bd9d868494d078d4394809f5ca7ac --host 192.168.5.2 --plan default");            

            return usage.ToString();
        }
    }
}
