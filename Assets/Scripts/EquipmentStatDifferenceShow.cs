using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EquipmentStatDifferenceShow : MonoBehaviour
{
    public GameObject hpArrow;
    public GameObject dmgArrow;
    public TMP_Text hpChange;
    public TMP_Text dmgChange;

    public static EquipmentStatDifferenceShow equipmentStatDifferenceShow;

    private void Awake()
    {
        equipmentStatDifferenceShow = this;
    }

    private void Start()
    {
        
    }

    public void ShowStatDifferences(int hpNew, int dmgNew, int hpOld=0, int dmgOld=0)
    {
        if (hpNew > hpOld)
        {
            hpChange.gameObject.SetActive(true);
            hpArrow.gameObject.SetActive(true);
            hpChange.text = (hpNew-hpOld).ToString();
            hpArrow.GetComponent<Image>().color = Color.green; 
            hpArrow.transform.rotation = Quaternion.Euler(180, 0, 0);
        }
        else if(hpNew<hpOld)
        {
            hpChange.gameObject.SetActive(true);
            hpArrow.gameObject.SetActive(true);
            hpChange.text = (hpNew - hpOld).ToString();
            hpArrow.GetComponent<Image>().color = Color.red;
            hpArrow.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            hpChange.gameObject.SetActive(true);
            hpChange.text = "0";
        }


        if (dmgNew > dmgOld)
        {
            dmgChange.gameObject.SetActive(true);
            dmgArrow.gameObject.SetActive(true);
            dmgChange.text = (dmgNew - dmgOld).ToString();
            dmgArrow.GetComponent<Image>().color = Color.green;
            dmgArrow.transform.rotation = Quaternion.Euler(180, 0, 0);
        }
        else if (dmgNew < dmgOld)
        {
            dmgChange.gameObject.SetActive(true);
            dmgArrow.gameObject.SetActive(true);
            dmgChange.text = (dmgNew - dmgOld).ToString();
            dmgArrow.GetComponent<Image>().color = Color.red;
            dmgArrow.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            dmgChange.gameObject.SetActive(true);
            dmgChange.text = "0";
        }

    }


}
