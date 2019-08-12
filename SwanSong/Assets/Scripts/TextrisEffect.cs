using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class TextrisEffect : MonoBehaviour
{
    public float speed = 0.05f;
    [TextArea]
    public string message;
    public TextMeshProUGUI dialogueText;

    public bool canBeSped;  // Toggles whether holding a button will speed up text speed

    private List<SpecialCommand> specialCommands;

    public AudioSource audio;
    public AudioClip sfx;

    // Start is called before the first frame update
    void Start()
    {
        //AnimateDialogueBox(message);
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeSped)
        {
            //Simple controls to accelerate the text speed.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                speed = speed / 100;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                speed = 0.05f;
            }
        }

    }

    public void AnimateDialogueBox(string dialogue)
    {
        StartCoroutine(AnimateTextCoroutine(dialogue));
    }

    private IEnumerator AnimateTextCoroutine(string dialogue)
    {
        dialogueText.text = "";
        int i = 0;

        specialCommands = BuildSpecialCommandList(dialogue);
        string stripText = StripAllCommands(dialogue);

        while (i < stripText.Length)
        {
            if (specialCommands.Count > 0)
            {
                CheckCommand(i);
            }

            dialogueText.text += stripText[i];
            audio.PlayOneShot(sfx);
            i++;

            yield return new WaitForSeconds(speed);
        }
    }

    private string StripAllCommands(string text)
    {
        //Clean string to return.
        string cleanString;

        //Regex Pattern. Remove all "{stuff:value}" from our dialogue line.
        string pattern = "\\{.[^}]+\\}";

        cleanString = Regex.Replace(text, pattern, "");
        return cleanString;
    }

    private List<SpecialCommand> BuildSpecialCommandList(string text)
    {
        List<SpecialCommand> listCommand = new List<SpecialCommand>();

        string command = "";
        char[] braces = { '{', '}' };

        //Find start and end brace index
        for (int i = 0; i < text.Length; i++)
        {
            string currentChar = text[i].ToString();

            if (currentChar == "{")
            {
                while (currentChar != "}" && i < text.Length)
                {
                    currentChar = text[i].ToString();
                    command += currentChar;
                    text = text.Remove(i, 1); //Remove character to get the next
                }

                if (currentChar == "}")
                {
                    command = command.Trim(braces);
                    SpecialCommand newCommand = GetSpecialCommand(command);
                    newCommand.Index = i;
                    listCommand.Add(newCommand);

                    command = "";
                    i--;
                } else
                {
                    Debug.Log("Command error");
                }
            }
        }
        return listCommand;
    }

    private SpecialCommand GetSpecialCommand(string text)
    {
        SpecialCommand specialCommand = new SpecialCommand();
        string commandChar = "[:]";

        string[] match = Regex.Split(text, commandChar);

        //If there is a command beyond the name
        if (match.Length > 0)
        {
            for (int i = 0; i < match.Length; i++)
            {
                if (i == 0)
                {
                    specialCommand.Name = match[i];
                } else
                {
                    specialCommand.Values.Add(match[i]);
                }
            }
        } else
        {
            return null;
        }

        return specialCommand;

    }

    private void CheckCommand(int index)
    {
        for (int i = 0; i < specialCommands.Count; i++)
        {
            if (specialCommands[i].Index == index)
            {
                ExecuteCommand(specialCommands[i]);

                specialCommands.RemoveAt(i);
                i--;
            }
        }
    }

    private void ExecuteCommand(SpecialCommand command)
    {
        if (command == null)
        {
            return;
        }

        Debug.Log("Command" + command.Name + " found!");

        if (command.Name == "sound")
        {
            Debug.Log("SOUND IS MADE");
            dialogueText.fontStyle = FontStyles.Italic;
        }
        if (command.Name == "end")
        {
            Debug.Log("Noramlised");
            dialogueText.fontStyle = FontStyles.Normal;
        }
    }

    class SpecialCommand
    {
        public string Name { get; set; }

        public List<string> Values { get; set; }

        public int Index { get; set; }

        public SpecialCommand()
        {
            Name = "";
            Values = new List<string>();
            Index = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AnimateDialogueBox(message);
        }
    }
}
