﻿using UnityEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;

/// <summary>
/// 目線の追従を行うクラス
/// </summary>
public class GazeControllerDlib : MonoBehaviour
{
	[SerializeField]
	public CubismModel _model;
   	private CubismParameter _paramEyeR;
	private CubismParameter _paramEyeL;
	private CubismParameter _paramMouth; 

   	[SerializeField]
    	public string EyeRParameterID = "";
	public string EyeLParameterID = "";
	public string MouthParameterID = "";


    //[SerializeField]
    Transform Anchor = null;
    Vector3 centerOnScreen;
    void Start()
    {
	    	_model = this.FindCubismModel();
 
        	_paramEyeR = _model.Parameters.FindById(EyeRParameterID);
		_paramEyeL = _model.Parameters.FindById(EyeLParameterID);
		_paramMouth = _model.Parameters.FindById(MouthParameterID);

	//	Debug.Log(_paramEyeR.Value);


       // centerOnScreen = Camera.main.WorldToScreenPoint(Anchor.position);


    }
    void LateUpdate()
    {
	    //UnityEngine.Debug.Log(Dlib.lookPos.x);
	    //UnityEngine.Debug.Log(Dlib.lookPos.y);
	var mousePos = Dlib.lookPos;
	//Debug.Log("mousePos "+mousePos);
       	//var mousePos = Input.mousePosition - centerOnScreen;
        UpdateRotate(new Vector3(mousePos.x, mousePos.y, 0) *30f);

	var IsLaugh = Dlib.laugh;
	if(IsLaugh)
	{
		Debug.Log("(IsLaugh)");
		CubismAutoEyeBlinkInput.IsOn =false;
		CubismEyeBlinkController.IsOn =false;
		//AniLipSync.Live2D.AnimMorphTarget.IsOn = false;
		//CubismEyeBlinkController.EyeOpening=0f;
		_paramEyeR.Value=0;
		_paramEyeL.Value=0;
	
	
		//_paramMouth.Value=1;
	}
	else
	{
		_paramEyeR.Value=1;
		_paramEyeL.Value=1;
		CubismAutoEyeBlinkInput.IsOn =true;
		CubismEyeBlinkController.IsOn =true;
		//AniLipSync.Live2D.AnimMorphTarget.IsOn = true;
		//CubismEyeBlinkController.EyeOpening=1f;


	
		

		//Debug.Log(_param.Value);
	}
    }
    Vector3 currentRotateion = Vector3.zero;
    Vector3 eulerVelocity = Vector3.zero;

    [SerializeField]
    CubismParameter HeadAngleX = null, HeadAngleY = null, HeadAngleZ = null;
    [SerializeField]
    CubismParameter EyeBallX = null, EyeBallY = null;
    [SerializeField]
    float EaseTime = 0.2f;
    [SerializeField]
    float EyeBallXRate = 0.05f;
    [SerializeField]
    float EyeBallYRate = 0.02f;
    [SerializeField]
    bool ReversedGazing = false;
    void UpdateRotate(Vector3 targetEulerAngle)
    {
	   // Debug.Log("UpdateRotate "+targetEulerAngle);
        currentRotateion = Vector3.SmoothDamp(currentRotateion, targetEulerAngle, ref eulerVelocity, EaseTime);
        // 頭の角度
        SetParameter(HeadAngleX, currentRotateion.x);
        SetParameter(HeadAngleY, currentRotateion.y);
        SetParameter(HeadAngleZ, currentRotateion.z);
        // 眼球の向き
        SetParameter(EyeBallX, currentRotateion.x * EyeBallXRate * (ReversedGazing ? -1 : 1));
        SetParameter(EyeBallY, currentRotateion.y * EyeBallYRate * (ReversedGazing ? -1 : 1));
    }
    void SetParameter(CubismParameter parameter, float value)
    {
        if (parameter != null)
        {
	//	Debug.Log("(parameter != null)");
            parameter.Value = Mathf.Clamp(value, parameter.MinimumValue, parameter.MaximumValue);
        }
    }
}

