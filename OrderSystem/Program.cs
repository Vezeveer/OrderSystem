using System;

namespace OrderSystem
{
    class Program
    {
        static double[] itemPrices = { 1299.00, 1699.00, 1399.00 };
        static string[] itemNames = { "Nvidia GTX 1080 card", "8GB desktop RAM stick", "Power Supply Unit 500W" };
        static int[] itemsInCart = { 0, 0, 0 };
        static int[] itemAvailableStock = { 13, 66, 32 };
        static int[] itemMinStock = { 1, 1, 1 };
        static double totalCost = 0;

        static void Main(string[] args)
        {
            bool loopProgram = true;
            do
            {
                int[] loopXchoice = new int[2]; //to store loop indicator & said choice
                int max, min = 1, xID = 1; //delete xID soon
                int menuChoice, itemChoice, addRemoveChoice;

                string[] menuOptions = { "Browse Products", "Check Cart & Checkout", "Exit" };
                string[] addRemoveOptions = { "Add", "Remove" };

                menuChoice = choice(menuOptions, ""); //print screen & check choice

                switch(menuChoice)
                {
                    case 1: //Browse

                        itemChoice = choice(itemNames, "Go Back");
                        addRemoveChoice = choice(addRemoveOptions, "Go Back");
                        switch (addRemoveChoice)
                        {
                            case 1:
                                addOrRemoveFromCart(itemChoice, "add");
                                break;
                            case 2:
                                addOrRemoveFromCart(itemChoice, "remove");
                                break;
                            case 3:

                                break;
                        }
                        break;
                    case 2:

                        // items display
                        choice(new string[] { "Checkout", "Go Back" }, "");

                        break;
                }

                //Proceed to ADD or REMOVE selected item, Checkout, or exit
                if (xID >= 0 && xID <= 2)
                {
                    //Add or Delete, and check if cart & available stock is not empty
                    if(itemsInCart[xID] != 0)
                    {
                        int loopCart = 0;
                        int choiceAR;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("|| YOU ELECTRONICS");
                            Console.WriteLine("|| _______________");
                            Console.WriteLine("||");
                            Console.WriteLine("|| 1. Add");
                            Console.WriteLine("|| 2. Remove");
                            if (loopCart != 0)
                                Console.WriteLine(">Invalid Input. ");
                            Console.WriteLine("\nInput: ");
                            int[] y = checkInput(1, 2);
                            loopCart = y[0];
                            choiceAR = y[1];
                        } while (loopCart == 1);

                        if (choiceAR == 1) //ADD
                        {
                            if (itemAvailableStock[xID] != 0)
                            {
                                addDeleteCart(xID, itemMinStock[xID], itemAvailableStock[xID], "add");
                                displayCart(itemNames[xID], itemsInCart[xID], "added");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("|| YOU ELECTRONICS");
                                Console.WriteLine("|| _______________");
                                Console.WriteLine("||");
                                Console.WriteLine("|| No more stock available!");
                                Console.WriteLine("\n\nPress any key to continue...");
                                Console.ReadLine();
                            }

                        }
                        else //DELETE
                        {
                            addDeleteCart(xID, itemMinStock[xID], itemsInCart[xID], "delete");
                            displayCart(itemNames[xID], itemsInCart[xID], "deleted");
                        }
                    }
                    else //ADD
                    {
                        addDeleteCart(xID, itemMinStock[xID], itemAvailableStock[xID], "add");
                        displayCart(itemNames[xID], itemsInCart[xID], "added");
                    }
                }
                else if (xID == 4) //Checkout
                    checkOut();
                else //Exit
                    loopProgram = false;

            } while (loopProgram);
        }

        static void addDeleteCart(int itemID, int min, int max, string addOrRemove)
        {
            int[] loopXamount = new int[2];
            do
            {
                Console.Clear();
                Console.WriteLine("|| YOU ELECTRONICS");
                Console.WriteLine("|| _______________");
                Console.WriteLine("||");
                Console.Write("|| Pick amount of " + itemNames[itemID] + " to " + addOrRemove);

                if(addOrRemove == "add")
                    Console.WriteLine("\n|| Available stock: " + itemAvailableStock[itemID]);
                else
                    Console.WriteLine("\n|| Total of this type in cart: " + itemsInCart[itemID]);

                if (loopXamount[0] == 1)
                    Console.WriteLine("\n>Invalid Input.");
                Console.Write("\nInput: ");
                loopXamount = checkInput(min, max);

                if (loopXamount[0] == 0)
                {
                    switch(addOrRemove)
                    {
                        case "add":
                            itemsInCart[itemID] += loopXamount[1];
                            itemAvailableStock[itemID] -= loopXamount[1];
                            break;
                        case "delete":
                            itemsInCart[itemID] -= loopXamount[1];
                            itemAvailableStock[itemID] += loopXamount[1];

                            break;
                    }
                }
                    

            } while (loopXamount[0] == 1);

            calculateTotalCost();
            
        }

