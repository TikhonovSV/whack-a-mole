using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Range(2,6)]
    public int size = 2;

    [SerializeField] 
    private new Camera camera;



    private void Start()
    {
        camera.fieldOfView = 40+10*size;
    }
}
