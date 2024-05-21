using Cardealer;
using Cardealer.Models;
using System;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.ComponentModel;
using System.Data;
using System.Runtime.ConstrainedExecution;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;

namespace Cardealer
{
    class Program
    {
        static CarDealer dealer = new CarDealer(""); //"MyCarDealer"
        static Person? LoggedInPerson;
        static void Main(string[] args)
        {
            //dealer.CreatePerson("aron", "sucka", PersonType.Dealer, "gedepis"); Thomas smed den i cunstructoren CarDealer PGA. fejl programstartup  grundet createperson (max)

            while (true)
            {
                Login();

                Temp();
            }
        }


        #region -------------------------------- Cases --------------------------------
        static void Login()
        {
            while (true)
            {
                if (LoggedInPerson != null) 
                    break;

                Console.WriteLine("\n1. Login");
                Console.WriteLine("2. Create user\n");

                string loginCase = Console.ReadLine();
                Console.Clear();
                
                switch (loginCase)
                {
                    case "1":
                        HandleLogin();
                        break;

                    case "2":
                        HandleCreation();
                        break;
                }
            }
        }

        static void Temp()
        {
            /* Hovedmenu
            1. Car
            2. Profile
            3. New Person
            4. List of people
            5. Logout
            */

            Console.WriteLine("\n1. Car\n2. Profile\n3. New person\n4. List of people\n5. Logout\n");

            string mainCase = Console.ReadLine();
            Console.Clear();
            
            switch (mainCase)
            {
                case "1":
                    //Open Car Menu
                    CarCase();
                    break;

                case "2":
                    //Open profile menu
                    profileCase();
                    break;

                case "3":
                    //Create new person
                    HandleCreation();
                    break;

                case "4":
                    //Get list of persons
                    dealer.GetListOfPersons();
                    break;

                case "5":
                    Logout();
                    break;
                    
            }
            Console.WriteLine("Press any key to continue"); Console.ReadKey(); 
            Console.Clear();
        }

        static void CarCase()
        {
            Console.Clear();

            //CarMenu
            Console.WriteLine("\n1. Get car list");
            Console.WriteLine("2. Add new car");
            Console.WriteLine("3. Look up specific car");
            Console.WriteLine("4. Buy car");
            Console.WriteLine("5. Update specific car");
            Console.WriteLine("6. Remove car");
            Console.WriteLine("7. Back");

            string carCase = Console.ReadLine();

            switch (carCase)
            {
                case "1":
                    dealer.GetListOfCars();
                    break;

                case "2":
                    Console.WriteLine();
                    HandleCarCreation();
                    break;

                case "3":
                    Car car = new();      
                    do
                    {
                        IntChecker("\nInsert CarID\n", out int carId);
                        car = dealer.GetCarByID(carId);

                        if (car is null)
                            Console.WriteLine("Car does not exist. Try again.");


                    } while (car is null);

                    Console.Write("\nCarID: {0} \nCar: {1} {2} \nPrice: {3}kr\n", car.CarID, car.Brand, car.Model, car.Price);
                    break;

                case "4":
                    dealer.GetListOfCars();
                    IntChecker("Insert CarID\n", out int carID);
                    Console.Clear();
                    Console.WriteLine();
                    dealer.BuyCar(carID, LoggedInPerson);
                    break;

                case "5":
                    HandleUpdateCar();
                    break;

                case "6":
                    HandleDeleteCar();
                    break;

                case "7":
                    break;
            }
            
        }

