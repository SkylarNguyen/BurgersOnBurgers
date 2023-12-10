using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; //I need this for using the Where in the printouts

public class StackableObject : MonoBehaviour
{
    public bool onStack = false;
    public bool onGround = false;
    private StackableObject parentStackableObject;
    
    private Dictionary<string, int> ingredientsOnPlate = new Dictionary<string, int>();

    //string array for all the ingredient naming conventions
    public string[] _ingredients = new string[]
    {
        "cheese",
        "lettuce",
        "topBun",
        "bottomBun",
        "burntPatty",
        "cookedPatty",
        "rawPatty",
        "onions",
        "tomato",
        "veggiePatty"
    };

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // THIS IS FALLING OBJECT, COLLISION OBJECT IS ON STACK OR PLATE
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Table"))
        {
            gameObject.SetActive(false);
            onGround = true;
        }

        // if plate, handle
        if (collision.gameObject.CompareTag("Plate") && !onGround)
        {
            gameObject.transform.SetParent(collision.gameObject.transform);
            AlignStack(collision.gameObject);
            onStack = true;

            parentStackableObject = collision.gameObject.GetComponent<StackableObject>();
            Debug.Log("this is the parentStackableObject: " + collision.gameObject.name);

            if (!onStack)
            {
                parentStackableObject.UpdateIngredientsOnPlate();
            }

            //Debug.Log("Parent name: " + parentStackableObject.name);

            UpdateIngredientsOnPlate();

            Debug.Log(" printing out : " + gameObject.name);
        } 

        if (collision.gameObject.CompareTag("Ingredient") && !gameObject.CompareTag("Plate") && !onGround)
        {
            //this is the ingredient on the stack
            StackableObject stackableObject = collision.gameObject.GetComponent<StackableObject>();

            if (onStack && stackableObject.gameObject.name != gameObject.name)
            {
                Debug.Log("collision.gameObject here is: " + stackableObject.name + " and gameObject is " + gameObject.name);
                stackableObject.UpdateIngredientsOnPlate();
            }
            else if (stackableObject != null && onStack)
            {
                stackableObject.UpdateIngredientsOnPlate();
            }
            else //(stackableObject != null) //notnull and also not on the stack
            {
                AlignStack(collision.gameObject);
                onStack = true;

                parentStackableObject = collision.gameObject.GetComponent<StackableObject>();

                if (onStack && stackableObject.gameObject.name == gameObject.name)
                {
                    stackableObject.UpdateIngredientsOnPlate();
                }
            }

            // Debug.Log("Falling ingredient: " + this.gameObject.name);
        }
    }
    

    //According to the scene, it looks like I only have to offset the ingredient on the Z axis
    void AlignStack(GameObject collidedObject)
    {
        
        float ownZ = transform.position.z;
        float otherZ = collidedObject.transform.position.z;

        float zOffset = otherZ - ownZ;

        transform.position += new Vector3(0f, 0f, zOffset);

        //setting the parent
        gameObject.transform.SetParent(collidedObject.transform);
        
        

    }

    // Update ingredient count dictionary when stacked
    public void UpdateIngredientsOnPlate()
    {
        Debug.Log("Stacked Ingredients List: ");

        if (parentStackableObject != null)
        {
            string ingredientKey = gameObject.name;

            if (!ingredientsOnPlate.ContainsKey(ingredientKey))
            {
                // If the ingredient doesn't exist on the plate, add it with the count of 1
                ingredientsOnPlate.Add(ingredientKey, 1);
            }
            else
            {
                // If the ingredient exists, increment its count
                int existingCount = ingredientsOnPlate[ingredientKey];
                ingredientsOnPlate[ingredientKey] = existingCount + 1;
            }
            

            // Iterate through parent's ingredients and update the plate's ingredient count
            foreach (var pair in parentStackableObject.ingredientsOnPlate)
            {
                string key = pair.Key;
                int value = pair.Value;
                //Debug.Log("Current key: " + key + ", value: " + value);

                if (key != "Plate" && !ingredientsOnPlate.ContainsKey(key))
                {
                    //Debug.Log("Adding new ingredient: " + key + ", value: " + value);
                    ingredientsOnPlate.Add(key, value);
                }
                else if (key != "Plate" && ingredientsOnPlate.ContainsKey(key))
                {
                    int currentCount = ingredientsOnPlate[key];
                    //Debug.Log("Existing ingredient: " + key + ", current count: " + currentCount);
                    ingredientsOnPlate[key] = currentCount + value;
                    //Debug.Log("Updated ingredient count: " + key + ", new count: " + ingredientsOnPlate[key]);
                }
            }

        }
        else
        {
            // If there's no parent, just add the ingredient with the initial count
            string ingredientKey = gameObject.name;
            if (!ingredientsOnPlate.ContainsKey(ingredientKey))
            {
                ingredientsOnPlate.Add(ingredientKey, 1);
            }
            else
            {
                int existingCount = ingredientsOnPlate[ingredientKey];
                ingredientsOnPlate[ingredientKey] = existingCount + 1;
            }
        }

        PrintingredientsOnPlates();
    }

    private void PrintingredientsOnPlates()
    {
        foreach (var pair in ingredientsOnPlate)
        {
            if (pair.Key != "Plate" && pair.Value > 0)
            {
                Debug.Log(pair.Key + ":" + pair.Value);
            }
        }

    
    }
}
    
