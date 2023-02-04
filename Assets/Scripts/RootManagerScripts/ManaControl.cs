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
        int consumedMana = 0;
        for (int i=0; i<manaConsumers.Count; i++)
        {
            consumedMana += manaConsumers[i].ManaConsumed();
        }
        Instance.currentMana = maxMana - consumedMana;
    }

    public void ResetMana()
    {
        currentMana= maxMana;
    }




    






}
