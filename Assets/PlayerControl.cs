using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;      
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveUp = KeyCode.W;      
    public KeyCode moveDown = KeyCode.S;  
    public KeyCode pow = KeyCode.Space;    
    private float speed = 4f;             
    private Rigidbody2D rb2d;              
    public float boundX = 4.6f;            
    public float boundY = 2.6f;            
    float timer;
    float delay = 0.5f;
    public GameObject Bala;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(-3f, 0f, transform.position.z);
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        var vel = rb2d.linearVelocity;                // Acessa a velocidade da raquete
        if (Input.GetKey(moveRight)) {            
            vel.x = speed;
        }else if (Input.GetKey(moveLeft)) {      
            vel.x = -speed;                    
        }else if (Input.GetKey(moveUp)) {            
            vel.y = speed;
        }else if (Input.GetKey(moveDown)){      
            vel.y = -speed;                    
        }else{
            vel.x = 0;     
            vel.y = 0;                   // Velociade para manter a raquete parada
        }
        rb2d.linearVelocity = vel;                    // Atualizada a velocidade da raquete
        var pos = transform.position;           // Acessa a Posição da raquete
        if (pos.x > boundX) {                  
            pos.x = boundX;                     // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        }
        else if (pos.x < -boundX) {
            pos.x = -boundX;                    // Corrige a posicao da raquete caso ele ultrapasse o limite inferior
        }
        if (pos.y > boundY) {                  
            pos.y = boundY;                     // Corrige a posicao da raquete caso ele ultrapasse o limite superior
        }
        else if (pos.y < -boundY) {
            pos.y = -boundY;                    // Corrige a posicao da raquete caso ele ultrapasse o limite inferior
        }
        transform.position = pos;

        if (Input.GetKey(pow)) {      
            if(timer >= delay){
                Instantiate(Bala, new Vector3(pos.x, pos.y, 0), Quaternion.Euler(0f, 0f, 270f));
                timer = 0f;
            }  
        }
    }

    void OnTriggerEnter2D (Collider2D coll){
        if (coll.CompareTag("Boss") || (coll.CompareTag("EnemyBullet"))){
            GameManager.vida -= 1;
            if(GameManager.vida == 0){
                FindFirstObjectByType<GameManager>().perde();
            }
        }
    }



}
