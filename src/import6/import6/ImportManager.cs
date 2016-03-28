namespace import6
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management;
    using System.Text;

    public class ImportManager
    {
        private DataAccess data;

        public ImportManager()
        {
            data = new DataAccess();
        }

        public List<Domain> GetAllDomains(string where = "")
        {
            var tmp = new List<Domain>();

            var _query = "SELECT * FROM IIsWebServerSetting";

            if (!String.IsNullOrEmpty(where))
                _query += " WHERE " + where;


            using (var query = data.GetProperties(_query))
            {
                foreach (ManagementObject item in query)
                {
                    var d = new Domain();
                    d.Name = data.GetValue<string>(item, "ServerComment");
                    d.MetaName = data.GetValue<string>(item, "Name");
                    d.Path = GetDomainPath(d.MetaName);
                    d.EnableDirBrowsing = data.GetValue<bool>(item, "EnableDirBrowsing");
                    d.EnableSSL = isSSLEnabled(item);
                    d.Headers = GetCustomHeaders(item);
                    d.HttpErrors = GetErrorPages(item);
                    d.MimeTypes = GetMimeTypes(item);
                                        
                    tmp.Add(d);
                }
            }

            return tmp;
        }

        private string GetDomainPath(string metaName)
        {
            var domainPath = "";
            metaName = String.Format("{0}/root", metaName);
            
            var _query = String.Format("SELECT * FROM IIsWebVirtualDirSetting WHERE Name = '{0}'", metaName);

            using (var query = data.GetProperties(_query))
            {                
                foreach (ManagementObject item in query)
                {
                    domainPath = data.GetValue<string>(item, "Path");
                    break;
                }
            }

            return domainPath;
        }

        private bool isSSLEnabled(ManagementObject item)
        {
            var bindins = data.GetValue<ManagementBaseObject[]>(item, "ServerBindings");
            var result = false;

            if (bindins == null)
                return result;

            foreach (ManagementBaseObject binding in bindins)
            {
                foreach (PropertyData p in binding.Properties)
                {                    
                    if (p.Value.ToString() == "443")
                    {
                        result = true;
                        break;
                    }
                }

                if (result)
                    break;
            }

            return result;
        }

        private CustomHeader[] GetCustomHeaders(ManagementObject item)
        {
            var list = new List<CustomHeader>();
            var headers = data.GetValue<ManagementBaseObject[]>(item, "HttpCustomHeaders");

            if (headers == null)
                return list.ToArray();            

            foreach (ManagementBaseObject header in headers)
            {
                if (header != null)
                {                    
                    var ch = new CustomHeader();

                    var pvalue = header.GetPropertyValue("Value");
                    var pname = header.GetPropertyValue("Keyname");

                    ch.Name = pname != null ? pname.ToString() : "";
                    ch.Value = pvalue != null ? pvalue.ToString() : "";

                    list.Add(ch);

                    //foreach (PropertyData p in header.Properties)
                    //{
                    //    Console.WriteLine("{0}={1}",p.Name, p.Value);

                    //    if (p.Value != null)
                    //    {
                    //        var ch = new CustomHeader();
                    //        ch.Name = p.Name;
                    //        ch.Value = p.Value.ToString();                            

                    //        list.Add(ch);
                    //    }
                    //}
                }
                else
                {
                    Console.WriteLine("Header yok");
                }
            }

            return list.ToArray();
        }

        private CustomError[] GetErrorPages(ManagementObject item)
        {
            var list = new List<CustomError>();
            var errors = data.GetValue<ManagementBaseObject[]>(item, "HttpErrors");

            if (errors == null)
                return list.ToArray();

            foreach (ManagementBaseObject error in errors)
            {
                var err = new CustomError();
                err.HandlerLocation = error.GetPropertyValue("HandlerLocation").ToString();
                err.HandlerType = error.GetPropertyValue("HandlerType").ToString();
                err.HttpErrorCode = error.GetPropertyValue("HttpErrorCode").ToString();
                err.HttpErrorSubcode = error.GetPropertyValue("HttpErrorSubcode").ToString();
                                
                list.Add(err);
            }

            return list.ToArray();
        }

        private MimeType[] GetMimeTypes(ManagementObject item)
        {
            var list = new List<MimeType>();
            var mimes = data.GetValue<ManagementBaseObject[]>(item, "MimeMap");

            if (mimes == null)
                return list.ToArray();

            foreach (ManagementBaseObject mtype in mimes)
            {
                var mime = new MimeType();
                var mExtension = mtype.GetPropertyValue("Extension");
                var mMimeType = mtype.GetPropertyValue("MimeType");

                mime.Extension = mExtension != null ? mExtension.ToString() : "";
                mime.MType = mMimeType != null ? mMimeType.ToString() : "";
                
                list.Add(mime);
            }

            return list.ToArray();
        }

    }
}
