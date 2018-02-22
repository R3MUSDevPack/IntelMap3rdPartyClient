using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.ViewModels
{
    public class MapFiles
    {
        public List<MapFile> RegionMaps { get; set; }
        public string InitialMap { get; set; }

        public MapFiles()
        {
            RegionMaps = new List<MapFile>();
        }

        public void Add(string regionName, string path)
        {
            RegionMaps.Add(new MapFile() { RegionName = regionName, Path = path });
        }
    }

    public class MapFile
    {
        public string RegionName { get; set; }
        public string Path { get; set; }
    }
}