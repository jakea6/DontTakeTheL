using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Linq.Expressions;
using System.Collections.Generic;

public class Scene
{
    public string storyText;
    public string[] options;
    public int extraMethod;

    public Scene(string m_storyText, string[] m_options, int m_extraMethod = 0)
    {
        storyText = m_storyText;
        options = m_options;
        extraMethod = m_extraMethod;
    }
}

public class GM : MonoBehaviour
{
    public TMP_Text storyText;

    [SerializeField] GameObject[] optionButtons;
    [SerializeField] int buttonYPos;

    [SerializeField] Color[] plainsColours, savannahColours, jungleColours;

    enum Biome
    {
       Plains,
       Savannah,
       Jungle
    }
    enum Animal
    {
       Leopard,
       Leech,
       Llama,
       Lemur,
       Lobster,
       LeucophaeusFuliginosus,
       LeopardBlack
    }
    Animal animal;
    Biome biome;
#pragma warning disable IDE0052 // Remove unread private members
    bool begheeraFriend = false;
#pragma warning restore IDE0052 // Remove unread private members


    Dictionary<string, Scene> scenes;

    void Start()
    {
        CreateAllScenes();
        SetupScene(scenes["Starting scene"]);
    }

    void CreateAllScenes()
    {
        scenes = new Dictionary<string, Scene>()
        {
            {"Starting scene", new("You enter a bright new world, choose your animal: ", new string[6] { "Leopard", "Leech", "Llama", "Lemur", "Lobster", "Leucophaeus Fuliginosus" }) },
            {"Leopard", new("You chose Leopard! Where would you like to begin your adventure? ", new string[3] {"Plains", "Jungle", "Savannah"},1)},
            {"Leech", new("You chose Leech! Where would you like to begin your adventure? ", new string[3] {"Plains", "Jungle", "Savannah"},2)},
            {"Llama", new("You chose Llama! Where would you like to begin your adventure? ", new string[3] {"Plains", "Jungle", "Savannah"},3)},
            {"Lemur", new("You chose Lemur! Where would you like to begin your adventure? ", new string[3] {"Plains", "Jungle", "Savannah"},4)},
            {"Lobster", new("You chose Lobster! Where would you like to begin your adventure? ", new string[3] {"Plains", "Jungle", "Savannah"},5)},
            {"Leucophaeus Fuliginosus", new("You chose Leucophaeus Fuliginosus! Where would you like to begin your adventure? ", new string[3] {"Plains", "Jungle", "Savannah"},6)},
            {"Plains", new("You see a herd of wild horses. Do you approach them?", new string[2] {"Approach horses", "Continue journey"},7)},
            {"Approach horses", new("As you move toward them, they roll their eyes and trot around, do you attempt to tame one?", new string[2] {"Tame horse", "Continue journey"})},
            {"Tame horse", new("You chose a large black stallion but it easily turns around and kicks you", new string[1] {"Starting scene"}, 10)},
            {"Continue journey", new("xxx", new string[2] {"xxx", "xxx"})},
            {"Jungle", new("As you enter the jungle, you encounter Begheera", new string[2] {"Make friends with Begheera", "Continue journey"},8)},
            {"Make friends with Begheera", new("You now have a strong ally who can help you out in the future", new string[1]{"Continue journey"}, 11)},
            {"Play as a panther", new("You are now a black leopard", new string[1]{"Continue journey"},11)},
            {"Savannah", new("The sun scorches overhead, you must find a source of water to survive", new string[2] {"Search for water North", "Search for water South" },9)},
            {"Search for water North", new("As you climb a small hill, you see a huge expanse of desert ahead, there is also a small puddle at the base of the hill", new string[2]{"Continue searching North", "Search for water South"})},
            {"Continue searching North", new("You reach the base of the hill, the puddle is very muddy but you seem to notice a glimmer of blue in the desert", new string[2]{"Drink from muddy puddle", "Enter the desert"})},
            {"Drink from muddy puddle", new("You feel immediately refreshed but as you continue your journey, the world begins to tilt, you feel very ill", new string[1]{"Starting scene"})},
            {"Enter the desert", new("The heat is even more intense here", new string[2]{"Continue into the desert", "Search for water South"})},
            {"Continue into the desert", new("You begin to overheat but the faint blue in the distance seems to grow", new string[2]{"Search for the oasis", "Return to the Savannah"})},
            {"Search for the oasis", new("You begin to overheat but the faint blue in the distance seems to grow", new string[2]{"Search for the oasis", "Return to the Savannah"})},
            {"Return to the Savannah", new("The journey is difficult without any water", new string[2]{"Search for the oasis", "Return to the Savannah"})},
            {"Search for water South", new("You spend too many hours playing the game and die", new string[0])},
        };

    }

