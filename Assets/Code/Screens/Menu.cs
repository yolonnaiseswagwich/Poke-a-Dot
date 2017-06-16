using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UI;
public class Menu : MonoBehaviour
{
    private Material Shaders;
    int ValueX;
    int ValueY;
    List<UI.Image> Buttons;
    bool Credits;
    bool HTP;
    UI.SqrButton CredOverlay;
    UI.SqrButton HTPOverlay;
    UI.SqrButton CorruptSave;
    UI.SqrButton aWebsite;
    protected float Timer;
    protected Color Cl1;
    protected Color Cl2;
    protected bool InApp;
    AudioSource a;
    void OnApplicationPause()
    {
        InApp = false;
    }
    void Awake()
    {
        InApp = true;
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
        GameInfo.DC = false;
        a = GetComponent<AudioSource>();
        a.volume = GameGlobals.SoundVolume * 0.01f;
        a.loop = false;
        a.Pause();
        GameInfo.GameType = GameInfo.Menu;
        Timer = 0;
        Score.m_iColor = Color.white;
        Score.m_iCount = Score.m_iScore = 0;
        Score.m_fGameTime = 0;
        //Shaders = ShaderLib.GetShader(ShaderLib.Julia);
        Shaders = ShaderLib.GetShader(ShaderLib.Blank);
        ValueX = (int)(Screen.width * 0.12f);
        ValueY = (int)(Screen.height * 0.08f);
        Credits = false;
        Cl1 = GameGlobals.WhiteGray;
        Cl2 = GameGlobals.Red;
        Buttons = new List<UI.Image>();
        UI.CircleButton Temp;
        UI.SqrButton Temp2;
        UI.Image Temp3;
        Temp3 = new UI.Image();
        Temp3.Init(Screen.width * 0.5f, Screen.height * 0.9125f, (int)(Screen.width * 0.6f), (int)(Screen.height * 0.25f), "PaD");
        Temp3.SetTex(SpriteLib.PaD);
        Temp3.SetShader(ShaderLib.Neon);
        Buttons.Add(Temp3);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.5f, Screen.height * 0.565f, (int)(Screen.width * 0.6f), (int)(Screen.height * 0.45f), "Play");
        Temp.SetTex(SpriteLib.Play);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        // Temp.SetShader(ShaderLib.Planet);
        Buttons.Add(Temp);
        Temp2 = new UI.SqrButton();
        Temp2.Init(Screen.width * 0.5f, Screen.height * 0.22f, (int)(Screen.width * 0.6f), ValueY * 2, "GameModes");
        Temp2.SetTex(SpriteLib.GameModes);
        Temp2.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp2);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.1f, Screen.height * 0.935f, ValueX, ValueY, "HTP");
        Temp.SetTex(SpriteLib.HTP);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.9f, Screen.height * 0.81f, ValueX, ValueY, "NA");
        Temp.SetTex(SpriteLib.NA);
        Temp.SetShader(ShaderLib.DefaultColorTexGray);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.9f, Screen.height * 0.935f, ValueX, ValueY, "Credits");
        Temp.SetTex(SpriteLib.Credits);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.1f, Screen.height * 0.81f, ValueX, ValueY, "DC");
        Temp.SetTex(SpriteLib.DC);
        if (System.DateTime.Now.DayOfYear != GameGlobals.Day) {
            Temp.SetShader(ShaderLib.DefaultColorTex);
        } else {
            Temp.SetShader(ShaderLib.DefaultColorTexGray);
        }
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.1f, Screen.height * 0.06f, ValueX, ValueY, "Store");
        Temp.SetTex(SpriteLib.Store);
        Temp.SetShader(ShaderLib.DefaultColorTexGray);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.3f, Screen.height * 0.06f, ValueX, ValueY, "Achievments");
        Temp.SetTex(SpriteLib.Achievments);
        Temp.SetShader(ShaderLib.DefaultColorTexGray);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.5f, Screen.height * 0.06f, ValueX, ValueY, "Settings");
        Temp.SetTex(SpriteLib.Settings);
        Temp.SetShader(ShaderLib.DefaultColorTex);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.7f, Screen.height * 0.06f, ValueX, ValueY, "Rate");
        Temp.SetTex(SpriteLib.Rate);
        Temp.SetShader(ShaderLib.DefaultColorTexGray);
        Buttons.Add(Temp);
        Temp = new UI.CircleButton();
        Temp.Init(Screen.width * 0.9f, Screen.height * 0.06f, ValueX, ValueY, "Like");
        Temp.SetTex(SpriteLib.Like);
        Temp.SetShader(ShaderLib.DefaultColorTexGray);
        Buttons.Add(Temp);
        CredOverlay = new UI.SqrButton();
        CredOverlay.Init(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width, Screen.height, "CredOverlay");
        CredOverlay.SetTex(SpriteLib.CredOverlay);
        CredOverlay.SetShader(ShaderLib.Overlay);
        aWebsite = new UI.SqrButton();
        aWebsite.Init(Screen.width * 0.5f, Screen.height * 0.54f, Screen.width, (int)(Screen.height * 0.1f), "Website");
        aWebsite.SetTex(SpriteLib.Website);
        aWebsite.SetShader(ShaderLib.OverlayNBG);
        HTPOverlay = new UI.SqrButton();
        HTPOverlay.Init(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width, Screen.height, "HTPOverlay");
        HTPOverlay.SetTex(SpriteLib.HTPOverlay);
        HTPOverlay.SetShader(ShaderLib.Overlay);
        CorruptSave = new UI.SqrButton();
        CorruptSave.Init(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width, Screen.height, "Corrupt");
        CorruptSave.SetTex(SpriteLib.CorruptOverlay);
        CorruptSave.SetShader(ShaderLib.Overlay);
        if (GameGlobals.Corrupt)
        {
            a.clip = SoundLib.GetSound(SoundLib.Error);
            a.Play();
        }
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
    void Update()
    {
        Timer += Time.deltaTime;
        switch ((int)Timer % 7)
        {
            case 0:
                Cl1 = GameGlobals.Blue;
                Cl2 = GameGlobals.Red;
                break;
            case 1:
                Cl1 = GameGlobals.Red;
                Cl2 = GameGlobals.Yellow;
                break;
            case 2:
                Cl1 = GameGlobals.Yellow;
                Cl2 = GameGlobals.Brown;
                break;
            case 3:
                Cl1 = GameGlobals.Brown;
                Cl2 = GameGlobals.WhiteGray;
                break;
            case 4:
                Cl1 = GameGlobals.WhiteGray;
                Cl2 = GameGlobals.Green;
                break;
            case 5:
                Cl1 = GameGlobals.Green;
                Cl2 = GameGlobals.Purple;
                break;
            case 6:
                Cl1 = GameGlobals.Purple;
                Cl2 = GameGlobals.Blue;
                break;
        }
        if (!Credits && !HTP && !GameGlobals.Corrupt)
        {
            foreach (UI.Image i in Buttons)
            {
                if (i.Update())
                {
                    a.clip = i.GetClip();
                    a.Play();
                    switch (i.GetName())
                    {
                        case "Play":
                            GameInfo.GameType = GameInfo.Endless;
                            SceneManager.LoadScene(GameInfo.GameSceneNo + GameInfo.GameType);
                            break;
                        case "HTP":
                            HTP = true;
                            break;
                        case "Credits":
                            Credits = true;
                            break;
                        case "NA":
                            break;
                        case "DC":
                            if (System.DateTime.Now.DayOfYear != GameGlobals.Day) {
                                GameGlobals.Day = System.DateTime.Now.DayOfYear;
                                SaveLoadLib.Save();
                                if ((System.DateTime.Now.Day + System.DateTime.Now.DayOfYear * System.DateTime.Now.Year) % 2 == 0)
                                {
                                    GameInfo.GameType = GameInfo.Speed;
                                    GameInfo.ChallengeLevel = GameGlobals.ChallengeLevelSpeed + 1 + (System.DateTime.Now.Day + System.DateTime.Now.DayOfYear * System.DateTime.Now.Year) % 3;
                                }
                                else
                                {
                                    GameInfo.GameType = GameInfo.ColorSwitch;
                                    GameInfo.ChallengeLevel = GameGlobals.ChallengeLevelSwitch + 1 + (System.DateTime.Now.Day + System.DateTime.Now.DayOfYear * System.DateTime.Now.Year) % 3;
                                }
                                GameInfo.DC = true;
                                GameGlobals.SpeedTime = 30.0f + (System.DateTime.Now.Day * System.DateTime.Now.DayOfYear + System.DateTime.Now.Year) % 30;
                                SceneManager.LoadScene(GameInfo.GameSceneNo + GameInfo.GameType);
                            }
                            break;
                        case "GameModes":
                            GameInfo.GameType = GameInfo.Speed;
                            //SceneManager.LoadScene(8);
                            SceneManager.LoadScene(2);
                            break;
                        case "Store":
                            //SceneManager.LoadScene(4);
                            break;
                        case "Achievments": //also put highscores and progress here
                            break;
                        case "Settings":
                            GameInfo.GameType = GameInfo.Settings;
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
        else
        {
            if (Credits)
            {
                if (aWebsite.Update())
                {
                    a.clip = aWebsite.GetClip();
                    Application.OpenURL("https://www.implodingdragons.com.html");
                } else
                {
                    if (CredOverlay.Update())
                    {
                        a.clip = CredOverlay.GetClip();
                        a.Play();
                        Credits = false;
                    }
                }
            }
            else if (HTP)
            {
                if (HTPOverlay.Update())
                {
                    a.clip = HTPOverlay.GetClip();
                    a.Play();
                    HTP = false;
                }
            }
            else if (GameGlobals.Corrupt)
            {
                if (CorruptSave.Update())
                {
                    a.clip = CorruptSave.GetClip();
                    a.Play();
                    GameGlobals.Corrupt = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Credits || HTP)
            {
                Credits = false;
                HTP = false;
            }
            else
            {
                Application.Quit();
            }
        }

    }
    void OnPostRender()
    {
        GL.PushMatrix();
        Shaders.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
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
        ///////////////////////////////////
        /// Julia shader background code///
        ///////////////////////////////////
        //Vector4 Variables1 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 1.79f) * 0.02f + 0.37f, Mathf.Sin(Time.timeSinceLevelLoad * 1.22f) * 0.02f + 0.37f, 17.0f, 3.14159f);
        //Vector4 Variables2 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 0.61f), Mathf.Cos(Time.timeSinceLevelLoad * 1.21f), Mathf.Sin(Time.timeSinceLevelLoad * 0.87f), Mathf.Cos(Time.timeSinceLevelLoad * 0.87f));
        //Shaders.SetVector("MyVar", Variables1);
        //Shaders.SetVector("MyVarb", Variables2);
        //GL.PushMatrix();
        //Shaders.SetPass(0);
        //GL.LoadOrtho();
        //GL.Begin(GL.QUADS);
        //if (Screen.width > Screen.height)
        //{
        //    GL.TexCoord2(0, 0);
        //    GL.Vertex3(0, (0 - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
        //    GL.TexCoord2(0, 1);
        //    GL.Vertex3(0, (Screen.width - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
        //    GL.TexCoord2(1, 1);
        //    GL.Vertex3(1, (Screen.width - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
        //    GL.TexCoord2(1, 0);
        //    GL.Vertex3(1, (0 - ValueX) * IDrag.D2Camera.PixelSize.y, 0.1F);
        //}
        //else if (Screen.height > Screen.width)
        //{
        //    GL.TexCoord2(0, 0);
        //    GL.Vertex3((0 - ValueY) * IDrag.D2Camera.PixelSize.x, 0, 0.1F);
        //    GL.TexCoord2(0, 1);
        //    GL.Vertex3((0 - ValueY) * IDrag.D2Camera.PixelSize.x, 1, 0.1F);
        //    GL.TexCoord2(1, 1);
        //    GL.Vertex3((Screen.height - ValueY) * IDrag.D2Camera.PixelSize.x, 1, 0.1F);
        //    GL.TexCoord2(1, 0);
        //    GL.Vertex3((Screen.height - ValueY) * IDrag.D2Camera.PixelSize.x, 0, 0.1F);
        //}
        //else
        //{
        //    GL.TexCoord2(0, 0);
        //    GL.Vertex3(0, 0, 0.1F);
        //    GL.TexCoord2(0, 1);
        //    GL.Vertex3(0, 1, 0.1F);
        //    GL.TexCoord2(1, 1);
        //    GL.Vertex3(1, 1, 0.1F);
        //    GL.TexCoord2(1, 0);
        //    GL.Vertex3(1, 0, 0.1F);
        //}
        //GL.End();
        //GL.PopMatrix();
        foreach (UI.Image i in Buttons)
        {
            if (i.GetName() != "PaD")
            {
                if (i.GetName() != "Play" && i.GetName() != "GameModes")
                {
                    i.Draw(new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b));
                }
                else
                {
                    i.Draw(Color.Lerp(Cl1, Cl2, (Timer - (int)Timer) * 2.0f));
                }
            }
            else
            {
                Vector4 Variables3 = new Vector4(Mathf.Sin(Time.timeSinceLevelLoad * 1.61f), Mathf.Cos(Time.timeSinceLevelLoad * 1.21f), Mathf.Sin(Time.timeSinceLevelLoad * 1.87f), Mathf.Cos(Time.timeSinceLevelLoad * 0.83f));
                Color ColorVar = new Color(0.5f + (Mathf.Abs(Variables3.x) * 0.5f), Mathf.Abs(Variables3.y), Mathf.Abs(Variables3.z), 2.0f);
                i.DrawColor(ColorVar);
            }
        }
        if (Credits)
        {
            CredOverlay.Draw();
            aWebsite.Draw();
        }
        if (HTP)
        {
            HTPOverlay.Draw();
        }
        if (GameGlobals.Corrupt)
        {
            CorruptSave.Draw();
        }
    }
}
