﻿using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Managers {
	public class SceneManager : Singleton<SceneManager> {

		public List<TankSetting> TankSettings = new List<TankSetting>();
		
		private void Start() {
			LoadScene("Menu");
		}

		private Dictionary<string, object> _parameters;
		
		public void UnloadScene(string scene) {
			if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName(scene).isLoaded) return;
			StartCoroutine(UnloadSceneAsync(scene));
		}
		
		public void LoadScene(string scene) {
			if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(scene).isLoaded) return;
			StartCoroutine(LoadSceneAsync(scene));
		}

		public void ReloadScene(string scene) {
			StartCoroutine(ReloadSceneAsync(scene));
		}

		public IEnumerator ReloadSceneAsync(string scene) {
			yield return UnloadSceneAsync(scene);
			yield return LoadSceneAsync(scene);
		}
		
		private IEnumerator UnloadSceneAsync(string scene) {
			AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
			//Wait until the last operation fully loads to return anything
			while (!asyncLoad.isDone) {
				yield return null;
			}
		}

		private IEnumerator LoadSceneAsync(string scene) {
			AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
			//Wait until the last operation fully loads to return anything
			while (!asyncLoad.isDone) {
				yield return null;
			}
		}
		
		public object GetParam(string paramKey) {
			if (_parameters == null) return null;
			return _parameters.ContainsKey(paramKey) ? _parameters[paramKey] : null;
		}
 
		public void SetParam(string paramKey, object paramValue) {
			if (_parameters == null)
				_parameters = new Dictionary<string, object>();
			RemoveParam(paramKey);
			_parameters.Add(paramKey, paramValue);
		}

		public void RemoveParam(string paramKey) {
			if (_parameters.ContainsKey(paramKey))
				_parameters.Remove(paramKey);
		}

	}
}