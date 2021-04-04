using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statusIndicator : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthbar;
    [SerializeField]
    private Text healthtext;

    
    // Start is called before the first frame update
    void Start()
    {
        if (healthbar == null)
        {
            Debug.LogError("Helathbar not found");
        }
        if (healthtext == null)
        {
            Debug.LogError("healthtext not found");
        }
    }

    public void setHelth(int _cur, int _max)
    {
        Image im = healthbar.GetComponent<Image>();
        Text t = healthtext.GetComponent<Text>();
        

        if (_cur <= _max * 0.25)
        {
            im.color = new Color(255, 0, 0);
            t.color = new Color(255, 0, 0);
        }
        else if (_cur <= 0.5 * _max) { 
            im.color = new Color(229, 207, 0);
            t.color = new Color(229, 207, 0);
            

        }
        float _value = (float)_cur / _max;

        healthbar.localScale = new Vector3(_value, healthbar.localScale.y, healthbar.localScale.z);

        healthtext.text = _cur + "/" + _max + "HP";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
