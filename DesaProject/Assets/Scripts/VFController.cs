using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VFController : MonoBehaviour
{
    // Este es el Script q maneja la logica de las preguntas VF, Cambio, Verificacion ETC

    // Estos son los Atributos, las referencias para q funcione
    [SerializeField] private GameObject _question;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private JsonReader _questionList;

    // Este es el array de los generos escojidos estara aca de momento (Y publico para Testeo)
    [Tooltip("El id del genero debe ser el de 'el genero deseado' menos 1")]
    public int[] generosPregutas; // Nota cada objeto es el id del genero -1

    // Uso el start para consegir el texto de la pregunta y las posibles preguntas
    private void Start() {
        // Primero tomo el TMP (text mesh Pro) de la pregunta para luego cambiarlo
        _questionText = _question.GetComponentInChildren<TMP_Text>();

        // Luego tomo el array de preguntas leidas de Json
        _questionList = _question.GetComponent<JsonReader>();
    }

    // Esta es la funcion q se ejecuta al dar click en el boton (Cambia a una pregunta del Json)
    public void ChangeQuestionVF() {
        // Primero se escoje uno de los generos seleccionados
        int qstTopic = Random.Range(0, generosPregutas.Length);

        // Genero un random q sera el numero de la preunta del Genero (obio en rango de la cantidad de preguntas)
        int qstNum = Random.Range(0, _questionList.qst.preguntas[generosPregutas[qstTopic]].preguntas.Length);
        
        // Para luego cambiar el texto q se muestra
        _questionText.text = _questionList.qst.preguntas[generosPregutas[qstTopic]].preguntas[qstNum].pregunta;

        // Y dar la respuesta por consola (pa confirmar de momento)
        Debug.Log("Respuesta: " + GetAnswerVF(generosPregutas[qstTopic], qstNum));
    }

    // Esta funcion regresa si la respuesta correcta es verdadero o falso (True or False)
    public bool GetAnswerVF(int topic, int num) {
        // Primero separamos el string de respuesta en un array
        string[] answers = _questionList.qst.preguntas[topic].preguntas[num].opciones.Split(" || ");

        // Ahora se busca por el '*' q denota la respuesta correcta
        int i = 0; // denotador de si es 'V' o 'F'
        string search = "*"; // Caracter a buscar

        // el foreach con el if revisan si es el primero o segundo string el q tiene como ultimo caracter '*'
        foreach (string x in answers) {
            if (search.Contains(x.Substring(x.Length - 1))) {
                if (i == 0) { // Regresa q la respuesta es 'V'
                    return true;
                } else { // Regresa q la respuesta es 'F'
                    return false;
                }
            }

            i ++; // aumenta el denotador para segir evaluando
        }

        return  false; // Para q la funcion no pete (pero nunca llegara a el)
    }
}