        static void calculateTotalCost()
        {
            double tempAmount = 0;
            for (int i = 0; i < itemsInCart.Length; i++)
            {
                tempAmount += itemsInCart[i] * itemPrices[i];
            }
            totalCost = tempAmount;
        }

        static int[] checkInput(int min, int max)
        {
            int choice;
            int[] inputArray = new int[2]; //first element is for validity, 2nd is for input
            try
            {
                choice = int.Parse(Console.ReadLine());
                if (choice >= min && choice <= max)
                    inputArray[1] = choice;
                else
                    inputArray[0] = 1;
            }
            catch
            {
                inputArray[0] = 1;
            }
            return inputArray;
        }

        static void displayCart(string itemName,int item, string deletedOrAdded)
        {
            Console.Clear();
            Console.WriteLine("|| YOU ELECTRONICS");
            Console.WriteLine("|| _______________");
            Console.WriteLine("||");
            calculateTotalCost();
            if (deletedOrAdded == "added")
                Console.WriteLine("|| Successfully added!");
            else
                Console.WriteLine("|| Successfully removed!");
            Console.WriteLine("||");
            Console.WriteLine("|| Total " + itemName + " items in cart: " + item + " ");
            
            Console.WriteLine("|| Total Cost: " + totalCost + " PHP");

            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }

        static void checkOut()
        {
            Console.Clear();
            Console.WriteLine("|| YOU ELECTRONICS");
            Console.WriteLine("|| _______________");
            Console.WriteLine("||");
            Console.WriteLine("|| In Cart: ");
            for(int i = 0; i < itemsInCart.Length; i++)
            {
                Console.WriteLine("|| " + itemNames[i] + ": " + itemsInCart[i] + " items amounting " + (itemsInCart[i]*itemPrices[i]) + " PHP");
            }
            Console.WriteLine("|| Total Costs: " + totalCost);

            Console.WriteLine("\n|| 1. Continue checkout");
            Console.WriteLine("|| 2. Add more");
            Console.Write("\nInput: ");



            //MAKE MORE RICH ... missing checkout screen, and exit

            Console.ReadLine();
        }

        static void renderScreen(string [] optionsArray, string lastOptions)
        {
            int i = 0;
            Console.Clear();
            Console.WriteLine("|| PC PARTS");
            Console.WriteLine("|| _______________\n||");
            for(; i < optionsArray.Length; i++)
                Console.WriteLine("|| " + (i+1) + ". " + optionsArray[i]);
            Console.WriteLine("|| " +  lastOptions);
            Console.WriteLine("||\n");
        }

        static void addOrRemoveFromCart(int itemElement,string addRemove)
        {
            if (itemAvailableStock[itemElement] == 0) //check stock availability
            {
                renderScreen(new string[] { }, "We are out of stock!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }
            else
            {
                bool loop = false;
                int amount = 0;
                do //check amount validity
                {
                    renderScreen(new string[] { }, "Enter amount to " + addRemove);
                    if (loop)
                        Console.WriteLine("Invalid Input\n");
                    try
                    {
                        amount = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        loop = true;
                    }
                    if (amount > itemAvailableStock[itemElement] || amount == 0)
                    {
                        Console.WriteLine("Invalid Input\n");
                        loop = true;
                    }
                    else //if amount checks out, do add or remove & exit loop
                    {
                        if (addRemove == "add")
                        {
                            itemsInCart[itemElement] += amount;
                            itemAvailableStock[itemElement] -= amount;
                            loop = false;
                        }
                        else if (addRemove == "remove")
                        {
                            itemsInCart[itemElement] -= amount;
                            itemAvailableStock[itemElement] += amount;
                            loop = false;
                        }
                    }
                } while (loop);
            }
        }

        //returns choice if valid & in range - takes in array of options
        static int choice(string [] optionsArray, string lastOptions) 
        {
            bool error = false;
            int choice;
            do
            {
                try
                {
                    renderScreen(optionsArray, lastOptions);
                    if(error)
                        Console.WriteLine("Invalid Input");
                    choice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    choice = 0;
                }
                if (choice > 0 && choice <= optionsArray.Length)
                    return choice;
                else
                    error = true;
            } while (true);
        }

        static bool isCartEmpty()
        {
            for (int i = 0; i < itemNames.Length; i++)
                if (itemsInCart[i] >= 1)
                    return false;
            return true;
        }
    }
}
