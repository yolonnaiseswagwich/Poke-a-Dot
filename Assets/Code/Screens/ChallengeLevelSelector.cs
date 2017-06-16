using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class ChallengeLevelSelector : MonoBehaviour {
    Material Background;
    List<UI.Image> Buttons;
    List<UI.Image> Buttons3;
    List<UI.Image> Buttons4;
    AudioSource a;
    int Page, MaxPage, Challengelevel;
    private bool Mode, Speed, Switch;
    private UI.SqrButton SpeedHTP;
    private UI.SqrButton SwitchHTP;

    // Use this for initialization
    void Start() {
        a = GetComponent<AudioSource>();
        a.volume = GameGlobals.SoundVolume*0.01f;
        a.loop = false;
        a.Pause();
        Time.timeScale = 1.0f;
        Screen.orientation = ScreenOrientation.Portrait;
        IDrag.D2Camera.Calibrate();
        IDrag.D2Camera.Update(new Vector2(Screen.width*0.5f, Screen.height*0.5f));
        ColorLib.Init();
        ShaderLib.Init();
        SpriteLib.Init();
        SoundLib.Init();
        Page = 0;
        Mode = false;
        Speed = false;
        Switch = false;
        MaxPage = 3;
        Background = ShaderLib.GetShader(ShaderLib.Blank);
        Background.color = GameGlobals.BackGround;
        Buttons = new List<UI.Image>();
        UI.CircleButton Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.125f, Screen.height*0.915f, (int) (Screen.width*0.2f), (int) (Screen.height*0.15f), "Menu");
        Temp.SetTex(SpriteLib.Home);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.875f, Screen.height*0.915f, (int) (Screen.width*0.2f), (int) (Screen.height*0.15f), "Back");
        Temp.SetTex(SpriteLib.Back);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.125f, Screen.height*0.085f, (int) (Screen.width*0.2f), (int) (Screen.height*0.15f), "Prev");
        Temp.SetTex(SpriteLib.Back);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.875f, Screen.height*0.085f, (int) (Screen.width*0.2f), (int) (Screen.height*0.15f), "Next");
        Temp.SetTex(SpriteLib.Next);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        UI.Image Temp2 = new UI.Image();
        Temp2.Init(Screen.width*0.5f, Screen.height*0.915f, (int) (Screen.width*0.5f), (int) (Screen.height*0.15f), "Info");
        Temp2.SetTex(SpriteLib.SelectL);
        Temp2.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp2);
        float Width = 0.16f;
        float Height = 0.08f;
        int Num = 0;
        Challengelevel = 0;
        Buttons4 = new List<UI.Image>();
        for (int i = 0; i < 100/25; ++i) {
            for (int k = 0; k < 5; ++k) {
                for (int j = 0; j < 5; ++j) {
                    Temp = new UI.CircleButton();
                    Temp.Init(Screen.width*(0.12f + 0.19f*j), Screen.height*(0.75f - 0.12f*k), (int) (Screen.width*Width), (int) (Screen.height*Height), (Num).ToString());
                    Temp.SetTex(Resources.Load<Texture2D>("Count/" + ++Num));
                    Temp.SetShader(ShaderLib.DefaultColorTexGray);
                    Buttons4.Add(Temp);
                }
            }
        }
        Buttons3 = new List<UI.Image>();
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.125f, Screen.height*0.915f, (int) (Screen.width*0.2f), (int) (Screen.height*0.15f), "Menu");
        Temp.SetTex(SpriteLib.Home);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons3.Add(Temp);
        UI.SqrButton Temp3 = new UI.SqrButton();
        Temp3.Init(Screen.width*0.35f, Screen.height*0.7f, (int) (Screen.width*0.5f), (int) (Screen.height*0.15f), "Speed");
        Temp3.SetTex(SpriteLib.Speed);
        Temp3.SetShader(ShaderLib.DefaultColorTex);
        Buttons3.Add(Temp3);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.725f, Screen.height*0.7f, (int) (Screen.width*0.15f), (int) (Screen.height*0.15f), "SpeedHTP");
        Temp.SetTex(SpriteLib.HTP);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons3.Add(Temp);
        Temp3 = new UI.SqrButton();
        Temp3.Init(Screen.width*0.35f, Screen.height*0.5f, (int) (Screen.width*0.5f), (int) (Screen.height*0.15f), "Switch");
        Temp3.SetTex(SpriteLib.Switch);
        Temp3.SetShader(ShaderLib.DefaultColorTex);
        Buttons3.Add(Temp3);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width*0.725f, Screen.height*0.5f, (int) (Screen.width*0.15f), (int) (Screen.height*0.15f), "SwitchHTP");
        Temp.SetTex(SpriteLib.HTP);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons3.Add(Temp);
        Temp2 = new UI.Image();
        Temp2.Init(Screen.width*0.5f, Screen.height*0.915f, (int) (Screen.width*0.5f), (int) (Screen.height*0.15f), "Info");
        Temp2.SetTex(SpriteLib.SelectM);
        Temp2.SetShader(ShaderLib.DefaultColorTex);
        Buttons3.Add(Temp2);
        SpeedHTP = new UI.SqrButton();
        SpeedHTP.Init(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width, Screen.height, "HTPS");
        SpeedHTP.SetTex(SpriteLib.HTPS);
        SpeedHTP.SetShader(ShaderLib.Overlay);
        SwitchHTP = new UI.SqrButton();
        SwitchHTP.Init(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width, Screen.height, "HTPCS");
        SwitchHTP.SetTex(SpriteLib.HTPCS);
        SwitchHTP.SetShader(ShaderLib.Overlay);
    }

    // Update is called once per frame
    void Update() {
        if (!Switch && !Speed) {
            if (Mode) {
                foreach (UI.Image i in Buttons4) {
                    if (int.Parse(i.GetName()) <= Challengelevel) {
                        if (int.Parse(i.GetName()) >= Page*25 && int.Parse(i.GetName()) < Page*25 + 25) {
                            if (i.Update()) {
                                a.clip = i.GetClip();
                                a.Play();
                                GameInfo.ChallengeLevel = int.Parse(i.GetName());
                                SceneManager.LoadScene(GameInfo.GameSceneNo + GameInfo.GameType);
                            }
                        }
                    }
                }
                foreach (UI.Image i in Buttons) {
                    if (!(i.GetName() == "Next" && Page == MaxPage) && !(i.GetName() == "Prev" && Page == 0)) {
                        if (i.Update()) {
                            a.clip = i.GetClip();
                            a.Play();
                            switch (i.GetName()) {
                                case "Back":
                                    Mode = false;
                                    break;
                                case "Menu":
                                    SceneManager.LoadScene(1);
                                    break;
                                case "Prev":
                                    --Page;
                                    break;
                                case "Next":
                                    ++Page;
                                    break;
                            }
                        }
                    }
                }
            } else {
                foreach (UI.Image i in Buttons3) {
                    if (i.Update()) {
                        a.clip = i.GetClip();
                        a.Play();
                        switch (i.GetName()) {
                            case "Menu":
                                SceneManager.LoadScene(1);
                                break;
                            case "Speed":
                                GameInfo.GameType = GameInfo.Speed;
                                GameGlobals.SpeedTime = 30.0f;
                                DoLevels();
                                Mode = true;
                                break;
                            case "Switch":
                                GameInfo.GameType = GameInfo.ColorSwitch;
                                GameGlobals.SpeedTime = 60.0f;
                                DoLevels();
                                Mode = true;
                                break;
                            case "SwitchHTP":
                                Switch = true;
                                break;
                            case "SpeedHTP":
                                Speed = true;
                                break;
                        }
                    }
                }
            }
        } else {
            if (Switch) {
                if (SwitchHTP.Update()) {
                    a.clip = SwitchHTP.GetClip();
                    a.Play();
                    Switch = false;
                }
            }
            if (Speed) {
                if (SpeedHTP.Update()) {
                    a.clip = SpeedHTP.GetClip();
                    a.Play();
                    Speed = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Mode) {
                Mode = false;
            } else {
                SceneManager.LoadScene(1);
            }
        }
    }
    void DoLevels() {
        switch (GameInfo.GameType) {
            case GameInfo.Speed: {
                Challengelevel = GameGlobals.ChallengeLevelSpeed;
                break;
            }
            case GameInfo.ColorSwitch: {
                Challengelevel = GameGlobals.ChallengeLevelSwitch;
                break;
            }
        }
        int Num = 0;
        for (int i = 0; i < 100/25; ++i) {
            for (int k = 0; k < 5; ++k) {
                for (int j = 0; j < 5; ++j) {
                    if (Num <= Challengelevel) {
                        Buttons4[Num++].SetShader(ShaderLib.DefaultColorTex);
                    } else {
                        Buttons4[Num++].SetShader(ShaderLib.DefaultColorTexGray);
                    }
                }
            }
        }
    }
    void OnPostRender() {
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
        if (Mode) {
            foreach (UI.Image i in Buttons4) {
                if (int.Parse(i.GetName()) >= Page*25 && int.Parse(i.GetName()) < Page*25 + 25) {
                    i.Draw(new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b));
                }
            }
            foreach (UI.Image i in Buttons) {
                {
                    if (!(i.GetName() == "Next" && Page == MaxPage) && !(i.GetName() == "Prev" && Page == 0)) {
                        i.Draw(new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b));
                    }
                }
            }
        } else {
            foreach (UI.Image i in Buttons3) {
                i.Draw(new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b));
            }
        }
        if (Switch)
        {
            SwitchHTP.Draw();
        }
        if (Speed)
        {
            SpeedHTP.Draw();
        }
    }
}
