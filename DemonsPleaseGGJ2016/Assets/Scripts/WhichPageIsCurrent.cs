﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhichPageIsCurrent : MonoBehaviour
{
	public int currentPage = 0;
	int maxPage = 0;
	bool bestiaryActive = false;
    bool summonActive = false;
    bool phoneActive = false;
	
	List<GameObject> pages = new List<GameObject>();
	public GameObject menuCanvas;
	public GameObject bestiaryCanvas;
    public GameObject summoningCanvas;
    public GameObject phoneCanvas;
	public GameObject page0;
	public GameObject page1;
	public GameObject page2;
	public GameObject page3;
	public GameObject page4;
	public GameObject page5;
	public GameObject page6;
	public GameObject page7;
	public GameObject page8;

	private AudioSource audioOut;

	public AudioClip openBook;
	public AudioClip closeBook;
	public AudioClip openPhone;
	public AudioClip closePhone;
	public AudioClip openSummon;
	public AudioClip closeSummon;
	public AudioClip flipPage;

	// Use this for initialization
	void Start ()
	{
		audioOut = GetComponent<AudioSource>();

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
	
	}

	//Call this on the click of the button assigned to the left side of the book
	public void FlipLeft (){
		if (currentPage > 0){
			currentPage = currentPage - 1;
			audioOut.PlayOneShot(flipPage);
		}
	}

	//Call this on the click of the button assigned to the right side of the book
	public void FlipRight () {
		if (currentPage < maxPage){
			currentPage = currentPage + 1;
			audioOut.PlayOneShot(flipPage);
            print("hello hello");
		}
	}

	//Call this one on the click of the button that activates the bestiary
	public void ActivateBestiary (){
		bestiaryActive = true;
		bestiaryCanvas.SetActive(true);
		menuCanvas.SetActive(false);
		audioOut.PlayOneShot(openBook);
	}

	//Call this one on the click of the button that deactivates the bestiary
	public void DeactivateBestiary (){
		bestiaryActive = false;
		bestiaryCanvas.SetActive(false);
		menuCanvas.SetActive(true);
		audioOut.PlayOneShot(closeBook);
	}

    //Call this one on the click of the button that activates the Summon BOok
    public void ActiveSummonerbook()
    {
        summonActive = true;
        summoningCanvas.SetActive(true);
        menuCanvas.SetActive(false);
		audioOut.PlayOneShot(openSummon);
    }

    //Call this one on the click of the button that deactivates the Summon Book
    public void DeactivateSummonerbook()
    {
        summonActive = false;
        summoningCanvas.SetActive(false);
        menuCanvas.SetActive(true);
		audioOut.PlayOneShot(closeSummon);
    }

    //Call this one on the click of the button that activates the Phone
    public void ActivePhone()
    {
        phoneActive = true;
        phoneCanvas.SetActive(true);
        menuCanvas.SetActive(false);
		audioOut.PlayOneShot(openPhone);
    }

    //Call this one on the click of the button that deactivates the Summon Book
    public void DeactivatePhone()
    {
        phoneActive = false;
        phoneCanvas.SetActive(false);
        menuCanvas.SetActive(true);
		audioOut.PlayOneShot(closePhone);
    }
}
	