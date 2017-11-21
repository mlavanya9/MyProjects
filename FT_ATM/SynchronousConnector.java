import java.util.LinkedList;

public class SynchronousConnector {
    
    boolean messageBufferFull;
    boolean responseBufferFull;
    
    LinkedList<Message> messageBuffer;
    LinkedList<Message> responseBuffer;
    
    int messageBufferMax;  // max capacity of message buffer
    int responseBufferMax; // max capacity of response buffer
    
    int messageBufferCount; // current size of message buffer
    int responseBufferCount; // current size of response buffer
    
    public synchronized boolean isMessage()
    {
		//returns true if message buffer count is greater than zero
        return messageBufferCount > 0;
    }
    
    public SynchronousConnector()
    {
		//creating linked lists for the message and response buffers
        messageBuffer = new LinkedList<>();
        responseBuffer = new LinkedList<>();
        
        messageBufferFull = false;
        messageBufferMax = 1;
        messageBufferCount = 0;
        
        responseBufferFull = false;
        responseBufferMax = 1;
        responseBufferCount = 0;
    }
    //send
    public synchronized void send(Message m)
    {
		//till message buffer count is greater than or equal to maximum number
        while(messageBufferCount >= messageBufferMax)
        {
            try{
                wait();
            }catch(InterruptedException e)
            {
                
            }
        }
		//update the message buffer
        messageBufferCount++;
        messageBuffer.add(m);
        notifyAll(); 
    }
    //receive
    public synchronized Message receive()
    {
        while(messageBufferCount == 0) // empty
        {
            try{
                wait();
            }catch(InterruptedException e)
            {
                
            } 
        }
		//extract the message 
        Message m = messageBuffer.pollFirst();
		// update the buffer
        messageBufferCount--;
        notifyAll();
        return m;  //returning the extracted message
    }
    //reply
    public synchronized void replay(Message m)
    {
        while(responseBufferCount >= responseBufferMax)
        {
            try{
                wait();
            }catch(InterruptedException e)
            {
                
            }
        }
        responseBufferCount++;
        responseBuffer.add(m);
        notifyAll();
    }
    //receive reply
    public synchronized Message receiveReplay()
    {
        while(responseBufferCount == 0) // empty
        {
            try{
                wait();
            }catch(InterruptedException e)
            {
                
            } 
        }
        Message r = responseBuffer.pollFirst();
        responseBufferCount--;
        notifyAll();
        return r;
    }
    
}
