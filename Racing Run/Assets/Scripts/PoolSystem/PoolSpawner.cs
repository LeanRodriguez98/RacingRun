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
        RightArrow,
        Tutorial,
        TrainBarrierLeft,
        TrainBarrierRight
    };
    public enum TutorialEntities
    {
        LeftArrow,
        Cone,
        CrashBox,
        ToolBox,
        Nut
    };
    private ObjectPooler objectPoolerInstance;
    private LevelManager levelManagerInstance;
    private string tagOfObjectToSpawn;
    private float trainBarrierSpawnProbability = 0;
    public EntityToSpawn[] Entity;
    public Type typeEntity;
    public TutorialEntities tutorialTypeEntity;
    public string leftArrowTag;
    public string rightArrowTag;
    public string trainBarrierLeftTag;
    public string trainBarrierRightTag;
    public int minHeightSpawn = -2;
    public int maxHeightSpawn = 6;
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
                    Debug.LogError(this.gameObject.name + " have a probability summation of all entities diferent to 100%");
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

            case Type.TrainBarrierLeft:
                if (levelManagerInstance != null)
                    trainBarrierSpawnProbability = levelManagerInstance.getCurrentTrainBarrierSpawnPrbability();
                if (Random.Range(0, 100) < trainBarrierSpawnProbability)
                {
                    tagOfObjectToSpawn = trainBarrierLeftTag;
                    Invoke("SpawnObject", 0.1f);
                }
                
                

                break;

            case Type.TrainBarrierRight:
                if (levelManagerInstance != null)
                    trainBarrierSpawnProbability = levelManagerInstance.getCurrentTrainBarrierSpawnPrbability();
                if (Random.Range(0, 100) < trainBarrierSpawnProbability)
                {
                    tagOfObjectToSpawn = trainBarrierRightTag;
                    Invoke("SpawnObject", 0.1f);
                }

                break;
            case Type.Tutorial:
                switch (tutorialTypeEntity)
                {
                    case TutorialEntities.LeftArrow:
                        tagOfObjectToSpawn = leftArrowTag;
                        Invoke("SpawnObject", 0.1f);
                        break;   
                    case TutorialEntities.Cone:
                        tagOfObjectToSpawn = "Cone";
                        Invoke("SpawnObject", 0.1f);
                        break;
                    case TutorialEntities.CrashBox:
                        tagOfObjectToSpawn = "CrashBox";
                        Invoke("SpawnObject", 0.1f);
                        break;
                    case TutorialEntities.ToolBox:
                        tagOfObjectToSpawn = "ToolBox";
                        Invoke("SpawnObject", 0.1f);
                        break;
                    case TutorialEntities.Nut:
                        tagOfObjectToSpawn = "Nut";
                        Invoke("SpawnObject", 0.1f);
                        break;                  
                }
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
        if(levelManagerInstance.startToSpawnDelay < 0)
            objectPoolerInstance.SpawnForPool(tagOfObjectToSpawn, new Vector3(transform.position.x,transform.position.y + Random.Range(minHeightSpawn, maxHeightSpawn),transform.position.z), transform.rotation);        
    }

   
   
}