    void SetupScene(Scene scene)
    {
        storyText.SetText(scene.storyText);
        string[] m_options = scene.options;
        if (scene.extraMethod != 0)
        {
            switch (scene.extraMethod)
            {
                case 1:
                    animal = Animal.Leopard;
                    break;

                case 2:
                    animal = Animal.Leech;
                    break;

                case 3: 
                    animal = Animal.Llama;
                    break;

                case 4:
                    animal = Animal.Lemur;
                    break;

                case 5:
                    animal = Animal.Lobster;
                    break;

                case 6:
                    animal = Animal.LeucophaeusFuliginosus;
                    break;

                case 7:
                    biome = Biome.Plains;
                    if (!AnimalSurvivesBiome())
                    {
                        storyText.SetText("The " + animal + " did not survive in the plains");
                        AnimalDies();
                        return;
                    }
                    break;

                case 8:
                    biome = Biome.Jungle;
                    if (!AnimalSurvivesBiome())
                    {
                        storyText.SetText("The " + animal + " did not survive in the jungle");
                        AnimalDies();
                        return;
                    }
                    else if(animal == Animal.Leopard)
                    {
                        storyText.SetText("As you enter the jungle, you encounter Begheera. As a leopard, you can choose to switch to playing as a panther");
                        m_options = new string[2]{"Play as a panther", "Continue journey"};
                    }
                    break;

                case 9:
                    biome = Biome.Savannah;
                    if (!AnimalSurvivesBiome())
                    {
                        storyText.SetText("The " + animal + " did not survive in the savannah");
                        AnimalDies();
                        return;
                    }
                    break;
                case 10:
                    AnimalDies();
                    break;
                case 11:
                    if(animal == Animal.Leopard)
                    {
                        animal = Animal.LeopardBlack;
                    }
                    else if(Random.Range(0, 2) == 0)
                    {
                        print("roll of 0");
                        begheeraFriend = true;
                    }
                    else
                    {
                        storyText.SetText("Unfortunately, Begheera was a bit hungry today...");
                        AnimalDies();
                        return;
                    }
                    break;
            } 
        }

        for (int i = 0; i < 6; i++)
        {
            if (scene.options.Length > i)
            {
                string m_destinationScene = m_options[i];
                optionButtons[i].SetActive(true);
                optionButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                optionButtons[i].GetComponent<Button>().onClick.AddListener(delegate { OnOptionSelected(scenes[m_destinationScene]); });
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = scene.options[i];
            }
            else
            {
                optionButtons[i].SetActive(false);
            }
        }
    }

    void AnimalDies()
    {
        List<string> animalOptions = new(scenes["Starting scene"].options);
        int indexToRemove = animalOptions.IndexOf(animal.ToString());
        animalOptions.RemoveAt(indexToRemove);
        scenes["Starting scene"].options = animalOptions.ToArray();

        for (int i = 0; i < 6; i++)
        {
            if (i == 0)
            {
                optionButtons[i].SetActive(true);
                optionButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                optionButtons[i].GetComponent<Button>().onClick.AddListener(delegate { OnOptionSelected(scenes["Starting scene"]); });
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = "Starting scene";
            }
            else
            {
                optionButtons[i].SetActive(false);
            }
        }
    }

    bool AnimalSurvivesBiome()  
    {
        switch (biome)
        {
            case Biome.Plains:
                switch (animal) 
                {
                    case Animal.Leopard:
                        return true;
                    case Animal.Leech:
                        return false;
                    case Animal.Llama:
                        return true;
                    case Animal.Lemur:
                        return false;
                    case Animal.Lobster:
                        return false;
                    case Animal.LeucophaeusFuliginosus:
                        return true;
                }
                break;
            case Biome.Savannah:
                switch (animal)
                {
                    case Animal.Leopard:
                        return true;
                    case Animal.Leech:
                        return false;
                    case Animal.Llama:
                        return true;
                    case Animal.Lemur:
                        return true;
                    case Animal.Lobster:
                        return false;
                    case Animal.LeucophaeusFuliginosus:
                        return false;
                }
                break;
            case Biome.Jungle:
                switch (animal)
                {
                    case Animal.Leopard:
                        return true;
                    case Animal.Leech:
                        return true;
                    case Animal.Llama:
                        return false;
                    case Animal.Lemur:
                        return false;
                    case Animal.Lobster:
                        return false;
                    case Animal.LeucophaeusFuliginosus:
                        return true;
                }
                break;
        }
        print("no biome check");
        return true;
    }

    void OnOptionSelected(Scene destinationScene)
    {
        SetupScene(destinationScene);
    }
}
