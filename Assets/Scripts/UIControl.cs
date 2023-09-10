//Candidate for deletion. Check for references and remove
using UnityEngine;
using System.Collections;

public class UIControl : MonoBehaviour 
{
	public void ChangeScene(string sceneName)
	{
		Application.LoadLevel(sceneName);
	}

	public void quitGame()
    {
		Application.Quit(); 
    }
}
