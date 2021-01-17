using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Space : MonoBehaviour
{
    private int hp;
    private int score;
    [SerializeField] TextMeshProUGUI tmScore;
    [SerializeField] TextMeshProUGUI tmHP;
    Ship ship;

    private void Awake()
    {
        int numberOfSpaceObject = FindObjectsOfType<Space>().Length;
        if(numberOfSpaceObject>1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }
    private void Start()
    {
        ship = FindObjectOfType<Ship>();
        score = 0;
        hp = ship.GetHp();
        UpdateScoreText();
        UpdateHPText();
    }

    private void UpdateScoreText()
    {
        tmScore.text = "SCORE : " + score;
    }

    public void GetScore(int additional_score)
    {
        score += additional_score;
        UpdateScoreText();
        if(score==500)
        {
            FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>().ChangeNextScene();
            ship.StopFireRoutine();
        }
    }

    public void GetHP(int dmg)
    {
        hp -= dmg;
        UpdateHPText();
    }
    private void UpdateHPText()
    {
        tmHP.text = "HP : " + hp;
    }
    public void DestroyThisSpace()
    {
        Destroy(gameObject);
    }

    public int GetScoreToEnd()
    {
        return score;
    }
}
