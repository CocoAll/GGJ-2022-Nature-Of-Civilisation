using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Signal Sender",menuName = "ScriptableObjects/Signal Sender", order = 0)]
public class SignalSender : ScriptableObject
{
    public List<SignalListener> listeners = new List<SignalListener>();

    public void Raise()
    {
        for(int i = listeners.Count -1; i >=0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener signalListener)
    {
        listeners.Add(signalListener);
    }

    public void UnregisterListener(SignalListener signalListener)
    {
        listeners.Remove(signalListener);
    }
}
