using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateMovement : MonoBehaviour
{
    public GameObject plate;
    public float movementSpeed = 4.0f;
    

    public float leftLimit;
    public float rightLimit;

    private Dictionary<string, int> ingredientsOnPlate = new Dictionary<string, int>();
    public Stack<GameObject> stackedIngredients = new Stack<GameObject>();
    private bool hasStack = false;
    private int firstIngredientDown = 0;

    //Note: I changed some of the physics in the Edit -> Project Settings -> Physics area to reduce and 
    //hopefully eliminate the bounciness issues I was having which caused bugs

    void Start()
    {
        float tableWidth = GetComponent<Renderer>().bounds.size.x;
        Vector3 tablePosition = transform.position;

        leftLimit = tablePosition.z - tableWidth / 2.0f;
        rightLimit = tablePosition.z + tableWidth / 2.0f;


    }

    void Update()
    {

        Vector3 currentPlatePos = plate.transform.position;

        //The code for the arrow keys have to be this way because I accidentally set up my scene to be in a 
        //different perspective which made the controls inverted. This code below makes it "normal"

        if (Input.GetKey(KeyCode.LeftArrow) && currentPlatePos.z < rightLimit)  // Change '>' to '<'
        {
            plate.transform.position = new Vector3(currentPlatePos.x, currentPlatePos.y, currentPlatePos.z + movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow) && currentPlatePos.z > leftLimit)  // Change '<' to '>'
        {
            plate.transform.position = new Vector3(currentPlatePos.x, currentPlatePos.y, currentPlatePos.z - movementSpeed * Time.deltaTime);
        }



    }

    //this is what the plate does when it collides with another gameobject
    private void OnCollisionEnter(Collision collision)
    {
        //the collidedIngredient being the other item, not the plate
        GameObject collidedIngredient = collision.gameObject;
        string ingredientName = collidedIngredient.name;
        
        if(!hasStack)
        {
            PlaceOnPlate(collidedIngredient);
            stackedIngredients.Push(collidedIngredient); // Push the new ingredient onto the stack
            Debug.Log("inside collision, stack count: " + stackedIngredients.Count +" " +  collidedIngredient.name);
            hasStack = true;

            // Update ingredient count
            if (ingredientsOnPlate.ContainsKey(ingredientName))
            {
                ingredientsOnPlate[ingredientName]++;
            }
            else
            {
                ingredientsOnPlate.Add(ingredientName, 1);
            }
        }
        

    }

    public Dictionary<string, int> GetIngredientsOnPlate()
    {
        return ingredientsOnPlate;
    }

    public Stack<GameObject> GetStackedIngredients()
    {
        return stackedIngredients;
    }



    public void PlaceOnPlate(GameObject ingredient)
    {
        Debug.Log("stacked count: " + stackedIngredients.Count);
        //fixed the issue where if a falling ingredient touches the side of the ingredient stack, it may take 
        //some ingredients out the stack physically in the game. 
        //SOLUTION: I set all the ingredients rigidbody constraints to freeze the X and Z position.
        //we can't freeze Y because the ingredient won't fall if we do

        //Note: The plate needs to have both a box collider unchecked and a mesh collider checked.
        //why? I don't know. If it's just a mesh collider, it doesn't work correctly. If it's just a box collider
        //the ingredients have a gap between itself and the plate

        //putting the && hasStack == false to hopefully fix bugs
        if (stackedIngredients.Count == 0 && hasStack == false)
        {
            //this if statement works, don't need to change.
            ingredient.transform.position = transform.position;
            ingredient.transform.SetParent(transform);

            //this didn't help the visual bugs but leaving it in case it does even a little bit
            Physics.SyncTransforms(); 
            
        }
        else
        {
            GameObject topIngredient = stackedIngredients.Peek();
            Collider topIngredientCollider = topIngredient.GetComponent<Collider>();
            //Debug.Log("the else statement is working now");

            if (topIngredientCollider != null)
            {
                Debug.Log("is this null?");
                Bounds topIngredientBounds = topIngredientCollider.bounds;
                Bounds ingredientBounds = ingredient.GetComponent<Collider>().bounds;

                float offsetY = topIngredientBounds.extents.y + ingredientBounds.extents.y; // Using extents for a simpler calculation

                Vector3 offset = Vector3.up * offsetY;
                ingredient.transform.position = topIngredient.transform.position + offset;
                ingredient.transform.SetParent(topIngredient.transform);
                Physics.SyncTransforms();
            }
            else
            {
                Debug.LogError("Collider component not found on topIngredient.");
                // Handle the case where the topIngredient Collider component is missing
            }

            stackedIngredients.Push(ingredient); // Push the new ingredient onto the stack

            Debug.Log("Stacked Ingredients:");

            foreach (GameObject ingredientStackObject in stackedIngredients)
            {
                Debug.Log(ingredientStackObject.name);
            }
        }
    }
    

    
}