using System;
using System.Collections.Generic;
using System.Text;

namespace Utilerias
{
    public class ConfiguracionSAP
    {
        public string Usuario { get; set; }
        public string PSW { get; set; }
        public string Host { get; set; }
        public string Cliente { get; set; }
        public string Idioma { get; set; }
        public string SN { get; set; }
        public ConfiguracionSAP()
        {
            Usuario = string.Empty;
            PSW = string.Empty;
            Host = string.Empty;
            Cliente = string.Empty;
            Idioma = string.Empty;
            SN = string.Empty;
        }
    }
    public class ConfiguracionLocal
    {
        public static string StringConnectionSqlServer { get; set; }
        public static string StringConnectionSqlServerB2bProveedores { get; set; }
        public static string StringConnectionOracle { get; set; }
        public static ConfiguracionSAP SAPConnection { get; set; }

        public static string formId1 { get; set; }
        public static string formId2 { get; set; }

        public static string UrlGetLeadsFacebook { get; set; }
        
        public static string Amb { get; set; }
        public static string Patch { get; set; }

        public ConfiguracionLocal()
        {
            StringConnectionSqlServer = string.Empty;
            StringConnectionOracle = string.Empty;
            UrlGetLeadsFacebook = string.Empty;
            Amb = string.Empty;
            SAPConnection = new ConfiguracionSAP();
            Patch = string.Empty;
        }
    }
}