using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
	#region Attributes

	Image hpSlider;
	Image hpOverlay;
	private int hpValue;
	#endregion

	#region Methods

	void SetHealth(int health)
	{
		hpValue += -health;
		if (health > 0)
		{
			hpSlider.fillAmount = health;
			if (health > 0 && health < 20)
			{
				hpSlider.color = Color.red;
			}
		}	
	}
	#endregion

	#region Unity

	// Start is called before the first frame update
	void Start()
    {
        hpSlider = GameObject.Find("HPSlider").GetComponent<Image>();
		hpOverlay = GameObject.Find("HPOverlay").GetComponent<Image>();
    }
	#endregion
}
