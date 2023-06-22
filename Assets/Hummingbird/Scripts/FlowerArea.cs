using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a collection of flower plants and attached to flowers
/// </summary>
public class FlowerArea : MonoBehaviour
{
    // The diameter of the area where the agent and flowers can be
    // used for observing distance from agent to flower
    public const float AreaDiameter = 20f;

    // The list of all the flower plants in this flower area (flower area have multiple flowers)
    private List<GameObject> flowerPlants;

    // A Lookup dictionary for looking up a flower from a nectar collider
    private Dictionary<Collider, Flower> nectarFlowerDictionary;

    // The list of all flowers in the flower area
    public List<Flower> Flowers { get; private set; }
}
