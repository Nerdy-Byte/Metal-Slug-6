using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Controler : MonoBehaviour
{
    public static UI_Controler instance;

    private void Awake(){
        instance = this;
    }

    public Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealth(int currentHealth, int maxHealth){
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
