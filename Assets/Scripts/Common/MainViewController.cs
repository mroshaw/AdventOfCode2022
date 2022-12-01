using System;
using UnityEngine;
using UnityEngine.UI;

namespace Santa.Workshop
{
    /// <summary>
    /// View controller for the main panel
    /// </summary>
    public class MainViewController : MonoBehaviour
    {
        [Header("Basic Configuration")]
        public Text resultText;
        public GameObject buttonTemplatePrefab;
        public GameObject buttonPanel;

        [Header("Solutions")]
        public EventSolution[] allSolutions;

        /// <summary>
        /// Set up the UI buttons and handlers
        /// </summary>
        private void Start()
        {
            DateTime today = DateTime.Now;
            int dayToday = today.Day;

            for(int currentDay = 1; currentDay <= dayToday; currentDay++)
            {
                GameObject newButtonGameObject = Instantiate(buttonTemplatePrefab);
                newButtonGameObject.GetComponentInChildren<Text>().text = $"Run day: {currentDay}";
                newButtonGameObject.transform.SetParent(buttonPanel.transform);
                newButtonGameObject.transform.localScale = new Vector3(1, 1, 1);
                Button newButton = newButtonGameObject.GetComponentInChildren<Button>();
                int clickSolIndex = currentDay - 1;
                newButton.onClick.AddListener(delegate { HandleButtonClick(clickSolIndex);});
            }
        }

        /// <summary>
        /// Run the "Day 1 Event" solution code and display the result
        /// </summary>
        public void HandleButtonClick(int day)
        {
            resultText.text = allSolutions[day].RetrieveSolution();
        }
    }
}