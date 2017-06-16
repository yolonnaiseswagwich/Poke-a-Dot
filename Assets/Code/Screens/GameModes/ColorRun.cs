using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class ColorRun : BaseGame
{
    private List<Dot> m_oObjectList;
    private float m_fSpawnTimer;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        GameGlobals.Speed = (float)Screen.width * 0.08f;
        GameGlobals.SpawnSpeed = 0.25f * (float)Screen.width / GameGlobals.Speed;
        GameGlobals.WaitSpeed = 0.05f;
        GameGlobals.Lives = 3;
        GameGlobals.ShakeTime = 0;
        m_oObjectList = new List<Dot>();
        Spawn(5);
    }
    protected override void Update()
    {
        base.Update();
        Shake();
        if (timer > 4.5f)
        {
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
                    m_oObjectList[Temp].SetKilled();
                    GameGlobals.WaitTimer += GameGlobals.WaitSpeed * Score.m_iCount;
                }
                foreach (AudioSource a in GetComponents<AudioSource>())
                {
                    if (a.clip.name == SoundLib.GetSound(SoundLib.Dot).name)
                    {
                        a.pitch = 1 + Score.m_iCount * 0.15f;
                        a.Play();
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
            }
            if (!(GameGlobals.WaitTimer > 0.0f))
            {
                if (m_fSpawnTimer >= GameGlobals.SpawnSpeed)
                {
                    GameGlobals.Speed += Screen.width * 0.0015f;
                    m_fSpawnTimer -= GameGlobals.SpawnSpeed;
                    GameGlobals.SpawnSpeed = 0.25f * (float)Screen.width / GameGlobals.Speed;
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
        fHeight = 0.15f * (float)Screen.height;
        for (int i = 0; i < iCount; ++i)
        {
            for (int j = 0; j < 6; ++j)
            {
                Dot Temp2 = new Dot();
                //pass into size x the actuall size of the circle pass into size y for the size of the dot (for speed purposes)
                Temp2.Init((int)((i - 4) * fWidth), Screen.height - (int)((j + 1.0f) * fHeight), (int)(iSize * 0.95f), (int)(iSize * 0.95f));
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
