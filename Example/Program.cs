using BetterAsync;
using System;

class Program
{
    static Promise<string> Tasker()
    {
        return new Promise<string>((resolve, reject) => {
            Flow.Wait(1000);
            Console.WriteLine("Stopped sleeping once");
            resolve("Hello promises!");
            Flow.Wait(1000);
            Console.WriteLine("Stopped sleeping twice");
        });
    }
    static void Tasker2(Channel<string> ch)
    {
        Flow.Go(() => {
            Flow.Wait(3000);
            ch.Send("Hello channels!");
        });
    }
    static void FlagsTest()
    {
        Flag f = new Flag(false);
        f.OnChanged += e => { Console.WriteLine("Flag changed (" + e + ")"); };
        f.OnRaised += () => { Console.WriteLine("Flag raised"); };
        f.OnLowered += () => { Console.WriteLine("Flag changed lowered"); };
        Flow.Go(() =>
        {
            Flow.Wait(4000);
            Console.WriteLine(f.Toggle());
        });
        Flow.Go(() =>
        {
            Flow.Wait(2000);
            f.up = true;
        });
    }

    static void Main(string[] args)
    {
        Flow.Go(AsyncMain);
    }
    async static void AsyncMain()
    {
        Channel<string> receiveChannel = new Channel<string>();
        receiveChannel.OnReceived += (recv) => { Console.WriteLine(recv); };
        Tasker2(receiveChannel);
        FlagsTest();
        Console.WriteLine(await Tasker());
        Console.WriteLine("Channel receive: " + await receiveChannel.Receive());
    }
}