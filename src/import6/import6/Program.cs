namespace import6
{
    using System.Collections.Generic;
    using System.Management;    

    class Program
    {
        static void Main(string[] args)
        {

        }

        private static List<string> GetWebSiteList()
        {
            var _tmp = new List<string>();

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\MicrosoftIISv2", "SELECT * FROM IIsWebServerSetting"))
            {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["ServerComment"] != null)
                        _tmp.Add(queryObj["ServerComment"].ToString());
                }                
            }

            return _tmp;
        }        
    }    
}
