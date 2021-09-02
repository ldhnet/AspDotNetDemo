using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebMVC.Models
{
    public enum ConfigEnvironment
    {
        LOCAL,
        DEV,
        QA,
        UAT,
        PROD
    }
    public class ConnectionStringManager
    {
        public static string PinkCarsConnectionString { get; set; }
        public static string DataMartConnectionString { get; set; }
        public static string ConsultantsConnectionString { get; set; }
        public static string HangfireConnectionString { get; set; }
        public static ConfigEnvironment Env { get; set; }
        public static IDictionary CustomerSection { get; set; }

        static ConnectionStringManager()
        {
            //string env = ConfigManager.GetSetting("Env").ToLower();
            //PinkCarsConnectionString = ConfigManager.GetConnectionString(env);
            string envType = ConfigurationManager.AppSettings.Get("pinkCarEnvironment");
            Env = (ConfigEnvironment)Enum.Parse(typeof(ConfigEnvironment), envType.ToUpper());
            CustomerSection = (IDictionary)ConfigurationManager.GetSection("pinkCarEnvironment/" + envType);
            PinkCarsConnectionString = CustomerSection["pinkCarConnectionString"].ToString();
            DataMartConnectionString = CustomerSection["dataMartConnectionString"].ToString();
            ConsultantsConnectionString = CustomerSection["consultantsConnectionString"].ToString();
            HangfireConnectionString = CustomerSection["hangfireConnectionString"].ToString();
        }
    }
}