        static void profileCase()
        {
            //Profile overview
            Console.WriteLine("Overview\n");
            Console.WriteLine("Name: {0}, {1}", LoggedInPerson.firstname, LoggedInPerson.lastname);
            Console.WriteLine("Type: " + LoggedInPerson.Type);
            Console.WriteLine("ID: " + LoggedInPerson.personID);
            Console.WriteLine("Balance: " + LoggedInPerson.balance);

            //Profile Menu
            Console.WriteLine("\n1. Add balance");
            Console.WriteLine("2. Your cars");
            Console.WriteLine("3. Namechange");
            Console.WriteLine("4. back\n");

            string profileswitch = Console.ReadLine();

            switch (profileswitch)
            {
                case "1":
                    double balance;
                    do Console.WriteLine("\nInsert funds\n");
                    
                    while (!double.TryParse(Console.ReadLine(), out balance));

                    dealer.AddBalance(LoggedInPerson.personID, balance);
                    break;

                case "2":
                    dealer.GetListOfBoughtCars(LoggedInPerson);
                    break;

                case "3":
                    string firstname = "x";
                    string lastname = "x";
                    
                        Console.WriteLine("Enter your new firstname. Minimum 2 characters. (Write back to go back)");
                        firstname = Console.ReadLine().Trim().ToLower();
                        if (firstname == "back".Trim().ToLower()) return;

                        while (firstname != "x" && firstname.Length < 2)
                        {
                            Console.WriteLine("Enter your new firstname. Please enter atleast 2 characters. (Write back to go back)");
                            firstname = Console.ReadLine().Trim().ToLower();
                            Console.Clear();
                            if (firstname == "back".Trim().ToLower()) return;
                            continue;
                        }

                        Console.WriteLine("Enter your new lastname. Minimum 2 characters. (Write back to go back)");
                        lastname = Console.ReadLine().Trim().ToLower();
                        if (lastname == "back".Trim().ToLower()) return;

                        while (lastname != "x" && lastname.Length < 2)
                        {
                            Console.WriteLine("Enter your new lastname. Please enter atleast 2 characters. (Write back to go back)");
                            lastname = Console.ReadLine().Trim().ToLower();
                            if (lastname == "back".Trim().ToLower()) return;
                            continue;
                        }

                        LoggedInPerson.firstname = firstname;
                        LoggedInPerson.lastname = lastname;

                        Console.WriteLine($"Your new name is {LoggedInPerson.firstname} {LoggedInPerson.lastname}");
                        break;

                case "4":
                    break;
            }
        }

        #endregion

        #region -------------------------------- Person --------------------------------
        static void HandleLogin()
        {
            IntChecker("\nenter your ID\n", out int input1);

            Console.WriteLine("\nWhat's ya password den?\n");
            string input2 = Console.ReadLine().Trim().ToLower();

            LoggedInPerson = dealer.CheckLogin(input1, input2);
            Console.Clear();
        }

        static void Logout()
        {
            Console.WriteLine("Press y to confirm logout");
            string confirmLogout = Console.ReadLine();

            if (confirmLogout.ToLower() == "y") 
                LoggedInPerson = null;

            Console.Clear();
        }

        static bool IntChecker(string consoleText, out int returnInt)
        {
            do
            {
                Console.WriteLine(consoleText);
            }
            while (!int.TryParse(Console.ReadLine(), out returnInt));
            return true;
        }

        static bool DoubleChecker(string consoleText, out double returnDouble)
        {
            do
            {
                Console.WriteLine(consoleText);
            }
            while (!double.TryParse(Console.ReadLine(), out returnDouble));
            return true;
        }

        static async void HandleCreation()
        {
            string input1 = "x";
            string input2 = "x";
            string input3 = "x";
            PersonType input4 = PersonType.Customer;
            

            do
            {
                Console.WriteLine("\nWhat is your name?\n");
                input1 = Console.ReadLine().Trim().ToLower();
                Console.Clear();
                if (input1 == "back".Trim().ToLower()) return;

                while (input1 != "x" && input1.Length < 2)
                {
                    Console.WriteLine("\nEnter firstname. Please enter atleast 2 characters. (Write back to go back)");
                    input1 = Console.ReadLine().Trim().ToLower();
                    Console.Clear();
                    if (input1 == "back".Trim().ToLower()) return;
                    continue;
                }

                Console.WriteLine("\nWhat is your lastname?\n");
                input2 = Console.ReadLine().Trim().ToLower();
                Console.Clear();
                if (input2 == "back".Trim().ToLower()) return;

                while (input2 != "x" && input2.Length < 2)
                {
                    Console.WriteLine("\nEnter lastname. Please enter atleast 2 characters. (Write back to go back)");
                    input2 = Console.ReadLine().Trim().ToLower();
                    Console.Clear();
                    if (input2 == "back".Trim().ToLower()) return;
                    continue;
                }

                Console.WriteLine("\nChoose a password mutafucka\n");
                input3 = Console.ReadLine().Trim().ToLower();
                Console.Clear();

                while (input3 != "x" && input3.Length < 2)
                {
                    Console.WriteLine("\nEnter password. Please enter atleast 2 characters. (Write back to go back)");
                    input3 = Console.ReadLine().Trim().ToLower();
                    Console.Clear();
                    if (input3 == "back".Trim().ToLower()) return;
                    continue;
                }
                Console.Clear();
                
                while (AccesManager(false))
                {
                    IntChecker("\nIf you are custommer insert 0. If you are dealer insert 1\n", out int tal);
                    if (!Enum.IsDefined(typeof(PersonType), tal)) continue;
                    input4 = (PersonType)tal;
                    break;
                }
                break;
            } while (true);
            
            //Console.WriteLine($"\nUser creation complete.\nUserID: {dealer.People.Max(x => x.personID) + 1}\n");
            dealer.CreatePerson(firstname: input1, lastname: input2, password: input3,username: "", type: input4);
        }

