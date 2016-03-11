namespace import6
{
    public class Domain
    {

        public string Name { get; set; }
        public string MetaName { get; set; }
        public string State { get; set; }
        public bool HasSSL { get; set; }

        public CustomHeader[] Headers { get; set; }
        public MimeType[] MimeTypes { get; set; }
        public CustomError[] CustomErrors { get; set; }                        
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
        public string MessageType { get; set; }
        public string MessageValue { get; set; }
    }
}
