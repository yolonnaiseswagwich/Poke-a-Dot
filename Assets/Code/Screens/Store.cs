using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Store : MonoBehaviour
{
    Material Background;
    List<UI.Image> Buttons;
    AudioSource a;
    protected float Timer;
    protected Color Cl1;
    protected Color Cl2;
    UI.CircleButton Selected;
    // Use this for initialization
    void Start()
    {
        a = GetComponent<AudioSource>();
        a.volume = GameGlobals.SoundVolume * 0.01f;
        a.loop = false;
        a.Pause();
        Timer = 0;
        Time.timeScale = 1.0f;
        Screen.orientation = ScreenOrientation.Portrait;
        IDrag.D2Camera.Calibrate();
        IDrag.D2Camera.Update(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        ColorLib.Init();
        ShaderLib.Init();
        SpriteLib.Init();
        SoundLib.Init();
        Cl1 = GameGlobals.WhiteGray;
        Cl2 = GameGlobals.Red;
        float Width = 0.16f;
        float Height = 0.08f;
        Background = ShaderLib.GetShader(ShaderLib.Blank);
        Background.color = GameGlobals.BackGround;
        Buttons = new List<UI.Image>();
        UI.CircleButton Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.125f, Screen.height * 0.915f, (int)(Screen.width * 0.2f), (int)(Screen.height * 0.15f), "Menu");
        Temp.SetTex(SpriteLib.Home);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Selected = new UI.CircleButton();
        Selected.Init(Screen.width * 0.5f, Screen.height * 0.865f, (int)(Screen.width * 0.4f), (int)(Screen.height * 0.25f), "Selected");
        switch (GameGlobals.TexNum)
        {
            case 0:
                Selected.SetTex(SpriteLib.Circle0);
                break;
            case 1:
                Selected.SetTex(SpriteLib.Circle1);
                break;
            case 2:
                Selected.SetTex(SpriteLib.Circle2);
                break;
            case 3:
                Selected.SetTex(SpriteLib.Circle3);
                break;
            case 4:
                Selected.SetTex(SpriteLib.Circle4);
                break;
            case 5:
                Selected.SetTex(SpriteLib.Circle5);
                break;
            case 6:
                Selected.SetTex(SpriteLib.Circle6);
                break;
            case 7:
                Selected.SetTex(SpriteLib.Circle7);
                break;
            case 8:
                Selected.SetTex(SpriteLib.Circle8);
                break;
        }
        Selected.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Selected);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.15f, Screen.height * 0.68f, (int)(Screen.width * Width), (int)(Screen.height * Height), "0");
        Temp.SetTex(SpriteLib.Circle0);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.325f, Screen.height * 0.68f, (int)(Screen.width * Width), (int)(Screen.height * Height), "1");
        Temp.SetTex(SpriteLib.Circle1);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.5f, Screen.height * 0.68f, (int)(Screen.width * Width), (int)(Screen.height * Height), "2");
        Temp.SetTex(SpriteLib.Circle2);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.675f, Screen.height * 0.68f, (int)(Screen.width * Width), (int)(Screen.height * Height), "3");
        Temp.SetTex(SpriteLib.Circle3);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.85f, Screen.height * 0.68f, (int)(Screen.width * Width), (int)(Screen.height * Height), "4");
        Temp.SetTex(SpriteLib.Circle4);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.15f, Screen.height * 0.58f, (int)(Screen.width * Width), (int)(Screen.height * Height), "5");
        Temp.SetTex(SpriteLib.Circle5);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.325f, Screen.height * 0.58f, (int)(Screen.width * Width), (int)(Screen.height * Height), "6");
        Temp.SetTex(SpriteLib.Circle6);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.5f, Screen.height * 0.58f, (int)(Screen.width * Width), (int)(Screen.height * Height), "7");
        Temp.SetTex(SpriteLib.Circle7);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);

    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        switch ((int)Timer % 7)
        {
            case 0:
                Cl1 = GameGlobals.WhiteGray;
                Cl2 = GameGlobals.Red;
                break;
            case 1:
                Cl1 = GameGlobals.Red;
                Cl2 = GameGlobals.Yellow;
                break;
            case 2:
                Cl1 = GameGlobals.Yellow;
                Cl2 = GameGlobals.Green;
                break;
            case 3:
                Cl1 = GameGlobals.Green;
                Cl2 = GameGlobals.Blue;
                break;
            case 4:
                Cl1 = GameGlobals.Blue;
                Cl2 = GameGlobals.Purple;
                break;
            case 5:
                Cl1 = GameGlobals.Purple;
                Cl2 = GameGlobals.Brown;
                break;
            case 6:
                Cl1 = GameGlobals.Brown;
                Cl2 = GameGlobals.WhiteGray;
                break;
        }
        foreach (UI.Image i in Buttons)
        {
            if (i.Update())
            {
                a.clip = i.GetClip();
                a.Play();
                switch (i.GetName())
                {
                    case "Menu":
#if !UNITY_WEB
                        SaveLoadLib.Save();
#endif
                        GameInfo.GameType = GameInfo.Menu;
                        SceneManager.LoadScene(1);
                        break;
                    case "0":
                        GameGlobals.TexNum = 0;
                        Selected.SetTex(SpriteLib.Circle0);
                        break;
                    case "1":
                        GameGlobals.TexNum = 1;
                        Selected.SetTex(SpriteLib.Circle1);
                        break;
                    case "2":
                        GameGlobals.TexNum = 2;
                        Selected.SetTex(SpriteLib.Circle2);
                        break;
                    case "3":
                        GameGlobals.TexNum = 3;
                        Selected.SetTex(SpriteLib.Circle3);
                        break;
                    case "4":
                        GameGlobals.TexNum = 4;
                        Selected.SetTex(SpriteLib.Circle4);
                        break;
                    case "5":
                        GameGlobals.TexNum = 5;
                        Selected.SetTex(SpriteLib.Circle5);
                        break;
                    case "6":
                        GameGlobals.TexNum = 6;
                        Selected.SetTex(SpriteLib.Circle6);
                        break;
                    case "7":
                        GameGlobals.TexNum = 7;
                        Selected.SetTex(SpriteLib.Circle7);
                        break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if !UNITY_WEB
            SaveLoadLib.Save();
#endif
            GameInfo.GameType = GameInfo.Menu;
            SceneManager.LoadScene(1);
        }
    }
    void OnPostRender()
    {
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
            if (i.GetName() != "Menu")
            {
                i.Draw(Color.Lerp(Cl1, Cl2, (Timer - (int)Timer) * 2.0f));
                //switch ((int)Timer % 7)
                //{
                //    case 0:
                //        i.Draw(GameGlobals.Red);
                //        break;
                //    case 1:
                //        i.Draw(GameGlobals.Yellow);
                //        break;
                //    case 2:
                //        i.Draw(GameGlobals.Green);
                //        break;
                //    case 3:
                //        i.Draw(GameGlobals.Blue);
                //        break;
                //    case 4:
                //        i.Draw(GameGlobals.Purple);
                //        break;
                //    case 5:
                //        i.Draw(GameGlobals.Brown);
                //        break;
                //    case 6:
                //        i.Draw(GameGlobals.WhiteGray);
                //        break;
                //}
            }
            else
            {
                i.Draw(new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b));
            }
        }
    }
}
