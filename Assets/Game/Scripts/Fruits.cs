using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    public int id = 0;
    public int score = 0;
    [SerializeField] public Rigidbody2D rb;
    public bool fall = false;
    spawnPos spawnPosFruit;
    // Start is called before the first frame update
    void Start()
    {
        //rb=GetComponent<Rigidbody2D>();
        spawnPosFruit = GameObject.FindGameObjectWithTag("spawnPos").GetComponent<spawnPos>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !fall) {
            fall = true;
            //Debug.Log("roi"+ spawnPosFruit.newPos.x);
            transform.position = new Vector3(spawnPosFruit.newPos.x, transform.position.y, 0);
            rb.gravityScale = 1;
        }
    }
    public IEnumerator setFall()
    {
        yield return new WaitForSeconds(0.1f);
        rb.gravityScale = 1;
        fall = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="fruit")
        {
            //Debug.Log("va cham");
            if (id == collision.gameObject.GetComponent<Fruits>().id)
            {
                Debug.Log("merge");
                spawnPosFruit.spawnPosition=transform.position;
                spawnPosFruit.index = id + 1;
                spawnPosFruit.scoreIndex = score;
                spawnPosFruit.replaceFr = true;
                Destroy(gameObject);           
            }
        }
    }
}
