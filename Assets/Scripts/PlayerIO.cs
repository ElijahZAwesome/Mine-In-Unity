using UnityEngine;
using System.Collections;

public class PlayerIO : MonoBehaviour {

	public PlayerIO currentPlayerIO;
	public float maxInteractDistance = 8;
	public byte selectedInventory = 0;
	public bool resetCamera = false;
	public Vector3 campos;
	public Animator playerAnimator;
	public Animator handAnimator;
	public GameObject FPSGraph;
	public GameObject FPSText;
	public bool FPSDisplay = false;
	public GameObject Handcam;
	int layerMask = 1 << 8 | 1 << 9;
	public GameObject PauseMenu;

	// Use this for initialization
	void Start() {
		currentPlayerIO = this;
		layerMask = ~layerMask;
		/*
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 144;
        */
	}
	
	// Update is called once per frame
	void Update() {
		if (PauseMenu.activeSelf == false) {
			//if (gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer == false) {
			//	return;
			//}
			Debug.Log (FPSDisplay);
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				selectedInventory = 1;
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				selectedInventory = 2;
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				selectedInventory = 3;
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				selectedInventory = 4;
			}
			handAnimator.SetBool ("walking", Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D));
			playerAnimator.SetBool ("walking", Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D));
			if (GameObject.FindWithTag ("FPSController").transform.position.y < -20) {
				Debug.Log ("Player fell through world, resetting!");
				GameObject.FindWithTag ("FPSController").transform.position = new Vector3 (GameObject.FindWithTag ("FPSController").transform.position.x, 60, GameObject.FindWithTag ("FPSController").transform.position.z);
			}
			if (Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown (1) || Input.GetMouseButtonDown (2) || Input.GetKeyDown (KeyCode.C)) {
				Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0.5f));
				RaycastHit hit;
				float rayDistance = maxInteractDistance;
				if (!resetCamera) {
					rayDistance *= 3.14159f;
				}
				if (Physics.Raycast (ray, out hit, rayDistance, layerMask)) {
					Chunk chunk = hit.transform.GetComponent<Chunk> ();
					if (chunk == null) {
						return;
					}
					if (Input.GetMouseButtonDown (0)) {
						Vector3 p = hit.point;
						p -= hit.normal / 4;
						chunk.SetBrick (0, p);
					} 
					if (Input.GetMouseButtonDown (1)) {
						Vector3 p = hit.point;
						if (selectedInventory != 0) {
							p += hit.normal / 4;
							chunk.SetBrick (selectedInventory, p);
							Debug.Log ("placed block");
						}
					}
					if (Input.GetMouseButtonDown (2) || Input.GetKeyDown (KeyCode.C)) {
						Vector3 p = hit.point;
						p -= hit.normal / 4;
						selectedInventory = chunk.GetByte (p);
					}
				}
			}
			if (Input.GetKeyDown (KeyCode.F5)) {
				if (!resetCamera) {
					Camera.main.transform.localPosition -= Vector3.forward * 3.14159f;
					Camera.main.cullingMask = 1 << 0 | 1 << 1 | 1 << 2 | 1 << 3 | 1 << 3 | 1 << 4 | 1 << 5 | 1 << 6 | 1 << 7 | 1 << 8;
					Handcam.SetActive (false);
				} else {
					Camera.main.transform.position = transform.position;
					Camera.main.cullingMask = 1 << 0 | 1 << 1 | 1 << 2 | 1 << 3 | 1 << 3 | 1 << 4 | 1 << 5 | 1 << 6 | 1 << 7;
					Handcam.SetActive (true);
					Handcam.GetComponent<Camera> ().cullingMask = 1 << 9;
				}
				resetCamera = !resetCamera;
			}
			if (Input.GetKeyDown (KeyCode.F3)) {
				if (FPSGraph.activeSelf == true) {
					FPSGraph.transform.root.gameObject.SetActiveRecursively(false);
					FPSText.transform.root.gameObject.SetActiveRecursively(false);
					FPSDisplay = false;
					Debug.Log ("Turned off FPS");
				} else {
					FPSGraph.transform.root.gameObject.SetActiveRecursively(true);
					FPSText.transform.root.gameObject.SetActiveRecursively(true);
					FPSDisplay = true;
					Debug.Log ("Turned On FPS");
				}
			}
		}
	}
}