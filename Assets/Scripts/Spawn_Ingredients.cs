using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawn_Ingredients : MonoBehaviour
{

    //Keep all the ingredients collision detection as continuous spectulative
    
    //creates a string which will store the next chosen ingredient
    public string _nextIngredient;

    //creates a variable to store the current ingredient game object
    public GameObject _currentIngredient;

    public Collider kitchenTable; //consider removing this

    //creates a variable to store the current timer value 
    public static float _currentTimerValue;

    //creates an array to store all drop timer values
    public float[] _dropTimer = new float[]
    {
        0.2f,   //0
        0.4f,   //1
        0.6f,   //2
        0.8f,   //3
        1.0f,   //4
        1.2f,   //5
        1.4f,   //6
        1.6f,   //7
        1.8f,   //8
        2.0f    //9

    };

    private bool isFalling = false;

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
        //Set _nextIngredient to equal an empty string at start
        _nextIngredient = "";

        //Set _currentIngredient to equal null at start
        _currentIngredient = null;

        //sets the current timer value to equal the drop timer value 9
        _currentTimerValue = _dropTimer[5];

        StartCoroutine(SpawnNextIngredient());



    }

    // Update is called once per frame
    void Update()
    {
        //if _currentIngredient is equal to null then choose an ingredient
        if(_currentIngredient == null)
        {
            //if there is no current ingredient, we're going to spawn in one and allow the bool of isFalling to be true
            ChooseNextIngredient();
            isFalling = true;
        }

        if(isFalling)
        {
            _currentIngredient.transform.position -= new Vector3(0, _currentTimerValue* Time.deltaTime, 0);
            
            //Note to self: DO NOT use mesh colliders. It won't work idk why

            OnTriggerEnter(kitchenTable);


        }
    }

    IEnumerator SpawnNextIngredient()
    {
        while(true)
        {
            yield return new WaitForSeconds(3f);

            ChooseNextIngredient();

        }
    }

    /*potential info for rigidbody stuff:
    *Continuous is for all interactions involving Rigidbody to check all interactions regardless of velocity or movement type
    *Coninuous Dynamic is for only detections to rigidbody for fast-moving objects
    *
    *I got the colliders to work by taking off the rigidbody off the table and plate, and unchecking the trigger boxes
    *on the ingredients. Not changing my code though in case it helped in that process
    *
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == kitchenTable.gameObject)
        {
            isFalling = false; // Stop the ingredient from falling further

            // Calculate the table's Y-position (adjust this based on your table's position)
            float tableSurfaceY = kitchenTable.transform.position.y;

            // Check if the ingredient has fallen below the table's Y-position
            if (_currentIngredient.transform.position.y < tableSurfaceY)
            {
                // Set the ingredient's Y-position to match the table's surface
                Vector3 ingredientPosition = _currentIngredient.transform.position;
                ingredientPosition.y = tableSurfaceY;
                _currentIngredient.transform.position = ingredientPosition;
            }
        }
    }

    private void ChooseNextIngredient()
    {
        //sets the next ingredient to equal a random ingredient string in the array _ingredients
        _nextIngredient = _ingredients[Random.Range(0, _ingredients.Length)];

        SpawnIngredient();
    }

    private void SpawnIngredient()
    {
        //this is so there's some variety in where the ingredients get spawned instead of right above the plate
        float randomZLocation = Random.Range(-7.5f, -9.50f);


        //if the next ingredient is equal to the "cheese" ingredient string 
        if(_nextIngredient == "cheese")
        {
            /*Instantiate means to load it into the unity scene
            Vector3 is the world space in a 3D scene giving the X,Y,Z using 0,0,0 temporarily
            setting the current game object to equal the "cheese" which we are loading from the resources folder
            spawning as a game object
            */
            _currentIngredient = Instantiate(Resources.Load("cheese"), new Vector3((float)-0.71,(float)2.213,randomZLocation), Quaternion.identity) as GameObject;
            //_currentIngredient.AddComponent<IngredientInteraction>();
        }

        if(_nextIngredient == "lettuce")
        {
        
            _currentIngredient = Instantiate(Resources.Load("lettuce"), new Vector3((float)-0.5676,(float)2.213,randomZLocation), Quaternion.identity) as GameObject;
            //_currentIngredient.AddComponent<IngredientInteraction>();
        }

        if(_nextIngredient == "topBun")
        {
        
            _currentIngredient = Instantiate(Resources.Load("topBun"), new Vector3((float)-0.71,(float)2.213,randomZLocation), Quaternion.identity) as GameObject;
            //_currentIngredient.AddComponent<IngredientInteraction>();
        }

        if(_nextIngredient == "bottomBun")
        {
        
            _currentIngredient = Instantiate(Resources.Load("bottomBun"), new Vector3((float)-0.71,(float)2.213,randomZLocation), Quaternion.identity) as GameObject;
            //_currentIngredient.AddComponent<IngredientInteraction>();
        }
        
        if(_nextIngredient == "burntPatty")
        {
        
            _currentIngredient = Instantiate(Resources.Load("burntPatty"), new Vector3((float)-0.71,(float)2.213,randomZLocation), Quaternion.identity) as GameObject;
            //_currentIngredient.AddComponent<IngredientInteraction>();
        }

        if(_nextIngredient == "cookedPatty")
        {
        
            _currentIngredient = Instantiate(Resources.Load("cookedPatty"), new Vector3((float)-0.71,(float)2.213,randomZLocation), Quaternion.identity) as GameObject;
            //_currentIngredient.AddComponent<IngredientInteraction>();
        }
        
        if(_nextIngredient == "rawPatty")
        {
        
            _currentIngredient = Instantiate(Resources.Load("rawPatty"), new Vector3((float)-0.71,(float)2.213,randomZLocation), Quaternion.identity) as GameObject;
            //_currentIngredient.AddComponent<IngredientInteraction>();
        }

        if(_nextIngredient == "onions")
        {
        
            _currentIngredient = Instantiate(Resources.Load("onions"), new Vector3((float)-0.71,(float)2.213,randomZLocation), Quaternion.identity) as GameObject;
            //_currentIngredient.AddComponent<IngredientInteraction>();
        }
        
        if(_nextIngredient == "tomato")
        {
        
            _currentIngredient = Instantiate(Resources.Load("tomato"), new Vector3((float)-0.71,(float)2.213,randomZLocation), Quaternion.identity) as GameObject;
            //_currentIngredient.AddComponent<IngredientInteraction>();
        }

        if(_nextIngredient == "veggiePatty")
        {
        
            _currentIngredient = Instantiate(Resources.Load("veggiePatty"), new Vector3((float)-0.71,(float)2.213,randomZLocation), Quaternion.identity) as GameObject;
            //_currentIngredient.AddComponent<IngredientInteraction>();
        }
        
    }
}
