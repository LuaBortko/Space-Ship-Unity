using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    int vida = 5;
    public float speed = 1.5f; 
    public static float multiplier = 1; 
    private Rigidbody2D rb2d; 
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player"); // Busca a referência da bola
    }

    void OnTriggerEnter2D (Collider2D coll){
        if (coll.CompareTag("Bullet")){
            Destroy(coll.gameObject);
            vida--;
            if(vida <= 0){
                GameManager.pontuacao += 10;
                Destroy(gameObject);
            }
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 180f * Time.deltaTime);

        Vector2 playerPos = player.transform.position;
        Vector2 pos = transform.position;

        Vector3 dir = playerPos - pos; 
        dir.Normalize();
        Vector2 forceVec = dir * speed;
        float forceX = forceVec.x;
        float forceY = forceVec.y;
        var vel = rb2d.linearVelocity;
        vel.x = forceX * multiplier;
        vel.y = forceY * multiplier;
        rb2d.linearVelocity = vel;
    }
}
