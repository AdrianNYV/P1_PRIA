using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
   void Start() {
        // A correct website page.
        StartCoroutine(GetRequest("https://opentdb.com/api.php?amount=10"));
    }

    IEnumerator GetRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    ParserJson(webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    private void ParserJson(string Json) {
        ClaseResponse claseResponse = JsonUtility.FromJson<ClaseResponse>(Json);
        
        Debug.Log("Nº de preguntas: " + claseResponse.results.Count);
        Debug.Log("Caterogía: " + claseResponse.results[0].category);
        Debug.Log("Tipo de pregunta: " + claseResponse.results[1].type);
        Debug.Log("Dificultad: " + claseResponse.results[2].difficulty);
        Debug.Log("Pregunta: " + claseResponse.results[3].question);
    }
}
