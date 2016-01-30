using UnityEngine;
using System.Collections;

public class WhichPageIsCurrent : MonoBehaviour
{
	public int currentPage = 0;
	int maxPage = 0;
	bool bestiaryActive = false;
	
	List<GameObject> pages = new List<GameObject>();
	public GameObject bestiaryCanvas;
	public GameObject page0;
	public GameObject page1;
	public GameObject page2;
	public GameObject page3;
	public GameObject page4;
	public GameObject page5;
	public GameObject page6;
	public GameObject page7;
	public GameObject page8;
	public GameObject page9;

	// Use this for initialization
	void Start ()
	{
		if (page0){
			pages.Add (page0);
		}
		if (page1){
			pages.Add (page1);
		}
		if (page2){
			pages.Add (page2);
		}
		if (page3){
			pages.Add (page3);
		}
		if (page4){
			pages.Add (page4);
		}
		if (page5){
			pages.Add (page5);
		}
		if (page6){
			pages.Add (page6);
		}
		if (page7){
			pages.Add (page7);
		}
		if (page8){
			pages.Add (page8);
		}
		if (page9){
			pages.Add (page9);
		}
		foreach (GameObject page in pages){
			maxPage++;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Activates the page that should be active
		if (bestiaryActive == true && currentPage == 0){
			page0.SetActive(true);
		}
		if (bestiaryActive == true && currentPage == 1){
			page1.SetActive(true);
		}
		if (bestiaryActive == true && currentPage == 2){
			page2.SetActive(true);
		}
		if (bestiaryActive == true && currentPage == 3){
			page3.SetActive(true);
		}
		if (bestiaryActive == true && currentPage == 4){
			page4.SetActive(true);
		}
		if (bestiaryActive == true && currentPage == 5){
			page5.SetActive(true);
		}
		if (bestiaryActive == true && currentPage == 6){
			page6.SetActive(true);
		}
		if (bestiaryActive == true && currentPage == 7){
			page7.SetActive(true);
		}
		if (bestiaryActive == true && currentPage == 8){
			page8.SetActive(true);
		}
		if (bestiaryActive == true && currentPage == 9){
			page9.SetActive(true);
		}

		//Deactivates the pages that shouldn't be active
		if (currentPage != 0){
			page0.SetActive(false);
		}
		if (currentPage != 1){
			page1.SetActive(false);
		}
		if (currentPage != 2){
			page2.SetActive(false);
		}
		if (currentPage != 3){
			page3.SetActive(false);
		}
		if (currentPage != 4){
			page4.SetActive(false);
		}
		if (currentPage != 5){
			page5.SetActive(false);
		}
		if (currentPage != 6){
			page6.SetActive(false);
		}
		if (currentPage != 7){
			page7.SetActive(false);
		}
		if (currentPage != 8){
			page8.SetActive(false);
		}
		if (currentPage != 9){
			page9.SetActive(false);
		}
	}

	//Call this on the click of the button assigned to the left side of the book
	public void FlipLeft (){
		if (currentPage > 0){
			currentPage = currentPage - 1;
		}
	}

	//Call this on the click of the button assigned to the right side of the book
	public void FlipRight () {
		if (currentPage < maxPage){
			currentPage = currentPage + 1;
		}
	}

	//Call this one on the click of the button that activates the bestiary
	public void ActivateBestiary (){
		bestiaryActive = true;
		bestiaryCanvas.SetActive(true);
	}

	//Call this one on the click of the button that deactivates the bestiary
	public void DeactivateBestiary (){
		bestiaryActive = false;
		bestiaryCanvas.SetActive(false);
	}
}
	