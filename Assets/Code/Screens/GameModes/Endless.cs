using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Endless : BaseGame
{
    private List<Dot> m_oObjectList;
    private float m_fSpawnTimer;
    private float IncSpeedTime;
    private UI.Image IncSpeed;
    ///TEMPVAR
	// Use this for initialization
    protected override void Start()
    {
        base.Start();
        GameInfo.GameType = GameInfo.Endless;
        GameGlobals.Speed = (float)Screen.height * 0.135f;
        GameGlobals.SpeedGrowth = 0.0234f;
        GameGlobals.SpawnSpeed = 0.175f * (float)Screen.height / GameGlobals.Speed;
        GameGlobals.WaitSpeed = 0.04f;
        GameGlobals.Lives = 1;
        GameGlobals.ShakeTime = 0;
        GameGlobals.WaitTimer = 0;
        m_oObjectList = new List<Dot>();
        Spawn(8);
        IncSpeedTime = 0;
        IncSpeed = new UI.Image();
        IncSpeed.Init(Screen.width * 0.5f, Screen.height * 0.5f, Screen.width, Screen.height, "IncSpeed");
        IncSpeed.SetTex(SpriteLib.SpeedInc);
        IncSpeed.SetShader(ShaderLib.DefaultAlpha);
        AudioTimer = 15.0f;
        foreach (AudioSource a in GetComponents<AudioSource>())
        {
            a.volume = GameGlobals.SoundVolume * 0.01f;
        }
    }
    int Math(int i)
    {
        int k = 0;
        while (i > 0)
        {
            k += --i;
        }
        return k;
    }
    protected override void Update()
    {
        base.Update();
        if (timer > 4.5f)
        {
            IncSpeedTime -= Time.deltaTime;
            Shake();
#if UNITY_EDITOR || UNITY_WEB
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                int Temp = IsCollision(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                if (Temp >= 0)
                {
                    if (m_oObjectList[Temp].GetColor() == Score.m_iColor)
                    {
                        Score.m_iScore += ++Score.m_iCount;
                    }
                    else
                    {
                        Score.m_iCount = 0;
                        Score.m_iColor = m_oObjectList[Temp].GetColor();
                        Score.m_iScore += ++Score.m_iCount;
                    }
                    foreach (AudioSource a in GetComponents<AudioSource>())
                    {
                        if (a.clip.name == SoundLib.GetSound(SoundLib.Dot).name)
                        {
                            a.pitch = 1 + Score.m_iCount * 0.25f;
                            a.Play();
                        }
                    }
                    GameGlobals.WaitTimer += IDrag.Math.Sum(GameGlobals.WaitSpeed,Score.m_iCount);
                    m_oObjectList[Temp].SetKilled();
                }
            }
#endif
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    int Temp = IsCollision(new Vector2(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y), Input.GetTouch(i).radius);
                    if (Temp >= 0)
                    {
                        if (m_oObjectList[Temp].GetColor() == Score.m_iColor)
                        {
                            Score.m_iScore += ++Score.m_iCount;
                        }
                        else
                        {
                            Score.m_iCount = 0;
                            Score.m_iColor = m_oObjectList[Temp].GetColor();
                            Score.m_iScore += ++Score.m_iCount;
                        }
                        foreach (AudioSource a in GetComponents<AudioSource>())
                        {
                            if (a.clip.name == SoundLib.GetSound(SoundLib.Dot).name)
                            {
                                a.pitch = 1 + Score.m_iCount * 0.25f;
                                a.Play();
                            }
                        }
                        GameGlobals.WaitTimer += IDrag.Math.Sum(GameGlobals.WaitSpeed, Score.m_iCount);
                        m_oObjectList[Temp].SetKilled();
                    }
                }
            }
            if (GameGlobals.WaitTimer <= 0.0f)
            {
                if (m_fSpawnTimer >= GameGlobals.SpawnSpeed)
                {
                    m_fSpawnTimer -= GameGlobals.SpawnSpeed;
                    Spawn(1);
                }
                for (int i = 0; i < m_oObjectList.Count; ++i)
                {
                    if (!m_oObjectList[i].Update())
                    {
                        Dot aDot = m_oObjectList[i];
                        m_oObjectList.Remove(aDot);
                        --i;
                    }
                }
                m_fSpawnTimer += Time.deltaTime;
            }
            else
            {
                GameGlobals.WaitTimer -= Time.deltaTime;
                if (m_fSpawnTimer >= GameGlobals.SpawnSpeed)
                {
                    m_fSpawnTimer -= GameGlobals.SpawnSpeed;
                    Spawn(1);
                }
                for (int i = 0; i < m_oObjectList.Count; ++i)
                {
                    if (!m_oObjectList[i].Update(true))
                    {
                        Dot aDot = m_oObjectList[i];
                        m_oObjectList.Remove(aDot);
                        --i;
                    }
                }
                m_fSpawnTimer += Time.deltaTime * 0.4f;
            }
           
        }
        else
        {
            timer += Time.deltaTime;
        }
        if (GameGlobals.Lives < 0)
        {
            SceneManager.LoadScene(5);
        }
    }
    protected override void DrawGame(Color aColor = new Color())
    {
        foreach (Dot i in m_oObjectList)
        {
            i.Draw(aColor);
        }
        if (IncSpeedTime > 0)
        {
            IncSpeed.Draw(new Color(1.0f - GameGlobals.BackGround.r, 1.0f - GameGlobals.BackGround.g, 1.0f - GameGlobals.BackGround.b, IncSpeedTime));
        }
    }
    protected override void DoAudio()
    {
        if (AudioTimer <= 0)
        {
            if (GameGlobals.SpeedGrowth <= 0.0175f)
            {
                GameGlobals.SpeedGrowth = 0.0175f;
            }
            else
            {
                GameGlobals.SpeedGrowth *= 0.9f;
            }
            GameGlobals.Speed += (float)Screen.height * GameGlobals.SpeedGrowth;
            ++GameGlobals.Lives;
            IncSpeedTime = 1.4f;
            GameGlobals.SpawnSpeed = 0.175f * (float)Screen.height / GameGlobals.Speed;
            AudioTimer += 15.0f + Time.deltaTime;
        }
        AudioTimer -= Time.deltaTime;
    }
    protected override bool Spawn(int iCount = 0)
    {
        float fHeight = 0.125f * (float)Screen.height;
        float fWidth = 0.22125f * (float)Screen.width;
        int iSize;
        if (fHeight > fWidth)
        {
            iSize = (int)fWidth;
        }
        else
        {
            iSize = (int)fHeight;
        }
        fHeight = 0.175f * (float)Screen.height;
        for (int i = 0; i < iCount; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                Dot Temp2 = new Dot();
                //pass into size x the actuall size of the circle pass into size y for the size of the dot (for speed purposes)
                Temp2.Init((int)((j + 0.75f) * fWidth), Screen.height - (int)((i - 4) * fHeight), (int)(iSize * 0.95f), (int)(iSize * 0.95f));
                m_oObjectList.Add(Temp2);
            }
        }
        return true;
    }
    protected override int IsCollision(Vector2 a_vMousePos, float Radius = 0)
    {
        for (int i = 0; i < m_oObjectList.Count; i++)
        {
            if (!m_oObjectList[i].GetKilled() && (m_oObjectList[i].GetPos() - a_vMousePos).sqrMagnitude < IDrag.Math.Sqr(m_oObjectList[i].GetRad() + Radius))
            {
                return i;
            }
        }
        return -1;
    }





}
