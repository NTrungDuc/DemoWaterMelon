using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class spawnPos : MonoBehaviour
{
    private static spawnPos instance;
    public static spawnPos Instance { get=> instance; }
    [SerializeField] public List<GameObject> fruitPrefabs;
    [SerializeField] private GameObject fruits;
    public Vector3 newPos;
    public bool hasSpawned=false;
    public Vector3 spawnPosition;
    public int index;
    public bool replaceFr=false;
    //ui
    public Text txtWatermelon;
    public Text txtScore;
    public int scoreIndex;
    int Score;
    int watermelon;
    //sound
    public AudioSource soundCol;
    public AudioSource soundMerge;
    private void Awake()
    {
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnFruits(0f));
        newPos = transform.position;
    }

    public void getMouseInputPos()
    {
        if (Input.GetMouseButtonDown(0) && !hasSpawned)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            newPos = worldPosition;
            //transform.position = new Vector3(worldPosition.x, transform.position.y, 0);
            StartCoroutine(spawnFruits(2f));
        }
    }
    public void replaceFruit()
    {
        if (replaceFr)
        {
            replaceFr = false;
            Score += scoreIndex;
            txtScore.text = Score.ToString();
            if (index < fruitPrefabs.Count)
            {
                GameObject newFruit = fruitPrefabs[index];
                GameObject newFruitReplace = Instantiate(newFruit, spawnPosition, newFruit.transform.rotation);
                newFruitReplace.transform.parent = fruits.transform;
                StartCoroutine(newFruitReplace.GetComponent<Fruits>().setFall());
            }
            else
            {
                watermelon += 1;
                txtWatermelon.text = watermelon.ToString();
            }
        }
    }
    public IEnumerator spawnFruits(float timeSpawn)
    {
        int randomIndex = Random.Range(0, fruitPrefabs.Count);
        //Vector3 fruitPos = new Vector3(newPos.x, transform.position.y, 0);
        GameObject objPrefab = fruitPrefabs[randomIndex];
        hasSpawned = true;
        yield return new WaitForSeconds(timeSpawn);
        hasSpawned = false;
        GameObject newFruit = Instantiate(objPrefab, transform.position, objPrefab.transform.rotation);
        newFruit.transform.parent = fruits.transform;
    }
}
