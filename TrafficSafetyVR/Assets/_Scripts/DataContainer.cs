using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using LumenWorks.Framework.IO.Csv;
using Object = UnityEngine.Object;

public class DataContainer
{
    private Game game { get { return Game.Instance; } }
    private Dictionary<VehicleType, VehicleData> vehicleDatas = new Dictionary<VehicleType, VehicleData>();

    public VehicleData GetVehicleData(VehicleType type)
    {
        VehicleData tmp = null;

        if(vehicleDatas.TryGetValue(type, out tmp))
            return tmp;

        return null;
    }

    public void LoadVehicleData()
    {
        vehicleDatas.Clear();

        string path = game.platform.GetStreamingAssetsPath("/VehicleData.csv");

        using (CsvReader csv = new CsvReader(new StreamReader(path), false))
        {
            csv.ReadNextRecord();
            while (csv.ReadNextRecord())
            {
                int count = 0;

                VehicleData data = new VehicleData();

                VehicleType type = (VehicleType)Enum.Parse(typeof(VehicleType), csv[count++]);
                data.type = type;
                data.resourcePath = csv[count++];
                data.index = Util.Int32ParseFast(csv[count++]);
                data.speed = float.Parse(csv[count++]);
                data.colliderZpos = float.Parse(csv[count++]);

                vehicleDatas.Add(type, data);
            }
        }
    }
}
