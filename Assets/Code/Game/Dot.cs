using UnityEngine;
using System.Collections;

public class Dot : Entity
{
    private bool m_bKilled;
    private float m_fKilledTimer;
    private bool m_bRemove;
    private bool m_bChecked;
    public override bool Init(float x, float y, int sx, int sy)
    {
        if (base.Init(x, y, sx, sy))
        {
            switch (IDrag.Random.GetRandom(0, 6))
            {
                case 0:
                    MyColor = GameGlobals.Red;
                    break;
                case 1:
                    MyColor = GameGlobals.Yellow;
                    break;
                case 2:
                    MyColor = GameGlobals.Brown;
                    break;
                case 3:
                    MyColor = GameGlobals.Green;
                    break;
                case 4:
                    MyColor = GameGlobals.Blue;
                    break;
                case 5:
                    MyColor = GameGlobals.Purple;
                    break;
                case 6:
                    MyColor = GameGlobals.WhiteGray;
                    break;
            }
            if (MyColor == ColorLib.GetColor(ColorLib.BlackGrays.Black))
            {
                Shaders = ShaderLib.GetShader(ShaderLib.InvColorTex);
            }
            m_bChecked = m_bKilled = m_bRemove = false;
            m_fLifeTimer = m_fKilledTimer = 0;
            return true;
        }
        return false;
    }
    public override bool Init(float x, float y, int sx, int sy, Color aColor)
    {
        if (base.Init(x, y, sx, sy, aColor))
        {
            if (GameInfo.GameType == GameInfo.Speed || GameInfo.GameType == GameInfo.Rush || GameInfo.GameType == GameInfo.ColorSwitch)
            {
                while (MyColor == aColor)
                {
                    switch (IDrag.Random.GetRandom(0, 6))
                    {
                        case 0:
                            MyColor = GameGlobals.Red;
                            break;
                        case 1:
                            MyColor = GameGlobals.Yellow;
                            break;
                        case 2:
                            MyColor = GameGlobals.Brown;
                            break;
                        case 3:
                            MyColor = GameGlobals.Green;
                            break;
                        case 4:
                            MyColor = GameGlobals.Blue;
                            break;
                        case 5:
                            MyColor = GameGlobals.Purple;
                            break;
                        case 6:
                            MyColor = GameGlobals.WhiteGray;
                            break;
                    }
                }
                if (MyColor == ColorLib.GetColor(ColorLib.BlackGrays.Black))
                {
                    Shaders = ShaderLib.GetShader(ShaderLib.InvColorTex);
                }
            }
            m_bShaked = m_bChecked = m_bKilled = m_bRemove = false;
            m_fLifeTimer = m_fKilledTimer = 0;
            return true;
        }
        return false;
    }

