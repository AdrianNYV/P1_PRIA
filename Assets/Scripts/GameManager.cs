using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
   void Start() {
        //Insertamos la URL de la API
        StartCoroutine(GetRequest("https://opentdb.com/api.php?amount=10"));
    }

    IEnumerator GetRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            //Solicitamos y esperamos a la página en cuestion
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result) {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Este es el caso que necesitamos y nos interesa, creamos una función para inicializar
                    //en la que enviaremos los datos serializados de la API
                    ParserJson(webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    //Función con la que imprimiremos los datos solicitados
    private void ParserJson(string Json) {
        ClaseResponse claseResponse = JsonUtility.FromJson<ClaseResponse>(Json);

        //Imprimimos en la consola de Unity el número de preguntas que recibimos de la API
        Debug.Log("Nº de preguntas: " + claseResponse.results.Count);
        //Ahora imprimimos cuatro de los datos de una de las preguntas (la primera)
        //1º La Categoría
        Debug.Log("Caterogía: " + claseResponse.results[0].category);
        //2º El Tipo
        Debug.Log("Tipo de pregunta: " + claseResponse.results[1].type);
        //3º La Dificultad
        Debug.Log("Dificultad: " + claseResponse.results[2].difficulty);
        //4º La Pregunta
        Debug.Log("Pregunta: " + claseResponse.results[3].question);
    }
}
