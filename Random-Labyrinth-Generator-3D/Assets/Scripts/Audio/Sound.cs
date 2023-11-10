using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//"https://forum.unity.com/threads/terminology-what-does-serializable-mean-in-unity-terms.546241/"
[System.Serializable] // it's exposed in Inspector AND the value its set to will be saved in the scene file. yapabilmek adýna whole class serialize edilir.
public class Sound
{
    public string sName;
    public AudioClip clip;
}
