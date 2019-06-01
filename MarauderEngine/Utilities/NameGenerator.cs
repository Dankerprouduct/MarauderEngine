using System.Collections.Generic;

namespace MarauderEngine.Utilities
{
    public static class NameGenerator
    {

        public static List<string> firstNames = new List<string>()
        {
            "Cole",
            "Broderick",
            "Hunter",
            "Scotty",
            "Elvin",
            "Isaias",
            "Tobias",
            "Britt",
            "Harlan",
            "Raphael",
            "Essen",
            "Ruce",
            "Jimmy",
            "Markeith",
            "Patroy",
            "Dony",
            "Eveth",
            "Eustis",
            "Rege",
            "Billie",
            "Lynie",
            "Donne",
            "Ryna",
            "Kella",
            "Jeane",
            "Diane",
            "Deby",
            "Renie",
            "Rese",
            "Thera"
        };

        public static List<string> middleNames = new List<string>()
        {
            "Jammer",
            "Lien",
            "Voight",
            "Alers",
            "Sonoda",
            "Yueh",
            "Dituri"
        };

        public static List<string> lastNames = new List<string>()
        {
            "Drayton",
            "Hohstadt",
            "Locke",
            "Alder",
            "Chalet",
            "Rosek",
            "Burcham",
            "Dilucca",
            "Iavarone",
            "Haught",
            "Rogonz",
            "Marte",
            "Bennes",
            "Brookson",
            "Bellee",
            "Parker",
            "Gryante",
            "Colly",
            "Smorre",
            "Hezal",
            "Washy",
            "Coxand",
            "Hilly",
            "Grichy",
            "Hilley",
            "Milley",
            "Carte",
            "Heson",
            "Cotte",
            "Baily"

        };

        public static string GenerateFullName()
        {

            return GenerateFirstName() + " " + GenerateMiddleName() + " " + GenerateLastName();                         
        }

        public static string GenreateFirstLastName() 
        {

            return GenerateFirstName() + " " + GenerateLastName();
        }


        public static string GenerateFirstName()
        {
            int firstIndex = Game1.random.Next(0, firstNames.Count);
            return firstNames[firstIndex]; 
        }

        public static string GenerateMiddleName()
        {
            int middleIndex = Game1.random.Next(0, middleNames.Count);
            return middleNames[middleIndex]; 
        }

        public static string GenerateLastName()
        {
            int lastIndex = Game1.random.Next(0, lastNames.Count);
            return lastNames[lastIndex];
        }

    }
}
