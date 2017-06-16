using UnityEngine;
using System.Collections;

public class TextChanger : MonoBehaviour {
    UnityEngine.UI.Text ThisText;
    // Use this for initialization
    void Start () {
        ThisText = GetComponent<UnityEngine.UI.Text>();
        //ThisText.material = ShaderLib.GetShader(ShaderLib.DefaultColorTex);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameGlobals.ShakeTime > 0 && (int)(GameGlobals.ShakeTime * 10.0f) % 2 == 0)
        {
            ThisText.color = ColorLib.GetColor(ColorLib.Reds.Crismon);
        }
        else
        {
            ThisText.color = new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b);
        }
        if (GameInfo.GameType == GameInfo.Speed || GameInfo.GameType == GameInfo.ColorSwitch)
        {
            ThisText.text = "Dots Left: " + (Score.m_iGoal - Score.m_iScore).ToString() + "   Current Combo:" + Score.m_iCount.ToString() + "   Time Left: " + ((int)GameGlobals.TimeLeft).ToString();
        }
        else
        {
            ThisText.text = " Dots Poked: " + Score.m_iScore.ToString() + "   Current Combo:" + Score.m_iCount.ToString() + "   Lives Left: " + GameGlobals.Lives.ToString();
        }

    }
}
