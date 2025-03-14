class Program
{
    public static object CookieLock { get; set; } = new object();
    static async Task Main()
    {
        CookieJar.AmountOfCookies(25);

        Kid kid1 = new Kid("Bob");
        Kid kid2 = new Kid("Harry");
        Kid kid3 = new Kid("Marv");
        Kid kid4 = new Kid("Ronald");
        Kid kid5 = new Kid("Jessica");
        Kid kid6 = new Kid("Jeannette");
        Kid kid7 = new Kid("Ella");

        var task1 = kid1.GrabOneCookie();
        var task2 = kid2.GrabOneCookie();
        var task3 = kid3.GrabOneCookie();
        var task4 = kid4.GrabOneCookie();
        var task5 = kid5.GrabOneCookie();
        var task6 = kid6.GrabOneCookie();
        var task7 = kid7.GrabOneCookie();
        
        await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7);

        List<Kid> numberOfCookies = new List<Kid>()
        {
            { kid1 },
            { kid2 },
            { kid3 },
            { kid4 },
            { kid5 },
            { kid6 },
            { kid7 },
        };

        var numberOfCookiesSorted = numberOfCookies.OrderBy(kid => kid.CookiesGrabbed).ToList();

        Console.WriteLine(kid1.CookiesGrabbed);
        Console.WriteLine($"{numberOfCookiesSorted.Last().Name} grabbed {numberOfCookiesSorted.Last().CookiesGrabbed} cookies, which was the most cookies grabbed!");
    }
}

public class CookieJar
{
    public static int Cookies { get; set; } = 0;

    public static void AmountOfCookies(int cookies)
    {
        Cookies = cookies;
    }
    public static async Task GrabCookies(string name)
    {
        Random rand = new Random();
        int randomDely = rand.Next(500, 6001);
        await Task.Delay(randomDely);

        Console.WriteLine($"{name} goes to take a cookie.");

        await Task.Delay(randomDely);

        if (Cookies == 0)
        {
            Console.WriteLine($"There are no cookies left! {name} goes away sadly.");
            return;
        }

        Cookies--;

        Console.WriteLine($"{name} took a cookie. There are {Cookies} left.");
    }
}

class Kid
{
    public string Name { get; set; }
    public int CookiesGrabbed { get; set; }

    public Kid(string name)
    {
        Name = name;
        CookiesGrabbed = 0;
    }

    public async Task GrabOneCookie()
    {
        while (CookieJar.Cookies != 0)
        {
            await CookieJar.GrabCookies(Name);
            CookiesGrabbed++;
        }
    }
}