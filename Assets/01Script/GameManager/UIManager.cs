using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    Transform canvasTrans;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI jamText;
    TextMeshProUGUI bombText;
    [SerializeField]
    Image[] hearting;

    Transform tr;

    TextMeshProUGUI ScoreText
    {
        get
        {
            if (scoreText == null)
            {
                tr = MyUtility.FindChildRecursive(canvasTrans, "ScoreText");
                if (tr != null)
                    scoreText = tr.GetComponent<TextMeshProUGUI>();
                //scoreText = canvasTrans.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            }
            return scoreText;
        }
    }
    TextMeshProUGUI JamText
    {
        get
        {
            if (jamText == null)
            {
                tr = MyUtility.FindChildRecursive(canvasTrans, "JamText");
                if (tr != null)
                    jamText = tr.GetComponent<TextMeshProUGUI>();
            }
            return jamText;
        }
    }
    TextMeshProUGUI BombText
    {
        get
        {
            if (bombText == null)
            {
                tr = MyUtility.FindChildRecursive(canvasTrans, "BombText");
                if (tr != null)
                    bombText = tr.GetComponent<TextMeshProUGUI>();
            }
            return bombText;
        }
    }

    private void Awake()
    {
        GameObject obj = GameObject.Find("Canvas");
        canvasTrans = obj.GetComponent<Transform>();

        //canvasTrans.Find("ScoreText");
        // GameObject obj = GameObject.Find("ScoreText"); 한줄로 해도 되지만 효율이 나쁘니.
    }

    private void OnEnable()
    {
        ScoreManager.OnChangeScore += UpdateScoreText;
        ScoreManager.OnChangeBomb += UpdateBobmText;
        ScoreManager.OnChangeJamCount += UpdateJamText;
        ScoreManager.OnChangeHP += UpdatePlayerHP;
    }

    private void OnDisable()
    {
        ScoreManager.OnChangeScore -= UpdateScoreText;
        ScoreManager.OnChangeBomb -= UpdateBobmText;
        ScoreManager.OnChangeJamCount -= UpdateJamText;
        ScoreManager.OnChangeHP -= UpdatePlayerHP;
    }

    private void UpdateScoreText(int score)
    {
        if (score < 10)
            ScoreText.text = "SCORE : 0000000" + score.ToString();
        else if (score < 100)
            ScoreText.text = "SCORE : 000000" + score.ToString();
        else if (score < 1000)
            ScoreText.text = "SCORE : 00000" + score.ToString();
        else if (score < 10000)
            ScoreText.text = "SCORE : 0000" + score.ToString();
        else if (score < 100000)
            ScoreText.text = "SCORE : 000" + score.ToString();
        else if (score < 1000000)
            ScoreText.text = "SCORE : 00" + score.ToString();
        else if (score < 10000000)
            ScoreText.text = "SCORE : 0" + score.ToString();
        else
            ScoreText.text = "SCORE : " + score.ToString();

    }

    private void UpdateBobmText(int score)
    {
        BombText.text = "X " + score.ToString();
    }

    private void UpdateJamText(int score)
    {
        JamText.text = "X " + score.ToString();
    }

    private void UpdatePlayerHP(int score)
    {
        for(int i = 0; i < 5; i++)
        {
            if(i < score)
                hearting[i].enabled = true;
            else
                hearting[i].enabled = false;
        }
    }
}


