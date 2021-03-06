using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsernameFunctions : MonoBehaviour
{
    // game objects and elements

     private List<string> adjectives = new List<string>()
     {
        "Active",
        "Adventurous",
        "Agile",
        "Alert",
        "Amusing",
        "Angry",
        "Annoyed",
        "Athletic",
        "Aware",
        "Bashful",
        "Beaming",
        "Beautiful",
        "Big",
        "Bitter",
        "Blissful",
        "Brave",
        "Brilliant",
        "Busy",
        "Calm",
        "Capable",
        "Cautious",
        "Challenging",
        "Charming",
        "Cheerful",
        "Chilly",
        "Chocolatey",
        "Clever",
        "Cloudy",
        "Compassionate",
        "Considerate",
        "Cozy",
        "Cranky",
        "Creative",
        "Crispy",
        "Crunchy",
        "Dangerous",
        "Daring",
        "Dark",
        "Delicate",
        "Delightful",
        "Ecstatic",
        "Elated",
        "Empty",
        "Endless",
        "Enormous",
        "Entertaining",
        "Equal",
        "Exhausted",
        "Fantastic",
        "Flexible",
        "Fluffy",
        "Freezing",
        "Frenetic",
        "Funny",
        "Furious",
        "Fussy",
        "Generous",
        "Gentle",
        "Gigantic",
        "Glad",
        "Gleeful",
        "Gorgeous",
        "Graceful",
        "Harmonious",
        "Icky",
        "Icy",
        "Infinite",
        "Intelligent",
        "Jaded",
        "Jolly",
        "Jovial",
        "Joyful",
        "Joyous",
        "Jumpy",
        "Kind",
        "Kindly",
        "Knowledgeable",
        "Large",
        "Lazy",
        "Left",
        "Light",
        "Likely",
        "Lousy",
        "Loyal",
        "Lucky",
        "Lumpy",
        "Marvellous",
        "Mean",
        "Minty",
        "Mysterious",
        "Naive",
        "Nervous",
        "New",
        "Nice",
        "Nimble",
        "Optimistic",
        "Oval",
        "Peaceful",
        "Petite",
        "Pleasant",
        "Pleased",
        "Polite",
        "Precise",
        "Pretty",
        "Proud",
        "Quick",
        "Quiet",
        "Rainy",
        "Relaxing",
        "Restful",
        "Right",
        "Serene",
        "Shocking",
        "Short",
        "Simple",
        "Skilful",
        "Slow",
        "Small",
        "Soothing",
        "Sour",
        "Sparkling",
        "Speedy",
        "Spiky",
        "Still",
        "Straight",
        "Strong",
        "Stubborn",
        "Stunning",
        "Sunny",
        "Swift",
        "Tall",
        "Terrified",
        "Thrilled",
        "Timid",
        "Tiny",
        "Tranquil",
        "Tricky",
        "Truthful",
        "Whimsical",
        "Wise",
        "Young"

     };
    private List<string> colours = new List<string>()
    {
        "Black",
        "Blue",
        "Bronze",
        "Brown",
        "Burgundy",
        "Copper",
        "Coral",
        "Crimson",
        "Cyan",
        "Emerald",
        "Fuchsia",
        "Gold",
        "Gray",
        "Green",
        "Indigo",
        "Ivory",
        "Khaki",
        "Lavendar",
        "Lilac",
        "Lime",
        "Magenta",
        "Maroon",
        "Navy",
        "Orange",
        "Peach",
        "Red",
        "Silver",
        "Teal",
        "Turquoise",
        "Violet",
        "White",
        "Yellow"

    };
    private List<string> animals = new List<string>()
    {
        "Alligator",
        "Alpaca",
        "Antelope",
        "Ape",
        "Armadillo",
        "Baboon",
        "Badger",
        "Bat",
        "Bear",
        "Bee",
        "Bison",
        "Boar",
        "Buffalo",
        "Butterfly",
        "Cat",
        "Cattle",
        "Cheetah",
        "Chicken",
        "Cod",
        "Coyote",
        "Crow",
        "Deer",
        "Dinosaur",
        "Dog",
        "Dolphin",
        "Donkey",
        "Dove",
        "Duck",
        "Eagle",
        "Eel",
        "Elephant",
        "Elk",
        "Emu",
        "Falcon",
        "Ferret",
        "Finch",
        "Fish",
        "Flamingo",
        "Fly",
        "Fox",
        "Frog",
        "Gerbil",
        "Giraffe",
        "Goat",
        "Goose",
        "Gorilla",
        "Grasshopper",
        "Grouse",
        "Guineapig",
        "Gull",
        "Hamster",
        "Hawk",
        "Hedgehog",
        "Heron",
        "Hippopotamus",
        "Hog",
        "Hornet",
        "Horse",
        "Hound",
        "Hummingbird",
        "Hyena",
        "Jellyfish",
        "Kangaroo",
        "Koala",
        "Lark",
        "Leopard",
        "Lion",
        "Llama",
        "Magpie",
        "Mallard",
        "Mole",
        "Monkey",
        "Moose",
        "Mosquito",
        "Mouse",
        "Mule",
        "Nightingale",
        "Ostrich",
        "Otter",
        "Owl",
        "Ox",
        "Oyster",
        "Panda",
        "Parrot",
        "Penguin",
        "Pheasant",
        "Pig",
        "Pigeon",
        "Platypus",
        "Porpoise",
        "Possum",
        "Rabbit",
        "Raccoon",
        "Rat",
        "Raven",
        "Reindeer",
        "Rhinoceros",
        "Seal",
        "Shark",
        "Sheep",
        "Snake",
        "Sparrow",
        "Spider",
        "Squid",
        "Squirrel",
        "Swan",
        "Tiger",
        "Toad",
        "Trout",
        "Turkey",
        "Turtle",
        "Walrus",
        "Wasp",
        "Weasel",
        "Whale",
        "Wolf",
        "Wombat",
        "Woodpecker",
        "Wren",
        "Yak",
        "Zebra"
    };
    // Start is called before the first frame update
    
        public void GenerateUsername()
    {
        // generate a random number between 1 and the max number of username combos
        System.Random rnd = new System.Random();
        int usernameNum = rnd.Next(1, adjectives.Count * colours.Count * animals.Count);
        
        string name = GetNthUsername(usernameNum, adjectives, colours, animals);
        // get the username for this number and assign it to the username textbox
        usernameField.text = name;

        UserInfo.Instance.username = name;

    }

  /// Get the n-th unique user name constructed using one element from each of the given lists in order
  public static string GetNthUsername(int n, List<string> pt1, List<string> pt2, List<string> pt3)
  {
    // The maximum number of unique names possible
    int maxNames = pt1.Count * pt2.Count * pt3.Count;
    // If n is bigger than the maximum number of names (minus 1), add a counter to the end of the generated name
    int overflowLoops = n / maxNames;
    string overflowId = overflowLoops > 0 ? "-"+overflowLoops.ToString() : "";

    // Get the correct index for each list, given the current ID number n
    n = n % maxNames;
    int index1 = n / (pt2.Count * pt3.Count);
    int index2 = (n - index1 * pt2.Count * pt3.Count) / pt3.Count;
    int index3 = (n - (index1 * pt2.Count * pt3.Count) - (index2 * pt3.Count));

    return pt1[index1] + "-" + pt2[index2] + "-" + pt3[index3] + overflowId;
  } 
     
}
