using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CanvasManagerScript : MonoBehaviour
    {
        private GameObject _canvasGameObject;
        public RawImage ActivePlayerRawImage;
        private GameObject _finishScreenGameObject;
        private GameObject _activePlayerDisplayGameObject;
        private TextMeshProUGUI _pointsOfPlayersTmp;

        private void Awake()
        {
            _canvasGameObject = Instantiate(Resources.Load<GameObject>("Prefabs/CanvasPrefab"));

            _activePlayerDisplayGameObject = _canvasGameObject.transform.Find("ActivePlayerDisplayGameObject").gameObject;
            ActivePlayerRawImage = _activePlayerDisplayGameObject.transform.Find("ActivePlayerImageGameObject").gameObject.GetComponent<RawImage>();

            _finishScreenGameObject = _canvasGameObject.transform.Find("FinishScreenGameObject").gameObject;
            _finishScreenGameObject.SetActive(false);

            _pointsOfPlayersTmp = _finishScreenGameObject.transform.Find("PointsOfPlayers").gameObject.GetComponent<TextMeshProUGUI>();           
        }
      
        public void ShowFinishScreen()
        {
            
            List<PlayerScript> playerList = GameSceneCoreScript.Instance.PlayerManagerScriptInstance.GetPlayerList();
            string esIstScheissEgal = "";           

            for (int i = 0; i < playerList.Count; i++)
            {
                esIstScheissEgal += $"{playerList[i].Name}: {GameSceneCoreScript.Instance.FieldManagerScriptInstance.GetFieldCount(playerList[i])}";
                
                if (i < playerList.Count - 1)
                {
                    esIstScheissEgal += "\n";
                }
            }

            _pointsOfPlayersTmp.text = esIstScheissEgal;

            _finishScreenGameObject.SetActive(true);
            _activePlayerDisplayGameObject.SetActive(false);
        }

        public void ShowStartScreen()
        {
            _finishScreenGameObject.SetActive(false);
            _activePlayerDisplayGameObject.SetActive(true);
        }
    }
}
