using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrderMenu : MonoBehaviour
{
    //creates a string which will store the next chosen ingredient
    public string _nextIngredient;
    public int howManyIngredients;

    //needed because the ingredients that spawn for this script kept falling through the world map
    public bool enableRigidbody = false;

    //creates a variable to store the current ingredient game object
    public GameObject _currentIngredient;
    public GameObject topBun;
    public GameObject bottomBun;

    //the scaleChange is because the assets are bigger than what I want for this script
    public Vector3 scaleChange;

    private Dictionary<string, int> ingredientCount = new Dictionary<string, int>();
    private List<GameObject> spawnedIngredients = new List<GameObject>();

    private bool ingredientsPrinted = false;
    private bool allIngredientsSpawned = false;

    //the whole buns spawning thing is probably not needed because they're already in the scene 
    //but im scared to change stuff around now
    private string[] buns = new string[] {"topBun", "bottomBun"};

    //string array for all the ingredient naming conventions
    private string[] _ingredients = new string[]
    {
        "cheese",   //0
        "lettuce",  //1
        "cookedPatty",  //2
        "onions",   //3
        "tomato",   //4
        "veggiePatty"   //5
    };


    // Start is called before the first frame update
    void Start()
    {
        //Set _nextIngredient to equal an empty string at start
        _nextIngredient = "";
        howManyIngredients = 0;

        //Set _currentIngredient to equal null at start
        _currentIngredient = null;

        foreach(string ingredient in _ingredients)
        {
            ingredientCount[ingredient] = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if _currentIngredient is equal to null then choose an ingredient
        if(_currentIngredient == null)
        {
            ChooseNextIngredient();
        }

        if (!ingredientsPrinted && AllIngredientsSpawned())
        {
            ingredientsPrinted = true;
            PrintIngredientCounts();
        }

    }

    private void spawnBuns()
    {
        topBun = Instantiate(Resources.Load("topBun"), new Vector3((float)3.604,(float)1.475,(float)-5.9), Quaternion.identity) as GameObject;
        bottomBun = Instantiate(Resources.Load("bottomBun"), new Vector3((float)3.604,(float)0.781,(float)-5.9), Quaternion.identity) as GameObject;
        
        topBun.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        bottomBun.transform.localScale = new Vector3(0.5f,0.5f,0.5f);

        
        //TURNS OUT I DON'T NEED ALL THIS CODE because the buns are prespawned into the scene anyways
        //so I can just go to the inspector for those gameobjects in particular and set their rigidbody constraints 
        //to freeze the position and rotation for X,Y,Z on both.
        //I don't know if that's the correct way to solve it but that's what I'll do
        //specifically solving that the buns get their physics affected along with the Spawn_Ingredients
        //ingredients. It got fixed in these ingredients but not the buns.
        /*Rigidbody topBunRigidbody = topBun.GetComponent<Rigidbody>();
        Rigidbody bottomBunRigidbody = bottomBun.GetComponent<Rigidbody>();

        if(topBunRigidbody != null && bottomBunRigidbody != null)
        {
            topBunRigidbody.isKinematic = !enableRigidbody;
            bottomBunRigidbody.isKinematic = !enableRigidbody;
        }*/
        
        
    }

    private void ChooseNextIngredient()
    {

        howManyIngredients = Random.Range(1,7);

        for(int i = 0; i < howManyIngredients; i++)
        {
            //sets the next ingredient to equal a random ingredient string in the array _ingredients
            _nextIngredient = _ingredients[Random.Range(0, _ingredients.Length)];

            
            switch(i)
            {
                case 0:

                    SpawnIngredient();
                    _currentIngredient.transform.position = new Vector3((float)3.544,(float)1.29,(float)-5.589);
                    _currentIngredient.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
                    _currentIngredient.transform.localRotation = Quaternion.Euler(0,0,90);
                    break;

                case 1:
                    
                    SpawnIngredient();
                    _currentIngredient.transform.position = new Vector3((float)3.544,(float)1.29,(float)-5.908);
                    _currentIngredient.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
                    _currentIngredient.transform.localRotation = Quaternion.Euler(0,0,90);
                    break;

                case 2:

                    SpawnIngredient();
                    _currentIngredient.transform.position = new Vector3((float)3.544,(float)1.29,(float)-6.227);
                    _currentIngredient.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
                    _currentIngredient.transform.localRotation = Quaternion.Euler(0,0,90);
                    break;

                case 3:

                    SpawnIngredient();
                    _currentIngredient.transform.position = new Vector3((float)3.544,(float)1.027,(float)-5.589);
                    _currentIngredient.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
                    _currentIngredient.transform.localRotation = Quaternion.Euler(0,0,90);
                    break;

                case 4:

                    SpawnIngredient();
                    _currentIngredient.transform.position = new Vector3((float)3.544,(float)1.027,(float)-5.908);
                    _currentIngredient.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
                    _currentIngredient.transform.localRotation = Quaternion.Euler(0,0,90);
                    break;

                case 5:

                    SpawnIngredient();
                    _currentIngredient.transform.position = new Vector3((float)3.544,(float)1.027,(float)-6.227);
                    _currentIngredient.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
                    _currentIngredient.transform.localRotation = Quaternion.Euler(0,0,90);
                    break;

            }

            
        }

    }

    private void SpawnIngredient()
    {

        GameObject newIngredient = null;

        //if the next ingredient is equal to the "cheese" ingredient string 
        if(_nextIngredient == "cheese")
        {
            /*Instantiate means to load it into the unity scene
            Vector3 is the world space in a 3D scene giving the X,Y,Z using 0,0,0 temporarily
            setting the current game object to equal the "cheese" which we are loading from the resources folder
            spawning as a game object
            */
            //_currentIngredient = Instantiate(Resources.Load("cheese"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
            newIngredient = Instantiate(Resources.Load("cheese"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
        }
        

        if(_nextIngredient == "lettuce")
        {
        
            //_currentIngredient = Instantiate(Resources.Load("lettuce"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
            newIngredient = Instantiate(Resources.Load("lettuce"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
        }
        

        if(_nextIngredient == "cookedPatty")
        {
        
            //_currentIngredient = Instantiate(Resources.Load("cookedPatty"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
            newIngredient = Instantiate(Resources.Load("cookedPatty"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
        }

        if(_nextIngredient == "onions")
        {
        
            //_currentIngredient = Instantiate(Resources.Load("onions"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
            newIngredient = Instantiate(Resources.Load("onions"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
                
        }
        
        if(_nextIngredient == "tomato")
        {
        
            //_currentIngredient = Instantiate(Resources.Load("tomato"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
            newIngredient = Instantiate(Resources.Load("tomato"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
        }

        if(_nextIngredient == "veggiePatty")
        {
        
            //_currentIngredient = Instantiate(Resources.Load("veggiePatty"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
            newIngredient = Instantiate(Resources.Load("veggiePatty"), new Vector3((float)-0.71,(float)2.213,(float)-8.418), Quaternion.identity) as GameObject;
        }

        if (newIngredient != null)
        {
            //adding rigidbody components because otherwise the spawn_ingredients script will add physics to the
            //ingredients that get spawned in OrderMenu
            Rigidbody ingredientRigidbody = newIngredient.GetComponent<Rigidbody>();
            Destroy(newIngredient.GetComponent<IngredientInteraction>());
            
            if(ingredientRigidbody != null)
            {
                ingredientRigidbody.isKinematic = !enableRigidbody;
            }

            _currentIngredient = newIngredient;
            
            spawnedIngredients.Add(newIngredient);

            string ingredientKey = newIngredient.name;

            if (ingredientCount.ContainsKey(ingredientKey))
            {
                //[_nextIngredient] = _currentIngredient;
                ingredientCount[ingredientKey]++;
            }
            else
            {
                ingredientCount[ingredientKey] = 1;
            }

            

            
        }
    }
        

    private void PrintIngredientCounts()
    {
        Dictionary<string, int> onlyOnMenu = ingredientCount.Where(pair => pair.Value > 0).ToDictionary(pair => pair.Key, pair => pair.Value);
        Debug.Log("OrderMenu ingredients listed right now: ");
        foreach (var pair in onlyOnMenu)
        {
            Debug.Log(pair.Key + ": " + pair.Value);
        }
    }

    private bool AllIngredientsSpawned()
    {
        if (!allIngredientsSpawned)
        {
            // Logic to determine if all ingredients are spawned
            // For instance, assuming all ingredients are spawned after a certain time delay:
            allIngredientsSpawned = Time.timeSinceLevelLoad > 1.0f; 
        }
        
        return allIngredientsSpawned;
    }

    public void ResetIngredients()
    {
        ingredientCount.Clear();

        DestroySpawnedIngredients();

        foreach(string ingredient in _ingredients)
        {
            ingredientCount[ingredient] = 0;
        }

        _currentIngredient = null;

        ChooseNextIngredient();

        PrintIngredientCounts();
        
    }

    private void DestroySpawnedIngredients()
    {
        foreach(GameObject ingredient in spawnedIngredients)
        {
            Destroy(ingredient);
        }

        spawnedIngredients.Clear();
    }

}
