using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ObjectPlacement : MonoBehaviour
{

	public bool editMode;

	public Text editPlayButtonText;

	public GameObject[] defencePrefabs;
	private int selectedPrefab;
	public Transform minXObject;

	private Vector3 mousePos;

	public LayerMask allTilesLayer;
	public LayerMask groundLayer;

	private float allowedHeight;
	private bool airPlacementAllowed;

	private DatabaseReference dbReference;
	private int objectNumber;
	// Use this for initialization
	void Start ()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://statuevandalism.firebaseio.com/");
		dbReference = FirebaseDatabase.DefaultInstance.RootReference;

		FirebaseDatabase.DefaultInstance
			.GetReference ("UserID").Child ("Testing")
			.GetValueAsync ().ContinueWith (task => {
			if (task.IsFaulted) {
				// Handle the error...
			} else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
					Debug.Log((double)snapshot.Child ("1").Child ("xAxis").GetValue(false));
					/*ObjectInScene myObject = JsonUtility.FromJson<ObjectInScene>(snapshot.Child ("1").Child ("xAxis").GetRawJsonValue);
					Debug.Log("A");*/
				//Vector2 aa = new Vector2(a, a);
			}
		});

		selectedPrefab = 0;
		allowedHeight = 1;
		airPlacementAllowed = defencePrefabs [selectedPrefab].GetComponent <DefenceController> ().airPlacementAllowed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (editMode) {
			mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position = new Vector2 (Mathf.Round (mousePos.x), Mathf.Round (mousePos.y));
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit2D rayHit = Physics2D.Raycast (transform.position, Vector2.zero, Mathf.Infinity, allTilesLayer);

				if (!rayHit.collider && ((Physics2D.Raycast (transform.position, Vector2.down, allowedHeight, groundLayer) && transform.position.x > minXObject.position.x + 1) || airPlacementAllowed)) {
					Instantiate (defencePrefabs [selectedPrefab], transform.position, Quaternion.identity);
					objectNumber++;
					AddObjectToDatabase ((double)transform.position.x, (double)transform.position.y, (double)selectedPrefab, objectNumber);
				}
			}

			if (Input.GetMouseButtonDown (1)) {
				selectedPrefab++;
				if (selectedPrefab > defencePrefabs.Length - 1)
					selectedPrefab = 0;
				airPlacementAllowed = defencePrefabs [selectedPrefab].GetComponent <DefenceController> ().airPlacementAllowed;
			}
		}

	}

	public void SwitchGameMode ()
	{
		editMode = !editMode;
		if (editMode) {
			editPlayButtonText.text = "Play";
		} else {
			editPlayButtonText.text = "Edit";
		}
	}

	private void AddObjectToDatabase (double xAxis, double yAxis, double defencePrefab, int id)
	{
		ObjectInScene objectTest = new ObjectInScene (xAxis, yAxis, defencePrefab);
		string json = JsonUtility.ToJson (objectTest);
		dbReference.Child ("UserID").Child ("Testing").Child ("" + id).SetRawJsonValueAsync (json);

	}
}

public class ObjectInScene
{
	public double xAxis;
	public double yAxis;
	public double defencePrefab;

	public ObjectInScene ()
	{
	
	}

	public ObjectInScene (double xAxis, double yAxis, double defencePrefab)
	{
		this.xAxis = xAxis;
		this.yAxis = yAxis;
		this.defencePrefab = defencePrefab;
	}
}
