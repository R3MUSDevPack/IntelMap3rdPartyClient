using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.ViewModels
{
    public class MapFiles
    {
        public List<List<MapFile>> RegionMaps { get; set; }
        public string InitialMap { get; set; }

        public MapFiles()
        {
            RegionMaps = new List<List<MapFile>>();
            RegionMaps.Add(new List<MapFile>());
        }

        public void Add(string regionName, string path)
        {
            if (!RegionMaps.Any(w => w.Count <= 5))
            {
                RegionMaps.Add(new List<MapFile>());
            }
            RegionMaps.First(w => w.Count <= 5).Add(new MapFile() { RegionName = regionName, Path = path });
        }
    }

    public class MapFile
    {
        public string RegionName { get; set; }
        public string Path { get; set; }
    }
}