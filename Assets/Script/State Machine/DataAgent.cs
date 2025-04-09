using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[System.Serializable]
public class Data
{
    [Range(0f, 1f)]  
    public float value;
    public float valueMax=1;
    public float time;
    public float timeRate;
    public float timeFrameRate =0;
    public Data() { 
    
    
    }
}
public class DataAgent : MonoBehaviour
{
    public Data Energy = new Data();
    public Data Hunger = new Data();
    public Data Sleep = new Data();
    public Data Bathroom = new Data();
    Coroutine CoroutineEnergy=null;

    public bool CantLoadEnergy { get => CoroutineEnergy == null; }

    IEnumerator LoadEnergyTime(float time)
    {

        while(time>0)
        {
            time--;
            Energy.value = Mathf.Lerp(Energy.value, Energy.valueMax, Time.deltaTime * 20f);
            yield return new WaitForSecondsRealtime(1);
        }
        Energy.value = Energy.valueMax;
        StopCoroutine(CoroutineEnergy);
        CoroutineEnergy = null;

    }
    public void LoadEnergy()
    {
        if(CoroutineEnergy==null)
            CoroutineEnergy = StartCoroutine(LoadEnergyTime(Energy.time));
    }

    public void DiscountEnergy()
    {
        if(Energy.timeFrameRate > Energy.timeRate)
        {
            Energy.timeFrameRate = 0;
            Energy.value-=0.03f;
        }
        Energy.timeFrameRate += Time.deltaTime;
    }
    public void UpdateStats()
    {
        UpdateStat(Hunger);
        UpdateStat(Sleep);
        UpdateStat(Bathroom);
    }

    private void UpdateStat(Data data)
    {
        if (data.timeFrameRate > data.timeRate)
        {
            data.timeFrameRate = 0;
            data.value += 0.05f;
            data.value = Mathf.Clamp01(data.value);
        }
        data.timeFrameRate += Time.deltaTime;
    }

}
