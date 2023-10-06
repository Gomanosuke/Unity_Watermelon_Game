using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class master : MonoBehaviour
{

    public GameObject fruit_prefab;

    public GameObject next_fruit_obj;
    next_con next_con;
    
    int number_of_fruit = 0;
    int next_fruit;

    bool can_con = true;//ŽŸ‚Ìfruit‚Ü‚¿

    int score_int;
    public Text score_text;

    public GameObject gameover_ui;
    public Text last_score;

    bool gameover = false;

    // Start is called before the first frame update
    void Start()
    {
        next_con = next_fruit_obj.GetComponent<next_con>();

        can_con = false;
        Invoke(nameof(Lottery), 0.5f);
        next_fruit = Random.Range(1, 5);
        next_con.Next_look(next_fruit);
        number_of_fruit++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && can_con == true)
        {
            can_con = false;
            Invoke(nameof(Lottery), 0.5f);
            next_fruit = Random.Range(1,5);
            next_con.Next_look(next_fruit);
            number_of_fruit++;
        }

        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(0);
        }
    }

    void Lottery()
    {
        can_con = true;
        GameObject fruit = Instantiate(fruit_prefab, transform.position, transform.rotation);
        fruit_con controller = fruit.GetComponent<fruit_con>();
        controller.Create(number_of_fruit, next_fruit);
    }

    public void Gameover()
    {
        if(gameover == false)
        {
            //can_con = false;
            gameover_ui.SetActive(true);
            last_score.text = "score : " + score_int.ToString("0000");
        }
        gameover = true;
    }

    public void Score_add(int score)
    {
        score_int += score;
        score_text.text = score_int.ToString("0000");
    }
}
