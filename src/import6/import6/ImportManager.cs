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

                    tmp.Add(d);
                }
            }

            return tmp;
        }

    }
}
