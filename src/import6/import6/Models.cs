namespace import6
{
    public class Domain
    {

        public string Name { get; set; }
        public string MetaName { get; set; }
        public string State { get; set; }
        public bool EnableSSL { get; set; }
        public bool EnableDirBrowsing { get; set; }
        public string Path { get; set; }

        public CustomHeader[] Headers { get; set; }
        public MimeType[] MimeTypes { get; set; }
        public CustomError[] HttpErrors { get; set; }                        
    }

    public class CustomHeader
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class MimeType
    {
        public string Extension { get; set; }
        public string MType { get; set; }
    }

    public class CustomError
    {
        public string HandlerLocation { get; set; }
        public string HandlerType { get; set; }
        public string HttpErrorCode { get; set; }
        public string HttpErrorSubcode { get; set; }
    }
}
