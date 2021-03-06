using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterStateIconSystem : MonoBehaviour
{
	[Header ("Main Settings")]
	[Space]

	public bool statesEnabled = true;

	public bool useIconsOnlyOnScreen;

	[Space]
	[Header ("Character States List Settings")]
	[Space]

	public List<characterStateInfo> characterStateInfoList = new List<characterStateInfo> ();

	[Space]
	[Header ("Debug")]
	[Space]

	public string currentCharacterStateName;
	public characterStateInfo currentCharacterState;

	public bool hideAfterTime;

	[Space]
	[Header ("Components")]
	[Space]

	public AudioSource mainAudioSource;
	public playerController playerControllerManager;
	public Transform mainCameraTransform;

	Vector3 directionToCamera;
	Transform currentIconGameObjectThirdPerson;

	bool currentIconGameObjectThirdPersonAssigned;

	void Start ()
	{
		if (mainCameraTransform == null) {
			gameManager mainGameManager = FindObjectOfType<gameManager> ();

			if (mainGameManager != null) {
				mainCameraTransform = mainGameManager.getMainCamera ().transform;
			}
		}
	}

	public void enableOrDisableCharacterIcon (characterStateInfo characterState, bool state)
	{
		if (!statesEnabled) {
			return;
		}

		if (playerControllerManager != null) {
			if (playerControllerManager.isPlayerOnFirstPerson () || playerControllerManager.isUsingDevice () || useIconsOnlyOnScreen) {
				characterState.iconGameObjectThirdPerson.SetActive (false);

				if (characterState.iconGameObjectFirstPerson != null) {
					characterState.iconGameObjectFirstPerson.SetActive (state);
				}
			} else {
				characterState.iconGameObjectThirdPerson.SetActive (state);

				if (characterState.iconGameObjectFirstPerson != null) {
					characterState.iconGameObjectFirstPerson.SetActive (false);
				}
			}
		} else {
			if (characterState.iconGameObjectFirstPerson != null) {
				characterState.iconGameObjectFirstPerson.SetActive (state);
			}

			characterState.iconGameObjectThirdPerson.SetActive (state);
		}
	}

	public void checkCharacterStateIconForViewChange ()
	{
		if (currentIconGameObjectThirdPerson != null) {
			enableOrDisableCharacterIcon (currentCharacterState, true);
		}
	}

	void Update ()
	{
		if (hideAfterTime) {
			if (Time.time > currentCharacterState.lastTimeHidden + currentCharacterState.hideAfterTimeAmount) {
				
				enableOrDisableCharacterIcon (currentCharacterState, false);

				hideAfterTime = false;
				currentCharacterState = null;
				currentIconGameObjectThirdPerson = null;

				currentIconGameObjectThirdPersonAssigned = false;

				currentCharacterStateName = "";
			}
		}

		if (currentIconGameObjectThirdPersonAssigned) {
			if (playerControllerManager != null) {
				if (!playerControllerManager.isPlayerOnFirstPerson () && !useIconsOnlyOnScreen) {
					directionToCamera = currentIconGameObjectThirdPerson.position - mainCameraTransform.position;

					currentIconGameObjectThirdPerson.rotation = Quaternion.LookRotation (directionToCamera);
				}
			} else {
				directionToCamera = currentIconGameObjectThirdPerson.position - mainCameraTransform.position;

				currentIconGameObjectThirdPerson.rotation = Quaternion.LookRotation (directionToCamera);
			}
		}
	}

	public void setCharacterStateIcon (string stateName)
	{
		if (!statesEnabled) {
			return;
		}

		if (currentCharacterStateName.Equals (stateName)) {
			return;
		}

		currentCharacterState = null;

		for (int i = 0; i < characterStateInfoList.Count; i++) {
			characterStateInfo temporalCharacterStateInfo = characterStateInfoList [i];

			if (temporalCharacterStateInfo.Name.Equals (stateName)) {

				currentCharacterState = temporalCharacterStateInfo;

				currentCharacterStateName = currentCharacterState.Name;

				currentIconGameObjectThirdPerson = currentCharacterState.iconGameObjectThirdPerson.transform;

				currentIconGameObjectThirdPersonAssigned = true;

				enableOrDisableCharacterIcon (currentCharacterState, true);

				if (currentCharacterState.hideAfterTime) {
					currentCharacterState.lastTimeHidden = Time.time;
					hideAfterTime = true;
				} else {
					hideAfterTime = false;
				}

				if (currentCharacterState.useSound) {
					playSound (currentCharacterState.stateClip);
				}
			} else {
				enableOrDisableCharacterIcon (temporalCharacterStateInfo, false);
			}
		}
	}

	public void disableCharacterStateIcon ()
	{
		//print ("disable states icon");
		for (int i = 0; i < characterStateInfoList.Count; i++) {
			enableOrDisableCharacterIcon (characterStateInfoList [i], false);
		}

		currentCharacterStateName = "";
	}

	public void playSound (AudioClip sound)
	{
		if (mainAudioSource != null) {
			mainAudioSource.PlayOneShot (sound);
		}
	}

	[System.Serializable]
	public class characterStateInfo
	{
		public string Name;
		public GameObject iconGameObjectThirdPerson;
		public GameObject iconGameObjectFirstPerson;
		public bool hideAfterTime;
		public float hideAfterTimeAmount;
		public float lastTimeHidden;
		public bool useSound;
		public AudioClip stateClip;
	}
}
