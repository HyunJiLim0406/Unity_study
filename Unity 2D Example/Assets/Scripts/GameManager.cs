using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerMove player;
    public GameObject[] Stages;
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject UIRestartBtn;
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;

    private void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }
    public void NextStage()
    {
        if (stageIndex < Stages.Length-1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();
            UIStage.text = "STAGE " + (stageIndex + 1);
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("Clear!");
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Clear!";
            UIRestartBtn.SetActive(true);
        }
        totalPoint += stagePoint;
        stagePoint = 0;
    }
    
    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            player.OnDie();
            UIRestartBtn.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (health > 1)
            {
                PlayerReposition();
            }
            HealthDown();
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0.5f, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
