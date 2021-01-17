using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    private int score;
    [SerializeField] TextMeshProUGUI tmScore;
    // Start is called before the first frame update
    private void Awake()
    {
        var space = FindObjectOfType<Space>();
        score = space.GetComponent<Space>().GetScoreToEnd();
        space.DestroyThisSpace();
    }
    void Start()
    {
        tmScore.text = "SCORE " + score;
    }
}
