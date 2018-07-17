using AppKit;

namespace OSXBaiduMapSearch
{
    static class MainClass
    {
        static void Main(string[] args)
        {
            
            NSApplication.Init();
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<Middle.AutoMapperConfig>());
            NSApplication.Main(args);
        }
    }
}
