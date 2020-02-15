using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private int totalOutcomes;

    [SerializeField] private int noDropOutcomes;

    [SerializeField] private List<ItemDrop> itemDrops = new List<ItemDrop>();

    public void GetRandomDrop()
    {
        totalOutcomes = noDropOutcomes;
        for(int i = 0; i < itemDrops.Count; i++)
        {
            itemDrops[i].firstIndex = totalOutcomes;
            totalOutcomes += itemDrops[i].outComes;
            itemDrops[i].lastIndex = totalOutcomes;
        }

        int r = Random.Range(0, totalOutcomes);
        r++;

        if (r <= noDropOutcomes)
        {
            return;
        }

        for(int i = 0; i < itemDrops.Count; i++)
        {
            if(r >= itemDrops[i].firstIndex && r <= itemDrops[i].lastIndex)
            { 
                Instantiate(itemDrops[i].drop, transform.position, Quaternion.identity);
                return;
            }

        }
    }
}

[System.Serializable]
public class ItemDrop
{
    public int outComes;
    public GameObject drop;
   
    public int firstIndex;
    public int lastIndex;
}