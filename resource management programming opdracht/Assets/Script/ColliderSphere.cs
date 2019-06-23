using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSphere : MonoBehaviour
{
    public int searchTries;
    public float sizeIncrease;
    public Scout scout;
    public LayerMask stone, wood;

    public void SearchforObject(LayerMask mask)
    {
        float currentsize = sizeIncrease;
        bool found = false;
        for (int i = 0; i < searchTries; i++)
        {
            // een for loop die ervoor zorgt dat hij steeds groter word totdat hij iets heeft gevonden en dan breakt hij de forloop
            if (Physics.CheckSphere(transform.position, currentsize, mask))
            {
                found = true;
                break;
            }
            //checkt hier of er iets in de radius is, zoja dan geeft hij aan dat hij iets heeft gevonden
            currentsize += sizeIncrease;
            //increased hiermee de size totdat hij iets heeft gevonden
        }
        if (found)
        {

            scout.target = Physics.OverlapSphere(transform.position, currentsize, mask)[0].transform;
            scout.Exploring();
            //wanneer hij iets heeft gevonden zorgt hij ervoor dat hij de locatie van de dichstbijzijnde target weet
        }
    }
    public void SearchStone()
    {
        SearchforObject(stone);
        // hij pakt de void SearchForObject en gaat daarin met de mask steen om de actie voor alleen steen te activeren en niet voor wood
    }
    public void SearchWood()
    {
        SearchforObject(wood);
    }
// hij pakt de void SearchForObject en gaat daarin met de mask wood om de actie voor alleen wood te activeren en niet voor steen
}