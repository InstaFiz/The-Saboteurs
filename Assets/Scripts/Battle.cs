using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Threading.Tasks;

public class Battle : MonoBehaviour
{
    // Stress bar
    public GameObject stressBarObject;
    private Slider stressBar;

    // Text
    public GameObject tmpObject;
    private TextMeshProUGUI tmp;

    // Button array
    public GameObject buttons;

    // Delay between each task in miliseconds
    private int delay = 500;

    async void Start()
    {
        stressBar = stressBarObject.GetComponent<Slider>();
        stressBar.value = 0.6f;

        tmp = tmpObject.GetComponent<TextMeshProUGUI>();

        await TypewriteText("A new saboteur appears!");
        await Task.Delay(delay);
        // EnemyTurn would be called here, so the player can have at least one negative thought to assess
    }

    // Turn functions
    async void PlayerTurn(string action)
    {
        buttons.SetActive(false);
        string nextMessage = "Default Message";
        float stressDiff = 0.4f;

        /*
         * A case statement would go here that modifies the stress difference and next message depending on the "action" parameter
         * Each button should execute PlayerTurn, with a different value for "action" being passed depending on which button is pressed
         * This would be influenced by the current saboteur being fought
         */

        await TypewriteText(nextMessage);
        await Task.Delay(delay);
        await StressChange(stressDiff);
        await Task.Delay(delay);
        
        /*
         * If stress is 0, you win.
         * If stress is full, you lose.
         * Otherwise, call EnemyTurn
         */
    }

    async void EnemyTurn()
    {
        string nextMessage = "Default Message";

        /*
         * Each saboteur has their own set of negative thoughts. The game will cycle through each of these thoughts every emeny turn and type it out.
         * It will be assigned to nextMessage.
         */

        await TypewriteText(nextMessage);
        await Task.Delay(delay);
        await StressChange(0.1f);
        await Task.Delay(delay);

        /*
         * If stress is 0, you win.
         * If stress is full, you lose.
         * Otherwise, bring up the battle buttons again so the player can make their next move.
         */
    }

    // Async tasks, for use in turn functions
    async Task TypewriteText(string message)
    {
        tmp.maxVisibleCharacters = 0;
        tmp.text = message;

        while (tmp.maxVisibleCharacters < tmp.text.Length)
        {
            tmp.maxVisibleCharacters++;
            await Task.Delay(50);
        }

        await Task.Yield();
    }

    async Task StressChange(float difference)
    {
        stressBar.value += difference;
        await Flicker(stressBarObject);

        await Task.Yield();
    }

    // Utility tasks
    async Task Flicker(GameObject gameObject)
    {
        int flickerAmount = 0;

        while (flickerAmount < 14)
        {
            if (flickerAmount % 2 == 0)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);

            flickerAmount++;
            await Task.Delay(50);
        }

        await Task.Yield();
    }
}