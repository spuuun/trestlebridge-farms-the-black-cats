using System;
using System.Collections.Generic;
using System.Linq;
using Trestlebridge.Interfaces;
using Trestlebridge.Models;
using Trestlebridge.Models.Animals;

namespace Trestlebridge.Actions
{
    public class ChooseGrazingField
    {
        public static void CollectInput(Farm farm, IGrazing animal)
        {
            Console.Clear();

            if (farm.GrazingFields.Count() == 0 || farm.GrazingFields.Where(field => field.Animals.Count == field.Capacity).ToList().Count == farm.GrazingFields.Count())
            {
                Console.WriteLine("There are no available grazing fields. Try creating a new one.");
                Console.WriteLine("Press return... or else");
                Console.ReadLine();
            }
            else
            {


                for (int i = 0; i < farm.GrazingFields.Count; i++)
                {
                    // Only display grazing fields that have room
                    if (farm.GrazingFields[i].Animals.Count < farm.GrazingFields[i].Capacity)
                    {
                        Console.WriteLine("=================================================");
                        Console.WriteLine($"{i + 1}. Grazing Field. Current Animal Count: {farm.GrazingFields[i].Animals.Count}");

                        // Group animals by their type to display their counts
                        List<IGrouping<string, IGrazing>> groupedAnimals = farm.GrazingFields[i].Animals.GroupBy(anml => anml.Type).ToList();

                        foreach (IGrouping<string, IGrazing> anml in groupedAnimals)
                        {
                            Console.WriteLine($"{anml.Key}: {anml.Count()}");
                        }
                        Console.WriteLine("=================================================");
                    }
                }

                Console.WriteLine();

                // How can I output the type of animal chosen here?
                Console.WriteLine($"Place the {animal.GetType().ToString().Split(".")[3]} where?");

                Console.Write("> ");
                try
                {


                    int choice = Int32.Parse(Console.ReadLine());


                    if (farm.GrazingFields[choice - 1].Animals.Count < farm.GrazingFields[choice - 1].Capacity)
                    {
                        farm.GrazingFields[choice - 1].AddResource(animal);
                    }
                    else if (farm.GrazingFields.Where(field => field.Animals.Count < field.Capacity).ToList().Count > 0)
                    {
                        Console.Write("Facility is full. Please select another facility. Press any key to continue...");
                        Console.ReadLine();
                        ChooseGrazingField.CollectInput(farm, animal);
                    }
                    else
                    {
                        Console.Write("All facilities full. Press any key to continue...");
                        Console.ReadLine();
                    }
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Please enter one of the specified options...\nI love you.\nPress return to continue");
                    Console.ReadLine();
                    ChooseGrazingField.CollectInput(farm, animal);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    Console.WriteLine("The grazing field you selected does not exist\nPress return to continue");
                    Console.ReadLine();
                    ChooseGrazingField.CollectInput(farm, animal);
                }

            }
        }
    }
}