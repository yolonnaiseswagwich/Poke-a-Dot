using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Settings : MonoBehaviour {
    Dot Red, Yellow, Blue, Brown, Green, WhiteGray, Purple;
    Material Background;
    bool Reset;
    List<UI.Image> Buttons;
    AudioSource a;
    // Use this for initialization
    void Awake()
    {
        Time.timeScale = 1.0f;
        Screen.orientation = ScreenOrientation.Portrait;
        IDrag.D2Camera.Calibrate();
        IDrag.D2Camera.Update(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        ColorLib.Init();
        ShaderLib.Init();
        SpriteLib.Init();
        SoundLib.Init();

    }
    void Start()
    {
        a = GetComponent<AudioSource>();
        a.volume = GameGlobals.SoundVolume * 0.01f;
        a.loop = false;
        a.Pause();
        GameInfo.GameType = GameInfo.Settings;
        Spawn();
        Reset = false;
        Background = ShaderLib.GetShader(ShaderLib.Blank);
        Background.color = GameGlobals.BackGround;
        Buttons = new List<UI.Image>();
        UI.SqrButton Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.125f, Screen.height * 0.9f, (int)(Screen.width * 0.2f), (int)(Screen.height * 0.15f), "Menu");
        Temp.SetTex(SpriteLib.Home);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.875f, Screen.height * 0.9f, (int)(Screen.width * 0.2f), (int)(Screen.height * 0.15f), "Reset");
        Temp.SetTex(SpriteLib.Reset);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.775f, Screen.height * 0.125f, (int)(Screen.width * 0.4f), (int)(Screen.height * 0.225f), "MusicVolume");
        if (GameGlobals.MusicVolume > 0.0f)
        {
            Temp.SetTex(SpriteLib.MusicOn);
        }
        else
        {
            Temp.SetTex(SpriteLib.MusicOff);
        }
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.SqrButton();
        Temp.Init(Screen.width * 0.225f, Screen.height * 0.125f, (int)(Screen.width * 0.4f), (int)(Screen.height * 0.225f), "SoundVolume");
        if (GameGlobals.SoundVolume > 0.0f)
        {
            Temp.SetTex(SpriteLib.SoundOn);
        }
        else
        {
            Temp.SetTex(SpriteLib.SoundOff);
        }
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        UI.Image Temp2 = new UI.Image();
        Temp2.Init(Screen.width * 0.5f, Screen.height * 0.9f, (int)(Screen.width * 0.5f), (int)(Screen.height * 0.15f), "Info");
        Temp2.SetTex(SpriteLib.Settingsinfo);
        Temp2.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp2);
    }
	// Update is called once per frame
	void Update ()
    {
        foreach (UI.Image i in Buttons)
        {
            if (i.Update())
            {
                a.clip = i.GetClip();
                a.Play();
                switch (i.GetName())
                {
                    case "Menu":
                        Reset = true;

#if !UNITY_WEB
                        SaveLoadLib.Save();
#endif
                        GameInfo.GameType = GameInfo.Menu;
                        SceneManager.LoadScene(1);
                        break;
                    case "Reset":
                        Reset = true;
                        GameGlobals.iRed = ColorLib.Reds.Crismon;
                        GameGlobals.iYellow = ColorLib.Yellows.Yellow;
                        GameGlobals.iBrown = ColorLib.Browns.SaddleBrown;
                        GameGlobals.iGreen = ColorLib.Greens.Lime;
                        GameGlobals.iBlue = ColorLib.Blues.DarkTurqoise;
                        GameGlobals.iPurple = ColorLib.Purples.HotPink;
                        GameGlobals.iWhiteGray = ColorLib.WhiteGrays.White;
                        GameGlobals.iBackGround = ColorLib.BlackGrays.Black;
                        GameGlobals.MusicVolume = 100;
                        GameGlobals.SoundVolume = 100;
                        GameGlobals.Red = ColorLib.GetColor(GameGlobals.iRed);
                        GameGlobals.Yellow = ColorLib.GetColor(GameGlobals.iYellow);
                        GameGlobals.Brown = ColorLib.GetColor(GameGlobals.iBrown);
                        GameGlobals.Green = ColorLib.GetColor(GameGlobals.iGreen);
                        GameGlobals.Blue = ColorLib.GetColor(GameGlobals.iBlue);
                        GameGlobals.Purple = ColorLib.GetColor(GameGlobals.iPurple);
                        GameGlobals.WhiteGray = ColorLib.GetColor(GameGlobals.iWhiteGray);
                        GameGlobals.BackGround = ColorLib.GetColor(GameGlobals.iBackGround);
                        Spawn();
                        foreach (UI.Image k in Buttons) {
                            switch (k.GetName()) {
                                case "MusicVolume":
                                    if (GameGlobals.MusicVolume > 0.0f) {
                                        k.SetTex(SpriteLib.MusicOn);
                                    } else {
                                        k.SetTex(SpriteLib.MusicOff);
                                    }
                                    break;
                                case "SoundVolume":
                                    a.volume = GameGlobals.SoundVolume*0.01f;
                                    if (GameGlobals.SoundVolume > 0.0f) {
                                        k.SetTex(SpriteLib.SoundOn);
                                    } else {
                                        k.SetTex(SpriteLib.SoundOff);
                                    }
                                    break;
                            }
                        }
                        break;
                    case "MusicVolume":
                        GameGlobals.MusicVolume += 100;
                        if (GameGlobals.MusicVolume > 100)
                        {
                            GameGlobals.MusicVolume = 0;
                        }
                        if (GameGlobals.MusicVolume > 0.0f)
                        {
                            i.SetTex(SpriteLib.MusicOn);
                        }
                        else
                        {
                            i.SetTex(SpriteLib.MusicOff);
                        }
                        break;
                    case "SoundVolume":
                        GameGlobals.SoundVolume += 100;
                        if (GameGlobals.SoundVolume > 100)
                        {
                            GameGlobals.SoundVolume = 0;
                        }
                        a.volume = GameGlobals.SoundVolume * 0.01f;
                        if (GameGlobals.SoundVolume > 0.0f)
                        {
                            i.SetTex(SpriteLib.SoundOn);
                        }
                        else
                        {
                            i.SetTex(SpriteLib.SoundOff);
                        }
                        break;
                }
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            GameInfo.GameType = GameInfo.Menu;
            SceneManager.LoadScene(1);
        }
        if (!Reset)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Vector2 Mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                if ((Red.GetPos() - Mouse).sqrMagnitude < Red.GetRad() * Red.GetRad())
                {
                    a.clip = SoundLib.GetSound(SoundLib.Dot);
                    a.Play();
                    ++GameGlobals.iRed;
                    if (GameGlobals.iRed > ColorLib.Reds.Orange)
                    {
                        GameGlobals.iRed = ColorLib.Reds.IndianRed;
                    }
                    GameGlobals.Red = ColorLib.GetColor(GameGlobals.iRed);
                    Spawn();
                }
                else if ((Yellow.GetPos() - Mouse).sqrMagnitude < Yellow.GetRad() * Yellow.GetRad())
                {
                    a.clip = SoundLib.GetSound(SoundLib.Dot);
                    a.Play();
                    ++GameGlobals.iYellow;
                    if (GameGlobals.iYellow > ColorLib.Yellows.GoldenRod)
                    {
                        GameGlobals.iYellow = ColorLib.Yellows.Yellow;
                    }
                    GameGlobals.Yellow = ColorLib.GetColor(GameGlobals.iYellow);
                    Spawn();
                }
                else if ((Brown.GetPos() - Mouse).sqrMagnitude < Brown.GetRad() * Brown.GetRad())
                {
                    a.clip = SoundLib.GetSound(SoundLib.Dot);
                    a.Play();
                    ++GameGlobals.iBrown;
                    if (GameGlobals.iBrown > ColorLib.Browns.Brown)
                    {
                        GameGlobals.iBrown = ColorLib.Browns.Peru;
                    }
                    GameGlobals.Brown = ColorLib.GetColor(GameGlobals.iBrown);
                    Spawn();
                }
                else if ((Green.GetPos() - Mouse).sqrMagnitude < Green.GetRad() * Green.GetRad())
                {
                    a.clip = SoundLib.GetSound(SoundLib.Dot);
                    a.Play();
                    ++GameGlobals.iGreen;
                    if (GameGlobals.iGreen > ColorLib.Greens.Green)
                    {
                        GameGlobals.iGreen = ColorLib.Greens.LimeGreen;
                    }
                    GameGlobals.Green = ColorLib.GetColor(GameGlobals.iGreen);
                    Spawn();
                }
                else if ((Blue.GetPos() - Mouse).sqrMagnitude < Blue.GetRad() * Blue.GetRad())
                {
                    a.clip = SoundLib.GetSound(SoundLib.Dot);
                    a.Play();
                    ++GameGlobals.iBlue;
                    if (GameGlobals.iBlue > ColorLib.Blues.MidnightBlue)
                    {
                        GameGlobals.iBlue = ColorLib.Blues.Cyan;
                    }
                    GameGlobals.Blue = ColorLib.GetColor(GameGlobals.iBlue);
                    Spawn();
                }
                else if ((Purple.GetPos() - Mouse).sqrMagnitude < Purple.GetRad() * Purple.GetRad())
                {
                    a.clip = SoundLib.GetSound(SoundLib.Dot);
                    a.Play();
                    ++GameGlobals.iPurple;
                    if (GameGlobals.iPurple > ColorLib.Purples.Indigo)
                    {
                        GameGlobals.iPurple = ColorLib.Purples.Pink;
                    }
                    GameGlobals.Purple = ColorLib.GetColor(GameGlobals.iPurple);
                    Spawn();
                }
                else if ((WhiteGray.GetPos() - Mouse).sqrMagnitude < WhiteGray.GetRad() * WhiteGray.GetRad())
                {
                    a.clip = SoundLib.GetSound(SoundLib.Dot);
                    a.Play();
                    ++GameGlobals.iWhiteGray;
                    if (GameGlobals.iWhiteGray == GameGlobals.iBackGround)
                    {
                        ++GameGlobals.iWhiteGray;
                    }
                    if (GameGlobals.iWhiteGray > ColorLib.BlackGrays.Black)
                    {
                        GameGlobals.iWhiteGray = ColorLib.WhiteGrays.White;
                        if (GameGlobals.iWhiteGray == GameGlobals.iBackGround)
                        {
                            ++GameGlobals.iWhiteGray;
                        }
                    }
                    GameGlobals.WhiteGray = ColorLib.GetColor(GameGlobals.iWhiteGray);
                    Spawn();
                }
                else
                {
                    if (GameGlobals.iBackGround == ColorLib.BlackGrays.Black)
                    {
                        GameGlobals.iBackGround = ColorLib.WhiteGrays.White;
                        if (GameGlobals.iWhiteGray == GameGlobals.iBackGround)
                        {
                            GameGlobals.iWhiteGray = ColorLib.BlackGrays.Black;
                            GameGlobals.WhiteGray = ColorLib.GetColor(GameGlobals.iWhiteGray);
                            Spawn();
                        }
                    }
                    else
                    {
                        GameGlobals.iBackGround = ColorLib.BlackGrays.Black;
                        if (GameGlobals.iWhiteGray == GameGlobals.iBackGround)
                        {
                            GameGlobals.iWhiteGray = ColorLib.WhiteGrays.White;
                            GameGlobals.WhiteGray = ColorLib.GetColor(GameGlobals.iWhiteGray);
                            Spawn();
                        }
                    }
                    GameGlobals.BackGround = ColorLib.GetColor(GameGlobals.iBackGround);
                    Background.color = GameGlobals.BackGround;
                }
            }
        }
        else
        {
            Reset = false;
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
        Red.Draw();
        Yellow.Draw();
        Blue.Draw();
        Brown.Draw();
        Green.Draw();
        Purple.Draw();
        WhiteGray.Draw();
        foreach (UI.Image i in Buttons)
        {
            i.Draw(new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b));
        }
    }
    bool Spawn()
    {
        float fHeight = 0.125f * (float)Screen.height;
        float fWidth = 0.177f * (float)Screen.width;
        int iSize;
        iSize = (int)fHeight;
        fHeight = 0.135f * (float)Screen.height;
        Red = new Dot();
        Red.Init((int)((1.8f) * fWidth), Screen.height - (int)((2.0f) * fHeight), (int)(iSize * 0.95f), (int)(iSize * 0.95f), GameGlobals.Red);
        Yellow = new Dot();
        Yellow.Init((int)((3.8f) * fWidth), Screen.height - (int)((2.0f) * fHeight), (int)(iSize * 0.95f), (int)(iSize * 0.95f), GameGlobals.Yellow);
        Blue = new Dot();
        Blue.Init((int)((1.2f) * fWidth), Screen.height - (int)((3.5f) * fHeight), (int)(iSize * 0.95f), (int)(iSize * 0.95f), GameGlobals.Blue);
        Brown = new Dot();
        Brown.Init((int)((2.75f) * fWidth), Screen.height - (int)((3.5f) * fHeight), (int)(iSize * 0.95f), (int)(iSize * 0.95f), GameGlobals.Brown);
        Green = new Dot();
        Green.Init((int)((4.3f) * fWidth), Screen.height - (int)((3.5f) * fHeight), (int)(iSize * 0.95f), (int)(iSize * 0.95f), GameGlobals.Green);
        Purple = new Dot();
        Purple.Init((int)((1.8f) * fWidth), Screen.height - (int)((5.0f) * fHeight), (int)(iSize * 0.95f), (int)(iSize * 0.95f), GameGlobals.Purple);
        WhiteGray = new Dot();
        WhiteGray.Init((int)((3.8f) * fWidth), Screen.height - (int)((5.0f) * fHeight), (int)(iSize * 0.95f), (int)(iSize * 0.95f), GameGlobals.WhiteGray);
        return true;
    }
}
