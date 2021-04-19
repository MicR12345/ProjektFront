using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.IO;
using UnityEngine;

public class Layout
{
    private PackageJSON[] packageJSONs;
    private SectorJSON[] sectorJSONs;
    public List<Tuple<Sector, Vector3>> ShelfPreparation;
    public Layout(string packagesJSON, string sectorsJSON)
    {
        string jsonPackages = File.ReadAllText(packagesJSON);
        string jsonSectors = File.ReadAllText(sectorsJSON);
        jsonPackages = "{\n\"packageobjects\":" + jsonPackages + "}";
        jsonSectors = "{\n\"sectorobjects\":" + jsonSectors + "}";
        PackageWrapper packageobjects = JsonUtility.FromJson<PackageWrapper>(jsonPackages);
        SectorWrapper sectorobjects = JsonUtility.FromJson<SectorWrapper>(jsonSectors);
        sectorJSONs = sectorobjects.sectorobjects.ToArray();
        packageJSONs = packageobjects.packageobjects.ToArray();
        ShelfPreparation = GenerateShelfPreparation();
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
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float width { get; set; }
        public float depth { get; set; }
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
    public List<Tuple<Sector, Vector3>> GenerateShelfPreparation()
    {
        List<Tuple<Sector, Vector3>> CompleteSectors = new List<Tuple<Sector, Vector3>>();
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
            CompleteSectors.Add(Tuple.Create(newsector, new Vector3(sector.x, sector.y, sector.z)));
        }
        return CompleteSectors;
    }
}