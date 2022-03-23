using System;
using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Topo;
using UnityEngine;

public class Generator : MonoBehaviour
{
    void Start()
    {
        // Define some sample data
        ITopoArray<char> sample = TopoArray.Create(new[]
        {
            new[]{ 'M', 'M', 'M', 'M'},
            new[]{ 'M', 'P', 'P', 'M'},
            new[]{ 'M', 'M', 'P', 'M'},
        }, periodic: true);
        // Specify the model used for generation
        var model = new AdjacentModel(sample.ToTiles());
        // Set the output dimensions
        var topology = new GridTopology(15, 10, periodic: false);
        // Acturally run the algorithm
        var propagator = new TilePropagator(model, topology);
        var status = propagator.Run();
        if (status != DeBroglie.Resolution.Decided) throw new Exception("Undecided");
        var output = propagator.ToValueArray<char>();
        // Display the results
        for (var y = 0; y < 10; y++)
        {
            var mapLine = "";
            for (var x = 0; x < 10; x++)
            {
                mapLine += output.Get(x, y);
                var prefab = Resources.Load<GameObject>(output.Get(x, y).ToString());
                var go = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
            }
            Debug.Log(mapLine);
        }
        Debug.Log("############");
    }

}
