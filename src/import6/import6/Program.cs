namespace import6
{
    using System.Collections.Generic;
    using System.Management;

    class Program
    {
        static void Main(string[] args)
        {
            var mng = new ImportManager();
            var script = mng.GetAllDomainsAsScript();
            System.Console.WriteLine(script);

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
}
