using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class Layout
{
    static string url = "https://localhost:44374";
    static string urlPackages = "/api/Package/GetPackages";
    static string urlSectors = "/api/Sector/GetSectors";

    string packageString;
    string sectorString;

    private PackageJSON[] packageJSONs;
    private SectorJSON[] sectorJSONs;
    public List<Tuple<Sector, Vector3>> ShelfPreparation;

    private string GetStringFromUrl(string url)
    {
        WebClient webClient = new WebClient();
        string result = webClient.DownloadString(url);
        return result;
    }
    public Layout(string packagesJSON, string sectorsJSON)
    {
        packageString = GetStringFromUrl(url+urlPackages);
        sectorString = GetStringFromUrl(url + urlSectors);
        packageString = "{\n\"packageobjects\":" + packageString + "}";
        sectorString = "{\n\"sectorobjects\":" + sectorString + "}";
        PackageWrapper packageobjects = JsonUtility.FromJson<PackageWrapper>(packageString);
        SectorWrapper sectorobjects = JsonUtility.FromJson<SectorWrapper>(sectorString);
        sectorJSONs = new SectorJSON[sectorobjects.sectorobjects.Count];
        sectorJSONs = sectorobjects.sectorobjects.ToArray();
        packageJSONs = new PackageJSON[packageobjects.packageobjects.Count];
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
        public int id;
        public float x;
        public float y;
        public float z;
        public float width;
        public float depth;
        public float height;
    }

    [Serializable]
    public class PackageWrapper
    {
        public List<PackageJSON> packageobjects;
    }
    [Serializable]
    public class PackageJSON
    {
        public int systemNumber;
        public int specimen;
        public int package;
        public string articleCode;
        public Dimensions dimensions;
        public Location location;
        [Serializable]
        public class Dimensions
        {
            public float height;
            public float width;
            public float depth;
        }
        [Serializable]
        public class Location
        {
            public string arrangement;
            public int id;
        }
    }
    public Vector3 PositionFromArrangement(PackageJSON package,SectorJSON sector)
    {
            if (package.location.arrangement[0] == '1')
            {
                return new Vector3(0, 0, 0);
            }
            else if (package.location.arrangement[1] == '1')
            {
                return new Vector3(2f / 3f, 0, 0);
            }
            else if (package.location.arrangement[2] == '1')
            {
                return new Vector3(2f / 3f * 2f, 0, 0);
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
                        PositionFromArrangement(package, sector),
                        package.systemNumber,
                        package.package,
                        package.specimen
                        )) ;
                }
            }
            CompleteSectors.Add(Tuple.Create(newsector, new Vector3(sector.x, sector.y, sector.z)));
        }
        return CompleteSectors;
    }
}
