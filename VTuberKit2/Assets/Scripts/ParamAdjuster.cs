using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework;

public class ParamAdjuster: MonoBehaviour
{
	public CubismModel _model;
   	private CubismParameter _param;
 
   	[SerializeField]
    	public string ParameterID = "";

	public float _value;

	void Start()
    	{
        	 _model = this.FindCubismModel();
 
        	_param = _model.Parameters.FindById(ParameterID);

		Debug.Log(_param.Value);
    	}

    	void LateUpdate()
    	{
		_param.Value=_value;
    	}

	public void SetParam(CubismModel Model, string ID, float Value)
	{
		_model = Model;
		ParameterID = ID;
		_value = Value;
	}
}
