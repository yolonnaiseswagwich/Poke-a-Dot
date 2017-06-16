using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ColorSwitch : BaseGame
{
    private Dot[] m_oObjectList;
    private int iSize;
    private int Selected;
    bool ClearMe;
    const int Num = 35;
    ///TEMPVAR
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        GameInfo.GameType = GameInfo.ColorSwitch;
        GameGlobals.WaitSpeed = 0.05f;
        GameGlobals.ShakeTime = 0;
        GameGlobals.WaitTimer = 0;
        GameGlobals.TimeLeft = GameGlobals.SpeedTime;
        ClearMe = false;
        m_oObjectList = new Dot[Num];
        Selected = -1;
        Spawn();
        Score.m_iGoal = (int)(4.0f * GameGlobals.TimeLeft + (GameInfo.ChallengeLevel * 1.0f / 6.0f * GameGlobals.TimeLeft));
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
                    if (Selected < 0)
                    {
                        Selected = Temp;
                    }
                    else
                    {
                        Color Swap = m_oObjectList[Temp].GetColor();
                        m_oObjectList[Temp].SetColor(m_oObjectList[Selected].GetColor());
                        m_oObjectList[Selected].SetColor(Swap);
                        Selected = -1;
                        ClearMe = true;
                    }
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
                        if (Selected < 0)
                        {
                            Selected = Temp;
                        }
                        else
                        {
                            Color Swap = m_oObjectList[Temp].GetColor();
                            m_oObjectList[Temp].SetColor(m_oObjectList[Selected].GetColor());
                            m_oObjectList[Selected].SetColor(Swap);
                            Selected = -1;
                            ClearMe = true;
                        }
                    }
                }
            }
            for (int i = 0; i < Num; ++i)
            {
                if (!m_oObjectList[i].Update())
                {
                    Color Temp = new Color(m_oObjectList[i].GetColor().r, m_oObjectList[i].GetColor().g, m_oObjectList[i].GetColor().b);
                    m_oObjectList[i].Init(m_oObjectList[i].GetPos().x, m_oObjectList[i].GetPos().y, (int)(iSize * 0.95f), (int)(iSize * 0.95f), Temp);
                    ClearMe = true;
                }
            }
            if (ClearMe)
            {
                Clear();
                ClearMe = false;
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
            else if (++GameInfo.ChallengeLevel >= GameGlobals.ChallengeLevelSwitch)
            {
                Score.m_fGameTime = GameGlobals.TimeLeft;
                GameGlobals.ChallengeLevelSwitch = GameInfo.ChallengeLevel;
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
    protected override bool Spawn(int iCount = 0)
    {
        float fHeight = 0.125f * (float)Screen.height;
        float fWidth = 0.177f * (float)Screen.width;
        if (fHeight > fWidth)
        {
            iSize = (int)fWidth;
        }
        else
        {
            iSize = (int)fHeight;
        }
        for (int i = 0; i < 7; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                m_oObjectList[i * 5 + j] = new Dot();
                m_oObjectList[i * 5 + j].Init((int)((j + 0.75f) * fWidth), Screen.height - (int)((i + 1.25f) * fHeight), (int)(iSize * 0.95f), (int)(iSize * 0.95f));
            }
        }
        while (SpawnClear())
        {
            for (int i = 0; i < Num; ++i)
            {
                if (m_oObjectList[i].GetKilled())
                {
                    m_oObjectList[i].Init(m_oObjectList[i].GetPos().x, m_oObjectList[i].GetPos().y, (int)(iSize * 0.95f), (int)(iSize * 0.95f), m_oObjectList[i].GetColor());
                }
            }
        }
        return true;
    }
    protected override int IsCollision(Vector2 a_vMousePos, float Radius = 0)
    {
        for (int i = 0; i < Num; i++)
        {
            if (!m_oObjectList[i].GetKilled() && (m_oObjectList[i].GetPos() - a_vMousePos).sqrMagnitude < IDrag.Math.Sqr(m_oObjectList[i].GetRad() + Radius))
            {
                return i;
            }
        }
        return -1;
    }
    void Search(int i, ref int Count, ref List<int> Dots)
    {
        int j;
        ++Count;
        m_oObjectList[i].SetCheck(true);
        Dots.Add(i);
        j = i - 5;
        if (j >= 0 && !m_oObjectList[j].GetCheck() && m_oObjectList[i].GetColor() == m_oObjectList[j].GetColor())
        {
            Search(j, ref Count, ref Dots);
        }
        j = i - 1;
        if (j >= 0 && i % 5 - 1 >= 0 && !m_oObjectList[j].GetCheck() && m_oObjectList[i].GetColor() == m_oObjectList[j].GetColor())
        {
            Search(j, ref Count, ref Dots);
        }
        j = i + 1;
        if (j <= Num - 1 && i % 5 + 1 <= 4 && !m_oObjectList[j].GetCheck() && m_oObjectList[i].GetColor() == m_oObjectList[j].GetColor())
        {
            Search(j, ref Count, ref Dots);
        } 
        j = i + 5;
        if (j <= Num - 1 && !m_oObjectList[j].GetCheck() && m_oObjectList[i].GetColor() == m_oObjectList[j].GetColor())
        {
            Search(j, ref Count, ref Dots);
        }
    }
    bool SpawnClear()
    {
        bool Cleared = false;
        int Count = 0;
        List<int> Dots = new List<int>();
        for (int i = 0; i < Num; ++i)
        {
            if (!m_oObjectList[i].GetCheck())
            {
                Count = 0;
                Dots.Clear();
                Search(i, ref Count, ref Dots);
                if (Count >= 3)
                {
                    Cleared = true;
                    foreach (int k in Dots)
                    {
                        m_oObjectList[k].SetKilled();
                    }
                }
            }
        }
        for (int i = 0; i < Num; ++i)
        {
            m_oObjectList[i].SetCheck(false);
        }
        return Cleared;
    }
    bool Clear()
    {
        bool Cleared = false;
        int Count = 0;
        List<int> Dots = new List<int>();
        int SoundCount = 0;
        for (int i = 0; i < Num; ++i)
        {
            if (!m_oObjectList[i].GetCheck())
            {
                Count = 0;
                Dots.Clear();
                Search(i, ref Count, ref Dots);
                if (Count >= 3)
                {
                    Cleared = true;
                    foreach (int k in Dots)
                    {
                        ++SoundCount;
                        m_oObjectList[k].SetKilled();
                    }
                    while (Count > 0)
                    {
                        Score.m_iScore += Count;
                        GameGlobals.WaitTimer += GameGlobals.WaitSpeed * Count;
                        --Count;
                    }
                }
            }
        }
        for (int i = 0; i < Num; ++i)
        {
            m_oObjectList[i].SetCheck(false);
        }
        if (Cleared)
        {
            foreach (AudioSource a in GetComponents<AudioSource>())
            {
                if (a.clip.name == SoundLib.GetSound(SoundLib.DotAlt).name)
                {
                    a.volume = Count * 0.175f;
                    a.Play();
                }
            }
        }
        return Cleared;
    }






}
