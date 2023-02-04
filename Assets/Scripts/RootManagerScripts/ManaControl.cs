using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManaControl : Singleton<ManaControl>
{
    public int maxMana;
    public int currentMana;
    public int distanceToToggleOneMana;
    public List<ManaConsumer> manaConsumers;

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
        if(usingMana)
        {
            /*float[] pointDistances = drawManager.fingerPointsDistances.ToArray();
            float totalDistances = pointDistances.Sum();
            //calcolo togli punti
            Debug.Log(totalDistances);
            int temporanea = (int)Mathf.Round(totalDistances / distanceToToggleOneMana);

             currentMana = maxMana- temporanea;

            usingMana = false;*/
            
        }
    }

    public void ResetMana()
    {
        currentMana= maxMana;
        
    }




    






}
