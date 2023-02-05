using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ManaControl : Singleton<ManaControl>
{
    public int maxMana;
    public int capMana;
    public int currentMana { get; private set; }

    private List<ManaConsumer> manaConsumers = new List<ManaConsumer>();

    public TextMeshProUGUI manaText;


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

        if (manaText != null) { 
            manaText.text = "Mana: " + (currentMana > 0 ? currentMana : 0);
        }
    }

    public void AddMana(int manaToAdd)
    {
        if(maxMana < capMana)
        {
            maxMana += manaToAdd;
            if(maxMana >= capMana)
            {
                maxMana = capMana;
            }
        }
        
    }

    public void ResetMana()
    {
        currentMana= maxMana;
    }

    




    






}
