using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit_con : MonoBehaviour
{

    Rigidbody2D rb2d;
    bool can_con = true;
    Transform tf;
    int number;
    int scale;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(can_con)
        {
            tf.position = new Vector2(Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -3.4f, 3.4f), 4.2f);
        }

        if(Input.GetMouseButtonDown(0))
        {
            rb2d.simulated = true;
            can_con = false;
        }
    }

    public void Create(int number_of_fruit, int scale_of_fruit)
    {
        number = number_of_fruit;
        scale = scale_of_fruit;
        Scale_Change(scale);
    }

    void Scale_Change(int next_scale)
    {
        float scale_float = next_scale * 0.25f + 0.25f;
        transform.localScale = new Vector2(scale_float,scale_float);
    }
}

