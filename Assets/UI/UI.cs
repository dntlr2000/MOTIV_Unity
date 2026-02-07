using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Slider HP_bar;               //enemy HP바
    public GameObject HP_image;         //enemy 남은 생명 이미지
    public GameObject Enemy;            //enemy
    public GameObject Player;           //player
    public TextMeshProUGUI player_life; //player 남은 생명(텍스트)
    public GameObject damage_effect_image;   //damage_effect_image
    public GameObject enemy_die_effect_image;//enemy_die_effect_image
    public GameObject Menu;             //Menu
    public GameObject End_UI;           //End_UI

    private int Player_life;
    private int Enemy_life;

    float p_effect_index;   //플레이어 피격시 체력 비례 이펙트 정도
    float e_effect_index;   //enemy 사망시 남은 life 비례 이펙트 정도

    // Start is called before the first frame update
    void Start()
    {
        Player_life = Player.GetComponent<CharacterLife>().life;
        Enemy_life = 0;
        HP_image.transform.GetChild(0).gameObject.SetActive(false);
        End_UI = GameObject.Find("Canvas").transform.Find("End_UI").gameObject;
    }

    void Change_Eneny_life()//enemy체력 수정 반영
    {
        //표기된 체력보다 실제 체력이 낮아질 때
        if (Enemy_life > Enemy.GetComponent<Enemy_Health>().life)
        {
            e_effect_index = 1 - Enemy_life * 0.25f;
            StartCoroutine(Enemy_die(e_effect_index));
            if (Enemy_life <= 1)
            {
                Enemy_life -= 1;
            }
            else if (Enemy_life == 2)
            {
                Enemy_life -= 1;
                HP_image.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                Enemy_life -= 1;
                Destroy(HP_image.transform.GetChild(0).gameObject);
            }
        }

        //표기된 체력보다 실제 체력이 높을 때
        if (Enemy_life < Enemy.GetComponent<Enemy_Health>().life)
        {
            if (Enemy_life == 0)
            {
                Enemy_life += 1;
            }
            else if (Enemy_life == 1)
            {
                Enemy_life += 1;
                HP_image.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                Enemy_life += 1;
                Instantiate(HP_image.transform.GetChild(0), HP_image.transform.GetChild(0).position, Quaternion.identity, HP_image.transform);
                HP_image.transform.GetChild(0).Translate(-45, 0, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        //메뉴 열기
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Menu.SetActive(true);
        }

        //플레이어 현재 체력 받아오기
        if (Player_life != Player.GetComponent<CharacterLife>().currentLife)
        {
            Player_life = Player.GetComponent<CharacterLife>().currentLife;
            if (Player_life == 0)    //플레이어 사망시
            {
                End_UI.gameObject.SetActive(true);
                damage_effect_image = End_UI.transform.Find("End_effect_Image").gameObject;
                End_UI.transform.Find("YOU_DIED").gameObject.SetActive(true);
                Time.timeScale = 0;
                End_UI.transform.Find("End_effect_Image").gameObject.gameObject.SetActive(true);
            }
            p_effect_index = 1 - Player_life * 0.25f;
            StartCoroutine(Hit_detectiony(p_effect_index));
        }

        player_life.text = Player_life.ToString();//플레이어 현재 체력 반영

        if (Enemy.gameObject == null)
        {
            End_UI.gameObject.SetActive(true);
            enemy_die_effect_image = End_UI.transform.Find("End_effect_Image").gameObject;
            End_UI.transform.Find("YOU_WIN").gameObject.SetActive(true);
            Time.timeScale = 0;
            End_UI.transform.Find("End_effect_Image").gameObject.gameObject.SetActive(true);
            StartCoroutine(Enemy_die(1));
        }
        HP_bar.maxValue = Enemy.GetComponent<Enemy_Health>().maxHealth;     //enemy 최대 체력 불러오기
        HP_bar.value = Enemy.GetComponent<Enemy_Health>().currentHealth;    //enemy 현재 체력 불러오기

        //enemy 남은 생명 받아오기
        if (Enemy_life != Enemy.GetComponent<Enemy_Health>().life)
        {
            Change_Eneny_life();
        }
    }

    IEnumerator Enemy_die(float i)
    {
        enemy_die_effect_image.GetComponent<Image>().color = new Color(0, 1, 0.7f, i);
        yield return new WaitForSecondsRealtime(0.02f);
        if (i <= 0)
        {
            End_UI.transform.Find("End_effect_Image").gameObject.gameObject.SetActive(false);
        }
        else
        {
            i -= 0.01f;
            StartCoroutine(Enemy_die(i));
        }
    }

    IEnumerator Hit_detectiony(float i) //피해 효과
    {
        damage_effect_image.GetComponent<Image>().color = new Color(1, 0, 0, i);
        yield return new WaitForSecondsRealtime(0.02f);
        if (i <= 0)
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            End_UI.transform.Find("End_effect_Image").gameObject.gameObject.SetActive(false);
        }
        else
        {
            i -= 0.01f;
            StartCoroutine(Hit_detectiony(i));
        }
    }
}
