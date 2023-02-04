using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ManaControl : Singleton<ManaControl>
{
    public int maxMana = 100;
    public int currentMana { get; private set; }

    private List<ManaConsumer> manaConsumers = new List<ManaConsumer>();

    public TextMeshProUGUI displayText;


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
        displayText.text = "Mana: " + (currentMana > 0 ? currentMana : 0);
    }

    public void ResetMana()
    {
        currentMana= maxMana;
    }

    




    






}
