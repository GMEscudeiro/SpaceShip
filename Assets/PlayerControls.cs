using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode shoot = KeyCode.Space;

    int lives = 3;

    public float speed = 3.0f;
    public float boundX = 4.0f;
    public float boundY = 3.0f;
    
    private Rigidbody2D rb2d;

    public GameObject bullet;

    void Start()
    {
       rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var vel = rb2d.linearVelocity;

        if(Input.GetKey(moveUp)){
            vel.y = speed;
        }
        else if(Input.GetKey(moveDown)){
            vel.y = -speed;
        }
        else{
            vel.y = 0;
        }
        if(Input.GetKey(moveRight)){
            vel.x = speed;
        }
        else if(Input.GetKey(moveLeft)){
            vel.x = -speed;
        }
        else{
            vel.x = 0;
        }
        rb2d.linearVelocity = vel;

        var pos = transform.position;
        if(pos.y > boundY){
            pos.y = -boundY;
        }
        if(pos.y < -boundY){
            pos.y = boundY;
        }
        if(pos.x > boundX){
            pos.x = -boundX;
        }
        if(pos.x < -boundX){
            pos.x = boundX;
        }
        transform.position = pos;

        if(Input.GetKeyDown(shoot)){
            Shoot();
        }
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (coll.collider.CompareTag("Enemy")){
            Destroy(coll.gameObject);
            lives--;
        }
        if(lives <= 0){
            // SceneManager.LoadScene("BadEnding");
            Debug.Log("PERDEU");
        }
    }

    void Shoot(){
        var pos = transform.position;
        GameObject bulletInstance = Instantiate(bullet, new Vector3(pos.x + 0.5f, pos.y, 0), Quaternion.identity);
        var bulletRb2d = bulletInstance.GetComponent<Rigidbody2D>();
        var vel = bulletRb2d.linearVelocity;
        vel.x = 5.0f;
        bulletRb2d.linearVelocity = vel;
    }
}
