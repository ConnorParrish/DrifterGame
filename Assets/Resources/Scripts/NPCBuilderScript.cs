using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCBuilderScript : MonoBehaviour {

    public GameObject obj;
    public Vector3 spawnPoint;
    public Transform p;

    public enum NPCs { Select, Pedestrian, PoliceOfficer, DrugDealer, FastFoodGuy, RecyclablesGuy }
    public enum Wealth { Select, Poor, MiddleClass, Wealthy }
    public enum Hats { None, Tophat }
    public enum LeftHandAccessories { None, Watch }
    public NPCs SpawnNPC = NPCs.Select;
    public Wealth NPCWealth = Wealth.Select;
    public Hats Hat;
    public LeftHandAccessories LeftHand;
    public Material Skin;
    //public GameObject Hat;

    public void BuildObject()
    {
        if (SpawnNPC == NPCs.Pedestrian) //&& NPCWealth == Wealth.Wealthy)
        {
            obj = Resources.Load<GameObject>("Prefabs/NPCs/WealthyPedestrian_M");
            obj.transform.GetChild(0).GetChild(6).GetComponent<SkinnedMeshRenderer>().material = Skin;
            if (Hat == Hats.Tophat)
            {
                GameObject hat = Resources.Load<GameObject>("Prefabs/Accessories/TopHat");
                Instantiate(hat, obj.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1));

            }
            if (LeftHand == LeftHandAccessories.Watch)
            {
                GameObject lHand = Resources.Load<GameObject>("Prefabs/Accessories/Watch");
                Instantiate(lHand, obj.transform.GetChild(0).GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0));
            }
            p = transform.GetChild(0);
        }
        else if (SpawnNPC == NPCs.PoliceOfficer)
        {
            obj = Resources.Load<GameObject>("Prefabs/NPCs/Cop");
            p = transform.GetChild(1);
        }
        else if (SpawnNPC == NPCs.DrugDealer)
        {
            obj = Resources.Load<GameObject>("Prefabs/NPCs/DrugDealer");
            p = transform.GetChild(2);
        }
        else if (SpawnNPC == NPCs.FastFoodGuy)
        {
            obj = Resources.Load<GameObject>("Prefabs/NPCs/FastFood");
            p = transform.GetChild(2);
        }
        else if (SpawnNPC == NPCs.RecyclablesGuy)
        {
            obj = Resources.Load<GameObject>("Prefabs/NPCs/RecyclablesBuyer");
            p = transform.GetChild(2);
        }

        Instantiate(obj, SceneView.lastActiveSceneView.camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f)), new Quaternion(), p);


    }



}
