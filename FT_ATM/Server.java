
import java.util.ArrayList;

public class Server extends Thread{
    
    public Server(SynchronousConnector conn, HeartBeatConnector hbConn, ArrayList<CheckingAccount> accounts, ArrayList<DebitCard> cards, boolean faultTolerant)
    {
        this.conn = conn;
        this.hbConn = hbConn;
        this.accounts = accounts;
        this.cards = cards;
        stillAlive = true;
        this.faultTolerant = faultTolerant;
    }
    
    SynchronousConnector conn;
    HeartBeatConnector hbConn;
    ArrayList<CheckingAccount> accounts;
    ArrayList<DebitCard> cards;
    //process the message through synchronous connector
    void processMessage()
    {
		//put the obtained message in petition
            Message petition = conn.receive();
			//initial position extracted
            int type = (int) petition.data.get(0);
            
            if (type == 1) // check balance
            {
                int cardId = (int) petition.data.get(1);
                int PIN = (int) petition.data.get(2);
                
                if(!faultTolerant && cardId == 3005) // fail when using card 3005
                {
                    stillAlive = false;//connection goes off
                    conn.replay(new Message(2)); // send message 2 (no server)
                    return;
                }
                
                float balance = checkBalanceTransaction.checkBalance(cardId, PIN, accounts, cards);
                if(((Float)balance).isNaN())
                {
                    conn.replay(new Message(0)); // there was en error
                }
                else
                {
                    conn.replay(new Message(1, checkBalanceTransaction.account, balance));
                }
            }
            else if(type == 2) // withdrawal
            {
                int cardId = (int) petition.data.get(1);
                int PIN = (int) petition.data.get(2);
                float amount = (float) petition.data.get(3);
                
                if(!faultTolerant && cardId == 3005) // fail when using card 3005
                {
                    stillAlive = false;
                    conn.replay(new Message(2)); // send message 2 (no server)
                    return;
                }               
                                
                float balance = withdrawalTransaction.withDrawFunds(cardId, PIN, amount, accounts, cards);
                if(((Float)balance).isNaN())
                {
                    conn.replay(new Message(0)); // there was en error
                }
                else
                {
                    conn.replay(new Message(1, withdrawalTransaction.account, balance));
                }                                 
            }
    }
    
    CheckBalanceTransaction checkBalanceTransaction;
    WithdrawalTransaction withdrawalTransaction;
    
    boolean stillAlive;
    boolean faultTolerant;
	//process through heart beat 
    
    void processHeartBeatMessage()
    {
        hbConn.receive();
        if(stillAlive)
        {
            hbConn.replay(new Message(1)); // we are alive!
        }
        else
        {
            hbConn.replay(new Message(0)); // we are not alive!
        }
    }
    
    void kill()
    {
        keepGoing = false;
    }
    
    boolean keepGoing = true;
	// process either through sychronous or heart beat connector
        
    public void run()
    {
        checkBalanceTransaction = new CheckBalanceTransaction();
        withdrawalTransaction = new WithdrawalTransaction();
        
        while(keepGoing)
        {
            if(conn.isMessage())
                processMessage();
            else if(hbConn.isMessage())
                processHeartBeatMessage();

        }
    }
}
