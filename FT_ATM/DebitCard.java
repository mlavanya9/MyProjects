public class DebitCard {
    int cardId;
    int PIN;
    int accountNo;
    float dailyDebitTotal;  
    
	//constructor
    public DebitCard(int cardId, int PIN, int accoutNo, float dailyDebitTotal)
    {
        this.cardId = cardId;
        this.PIN = PIN;
        this.accountNo = accoutNo;
        this.dailyDebitTotal = dailyDebitTotal;
    }
    
	//check whether cardId matches with the pin
    public int validatePIN(int cardId, int PIN)
    {
        if(cardId == this.cardId && PIN == this.PIN)
            return accountNo;
        
        return 0;
    }
    
	//update total debited money
    public void updateDailyDebitTotal(float amount)
    {
        dailyDebitTotal += amount;
    }
    
	//check whether daily debited amount < 300
    public boolean checkDailyDebitLimit(float amount)
    {
        return dailyDebitTotal + amount <= 300;
    }
}
