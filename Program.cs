using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;



namespace InstrumentInventory2._0
{
    class Program
    {
        // Magic file name so I can keep code clean
        public static string DefaultInventory = "DefaultInventory.txt";
        public static string FileName = "Instrument.txt";

        static void Main(string[] args)
        {
            List<Instrument> instruments = Load();

            /// Main Method, displays list of instruments and possible actions to be taken.
            while (true)
            {
                
                DisplayInstruments(instruments);

                string input = Console.ReadLine();

                if (input.ToUpper() == "Q")
                {
                    return;
                }
                else if (input.ToUpper() == "N")
                {
                    var instrument = CreateNewInstrument();
                    instruments.Add(instrument);
                    Save(instruments);
                }
                else if (input.ToUpper() == "R")
                {
                    RemoveInstrument(instruments);
                    Save(instruments);
                }
                else if (int.TryParse(input, out int number) && number <= instruments.Count)
                {   
                    Instrument instrument = instruments[number - 1];
                    DisplayInstrumentDetails(instrument);

                    input = Console.ReadLine();
                    if (input.ToUpper() == "V")
                    {
                        UpdateValue(instrument, instruments);
                    }
                    else if (input.ToUpper() == "O")
                    {
                        UpdateOther(instrument, instruments);
                    }
                }
                else
                {
                    Console.WriteLine("I'm not sure that's  valid input, please try again. ");
                    Console.WriteLine("Press Enter to return. ");
                    Console.ReadLine();
                }
            }


        }

        /// <summary>
        /// Method to load the instrument inventory list
        /// </summary>
        /// <returns></returns>
        public static List<Instrument> Load()
        {
            try
            {
                string fileToLoad = DefaultInventory;

                if (File.Exists(FileName)) { 
                    fileToLoad = FileName; 
                }
                string jsonString = File.ReadAllText(fileToLoad);
                var instruments = JsonConvert.DeserializeObject<List<Instrument>>(jsonString);
                return instruments;
            }
            catch
            {
                Console.WriteLine("There were no saved instruments.");
                return new List<Instrument>();
            }   

        }   

        /// <summary>
        /// Method to save the updated instrument inventory list
        /// </summary>
        /// <param name="instruments"></param>
        public static void Save (List<Instrument> instruments)
        {
            string jsonString = JsonConvert.SerializeObject(instruments);
            File.WriteAllText(FileName, jsonString);
        }

        /// <summary>
        /// Method for adding new instruments to the inventory
        /// </summary>
        /// <returns></returns>
        public static Instrument CreateNewInstrument()
        {
            var instrument = new Instrument();
            Console.WriteLine("Enter Brand: ");
            instrument.Brand = Console.ReadLine();
            Console.WriteLine("Enter Model: ");
            instrument.Model = Console.ReadLine();

            Console.WriteLine("Enter Value: ");
            instrument.Value = Console.ReadLine();
            Console.WriteLine("Enter Other: ");
            instrument.Other = Console.ReadLine();
            return instrument;
        }        
        
        /// Allows user to remove an instrument from the list by selecting the model via its index.
        /// 
        public static void RemoveInstrument(List<Instrument> instruments)
        {
            Console.WriteLine("Please type the model you would like to remove from the list:");
            var input = Console.ReadLine();
            
            for (int xx = 0 ; xx < instruments.Count;xx++)
            {
                if (instruments[xx].Model == input)
                {
                    instruments.RemoveAt(xx);
                    break;
                }
            }
            
            Save(instruments);

        }


        /// Allows user to update the "value" field for their instrument
        /// 
        public static void UpdateValue(Instrument instrument, List<Instrument> instruments)
        {
            Console.WriteLine("Enter new value:");
            var input = Console.ReadLine();

            if (input != instrument.Value)
            {
                instrument.Value = input;
                Save(instruments);
            }
        }
        /// Allows user to update the "other" field for their instrument
        /// 
        public static void UpdateOther(Instrument instrument, List<Instrument> instruments)
        {
            Console.WriteLine("Enter new value for other");
            var input = Console.ReadLine();
           
            if (input != instrument.Other)
            {
                instrument.Other = input;
                Save(instruments);
            }
        }
        /// <summary>
        /// Shows the current details on a given instrument in the inventory.
        /// </summary>
        /// <param name="instrument"></param>
        public static void DisplayInstrumentDetails(Instrument instrument)
        {
            Console.Clear();
            Console.WriteLine("Press Enter to return to main menu.");
            Console.WriteLine("Press (V Enter) to edit current value field.");
            Console.WriteLine("Press (O Enter) to edit current other field.");
            Console.WriteLine($"Brand:   {instrument.Brand}");
            Console.WriteLine($"Model:   {instrument.Model}");
            Console.WriteLine($"Type:    {instrument.Type}");
            Console.WriteLine($"(V)alue: {instrument.Value}");
            Console.WriteLine($"(O)ther: {instrument.Other}");
        }
       /// <summary>
       /// /Main Menu display
       /// </summary>
       /// <param name="instruments"></param>
        public static void DisplayInstruments(List<Instrument> instruments)
        {
            Console.Clear();
            Console.WriteLine("__________");
            Console.WriteLine("Main Menu.");
            Console.WriteLine("▄▀▄▀▄▀▄▀▄▀");

            Console.WriteLine("Enter a Number to select Instrument.");
            Console.WriteLine("Press (N Enter) to add New Instrument.");
            Console.WriteLine("Press (R Enter) to remove an Insrument.");
            Console.WriteLine("Press (Q Enter) to Quit.");

            if (instruments.Count == 0)
            {
                Console.WriteLine("You don't have any instruments entered yet.");
                return;
            }
            
            for (int i = 0; i < instruments.Count; i++)
            {
                Instrument instrument = instruments[i];
                Console.WriteLine($"{i + 1}. {instrument}");
            }
        }

        public static List<T> GetEnumList<T>() where T : Enum
        {
            var enumValues = Enum.GetValues(typeof(T));
            return ((T[])enumValues).ToList();
        }
    }
}