        /// <summary>
        /// Checker op på om man er dealer eller ikke. Printer Acces denied hvis bool -> <paramref name="print"/> er true
        /// </summary>
        /// <param name="print"></param>
        /// <returns></returns>
        static bool AccesManager(bool print)
        {
            if (print && LoggedInPerson.Type != PersonType.Dealer)
                Console.WriteLine("\nAcces Denied Mofo");

            return LoggedInPerson is not null && LoggedInPerson.Type == PersonType.Dealer;
        }

        #endregion

        #region -------------------------------- Cars --------------------------------
        static void HandleCarCreation()
        {
            if (LoggedInPerson.Type != PersonType.Dealer)
            {
                Console.WriteLine("\nOnly staff can add cars. Request not authorized\n");
                return;
            }

            Car car = new Car();

            car.Brand = ChooseCarEnum();

            Console.WriteLine("\nWhat model is the car?\n");
            car.Model = Console.ReadLine();

            do
            { Console.Clear();

                double price;
                DoubleChecker("\nWhat is the price of the car?\n", out price);
                if (price < 10000)
                {
                    Console.WriteLine("\nWe are a business. No Charity here. Enter acceptable price\n\nPress any key to try again\n");
                    Console.ReadKey();
                    continue;
                }
                car.Price = price;
                break;
            } while (true);


            Console.WriteLine("\nIs the car available in the store: press y for yes\n");
            string input2 = Console.ReadLine().ToLower();
            if (input2 != "y") car.InStock = false;

            dealer.AddCar(car);
            Console.WriteLine("\nCar sucessfully created\n");
        }

        static CarBrand ChooseCarEnum()
        {
            int result;
            do
            {
                foreach (CarBrand item in Enum.GetValues(typeof(CarBrand)))
                {
                    Console.WriteLine($"{(int)item}. {item}");
                }
            }
            while (!IntChecker("\nChoose cardbrand for new car\n", out result) || !Enum.IsDefined((CarBrand)result));
            return (CarBrand)result; 
        }

        static void HandleUpdateCar()
        {
            if (!AccesManager(true)) return;

            Car car = new Car();
            do
            {
                int carID;
                IntChecker("\nEnter CarID\n", out carID);
                car = dealer.GetCarByID(carID);
            } while (car is null);

            Console.WriteLine("\nUpdate Menu\n");
            Console.WriteLine("1. Update everything");
            Console.WriteLine("2. Update Brand");
            Console.WriteLine("3. Update Model");
            Console.WriteLine("4. Update Price");
            Console.WriteLine("5. Update InStock");

            Console.WriteLine("\nChoose one of many\n");
            string updateCarCase = Console.ReadLine();
            double price;

            switch (updateCarCase)
            {
                case "1":
                    Console.WriteLine("\nWhich Carbrand is the vehicle?\n");
                    car.Brand = ChooseCarEnum();

                    Console.WriteLine("\nInsert Model\n");
                    car.Model = Console.ReadLine();
                    
                    DoubleChecker("\nInsert price(kr)\n", out price);

                    car.Price = price;

                    Console.WriteLine("\nChange instock or not. Press y for instock\n");
                    string instock = Console.ReadLine();
                    if (instock.ToLower() != "y") car.InStock = false;
                    else car.InStock = true;
                    break;

                case "2":
                    Console.WriteLine("\nWhich Carbrand is the vehicle?\n");
                    ChooseCarEnum();
                    break;

                case "3":
                    Console.WriteLine("\nInsert model\n");
                    car.Model = Console.ReadLine();
                    break;

                case "4":
                    DoubleChecker("\nInsert price(kr)\n", out price);
                    car.Price = price;
                    break;

                case "5":
                    Console.WriteLine("\nChange instock or not. Press y for instock\n");
                    string instock1 = Console.ReadLine();
                    if (instock1.ToLower() != "y") car.InStock = false;
                    else car.InStock = true;
                    break;

                default:
                    Console.WriteLine("\nWrong input. Try again\n");
                    break;

            }
            Console.WriteLine("\nPress any key to continue.\n"); Console.ReadKey();
        }

        static void HandleDeleteCar() 
        {
            if (!AccesManager(true)) 
                return;

            dealer.GetListOfCars();

            Console.WriteLine("\nEnter CarID of the car you wish to remove\n");
            int carID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(dealer.DeleteCarByID(carID));

            return;
        }

        #endregion

    }
}