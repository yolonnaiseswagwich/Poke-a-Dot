using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class End : MonoBehaviour
{
    Material Background;
    float Timer = 0;
    List<UI.Image> Buttons;
    protected bool InApp;
    private int type = 0;
    // Use this for initialization
    void Start ()
    {
        InApp = true;
        Timer = 0;
        Time.timeScale = 1.0f;
        Screen.orientation = ScreenOrientation.Portrait;
        IDrag.D2Camera.Calibrate();
        IDrag.D2Camera.Update(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        ColorLib.Init();
        ShaderLib.Init();
        SpriteLib.Init();
        SoundLib.Init();
        Background = ShaderLib.GetShader(ShaderLib.Blank);
        Background.color = GameGlobals.BackGround;
        if (GameInfo.DC) {
            GameGlobals.Dots += (int) (Score.m_iScore*0.2f);
        } else {
            GameGlobals.Dots += (int) (Score.m_iScore*0.05f);
        }
        SaveLoadLib.Save();
        Buttons = new List<UI.Image>();
        UI.Image Temp3;
        Temp3 = new UI.Image();
        Temp3.Init(Screen.width * 0.5f, Screen.height * 0.9425f, (int)(Screen.width * 0.6f), (int)(Screen.height * 0.25f), "PaD");
        Temp3.SetTex(SpriteLib.PaD);
        Temp3.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp3);
        UI.CircleButton Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.5f, Screen.height * 0.375f, (int)(Screen.width * 0.36f), (int)(Screen.height * 0.24f), "Play");
        Temp.SetTex(SpriteLib.Play);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.1f, Screen.height * 0.935f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "Menu");
        Temp.SetTex(SpriteLib.Home);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.1f, Screen.height * 0.06f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "Store");
        Temp.SetTex(SpriteLib.Store);
        Temp.SetShader(ShaderLib.DefaultColorTexGray);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.3f, Screen.height * 0.06f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "Achievments");
        Temp.SetTex(SpriteLib.Achievments);
        Temp.SetShader(ShaderLib.DefaultColorTexGray);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.5f, Screen.height * 0.06f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "Settings");
        Temp.SetTex(SpriteLib.Settings);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.7f, Screen.height * 0.06f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "Rate");
        Temp.SetTex(SpriteLib.Rate);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.9f, Screen.height * 0.06f, (int)(Screen.width * 0.12f), (int)(Screen.height * 0.08f), "Like");
        Temp.SetTex(SpriteLib.Like);
        Buttons.Add(Temp);
        if (GameInfo.GameType == GameInfo.Endless) {
            if (Score.m_iScore > GameGlobals.Best)
            {
                type = 2;
                GameGlobals.Best = Score.m_iScore;
                SaveLoadLib.Save();
            }
            else if (Score.m_iScore == GameGlobals.Best)
            {
                type = 1;
            }
            else
            {
                type = 0;
            }
        }
    }
    void OnApplicationPause()
    {
        InApp = false;
    }
    IEnumerator Like()
    {
        Application.OpenURL("https://dl.dropboxusercontent.com/u/117031733/ImplodingDragons.com/like.html");
        yield return new WaitForSeconds(1);
        if (!InApp)
        {
            InApp = true;
        }
        else
        {
            Application.OpenURL("https://dl.dropboxusercontent.com/u/117031733/ImplodingDragons.com/like.html");
        }
    }
    IEnumerator Rate()
    {
        Application.OpenURL("https://dl.dropboxusercontent.com/u/117031733/ImplodingDragons.com/rate.html");
        yield return new WaitForSeconds(1);
        if (!InApp)
        {
            InApp = true;
        }
        else
        {
            Application.OpenURL("https://dl.dropboxusercontent.com/u/117031733/ImplodingDragons.com/rate.html");
        }
    }
    // Update is called once per frame
    void Update ()
    {
        if (GameInfo.DC)
        {

        }
        else
        {
            if (Time.timeSinceLevelLoad > 1.0f)
            {
                foreach (UI.Image i in Buttons)
                {
                    if (i.Update())
                    {
                        switch (i.GetName())
                        {
                            case "Play":
                                SceneManager.LoadScene(GameInfo.GameSceneNo + GameInfo.GameType);
                                break;
                            case "Menu":
                                GameInfo.GameType = GameInfo.Menu;
                                SceneManager.LoadScene(1);
                                break;
                            case "Store":
                                GameInfo.GameType = GameInfo.Menu;
                                SceneManager.LoadScene(4);
                                break;
                            case "Achievments":
                                break;
                            case "Settings":
                                GameInfo.GameType = GameInfo.Menu;
                                SceneManager.LoadScene(3);
                                break;
                            case "Rate":
                            //StartCoroutine(Rate());
                            break;
                            case "Like":
                            //StartCoroutine(Like());
                            break;
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
	}

    void OnGUI()
    {
        Timer += Time.deltaTime;
        if (Timer > 1.0f)
        {
            GUI.skin.label.normal.background = null;
            GUI.skin.label.active.background = null;
            GUI.skin.label.onHover.background = null;
            GUI.skin.label.hover.background = null;
            GUI.skin.label.onFocused.background = null;
            GUI.skin.label.focused.background = null;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.normal.textColor = ColorLib.GetColor(ColorLib.WhiteGrays.White);
            GUI.skin.label.fontSize = 80;
            if (GameInfo.Speed != GameInfo.GameType)
            {
                if (type == 0)
                {
                    GUI.Label(new Rect(0, Screen.height * 0.15f, Screen.width, Screen.height * 0.4f), "Poked " + Score.m_iScore.ToString() + "\n" + "Best: " + GameGlobals.Best.ToString());
                }else if (type == 1)
                {
                    GUI.Label(new Rect(0, Screen.height * 0.15f, Screen.width, Screen.height * 0.4f), "Poked " + Score.m_iScore.ToString() + "\n" + "Same as Best!");
                } else if (type == 2) {
                    GUI.Label(new Rect(0, Screen.height * 0.15f, Screen.width, Screen.height * 0.4f), "Poked " + Score.m_iScore.ToString() + "\n"  +"New Best!");
                }
            }
            else
            {
                if (Score.m_fGameTime > 0)
                {
                    GUI.Label(new Rect(0, Screen.height * 0.25f, Screen.width, Screen.height * 0.25f), ((int)Score.m_fGameTime).ToString() + " Seconds Remaining!");
                }
                else
                {
                    GUI.Label(new Rect(0, Screen.height * 0.25f, Screen.width, Screen.height * 0.25f), "Out of Time");
                }
            }
           // GUI.Label(new Rect(0, Screen.height * 0.7625f, Screen.width, Screen.height * 0.125f), GameGlobals.Dots.ToString() + " Dots Total");
        }
    }
    void OnPostRender()
    {
     //   Vector4 Variables1 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 1.79f) * 0.02f + 0.37f, Mathf.Sin(Time.timeSinceLevelLoad * 1.22f) * 0.02f + 0.37f, 17.0f, 3.14159f);
    //    Vector4 Variables2 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 0.61f), Mathf.Cos(Time.timeSinceLevelLoad * 1.21f), Mathf.Sin(Time.timeSinceLevelLoad * 0.87f), Mathf.Cos(Time.timeSinceLevelLoad * 0.87f));
     //   Background.SetVector("MyVar", Variables1);
    //    Background.SetVector("MyVarb", Variables2);
        GL.PushMatrix();
        Background.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        //change it so that it draws in the right positon
        GL.Color(GameGlobals.BackGround);
        GL.TexCoord2(0, 0);
        GL.Vertex3(0, 0, 0.1F);
        GL.TexCoord2(0, 1);
        GL.Vertex3(0, 1, 0.1F);
        GL.TexCoord2(1, 1);
        GL.Vertex3(1, 1, 0.1F);
        GL.TexCoord2(1, 0);
        GL.Vertex3(1, 0, 0.1F);
        GL.End();
        GL.PopMatrix();
        foreach (UI.Image i in Buttons)
        {
            if (i.GetName() != "PaD")
            {
                    i.Draw(new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b));
            }
            else
            {
                Vector4 Variables3 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 1.61f), Mathf.Cos(Time.timeSinceLevelLoad * 1.21f), Mathf.Sin(Time.timeSinceLevelLoad * 1.87f), Mathf.Cos(Time.timeSinceLevelLoad * 0.83f));
                Color ColorVar = new Color(0.5f + (Mathf.Abs(Variables3.x) * 0.5f), Mathf.Abs(Variables3.y), Mathf.Abs(Variables3.z), 2.0f);
                i.DrawColor(ColorVar);
            }
        }
    }
}
