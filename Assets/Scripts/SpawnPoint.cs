using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> moles;
    public bool isMole;

    private Spawner spawner;

    private void Start()
    {
        isMole = false;
        spawner = transform.parent.GetComponent<Spawner>();
    }

    public void MoleLose()
    {
        isMole=false;
        spawner.AddCanSpawnCount();
    }

    public void Spawn()
    {
        isMole = true;
        int idx = Random.Range(0, moles.Count);       
        Instantiate(moles[idx], transform.position, Quaternion.identity,transform);
    }
}
