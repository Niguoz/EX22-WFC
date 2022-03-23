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
            new[]{ '_', '_', '_'},
            new[]{ '_', '*', '_'},
            new[]{ '_', '_', '_'},
        }, periodic: false);
        // Specify the model used for generation
        var model = new AdjacentModel(sample.ToTiles());
        // Set the output dimensions
        var topology = new GridTopology(10, 10, periodic: false);
        // Acturally run the algorithm
        var propagator = new TilePropagator(model, topology);
        var status = propagator.Run();
        if (status != DeBroglie.Resolution.Decided) throw new Exception("Undecided");
        var output = propagator.ToValueArray<char>();
        // Display the results
        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                Debug.Log(output.Get(x, y));
            }
            Debug.Log("############");
        }
    }

}
