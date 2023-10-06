using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using unityroom.Api;
public class master : MonoBehaviour
{

    public GameObject fruit_prefab;

    public GameObject next_fruit_obj;
    SpriteRenderer next_fruit_sprite;
    
    int number_of_fruit = 0;
    int next_fruit;
    public Sprite[] sprite_list = new Sprite[12];

    bool can_con = true;//ŽŸ‚Ìfruit‚Ü‚¿

    int score_int;
    public Text score_text;

    public GameObject gameover_ui;
    public Text last_score;

    bool gameover = false;

    // Start is called before the first frame update
    void Start()
    {
        next_fruit_sprite = next_fruit_obj.GetComponent<SpriteRenderer>();
        can_con = false;
        Invoke(nameof(Lottery), 0.5f);
        next_fruit = Random.Range(1, 5);
        Next_view(next_fruit);
        number_of_fruit++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && can_con == true)
        {
            can_con = false;
            Invoke(nameof(Lottery), 0.5f);
            number_of_fruit++;
        }

        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(0);
        }

        transform.position = new Vector2(Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -3f, 3f), 4.1f);
    }

    void Lottery()
    {
        can_con = true;
        GameObject fruit = Instantiate(fruit_prefab, transform.position, transform.rotation);
        fruit_con controller = fruit.GetComponent<fruit_con>();
        controller.Create(number_of_fruit, next_fruit);
        next_fruit = Random.Range(1, 6);
        Next_view(next_fruit);
    }

    public void Gameover()
    {
        if(gameover == false)
        {
            can_con = false;
            gameover_ui.SetActive(true);
            last_score.text = "score : " + score_int.ToString("0000");

            GameObject[] fruit_obj = GameObject.FindGameObjectsWithTag("fruit");

            for (int i = 0; i < fruit_obj.Length; ++i)
                fruit_obj[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            UnityroomApiClient.Instance.SendScore(1, score_int, ScoreboardWriteMode.HighScoreDesc);

        }
        gameover = true;
    }

    public void Score_add(int score)
    {
        score_int += score;
        score_text.text = score_int.ToString("0000");
    }

    void Next_view(int next_scale)
    {
        float scale_float = next_scale * 0.2f + 0.22f;
        next_fruit_obj.transform.localScale = new Vector2(scale_float, scale_float);
        next_fruit_sprite.sprite = sprite_list[next_scale - 1];
    }


}
