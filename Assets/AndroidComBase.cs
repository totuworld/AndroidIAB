using UnityEngine;
using System.Collections;
using System;

public class AndroidComBase : MonoBehaviour 
{
	string resultPrint = "ASKY";
	bool clicked = false;
	GUIStyle labelStyle = new GUIStyle();
	
	void Start()
	{
		labelStyle.fontSize = 30;
		labelStyle.normal.textColor = Color.white;
	}
	
	void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 300, 100), "Purchase"))
		{
			CallAndroid();
		}
        GUI.Label (new Rect (10, 200, 300, 20), resultPrint, labelStyle);
    }
	
	/// <summary>
	/// Unity using this method invokes Android
	/// </summary>
	void CallAndroid()
	{
		if( clicked || Application.platform != RuntimePlatform.Android) 
		{
			Debug.Log("Do not Execute this method");
			return;
		}
		clicked = true;
		resultPrint = "";
					
#if UNITY_ANDROID
		try
		{
			using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					Debug.Log("purchase"); 
					
					jo.Call("AnswerToUnity");
					
				}
			}
		}
		catch (Exception e)
		{
			Debug.Log(e.StackTrace);
		}
#endif
		
	}
	
	/// <summary>
	/// Android using the method returns the result. 
	/// </summary>
	void ResultFromAndroid(string result)
	{
		clicked = false;
		resultPrint = result;
	}
	
}
