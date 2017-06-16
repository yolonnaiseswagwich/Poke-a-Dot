using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Rush : BaseGame
{
    private Dot[] m_oObjectList;
    private int iSize;
    ///TEMPVAR
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        GameGlobals.TimeLeft = 30.0f;
        GameGlobals.WaitSpeed = 0.03f;
        GameGlobals.ShakeTime = 0;
        m_oObjectList = new Dot[20];
        Spawn();
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
                            a.pitch = 1 + Score.m_iCount * 0.15f;
                            a.Play();
                        }
                    }
                    m_oObjectList[Temp].SetKilled();
                    GameGlobals.WaitTimer += GameGlobals.WaitSpeed * Score.m_iCount;
                }
            }
#endif
            for (int i = 0; i < Input.touchCount; ++i)
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
                            a.pitch = 1 + Score.m_iCount * 0.15f;
                            a.Play();
                        }
                    }
                    m_oObjectList[Temp].SetKilled();
                    GameGlobals.WaitTimer += GameGlobals.WaitSpeed * Score.m_iCount;
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
        if (GameGlobals.TimeLeft < 0)
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
