using Cardealer.Models;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Cardealer;
public class CarDealer
{
    public List<Car> Cars { get; set; }

    public List<Person> People { get; set; }

    string firmanavn;

    public CarDealer(string firmanavn)
    {
        People = new List<Person>();
        People.Add(new Dealer(1, "aron", "sucka", "aropx0001", "gedepis", "“When I was a child, ladies and gentlemen, I was a dreamer.”"));
        People.Add(new Dealer(2, "emil", "sensei", "emilx0001", "gedepis", "“Before the battle of the fist comes the battle of the mind.”"));
        People.Add(new Dealer(3, "thomas", "hjælpelærer", "thomx007", "gedepis", "“Bluuuuuuuueew fortytuuuueew, tactic tuesday. hurtty hut”"));
        People.Add(new Dealer(4, "brian", "Translator", "brian0001", "gedepis", "He knows Spanish. ”Me estás tomando el pelo.”"));
        People.Add(new Dealer(5, "sammy", "html-ekspert", "sammy0001", "gedepis", "Sindsyg til HTML"));
        People.Add(new Dealer(6, "mohamad", "Director", "mohamad0001", "gedepis", "I decide if you flex or no"));
        People.Add(new Dealer(7, "max", "techguy", "max0001", "gedepis", "He fixes stuff. (breaks stuff most of the time)"));
        People.Add(new Dealer(8, "benjamin", "Kantinedame", "benjamin0001", "gedepis", "Good at looking important. He also plays League of Legends. Loves budget capri-sonne"));

        this.firmanavn = firmanavn;
        LoadCars();
    }

    public string GetCarDealerName()
    {
        return firmanavn;
    }

    public int CreatePerson(string firstname, string lastname, PersonType type, string username, string password, string quote)
    {
        int noget = People.Max(x => x.personID);

        switch (type)
        {
            case PersonType.Dealer:
                People.Add(new Dealer(++noget, firstname, lastname, username, password, quote));
                break;

            case PersonType.Customer:
                People.Add(new Customer(++noget, firstname, lastname, username, password, quote));
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
    } // tag dig sammen

    public List<Person> GetDealers()
    {
        return People.Where(x => x.Type == PersonType.Dealer).ToList();
    }

    public int AddCar(Car newCar)
    {
        int carID = 0;
        if (Cars.Count == 0)
        {
            carID = 1;
        }
        else
        {
            carID = Cars.Max(x => x.CarID) + 1;
        }
        newCar.CarID = carID;
        Cars.Add(newCar);
        
        return newCar.CarID;
    }
    //Fix handle
    public string DeleteCarByID(int carID)
    {
        Car car = Cars.FirstOrDefault(x => x.CarID == carID);
        if (car != null)
        {
            Cars.Remove(car);
            return "\nCar deleted\n";
        }
        return "\nCar not found\n";
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

        if (car == null || person.balance < car.Price)
        {
            return;
        }

        person.BoughtCars.Add(car);
        car.InStock = false;
        person.balance -= car.Price;
        Cars.Remove(car);
    }
    public void LoadCars()
    {
        Cars = new List<Car>();

        Cars.Add(new Car { CarID = 1, Model = "Octavia", Price = 40000, Brand = CarBrand.Skoda, InStock = true });
        Cars.Add(new Car { CarID = 2, Model = "Octavia", Price = 40000, Brand = CarBrand.Skoda, InStock = true });
        Cars.Add(new Car { CarID = 3, Model = "M5 CS", Price = 1700000, Brand = CarBrand.BMW, InStock = true });
        Cars.Add(new Car { CarID = 4, Model = "M2 Coupé", Price = 1650000, Brand = CarBrand.BMW, InStock = true });// luk dinpc
        Cars.Add(new Car { CarID = 5, Model = "Aventador", Price = 3500000, Brand = CarBrand.Lambo, InStock = true });
        Cars.Add(new Car { CarID = 6, Model = "Hurracan", Price = 1720000, Brand = CarBrand.Lambo, InStock = true });
    }
    //List<Person> People = new List<Person>();
}




