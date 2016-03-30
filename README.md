# import6 - IIS6 to MaestroPanel
IIS 6 Web sunucusundaki web sitelerini MaestroPanel'e aktaran ufak bir araçtır.

## Gereksinimler

* .NET Framework 4+
* CURL for Windows

## Download


## Kapsam

Import6 aracı IIS 6 Web Sunucusundaki web siteleri, web sitesine ait DirBrowsing, Custome HTTP Headers, HttpErrors, MimeTypes özelliklerinin varsayılan farklılıklarını algılayıp buna göre appcmd.exe komutları üretir.

Model

```charp
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
    }```

## Kullanım

Import6 aracı komut satırı (command line) üzerinden çalışmaktadır. Uygulamayı yönetmek için belirli parametrelere gereksinim duyuluyor. Aşağıda ilgili parametreleri bulabilirsiniz.

> __key:__ MaestroPanel'e erişimde kullanılan değeri barındırır.
>
> __host:__ MaestroPanel'in çalıştığı sunucu veya IP adresi.
>
> __port:__ MaestroPanel'in çalıştığı port. Varsayılan olarak: 9715 değerindedir.
>
> __ssl:__ MaestroPanel'e erişimde kullanılacak protokol. Varsayılan olarak HTTP kullanılır.
>
> __plan:__ Domain'in açılacağı Domain Planı Alias'ı. MaestroPanel üzerinden ayarlanabilir.
>
> __create:__ IIS 7+ komutları hazırlanırken MaestroPanel için yeni domain ekleme komutunun eklenip eklenmeyeceğini belirler.

Import6 aracının çıktısını direkt IIS 7+ çalışan sunucuda çalıştırmanız yeterli.

Örnekler:

Domain Açılış komutlarını dahil etmekmek için;

```import6 --create --key 1_885bd9d868494d078d4394809f5ca7ac --host 192.168.5.2 --plan default >.\script.txt```

Sadece IIS6 ayarları için

```import6.exe > .\script.txt```

## İletişim

ping@maestropanel.com

www.maestropanel.com

MaestroPanel Migration Team