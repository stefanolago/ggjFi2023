using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManaControl : Singleton<ManaControl>
{
    public int maxMana;
    public int currentMana;
    public int distanceToToggleOneMana;
    public List<ManaConsumer> manaConsumers = new List<ManaConsumer>();

    public bool usingMana;

    private void Start()
    {
        currentMana = maxMana;
    }

    public void RegisterAsManaConsumer(ManaConsumer manaConsumer)
    {
        manaConsumers.Add(manaConsumer);
    }

    public void RemoveAsManaConsumer(ManaConsumer manaConsumer)
    {
        manaConsumers.Remove(manaConsumer);
    }

    private void FixedUpdate()
    {
        /*if(usingMana)
        {
            float[] pointDistances = drawManager.fingerPointsDistances.ToArray();
            float totalDistances = pointDistances.Sum();
            //calcolo togli punti
            
            int temporanea = (int)Mathf.Round(totalDistances / distanceToToggleOneMana);

             currentMana = maxMana- temporanea;

            usingMana = false;
            
        }*/
        int consumedMana = 0;
        for (int i=0; i<manaConsumers.Count; i++)
        {
            consumedMana += manaConsumers[i].ManaConsumed();
        }
        Instance.currentMana = maxMana - consumedMana;

        Debug.Log("Current mana: " + Instance.currentMana);
    }

    public void ResetMana()
    {
        currentMana= maxMana;
    }




    






}
