using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MyMonoBehaviour
{
    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Vector3> spawnPoints;
    protected Transform holder;
    protected int maxSpawn = 10;

    protected override void Start()
    {
        this.SpawnRandom();
    }

    protected override void LoadComponents()
    {
        this.LoadHolder();
        this.LoadPrefabs();
        this.LoadSpawnPoints();
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
            prefab.gameObject.SetActive(false);
        }
    }

    protected virtual void LoadSpawnPoints()
    {
        if (this.spawnPoints.Count > 0) return;

        Transform spawnpointObj = GameObject.Find("Road").transform;
        foreach (Transform point in spawnpointObj)
        {
            this.spawnPoints.Add(point.position);
        }
        this.spawnPoints.RemoveAt(0);
    }

    public virtual void SpawnRandom()
    {
        if (this.holder.childCount >= maxSpawn) return; 
        Transform newPrefab = Instantiate(RandomPrefab(), RandomSpawnPos(), Quaternion.identity, this.holder);
        newPrefab.gameObject.SetActive(true);
        Invoke(nameof(this.SpawnRandom), 0.1f);
    }

    public virtual Transform RandomPrefab()
    {
        int rand = Random.Range(0, this.prefabs.Count);
        return this.prefabs[rand];
    }

    public virtual Vector3 RandomSpawnPos()
    {   
        float randOffset = Random.Range(1f, 2.5f);
        int rand = Random.Range(0, this.spawnPoints.Count);
        return this.spawnPoints[rand] + new Vector3(randOffset, 0.3f, randOffset);
    }
}
