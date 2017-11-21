
import java.util.ArrayList;

public class WithdrawalTransaction {
    
    public int account;
    
    synchronized public float withDrawFunds(int cardId, int PIN, float amount, 
            ArrayList<CheckingAccount> checkingAccountList,
            ArrayList<DebitCard> debitCardList)
    {
		//checking instances of debit card
        for(DebitCard dc : debitCardList)
        {
			//checking for the debit cardId and pin with the present instance
            if(dc.cardId == cardId && dc.PIN == PIN)
            {
				//checking instances of checking account
                for(CheckingAccount ca : checkingAccountList)
                {
					//Proceed only if checking account number and debit card account number matches
                    if(ca.getAccountNumber() == dc.accountNo)
                    {
                        if(dc.checkDailyDebitLimit(amount)) // we can withdraw this amount from the debit card
                        {
							//Withdraw only if balance in account number is greater than or equal to amount requested
                            if(ca.readBalance() >= amount)
                            {
                                dc.updateDailyDebitTotal(amount);		//updating daily debit total
                                ca.debit(amount);						//withdrawing amount
                                account = ca.getAccountNumber();		//Retrieving account number
                                return ca.balance;						//return balance
                            }
                            else
                            {
								//if balance is less than amount requested
                                System.out.println("Not enough balance");
                                return Float.NaN;
                            }
                        }
                        else
                        {
							//if daily debit limit is reached
                            System.out.println("Daily limit reached");
                            return Float.NaN;
                        }
                    }
                }
            }
        }
		//If cardId or pin doesn't match
        System.out.println("Invalid CardId or PIN");     
        return Float.NaN;
    }
}
