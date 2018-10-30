using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour {
    [System.Serializable]
    public struct EntityToSpawn
    {
        public string tagEntity;
        public float spawnProbability;
    };
    public enum Type
    {
        Entity,
        LeftArrow,
        RightArrow
    };
    private ObjectPooler objectPoolerInstance;
    private LevelManager levelManagerInstance;
    private string tagOfObjectToSpawn;
    public EntityToSpawn[] Entity;
    public Type typeEntity;
    public string leftArrowTag;
    public string rightArrowTag;
    private void OnEnable()
    {
        if(objectPoolerInstance == null)
            objectPoolerInstance = ObjectPooler.instance;
        if (levelManagerInstance == null)
            levelManagerInstance = LevelManager.instance;
        switch (typeEntity)
        {
            case Type.Entity:

                if (levelManagerInstance != null)
                    Entity = levelManagerInstance.getCurrenSpawnEntity();

                float probability = 0;
                for (int i = 0; i < Entity.Length; i++)
                {
                    probability += Entity[i].spawnProbability;
                }

                if (probability != 100)
                {
                    Debug.LogError(this.gameObject.name + " have a probability summation of all entities higher to 100%");
                }
                else
                {
                    probability = Random.Range(0, 100);
                    for (int i = 0; i < Entity.Length; i++)
                    {
                        if (probability < Entity[i].spawnProbability)
                        {
                            tagOfObjectToSpawn = Entity[i].tagEntity;
                            break;
                        }
                        else
                        {
                            probability -= Entity[i].spawnProbability;
                        }
                    }

                    Invoke("SpawnObject", 0.1f);
                }
                    break;

            case Type.LeftArrow:
                tagOfObjectToSpawn = leftArrowTag;
                Invoke("SpawnObject", 0.1f);

                break;

            case Type.RightArrow:
                tagOfObjectToSpawn = rightArrowTag;
                Invoke("SpawnObject", 0.1f);

                break;
            
            
        }
       
        
    }

    private void Start()
    {
        if (objectPoolerInstance == null)
            objectPoolerInstance = ObjectPooler.instance;
        if (levelManagerInstance == null)
            levelManagerInstance = LevelManager.instance;

    }

    public void SpawnObject()
    {
       objectPoolerInstance.SpawnForPool(tagOfObjectToSpawn, transform.position, Quaternion.identity);
        
    }

   
   
}

