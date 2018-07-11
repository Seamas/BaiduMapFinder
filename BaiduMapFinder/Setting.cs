using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiduMapFinder
{
    public class Setting
    {
        #region Data Member 
        public string Ak { get; set; }
        public string Sk { get; set; }
        #endregion



        #region static Member
        private static volatile Setting _setting = null;
        private static object lockObject = new object();
        private static string settingFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"setting.json");

        #endregion
        private Setting() { }

        #region static Function Member
        public static Setting Instance
        {
            get
            {
                if (_setting == null)
                {
                    lock (lockObject)
                    {
                        if (_setting == null)
                        {
                            _setting = Read();
                        }
                    }
                }
                return _setting;
            }
        }

        private static Setting Read()
        {
            try
            {
                var json = File.ReadAllText(settingFile);
                return JsonConvert.DeserializeObject<Setting>(json);
            }
            catch
            {
                return new Setting();
            }

        }

        #endregion

        #region function Member
        public void Save()
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            File.WriteAllText(settingFile, json);
        }
        #endregion
    }
}
