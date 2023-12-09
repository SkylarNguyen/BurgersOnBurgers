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
    
    //any visual glitching is probably coming from this code, not from PlateMovement
    void OnTriggerEnter(Collider other)
    {
        GameObject collidedIngredient = other.gameObject;
       

        if (collidedIngredient == kitchenTable)
        {
            // Handle collision with the table
        }
        else if (collidedIngredient == plateInteraction)
        {
            // Handle collision with the plate
        }
        else if (plateMovementScript.stackedIngredients.Count > 0)
        {
            RebuildingStackWith(collidedIngredient);
            Debug.Log("ingredient script stack count: " + plateMovementScript.stackedIngredients.Count);
        }
    }

    //For some reason when you add the falling ingredient on top it causes a lot of physics bugs
    //figured it would be easier to just remake the stack entirely
    void RebuildingStackWith(GameObject collidedIngredient)
    {
        Stack<GameObject> stackedIngredients = plateMovementScript.GetStackedIngredients();
        List<GameObject> reorderedIngredients = new List<GameObject>();

        bool foundInStack = false;

        //colliedIngredient is what the falling ingredient hits into
        foreach (GameObject ingredient in stackedIngredients)
        {
            if (ingredient.name == collidedIngredient.name)
            {
                foundInStack = true;
            }
            else
            {
                reorderedIngredients.Add(ingredient);
            }
        }

        // If the collided ingredient was found, rebuild the stack without it
        if (foundInStack)
        {
            
            // Rebuild the stack in the correct order
            plateMovementScript.PlaceOnPlate(this.gameObject);
            
            foreach (GameObject ingredient in reorderedIngredients)
            {
                stackedIngredients.Push(ingredient);
            }

            //stackedIngredients.Push(collidedIngredient);
            // Place the ingredient on the plate
            stackedIngredients.Clear();
            

            Dictionary<string, int> ingredientsOnPlate = plateMovementScript.GetIngredientsOnPlate();
            string scriptObjectName = this.gameObject.name;

            if (ingredientsOnPlate.ContainsKey(scriptObjectName))
            {
                ingredientsOnPlate[scriptObjectName]++;
            }
            else
            {
                ingredientsOnPlate.Add(scriptObjectName, 1);
            }

            
       
            foreach (GameObject ingredient in reorderedIngredients)
            {
                stackedIngredients.Push(ingredient);
            }
        }
    }


}