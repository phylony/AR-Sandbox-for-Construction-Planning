﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationManager : MonoBehaviour {
	private Camera mainCamera;							//Main camera for UI to world position translation
	private float terrainMoveSpeed = .01f;				//Speed at which the user can pan the terrain for proper alignment
	private float terrainScaleSpeed = .05f;				//Speed at which the user can grow or shrink the terrain to real-object dimentions
	private Vector3 terrainPos = new Vector3(0,0,0);	//Tracks the previos position of the terrain for persistance

	[SerializeField]
	Transform UIPanel; 

	[SerializeField]
	TerrainManager terrainManager;

	[SerializeField]
	GameObject terrain;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
	}
	// Update is called once per frame
	void Update () {
		SubmitCalibration ();
		if (ModeManager.dMode == DisplayMode.Calibrate) {
			//Have arrow keys pan the terrain
			if (Input.GetKey (KeyCode.LeftArrow)) {
				moveTerrainLeft ();
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				moveTerrainRight ();
			
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				moveTerrainUp ();
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				moveTerrainDown ();
			}
			if (Input.GetKey (KeyCode.Equals)) {
				scaleTerrainUp ();
			}
			if (Input.GetKey (KeyCode.Minus)) {
				scaleTerrainDown ();
			}
		}
	}

	public void SubmitCalibration() {
		//Converts position of calibraiton window to correspond with the terrain's world position and applies the transformation to the terrain itself
		Vector3 UpperRight, LowerLeft;


		//Getting the positions of the corners of the UIPanel
		UpperRight = UIPanel.GetChild (0).GetChild (0).transform.position;
		LowerLeft = UIPanel.GetChild (0).GetChild (2).transform.position;

		//Getting UI positions from camera positions
		Vector3 panelLL = mainCamera.ScreenToWorldPoint(LowerLeft);
		Vector3 panelUR = mainCamera.ScreenToWorldPoint(UpperRight);

		//Applying transformation
		terrainManager.terrainMask.SetDimensions (panelLL, panelUR);
	}

	public void moveTerrainLeft()
	{
		terrainPos = terrainPos + new Vector3 (-terrainMoveSpeed,0,0);
		terrain.transform.position = terrainPos;

	}

	public void moveTerrainRight()
	{
		terrainPos = terrainPos + new Vector3 (terrainMoveSpeed,0,0);
		terrain.transform.position = terrainPos;
	}

	public void moveTerrainUp()
	{
		terrainPos = terrainPos + new Vector3 (0,0,terrainMoveSpeed);
		terrain.transform.position = terrainPos;
	}

	public void moveTerrainDown()
	{
		terrainPos = terrainPos + new Vector3 (0,0,-terrainMoveSpeed);
		terrain.transform.position = terrainPos;
	}

	public void scaleTerrainUp()
	{
		terrainManager.terrainGenerator.scale += terrainScaleSpeed;
	}

	public void scaleTerrainDown()
	{
		terrainManager.terrainGenerator.scale -= terrainScaleSpeed;
	}
}
