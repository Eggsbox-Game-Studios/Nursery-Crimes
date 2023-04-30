using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenePicker : MonoBehaviour
{
    [SerializeField]
    public string scenePath;

    private int GetSceneIndex(string scene)
	{
		if (scene != null)
		{
			return SceneUtility.GetBuildIndexByScenePath(scene);
		}
		return 0;
	}
    public void LoadScene()
	{
		if (scenePath != null)
		{
			int index = GetSceneIndex(scenePath);
			if (index != -1)
			{
				SceneManager.LoadScene(index);
			}
		}
	}
}