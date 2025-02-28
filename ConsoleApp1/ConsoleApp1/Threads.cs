
using System.Diagnostics;

internal class Threads
{
static Random random = new Random();
static BankAccount account = new BankAccount(29000f);
static BankAccount account2 = new BankAccount(10000f);

//Randomizes Transfers between accounts for testing purposes
public static void threadTask()
{
    int action = random.Next(2);
    float amount = random.Next(500);

    if (action == 0){
        account.Withdraw(amount);
    }
    else {
        account.Deposit(amount);
    }
}
//Randomizes Transfers between accounts for testing purposes
public static void threadTask2()
{
    int action = random.Next(2);
    float amount = random.Next(500);
    if (action == 0){
        account.AccountTransfer(account2, amount);
    }
    else {
        account2.AccountTransfer(account, amount);
    }
}
    private static void Main(string[] args)
    {            
        int choice;
        choice = int.Parse(Console.ReadLine());

        //Simulation for Part A
        if (choice == 1){
        Thread thread1 = new Thread(new ThreadStart(threadTask2));
        Thread thread2 = new Thread(new ThreadStart(threadTask2));
        Thread thread3 = new Thread(new ThreadStart(threadTask2));
        Thread thread4 = new Thread(new ThreadStart(threadTask2));
        Thread thread5 = new Thread(new ThreadStart(threadTask2));
        Thread thread6 = new Thread(new ThreadStart(threadTask2));
        Thread thread7 = new Thread(new ThreadStart(threadTask2));
        Thread thread8 = new Thread(new ThreadStart(threadTask2));
        Thread thread9 = new Thread(new ThreadStart(threadTask2));
        Thread thread10 = new Thread(new ThreadStart(threadTask2));

        thread1.Start();
        thread2.Start();
        thread3.Start();
        thread4.Start();
        thread5.Start();
        thread6.Start();
        thread7.Start();
        thread8.Start();
        thread9.Start();
        thread10.Start();

        }
        //This shows PartB
        else{

            ProcessStartInfo pipe = new ProcessStartInfo("ls", "-1 /home/evanw"){
                
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            
            Process pro = Process.Start(pipe);

            string files = pro.StandardOutput.ReadToEnd();

            string[] allFiles = files.Split('\n');

            for (int i = 0; i < allFiles.Length; i++){             
                Console.WriteLine(allFiles[i]);
            }            
        }

    }
}