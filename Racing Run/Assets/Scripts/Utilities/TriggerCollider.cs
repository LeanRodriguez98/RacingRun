using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour {

    [HideInInspector] public bool isTrigger = false;
    [Header("TagsToCollide")]
    [Space(10)]
    public string[] tagsToCollide;
    [Header("CanDisable")]
    [Space(10)]
    public bool canDisable = false;
        
    public void OnTriggerEnter(Collider other)
    {
         if (tagsToCollide != null)
         {
             for (int i = 0; i < tagsToCollide.Length; i++)
             {
                 if (other.tag == tagsToCollide[i])
                 {
                     isTrigger = true;
                 }
             }
         } 

     
    }

    public void OnTriggerExit(Collider other)
    {
        if (canDisable)
        {
            if (tagsToCollide != null)
            {
                for (int i = 0; i < tagsToCollide.Length; i++)
                {
                    if (other.tag == tagsToCollide[i])
                    {
                        isTrigger = false;
                    }
                }
            }
        }
    }
}

