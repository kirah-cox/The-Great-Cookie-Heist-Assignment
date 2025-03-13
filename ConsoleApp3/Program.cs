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

        var task1 = CookieJar.GrabCookies(kid1.Name);
        var task2 = CookieJar.GrabCookies(kid2.Name);
        var task3 = CookieJar.GrabCookies(kid3.Name);
        var task4 = CookieJar.GrabCookies(kid4.Name);
        var task5 = CookieJar.GrabCookies(kid5.Name);
        var task6 = CookieJar.GrabCookies(kid6.Name);
        var task7 = CookieJar.GrabCookies(kid7.Name);

        await Task.WhenAll(task1, task2, task3, task4, task5, task6, task7);

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
        Console.WriteLine($"{name} goes to take a cookie.");

        Random rand = new Random();
        int randomDely = rand.Next(500, 6001);
        await Task.Delay(randomDely);
        
        if (Cookies == 0)
        {
            Console.WriteLine($"There are no cookies left! {name} goes away sadly.");
            return;
        }

        Cookies--;
        Kid.CookiesGrabbed++;

        Console.WriteLine($"{name} took a cookie. There are {Cookies} left.");
    }
}

class Kid : CookieJar
{
    public string Name { get; set; }
    public static int CookiesGrabbed { get; set; }

    public Kid(string name)
    {
        Name = name;
        CookiesGrabbed = 0;
    }
}