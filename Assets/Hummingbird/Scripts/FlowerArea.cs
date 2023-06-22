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

    private void Awake()
    {
        // Initialize variables
        flowerPlants = new List<GameObject>();
        nectarFlowerDictionary = new Dictionary<Collider, Flower>();
        Flowers = new List<Flower>();
    }

    private void Start()
    {
        // Find all flowers that are children of this GameObject/Transform
        FindChildFlowers(transform);
    }

    /// <summary>
    /// Rest the flowers and flower plants
    /// </summary>
    public void ResetFlower()
    {
        // Rotate each flower plant around Y axis and subtly around X and Z
        foreach (GameObject flowerPlant in flowerPlants)
        {
            float xRotation = Random.Range(-5f, 5f);
            float yRotation = Random.Range(80f, 180f);
            float zRotation = Random.Range(-5f, 5f);
            flowerPlant.transform.localRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }

        // Reset each flower
        foreach (Flower flower in Flowers)
        {
            flower.ResetFlower();
        }
    }

    /// <summary>
    /// Gets the <see cref="Flower"/> that a nectar belongs to
    /// </summary>
    /// <param name="collider">The nectar collider</param>
    /// <returns>The matching flower</returns>
    public Flower GetFlowerFromNectar(Collider collider)
    {
        return nectarFlowerDictionary[collider];
    }

    /// <summary>
    /// Recursively finds all flowers and flower plants that are children of a parent transform
    /// </summary>
    /// <param name="parent">The parent of the children to check</param>
    private void FindChildFlowers(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++) {
            Transform child = parent.GetChild(i);
            
            if (child.CompareTag("flower_plant")) {
                // Found a flower plant add it to the flowerPlants list
                flowerPlants.Add(child.gameObject);

                // Look for flowers within the flower plant
                FindChildFlowers(child);
            }
            else {
                // Not a flower plant, look for a Flower component
                Flower flower = child.GetComponent<Flower>();
                if (flower != null) {
                    // Found a flower, add it to the flowers list
                    Flowers.Add(flower);

                    // Add the nectar collider to the lookup dictionary
                    nectarFlowerDictionary.Add(flower.nectarCollider, flower);

                    // Note: there are no flowers that are children of other flowers
                }
                else {
                    // Flower component not found, so check children
                    FindChildFlowers(child);
                }
            }
        }
    }
}
