using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class commandExtend_bgm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        commandManager.instance.commandData.Add("set10", new Action(set10));
        commandManager.instance.commandData.Add("set11", new Action(set11));
        commandManager.instance.commandData.Add("set12", new Action(set12));
        commandManager.instance.commandData.Add("set13", new Action(set13));
        commandManager.instance.commandData.Add("set13.3", new Action(set13three));
        commandManager.instance.commandData.Add("set13.5", new Action(set13half));
        commandManager.instance.commandData.Add("set13.6", new Action(set13six));
    }

    // this aint the most elegant of code, but im too lazy to reconfigure this to take in an argument
    public void set10()
    {
        bgmScript.instance.SetParameter(10);
    }

    public void set11()
    {
        bgmScript.instance.SetParameter(11);
    }

    public void set12()
    {
        bgmScript.instance.SetParameter(12);
    }

    public void set13()
    {
        bgmScript.instance.SetParameter(13);
    }

    public void set13three()
    {
        Debug.Log("13.3 has been set");
        bgmScript.instance.SetParameter(13.3f);
    }

    public void set13half()
    {
        bgmScript.instance.SetParameter(13.5f);
    }

    public void set13six()
    {
        bgmScript.instance.SetParameter(13.6f);
    }

}
