using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public SnakeMovement follow;
    public SnakeMovement tail;

    public GameObject bodyPrefab;
    public GameManager gm;

    public int score = 0;
    public float speed;
    public float turnAngle;
    bool turnLeft = false;
    bool turnRight = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        GetComponent<Renderer>().material = gm.bodyMaterials[Random.Range(0, gm.bodyMaterials.Length)];

    }

    // Update is called once per frame
    void Update()
    {
        if (follow == null)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

            GetInput();
            if (turnLeft)
                RotateDirection(-turnAngle);
            if (turnRight)
                RotateDirection(turnAngle);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, follow.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, follow.transform.rotation, speed * Time.deltaTime);
        }

        //fallen off the edge
        if (transform.position.y < -100)
        {
            gm.Die();
        }
    }

    void RotateDirection(float angle)
    {
        transform.Rotate(new Vector3(0, angle, 0) * Time.deltaTime);
    }

    void GetInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            turnLeft = true;
        else
            turnLeft = false;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            turnRight = true;
        else
            turnRight = false;

        if (Input.GetKey(KeyCode.S))
            AddTail();
    }

    void AddTail()
    {
        SnakeMovement newTail = Instantiate(bodyPrefab).GetComponent<SnakeMovement>();

        newTail.transform.position = tail.transform.position - tail.transform.forward;
        if(score < 1)
        {
            Destroy(newTail.GetComponent<SphereCollider>());
        }

        newTail.follow = tail;
        tail = newTail;
        score++;

    }

    void OnCollisionEnter(Collision collision)
    {
        //Output the Collider's GameObject's name 
        Debug.Log(collision.collider.name);
        if(collision.collider.name == "Brush(Clone)")
        {
            Destroy(collision.collider.gameObject);
            AddTail();
            gm.SpawnFood();
        }
        if(collision.collider.name == "SnakeBody(Clone)")
        {
            gm.Die();
        }
    }
}
