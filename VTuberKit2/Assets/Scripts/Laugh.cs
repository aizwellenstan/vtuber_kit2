using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;

public class Laugh : MonoBehaviour
{
	public CubismModel _model;
	[SerializeField]
    	public string ParameterID = "";
	public float _value;

	ParamAdjuster _paramAdjuster = new ParamAdjuster();

    // Start is called before the first frame update
    void Start()
    {
//	ParamAdjuster _paramAdjuster = new ParamAdjuster();
    }

    // Update is called once per frame:
    void Update()
    {
	    	_paramAdjuster.SetParam(_model,ParameterID , _value);

        //_paramAdjuster.SetParam("ParamEyeROpen", 0);
    }

    	void LateUpdate()
	{
		//_paramAdjuster.SetParam(_model,ParameterID , _value);
		 //_paramAdjuster.SetParam(_model,"ParamEyeROpen", 0);
	}
}