    // Update is called once per frame
    public override bool Update(bool Slow = false)
    {
        if (m_bRemove)
        {
            return false;
        }
        if (base.Update(Slow))
        {
            switch (GameInfo.GameType)
            {
                case GameInfo.Endless:
                    if (!Slow)
                    {
                        m_aRect.y -= GameGlobals.Speed * Time.deltaTime;
                    }
                    else
                    {
                        m_aRect.y -= GameGlobals.Speed * Time.deltaTime * 0.6f;
                    }
                    if (!m_bKilled && m_aRect.y < m_aRect.width * -0.5f)
                    {
                        if (!m_bBad)
                        {
                            GameGlobals.Lives--;
                            GameGlobals.WaitTimer += 0.5f;
                            GameGlobals.ShakeTime = 0.5f;
                        }
                        return false;
                    }
                    break;
                case GameInfo.Reverse:
                    if (!Slow)
                    {
                        m_aRect.y += GameGlobals.Speed * Time.deltaTime;
                    }
                    else
                    {
                        m_aRect.y += GameGlobals.Speed * Time.deltaTime * 0.5f;
                    }
                    if (!m_bKilled && m_aRect.y > Screen.height + m_aRect.width * 0.5f)
                    {
                        GameGlobals.Lives--;
                        GameGlobals.WaitTimer += 2.0f;
                        GameGlobals.ShakeTime = 0.75f;
                        return false;
                    }
                    break;
                case GameInfo.Speed:
                    m_fLifeTimer += Time.deltaTime;
                    if (m_fLifeTimer > 4.0f)
                    {
                        m_bShaked = true;
                        if (m_fLifeTimer > 7.0f)
                        {
                            m_bKilled = true;
                        }
                    }
                    break;
                case GameInfo.Rush:
                    break;
                case GameInfo.Motorway:
                    if (m_aRect.x < Screen.width * 0.5f)
                    {
                        if (!Slow)
                        {
                            m_aRect.y -= GameGlobals.Speed * Time.deltaTime;
                        }
                        else
                        {
                            m_aRect.y -= GameGlobals.Speed * Time.deltaTime * 0.5f;
                        }
                        if (!m_bKilled && m_aRect.y < m_aRect.width * -0.5f)
                        {
                            GameGlobals.Lives--;
                            GameGlobals.WaitTimer += 2.0f;
                            GameGlobals.ShakeTime = 0.75f;
                            return false;
                        }
                    }
                    else
                    {
                        if (!Slow)
                        {
                            m_aRect.y += GameGlobals.Speed * Time.deltaTime;
                        }
                        else
                        {
                            m_aRect.y += GameGlobals.Speed * Time.deltaTime * 0.5f;
                        }
                        if (!m_bKilled && m_aRect.y > Screen.height + m_aRect.width * 0.5f)
                        {
                            GameGlobals.Lives--;
                            GameGlobals.WaitTimer += 2.0f;
                            GameGlobals.ShakeTime = 0.75f;
                            return false;
                        }
                    }
                    break;
                case GameInfo.ColorRun:
                    if (!Slow)
                    {
                        m_aRect.x += GameGlobals.Speed * Time.deltaTime;
                    }
                    else
                    {
                        m_aRect.x += GameGlobals.Speed * Time.deltaTime * 0.5f;
                    }
                    if (!m_bKilled && m_aRect.x > Screen.width + m_aRect.width * 0.5f)
                    {
                        GameGlobals.Lives--;
                        GameGlobals.WaitTimer += 2.0f;
                        GameGlobals.ShakeTime = 0.75f;
                        return false;
                    }
                    break;
                case GameInfo.ColorSwitch:
                    break;
            }
            return true;
        }
        return false;
    }
    public override bool Draw(Color aColor = new Color(), ShaderData aShaderData = new ShaderData())
    {
        if (!m_bKilled)
        {
            if (base.Draw(aColor, aShaderData))
            {
                return true;
            }
        }
        else
        {
            m_fKilledTimer += Time.deltaTime;
            if (m_fKilledTimer > 1.0f)
            {
                m_bRemove = true;
            }
            else if (m_fKilledTimer > 0.6f && GameInfo.GameType == GameInfo.ColorSwitch)
            {
                m_bRemove = true;
            }
            else if (m_fKilledTimer > 0.4f)
            {
                m_aRect.width = 0;
                m_aRect.height = 0;
            }
            else
            {
                m_aRect.width += m_aRect.width * 0.5f * Time.deltaTime;
                m_aRect.height += m_aRect.height * 0.5f * Time.deltaTime;
            }
            MyColor.a -= MyColor.a * 2.5f * Time.deltaTime;
            base.Draw(MyColor * aColor, aShaderData);
        }
        return false;
    }
    public virtual bool Remove()
    {
        return m_bRemove;
    }
    public virtual float GetRad()
    {
        return m_aRect.width * 0.6f;
    }
    public virtual void SetKilled()
    {
        m_bKilled = true;
    }
    public virtual bool GetKilled()
    {
        return m_bKilled;
    }
    public virtual void SetCheck(bool aChecked)
    {
        m_bChecked = aChecked;
    }
    public virtual bool GetCheck()
    {
        return m_bChecked;
    }
}
