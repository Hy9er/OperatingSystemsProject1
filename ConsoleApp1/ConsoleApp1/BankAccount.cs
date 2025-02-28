using System.Threading;

public class BankAccount{
    
private float money;
private Mutex mutex = new Mutex();

public BankAccount(float start){
    money = start;
}
//withdraws money from the account, locks the account with a mutex until the end of the method
public void Withdraw(float amount){
    mutex.WaitOne();
    money = money - amount;
    Console.WriteLine("Withdrew " + amount + " Balance is now " + money);
    mutex.ReleaseMutex();
}
//deposits money into the account, locks account with a mutex until the end of the method
public void Deposit(float amount){
    mutex.WaitOne();
    money = money + amount;
    Console.WriteLine("Deposited " + amount + " Balance is now " + money);
    mutex.ReleaseMutex();
}
//Transfers money between accounts
public void AccountTransfer(BankAccount otherAccount, float amount){
    
    bool lock1 = false;
    bool lock2 = false;
    //Monitor TryEnter is used to get you out of deadlock if 500 milliseconds pass without the transfer being made
   try{
    Monitor.TryEnter(this, 500, ref lock1);
    Monitor.TryEnter(otherAccount, 500, ref lock2);

    if(lock1 && lock2){ 
        money = money - amount;
        otherAccount.money = otherAccount.money + amount;

        Console.WriteLine(amount +" was transfered from one account to the other");
   }
   else{
    Console.WriteLine("Error with both accounts trying to make a transer.");
   }
}


//unlocks the accounts
finally{
    
    if (lock1){
        Monitor.Exit(this);
    }
    if (lock2){
        Monitor.Exit(otherAccount);
    }
}
}

public void AccountTransferWithDeadlock(BankAccount otherAccount, float amount){
    
    //Deaadlock can happen here since if two threadsy lock the accounts they need to access they can't do anything
    lock (this){
    
    lock (otherAccount){
        money = money - amount;
        otherAccount.money = otherAccount.money + amount;

        Console.WriteLine(amount +" was transfered from one account to the other");

    }
    }
}


public float get(){
    return money;
}
}