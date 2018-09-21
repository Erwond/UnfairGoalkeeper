using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

	public Button btn1v1;
	public Button btnTournament;
	public Button btnRandom;
	public GameObject mainCam;
	public GameObject oneVOneCan;
	public GameObject tournamentCan;

	// Use this for initialization
	void Start ()
	{
		btn1v1.onClick.AddListener (() => GoToOneVOne ());
		btnTournament.onClick.AddListener (() => GoToTournament ());
		btnRandom.onClick.AddListener (() => LoadLevel ("Tutorial"));

	}
	
	// Update is called once per frame
	void Update ()
	{
			
	}

	private void GoToOneVOne ()
	{
		mainCam.transform.position = new Vector3 (oneVOneCan.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
	}

	private void GoToTournament ()
	{
		mainCam.transform.position = new Vector3 (mainCam.transform.position.x, tournamentCan.transform.position.y + mainCam.transform.position.y, mainCam.transform.position.z);
	}

	private void LoadLevel (string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}
