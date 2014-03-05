drinks-server
===========

drinks-server provides backend support for the [Drinks RFID Terminal](https://github.com/bblanchon/DrinksRfidTerminal). Together, they compose a complete payment terminal system which allows users to purchase drinks or other products using RFID badges.

The drinks server consists of two components.

+ A RESTful API which communicates with the RFID terminal
+ A web portal used for account administration and account balance checking

##API
The API has two primary functions. The first is to keep the payment terminal in sync with the server. Being "in sync" means that the following three data are the same on both ends of the system.

+ The list of products proposed to terminal users
+ The current time

The second function of the API is to accept, authorize, process, and respond to purchase requests.

##Web portal
The web portal provides users with a means to consult their account balance. It also allows designated administrators to create, modify, and add funds to accounts.