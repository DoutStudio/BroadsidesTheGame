﻿using UnityEngine;
using System.Collections.Generic;


public class ShipProperties : MonoBehaviour
{
    public float health = 0;
    public float hpBuff = 0;
    public float damageBuffMultiplier = 0;
    [Range(0,1)]
    public float attackSpeedMultiplier = 0;
    public float shieldFlatBuff = 0;
    public float healthFlatBuff = 0;
    [Range(0,1)]
    public float damageReductionPercentage = 0;
    [Range(0,1)]
    public float accuracyMultiplier = 0;

    public List<GameObject> modules;
    public GameObject captain;

    // Used to notify any subscribers if
    // module has been added or removed
    public delegate void AddRmModuleDel(bool wasAdded, GameObject module);
    public event AddRmModuleDel addRmEvent;

    // TODO: active abilities

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void ModuleHasBeenAttached(GameObject module)
    {
        modules.Add(module);
        module.GetComponent<ModuleCondition>().initCondition();
        module.GetComponent<ModuleEffect>().initEffect();

        if (addRmEvent != null)
        {
            addRmEvent.Invoke(true, module);
        }
    }

    public void ModuleHasBeenRemoved(GameObject module)
    {
        modules.Remove(module);
        module.GetComponent<ModuleCondition>().removeCondition();
        module.GetComponent<ModuleEffect>().removeEffect();

        if (addRmEvent != null)
        {
            addRmEvent.Invoke(false, module);
        }
    }

    public GameObject[] GetAllShipModules()
    {
        ConnectModuleScript[] pms = GetComponentsInChildren<ConnectModuleScript>();
        GameObject[] modules = new GameObject[pms.Length];
        for (int i = 0; i < pms.Length; ++i)
        {
            modules[i] = pms[i].gameObject;
        }
        return modules;
    }
}
