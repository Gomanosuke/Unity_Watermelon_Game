using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit_con : MonoBehaviour
{

    Rigidbody2D rb2d;
    public SpriteRenderer spriterenderer;
    bool can_con = true;
    Transform tf;
    public int number;
    public int scale;
    public Sprite[] sprite_list = new Sprite[12];
    float overtime;
    bool over = false;
    master master;

    bool died = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        master = GameObject.FindWithTag("Player").GetComponent<master>();
    }

    // Update is called once per frame
    void Update()
    {
        if(can_con)
        {
            tf.position = new Vector2(Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -3f, 3f), 4.1f);
        }

        if (Input.GetMouseButtonDown(0) && can_con == true)
        {
            rb2d.simulated = true;
            can_con = false;
        }

        if(overtime >= 0.49f)
        {
            master.Gameover();
        }

        if(over)
        {
            overtime += Time.deltaTime;
            if(overtime >= 0.49f)
            {
                master.Gameover();
            }
        }
    }

    public void Create(int number_of_fruit, int scale_of_fruit, bool con = false)
    {
        number = number_of_fruit;
        scale = scale_of_fruit;
        Scale_Change(scale);
        if(con)
        {
            GetComponent<Rigidbody2D>().simulated = true;
            can_con = false;
        }
    }

    void Scale_Change(int next_scale)
    {
        float scale_float = next_scale * 0.2f + 0.22f;
        transform.localScale = new Vector2(scale_float,scale_float);
        spriterenderer.sprite = sprite_list[next_scale - 1];
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "over")
        {
            over = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "over")
        {
            overtime = 0;
            over = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="fruit")
        {
            fruit_con partner_con = collision.gameObject.GetComponent<fruit_con>();
            if (partner_con.scale == scale && scale != 12)
            {
                if(partner_con.number > number)
                {
                    StartCoroutine(destroyer(collision.gameObject.transform.position));
                }else
                {
                    StartCoroutine(Bigger());
                }
            }else if(partner_con.scale == 12 && scale == 12)
            {
                StartCoroutine(Bigger());
            }
        }
    }

    IEnumerator destroyer(Vector3 partner_posi)
    {
        yield return null;
        if(died)
        {
            master.Score_add(scale * scale, scale, (partner_posi + gameObject.transform.position) / 2);
        }
        died = false;
        Destroy(gameObject);

    }

    IEnumerator Bigger()
    {
        yield return null;
        died = false;
        Destroy(gameObject);
    }
}

