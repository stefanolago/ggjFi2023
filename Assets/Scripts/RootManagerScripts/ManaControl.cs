using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManaControl : MonoBehaviour
{
    public int maxMana;
    public int currentMana;
    public int distanceToToggleOneMana;
    public DrawManager drawManager;

    public bool temp;

    private void Start()
    {
        currentMana = maxMana;
    }
    private void Update()
    {
        if(temp)
        {
            float[] pointDistances = drawManager.fingerPointsDistances.ToArray();
            float totalDistances = pointDistances.Sum();
            //calcolo togli punti
            Debug.Log(totalDistances);
            int temporanea = (int)Mathf.Round(totalDistances / distanceToToggleOneMana);

             currentMana = maxMana- temporanea;

            temp = false;
            
        }
    }

    public void ResetMana()
    {
        currentMana= maxMana;
        
    }




    






}
