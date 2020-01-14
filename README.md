# DMV Code Challenge

### **Overview**
This is a small C# .net core application we developed and is an appointment scheduler for the Department of Motor Vehicles (DMV). The application reads in data for customers and randomly assigns a customer to a random teller.  Your mission, if you choose to accept, is to create an algorithm that more efficiently assigns customers to tellers.

 *Customer*
- 5,000 Total
- Available Data:
	- Id
	- Service Type
	- Duration of Appointment
	
*Teller*
- 150 Total
- Available Data:
	- Id
	- Specialty Type
	- Multiplier

**Details**
- When a customer's service type matches the specialty type of the assigned teller the multiplier with be multiplied against to customer's duration to reduce the appointment time
- Some customers' service type may not match any teller's specialty type
- Not all tellers will have the same multiplier even within the same specialty type
- Do **NOT** change Customer or Teller Data or the how the output is calculated. However, refactor of the code is strongly encouraged

**Goal**
We want to try to process all customers and have our tellers go home as early as possible. All the tellers will leave together once the last customer has been processed. Therefore, your results will **NOT** be measured accordingly to the total number of time the tellers spend with customers but when the last customer is processed.

**Submission**
Please create a public git repository and email us the link.

Thank you and good luck!

NOV Rigsentry Team

