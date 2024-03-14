using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{   
    public GameObject Gun;
    public static float ofs;
    private Guns mgun;

    public void Start()
    {
        mgun = Gun.GetComponent<Guns>();
        
        if (mgun != null)
        {
            ofs = mgun.offset;
        }
        else
        {
            Debug.LogError("Guns component not found on the GameObject.");
        }
    }

    public void Update()
    {
        if (mgun != null)
        {
            ofs = mgun.offset;
        }
    }
}
