using UnityEngine;
using System.Collections;
public class BossControl : MonoBehaviour
{
    public float speed; 
    public static float multiplier = 1;            
    private Rigidbody2D rb2d;               
    private Animator animator;
    bool isAttacking = false;
    bool isDeath = false;
    int vida;
    public Sprite spriteMorte;
    void Start()
    {
        vida = 30;
        speed = 2f;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        inicio();
        StartCoroutine(AttackRoutine());
    }

    public void inicio(){
        transform.position = new Vector3(4f, 5f, transform.position.z);
        var vel = rb2d.linearVelocity;
        vel.x = -speed;
        vel.y = -speed;
        rb2d.linearVelocity = vel;
    }

    void OnTriggerEnter2D (Collider2D coll){
        if (coll.CompareTag("Bullet")){
            Destroy(coll.gameObject);
            vida--;
            if(vida <= 0){
                Morrer();
            }
        }
    }

    public void StartCharging()
    {
        animator.SetBool("isCharging", true);
    }

    public void StopCharging()
    {
        animator.SetBool("isCharging", false);
    }

    public void StartAttack1(){
        animator.SetBool("attack1", true);
    }
    public void StopAttack1(){
        animator.SetBool("attack1", false);
    }

    public void Movimento()
    {
        var pos = transform.position;
        var vel = rb2d.linearVelocity;
        var sp = speed*multiplier;
        if(pos.y > 0.7f)
        {
            vel.y = -sp;
        }
        else if(pos.y < -0.7f)
        {
            vel.y = sp;
        }
        if(pos.x < 1.5f)
        {
            vel.x = sp;
        }
        else if(pos.x > 3.2f)
        {
            vel.x = -sp;
        }
        rb2d.linearVelocity = vel;
    }

    public void zerarSpeed(){
        var vel = rb2d.linearVelocity;
        vel.x = 0;
        vel.y = 0;
        rb2d.linearVelocity = vel;
    }

    IEnumerator Ataque1()
    {
        StartAttack1();
        Quaternion alvo = Quaternion.Euler(0f, 0f, 90f);
        while (Quaternion.Angle(transform.rotation, alvo) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                alvo,
                180f * Time.deltaTime
            );
            yield return null;
        }
        while (transform.position.x > -8)
        {
            rb2d.linearVelocity = new Vector2(-5f * multiplier, rb2d.linearVelocity.y);
            yield return null;
        }
        transform.position = new Vector3(8, transform.position.y, transform.position.z);
        while (transform.position.x > 3.2)
        {
            rb2d.linearVelocity = new Vector2(-5f * multiplier, rb2d.linearVelocity.y);
            yield return null;
        }
        rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y);
        Quaternion alvo2 = Quaternion.Euler(0f, 0f, 0f);
        while (Quaternion.Angle(transform.rotation, alvo2) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                alvo2,
                180f * Time.deltaTime
            );
            yield return null;
        }
        StopCharging();
        StopAttack1();
    }

    IEnumerator AttackRoutine(){
        while(!isDeath) // depois colocar um for como a vida
        {
            float tempo = Random.Range(5f, 7f);
            yield return new WaitForSeconds(tempo);
            isAttacking = true;
            zerarSpeed();
            StartCharging();
            yield return new WaitForSeconds(2f);
            yield return StartCoroutine(Ataque1());
            isAttacking = false;
            rb2d.linearVelocity = new Vector2(speed, speed);
        }
    }

    public void Morrer()
    {
        isDeath = true;
        animator.enabled = false;
        StopAllCoroutines();
        GetComponent<SpriteRenderer>().sprite = spriteMorte;
        rb2d.linearVelocity = new Vector2(0, -3f);
        GetComponent<Collider2D>().enabled = false;
    }

    void FixedUpdate()
    {
        if(!isAttacking && !isDeath)
        {
            Movimento();
        }
    }

    void Update()
    {
        if(transform.position.y < -5f)
        {
            FindFirstObjectByType<GameManager>().ganha();
            Destroy(gameObject);
        }
    }
}
