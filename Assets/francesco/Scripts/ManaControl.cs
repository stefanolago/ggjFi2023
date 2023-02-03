using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManaControl : MonoBehaviour
{
    public int maxMana;
    public int currentMana;
    public int distanceToToggleOneMana;
    public float rootLenght = 0;
    public DrawManager drawManager;

    public bool temp;
    

    private void Update()
    {
        if(temp)
        {
            float[] pointDistances = drawManager.fingerPointsDistances.ToArray();
            float totalDistances = pointDistances.Sum();
            //calcolo togli punti
            int temporanea = (int)Mathf.Round(totalDistances / distanceToToggleOneMana);
            for(int i = 0; i < temporanea; i++)
            {
                currentMana--;
            }
            temp = false;
            
        }
    }




}
