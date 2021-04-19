using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

public class Layout : MonoBehaviour
{
    private PackageJSON[] packageJSONs;
    private SectorJSON[] sectorJSONs;
    public List<Sector> sectors;
    public List<Tuple<int, Vector3>> ShelfPosition;
    public Layout(string path1, string path2)
    {
        sectors = GenerateSectors();
        ShelfPosition = GenerateShelfPosition();
        PackageWrapper packageobjects = JsonUtility.FromJson<PackageWrapper>(path2);
        SectorWrapper sectorobjects = JsonUtility.FromJson<SectorWrapper>(path1);
        sectorJSONs = sectorobjects.sectorobjects.ToArray();
        packageJSONs = packageobjects.packageobjects.ToArray();
    }
    [Serializable]
    public class SectorWrapper
    {
        public List<SectorJSON> sectorobjects;
    }
    [Serializable]
    public class SectorJSON
    {
        public int id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public int width { get; set; }
        public int depth { get; set; }
        public float height { get; set; }
    }

    [Serializable]
    public class PackageWrapper
    {
        public List<PackageJSON> packageobjects;
    }
    [Serializable]
    public class PackageJSON
    {
        public int systemNumber { get; set; }
        public int specimen { get; set; }
        public int package { get; set; }
        public string articleCode { get; set; }
        public Dimensions dimensions { get; set; }
        public Location location { get; set; }

        public class Dimensions
        {
            public float height { get; set; }
            public float width { get; set; }
            public float depth { get; set; }
        }

        public class Location
        {
            public string arrangement { get; set; }
            public int id { get; set; }
        }
    }
    public Vector3 PositionFromArrangement(PackageJSON package)
    {
            if (package.location.arrangement[0] == '1')
            {
                return new Vector3(0, 0, 0);
            }
            else if (package.location.arrangement[1] == '1')
            {
                return new Vector3(package.dimensions.width / 3, 0, 0);
            }
            else if (package.location.arrangement[2] == '1')
            {
                return new Vector3(package.dimensions.width / 3 * 2, 0, 0);
            }
            else
            {
                Debug.LogError("Package position error.");
                return Vector3.zero;
            }
    }
    public List<Tuple<int, Vector3>> GenerateShelfPosition()
    {
        List<Tuple<int, Vector3>> IdPosition = new List<Tuple<int, Vector3>>();
        foreach (SectorJSON idposition in sectorJSONs)
        {
            IdPosition.Add(Tuple.Create(idposition.id, new Vector3(idposition.x, idposition.y, idposition.z)));
        }
        return IdPosition;
    }
    public List<Sector> GenerateSectors()
    {
        List<Sector> CompleteSectors = new List<Sector>();
        foreach (SectorJSON sector in sectorJSONs)
        {
            Sector newsector = new Sector(sector.id);
            foreach (PackageJSON package in packageJSONs)
            {
                if(sector.id == package.location.id)
                {
                    newsector.AddPackageData(new Package(
                        package.articleCode,
                        new Vector3(package.dimensions.width, package.dimensions.height, package.dimensions.depth),
                        PositionFromArrangement(package),
                        package.systemNumber,
                        package.package
                        ));
                }
            }
            CompleteSectors.Add(newsector);
        }
        return CompleteSectors;
    }
}