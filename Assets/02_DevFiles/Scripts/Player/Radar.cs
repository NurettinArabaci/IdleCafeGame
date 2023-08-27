using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    
    private Transform mT;
    private PlayerController owner;
    private LayerMask layerMask;
    private float radius;


    public static Radar Spawn(GameObject parent, PlayerController owner, float radius, LayerMask layerMask)
    {
        Transform radarT = new GameObject("Radar").transform;
        radarT.parent = parent.transform;
        radarT.localPosition = Vector3.forward+Vector3.up;
        radarT.localScale = Vector3.one * 1.25f;
        radarT.localEulerAngles = Vector3.zero;
        radarT.gameObject.layer = LayerKeys.Radar;


        Radar radar = radarT.gameObject.AddComponent<Radar>();
        radar.owner = owner;
        radar.radius = radius;
        radar.tag = "Player";

        radar.layerMask = layerMask;
        radar.mT = radarT;


        SphereCollider collider = radarT.gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = radius;

        return radar;
    }

    public List<ICollectable> triggers { get; private set; } = new List<ICollectable>();
    public List<ICollectable> GetTriggers()
    {
        Collider[] cols = Physics.OverlapSphere(mT.position, radius, layerMask, QueryTriggerInteraction.Collide);
        triggers.Clear();

        for (int i = 0; i < cols.Length; i++)
        {
            ICollectable stack = cols[i].GetComponentInParent<ICollectable>();
            if (stack == null) continue;

            triggers.Add(stack);
        }
        return triggers;
    }

    public ICollectable GetNearest(Vector3 pos)
    {
        GetTriggers();

        if (triggers.Count == 0)
            return null;

        ICollectable nearest = null;
        float dist = float.MaxValue;

        for (int i = 0; i < triggers.Count; i++)
        {
             float temp = Vector3.SqrMagnitude(triggers[i].GetPose() - pos);
             if (temp < dist)
             {
                  dist = temp;
                  nearest = triggers[i];
             }
        }


        return nearest;
    }
    
}