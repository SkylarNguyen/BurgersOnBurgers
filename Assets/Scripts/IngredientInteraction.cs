using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInteraction : MonoBehaviour
{
    public GameObject kitchenTable;
    public GameObject plateInteraction;

    //Note: turned off anti aliasing because my gameobjects in the stack have the smallest white highlight
    //and I'm not sure what caused it. This was the easiest solution to that
    
    //I REPEAT DO NOT ADD MESH COLLIDERS TO THE INGREDIENTS, IT'LL MESS UP THE STACKING

    //Fixed the double script that was attached to the ingedient clones, turns out I was adding it when it gets spawned
    //but it's already attached to the gameobject prefab


    private PlateMovement plateMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        plateMovementScript = FindObjectOfType<PlateMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}