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


    public Dictionary<string, int> GetIngredientsOnPlate()
    {
        return ingredientsOnPlate;
    }

    public Stack<GameObject> GetStackedIngredients()
    {
        return stackedIngredients;
    }
    

    
}