using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Speed : BaseGame
{
    private Dot[] m_oObjectList;
    private int iSize;
    ///TEMPVAR
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        GameInfo.GameType = GameInfo.Speed;
        GameGlobals.WaitSpeed = 0.035f;
        GameGlobals.ShakeTime = 0;
        GameGlobals.Lives = 1;
        GameGlobals.ShakeTime = 0;
        GameGlobals.WaitTimer = 0;
        GameGlobals.TimeLeft = GameGlobals.SpeedTime;
        m_oObjectList = new Dot[20];
        Spawn();
        AudioTimer = 15.0f;
        Score.m_iGoal = (int)(4.5f * GameGlobals.TimeLeft + (GameInfo.ChallengeLevel * 1.0f / 4.75f * GameGlobals.TimeLeft));
    }
    protected override void Update()
    {
        base.Update();
        if (timer > 4.5f)
        {
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
                    GameGlobals.WaitTimer += GameGlobals.WaitSpeed * Score.m_iCount;
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
                        GameGlobals.WaitTimer += GameGlobals.WaitSpeed * Score.m_iCount;
                        m_oObjectList[Temp].SetKilled();
                    }
                }
            }
            for (int i = 0; i < 20; ++i)
            {
                if (!m_oObjectList[i].Update())
                {
                    m_oObjectList[i].Init(m_oObjectList[i].GetPos().x, m_oObjectList[i].GetPos().y, (int)(iSize * 0.95f), (int)(iSize * 0.95f), new Color(m_oObjectList[i].GetColor().r, m_oObjectList[i].GetColor().g, m_oObjectList[i].GetColor().b));
                }
            }
            if (!(GameGlobals.WaitTimer > 0.0f))
            {
                GameGlobals.TimeLeft -= Time.deltaTime;
            }
            else
            {
                GameGlobals.WaitTimer -= Time.deltaTime;
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
        if (Score.m_iScore >= Score.m_iGoal)
        {
            if (GameInfo.DC)
            {
                Score.m_fGameTime = GameGlobals.TimeLeft;
            }
            else if (++GameInfo.ChallengeLevel >= GameGlobals.ChallengeLevelSpeed)
            {
                Score.m_fGameTime = GameGlobals.TimeLeft;
                GameGlobals.ChallengeLevelSpeed = GameInfo.ChallengeLevel;
                SaveLoadLib.Save();
            }
            SceneManager.LoadScene(5);
        }
        if (GameGlobals.TimeLeft <= 0)
        {
            Score.m_fGameTime = GameGlobals.TimeLeft;
            SceneManager.LoadScene(5);
        }
    }
    protected override void DrawGame(Color aColor = new Color())
    {
        foreach (Dot i in m_oObjectList)
        {
            i.Draw(aColor);
        }
    }
    protected override void DoAudio()
    {
        if (AudioTimer <= 0)
        {
            AudioTimer = 15.0f;
        }
        AudioTimer -= Time.deltaTime;
    }
    protected override bool Spawn(int iCount = 0)
    {
        float fHeight = 0.125f * (float)Screen.height;
        float fWidth = 0.22125f * (float)Screen.width;
        if (fHeight > fWidth)
        {
            iSize = (int)fWidth;
        }
        else
        {
            iSize = (int)fHeight;
        }
        fHeight = 0.175f * (float)Screen.height;
        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                m_oObjectList[i * 4 + j] = new Dot();
                m_oObjectList[i * 4 + j].Init((int)((j + 0.75f) * fWidth), Screen.height - (int)((i + 1) * fHeight), (int)(iSize * 0.95f), (int)(iSize * 0.95f));
            }
        }
        return true;
    }
    protected override int IsCollision(Vector2 a_vMousePos, float Radius = 0)
    {
        for (int i = 0; i < 20; i++)
        {
            if (!m_oObjectList[i].GetKilled() && (m_oObjectList[i].GetPos() - a_vMousePos).sqrMagnitude < IDrag.Math.Sqr(m_oObjectList[i].GetRad() + Radius))
            {
                return i;
            }
        }
        return -1;
    }





}
