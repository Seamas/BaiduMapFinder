using System;
namespace OSXBaiduMapSearch.Middle
{
    public class Setting
    {
        public string Ak { get; set; }
        public string Sk { get; set; }
        public string BaseDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private Setting(){}

        private static volatile Setting _instance;
        private static readonly object lockObj = new object();

        public static Setting Instance
        {
            get 
            {
                if (_instance == null)
                {
                    lock(lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new Setting();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
