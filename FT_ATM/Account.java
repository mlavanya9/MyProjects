//Account class
public class Account {
    int accountNumber;
    float balance;
//initializing balance in float   
    public Account()
    {
        balance = 0.f;
    }
//refer to the selected account    
    public void open(int accountNumber)
    {
        this.accountNumber = accountNumber;
    }
//return the available balance
    public float readBalance()
    {
        return balance;
    }
//Put money in the account  
    public void credit(float amount)
    {
        balance += amount;
    }
// debited from account   
    public void debit(float amount)
    {
        balance -= amount;
    }
//get account number   
    int getAccountNumber()
    {
        return accountNumber;
    }        
}
