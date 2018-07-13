using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiduMapFinder.ExportModels
{
    public class PlaceDetail
    {
        public string Name { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Street_Id { get; set; }
        public string Telephone { get; set; }
        public int? Detail { get; set; }
        public string Uid { get; set; }
    }
}
