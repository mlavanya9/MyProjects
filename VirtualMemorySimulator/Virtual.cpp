//*******************************************************************************************
//VIRTUAL SIMULATOR



//Name :-Lavanya Mengaraboina			
//********************************************************************************************

//Library includes

#include "iostream"
#include "bitset"
#include "string"
#include "queue"
#include "fstream"
#include "sstream"

//Unchanged(constant) physical memory
// based on the value of k
const int p_memory = (4096 * 11);

using namespace std;

void main()
{
	//"for" loop for different page sizes i.e., 2 power 7,8,9,10,11,12
	for (int i = 7; i < 13; i = i++)
	{
		//accessing text file to myfile for read operation
		ifstream myfile("Text.txt");

		//if the given file name in above (parameter) is open
		if (myfile.is_open()) {
			int Page_size = pow(2, i);				//calculates page size for every loop iterator
			int page_hits = 0, page_miss = 0;		//intial faults and hits as zero
			int frames = p_memory / (Page_size);		//calculate frames using physical memory and page size

														//queue data structure for page table entry for easy FIFO(first in first out) operation
			queue<int> table;

			//string varible to read every line from input file
			string line;

			//while loop to read every address line till end of file
			while (getline(myfile, line))
			{
				//dirty bit to read,write,fetch data,hex for remaining hexadecimal address
				string dirty_bit, hex, address_binary;

				//string stream to split dirty bit and hex address
				istringstream iss(line);

				//reading read,write to dirty bit and hex decimal to hex
				iss >> dirty_bit >> hex;

				//if condition to make sure hex address is > 3(12 bits) so no duplicates
				//are caluclated for decimal number(page number)
				if (hex.length() <= 3) { address_binary.append("0000"); }

				//loop that calculates binary conversion of read hexadecimal number
				for (int j = 0; j < hex.size(); j++)
				{
					if (isdigit(hex[j])) {
						bitset<4> b(hex[j]);
						address_binary.append(b.to_string());
					}
					else {
						if (hex[j] == 'a') { address_binary.append("1010"); }
						if (hex[j] == 'b') { address_binary.append("1011"); }
						if (hex[j] == 'c') { address_binary.append("1100"); }
						if (hex[j] == 'd') { address_binary.append("1101"); }
						if (hex[j] == 'e') { address_binary.append("1110"); }
						if (hex[j] == 'f') { address_binary.append("1111"); }
					}
				}
				string page_binary;					//variable to store page number's binary value

													//this command cuts of the offset from  total binary address using sub-string
				page_binary = address_binary.substr(0, ((address_binary.size()) - i));

				int page_number = 0;				//variable for page number(decimal)
													//loop that converts binary into decimal format
				for (int c = 0; c < page_binary.length(); c++)
				{
					page_number = page_number + (((page_binary[page_binary.length() - (c + 1)] - '0')*pow(2, c)));
				}

				//conditon that takes the first page number into page table
				if (table.empty())
				{
					table.push(page_number);
					page_miss++;
				}

				//else if page table is not empty
				else {
					//temporary queue that can be used to check page miss or hit
					queue<int>t_table;
					t_table = table;				//copy main page table into temporary table

													//loop that traverse through the temporary page table queue 
					for (int k = 0; k < table.size() && k < frames; k++) {
						int n = t_table.front();	//takes top page number in queue 
						t_table.pop();				//pops up top element from queue
						if (n == page_number) {
							page_hits++;
							k = table.size();
						}
						else {
							//if loop value is at the end of queue length
							if (k == (table.size() - 1)) {
								if (n != page_number) {
									//if page table is full
									if (k == (frames - 1)) {
										page_miss++;
										table.pop();
										table.push(page_number);
									}
									else {
										page_miss++;
										table.push(page_number);
										k = table.size();
									}
								}
							}
						}
					}
				}
			}
			cout << page_miss << endl;
			myfile.close();
		}
	}
	system("pause\0");
}

