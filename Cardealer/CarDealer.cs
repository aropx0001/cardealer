using Cardealer.Models;
using System.Security.Cryptography.X509Certificates;

namespace Cardealer;
public class CarDealer
{
    public  List<Car> Cars { get; set; }

    public List<Person> People { get; set; }

    string firmanavn;

    public CarDealer(string firmanavn)
    {
        People = new List<Person>();
        People.Add(new Dealer(1, "aron", "sucka", "aropx0001", "gedepis"));
        People.Add(new Dealer(2, "emil", "sensei","emilx0001", "gedepis"));
        People.Add(new Dealer(3, "thomas", "hjælpelærer", "thomx007", "gedepis"));
        this.firmanavn = firmanavn;
        LoadCars();
    }

    public string GetCarDealerName()
    {
        return firmanavn;
    }

    public int CreatePerson(string firstname, string lastname, PersonType type, string username, string password)
    {
        int noget = People.Max(x => x.personID);

        switch (type)
        { 
            case PersonType.Dealer:
                People.Add(new Dealer(++noget, firstname, lastname, username, password));
                break;

            case PersonType.Customer:
                People.Add(new Customer(++noget, firstname, lastname, username, password));
                break;
        }
        return noget;
    }

    public string DeletePersonByID(int id)
    {
        People.Remove(People.Find(x => x.personID == id));
        return "Bruger slettet";
    }

    public string AddBalance(int id, double? balance)
    {
        
        if (balance < 0)
        {
            if (balance <= 0) Console.WriteLine("\nTransaction failed. Try again\n");
            return "0";
        }
        double? newBalance = People.Find(x => x.personID == id).balance += balance;
        return newBalance.ToString();
    }

    public string CheckBalance(int id, double? balance)
    {
        balance = People.Find(x => x.personID == id).balance;
        return balance.ToString();
    }

    public Person GetPersonById(int id)
    {
        return People.FirstOrDefault(person => person.personID == id);
    }

    public Person CheckLogin(int personID, string password)
    {
        if (People.Any(x => x.personID == personID && x.password == password))
            return GetPersonById(personID);

        Console.WriteLine("\nWrong input. Press any key to continue\n");
        Console.ReadKey();
        return null;
    }

    public Person HtmlLogin(string username, string password)
    {
        return People.FirstOrDefault(x => x.username == username && x.password == password);
    }

    public void GetListOfPersons()
    {
        if (People != null)
        {
            foreach (Person human in People)
            {
                //Console.WriteLine("{0}\n{1}\n{2}\n{3}\n", human.firstname, human.lastname, human.Type, human.personID, human.balance);

                Console.WriteLine($"\nID: {human.personID}\nType: {human.Type}\nFirstname: {human.firstname}\nLastname: {human.lastname}\n");
            }
        }
        else Console.WriteLine("The List is empty");
    }

    public List<Person> GetDealers()
    {
        return People.Where(x => x.Type == PersonType.Dealer).ToList();
    }

    public int AddCar(Car newCar)
    {
        newCar.CarID = Cars.Max(x => x.CarID) + 1;
        Cars.Add(newCar);
        return newCar.CarID;
    }
    //Fix handle
    public string DeleteCarByID(int carID)
    {
        Cars.Remove(Cars.Find(x => x.CarID == carID));
            return "\nCar deleted\n";
    }

    public string UpdateCar(Car updateCar)
    {
        Car carToUpdate = Cars.FirstOrDefault(car => car.CarID == updateCar.CarID);
        if (carToUpdate == null) 
            return "Car not found";

        carToUpdate = updateCar;
        return "Car updated";
    }

    public Car GetCarByID(int carID)
    {
        Car car;
        car = Cars.FirstOrDefault(x => x.CarID == carID);

        return car;
    }

    public void GetListOfCars()
    {
        foreach (Car car in Cars)
        {
            Console.Write("\nCarID: {0}, Car: {1} {2}, Price: {3}kr", car.CarID, car.Brand, car.Model, car.Price);
            if (car.InStock != true) 
                Console.Write(" Not Instock\n\n");
            
            else Console.Write(" Instock\n\n");
        }
    }

    public void GetListOfBoughtCars(Person person)
    {
        foreach (Car car in person.BoughtCars)
        {
            Console.WriteLine($"\nYour cars\n{car.Brand} {car.Model}");
        }
    }

    public void BuyCar(int CarID, Person person)
    { 
        Car? car = Cars.FirstOrDefault(x => x.CarID == CarID);

        if (car == null)
        {
            Console.WriteLine("Car not listed");
            return;
        } 

        Console.WriteLine("1. Buy the car");
        Console.WriteLine("2. Back\n");

        switch (Console.ReadLine())
        {
            case "1":

                if (person.balance < car.Price)
                {
                    double? needMoney = car.Price - person.balance;
                    Console.WriteLine($"You don't have enough money. Please insert {needMoney}DKK, if you want this car");
                    break;
                }

                person.BoughtCars.Add(car);
                car.InStock = false;
                person.balance -= car.Price;
                Cars.Remove(car);
                Console.WriteLine("\nCongrats. Enjoy your new ride\n");
                break;

            default:
            {
                Console.WriteLine("\nCar no good for u?\n");
                break;
            }     
        } 
    }

    public void LoadCars()
    {
        Cars = new List<Car>();

        Cars.Add(new Car { CarID = 1, Model = "Octavia", Price = 40000, Brand = CarBrand.Skoda, InStock = true });
        Cars.Add(new Car { CarID = 2, Model = "Octavia", Price = 40000, Brand = CarBrand.Skoda, InStock = true });
        Cars.Add(new Car { CarID = 3, Model = "M5 CS", Price = 1700000, Brand = CarBrand.BMW, InStock = true });
        Cars.Add(new Car { CarID = 4, Model = "M2 Coupé", Price = 1650000, Brand = CarBrand.BMW, InStock = true });
        Cars.Add(new Car { CarID = 5, Model = "Aventador", Price = 3500000, Brand = CarBrand.Lambo, InStock = true });
        Cars.Add(new Car { CarID = 6, Model = "Hurracan", Price = 1720000, Brand = CarBrand.Lambo, InStock = true });
    }

}

//List<Person> People = new List<Person>();