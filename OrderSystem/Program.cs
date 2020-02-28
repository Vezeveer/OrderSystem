using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    class Program
    {
        static double[] itemPrices = { 1299.00, 1699.00, 1399.00 };
        static string[] itemNames = { "Nvidia GTX 1080 card", "8GB desktop RAM stick", "Power Supply Unit 500W" };
        static int[] itemAmounts = { 0, 0, 0 };
        static int[] itemAvailableStock = { 13, 66, 32 };
        static int[] itemMinStock = { 1, 1, 1 };
        static double totalCost = 0;

        static void Main(string[] args)
        {
            bool loopProgram = true;
            do
            {
                int[] loopXchoice = new int[2];
                int max, min = 1;
                do
                {
                    Console.Clear();
                    Console.WriteLine("|| YOU ELECTRONICS");
                    Console.WriteLine("|| _______________");
                    Console.WriteLine("||");

                    Console.WriteLine("|| 1. " + itemNames[0]);
                    Console.WriteLine("|| 2. " + itemNames[1]);
                    Console.WriteLine("|| 3. " + itemNames[2]);
                    Console.WriteLine("|| 4. Cancel and Exit");
                    if (isCartEmpty() == false)
                    {
                        Console.WriteLine("|| 5. Checkout");
                        max = 5;
                    }
                    else
                        max = 4;
                    if (loopXchoice[0] == 1)
                        Console.WriteLine("\n>Invalid Input.");
                    Console.Write("\nInput: ");

                    loopXchoice = checkInput(min, max); //1 min, 5 max

                    Console.Clear();
                    
                } while (loopXchoice[0] == 1);

                int xID = loopXchoice[1] - 1;

                //Proceed to ADD or REMOVE selected item, Checkout, or exit
                if (xID >= 0 && xID <= 2)
                {
                    //Add or Delete, and check if cart & available stock is not empty
                    if(itemAmounts[xID] != 0)
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
                                displayCart(itemNames[xID], itemAmounts[xID], "added");
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
                            addDeleteCart(xID, itemMinStock[xID], itemAmounts[xID], "delete");
                            displayCart(itemNames[xID], itemAmounts[xID], "deleted");
                        }
                    }
                    else //ADD
                    {
                        addDeleteCart(xID, itemMinStock[xID], itemAvailableStock[xID], "add");
                        displayCart(itemNames[xID], itemAmounts[xID], "added");
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
                    Console.WriteLine("\n|| Total of this type in cart: " + itemAmounts[itemID]);

                if (loopXamount[0] == 1)
                    Console.WriteLine("\n>Invalid Input.");
                Console.Write("\nInput: ");
                loopXamount = checkInput(min, max);

                if (loopXamount[0] == 0)
                {
                    switch(addOrRemove)
                    {
                        case "add":
                            itemAmounts[itemID] += loopXamount[1];
                            itemAvailableStock[itemID] -= loopXamount[1];
                            break;
                        case "delete":
                            itemAmounts[itemID] -= loopXamount[1];
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
            for (int i = 0; i < itemAmounts.Length; i++)
            {
                tempAmount += itemAmounts[i] * itemPrices[i];
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
            for(int i = 0; i < itemAmounts.Length; i++)
            {
                Console.WriteLine("|| " + itemNames[i] + ": " + itemAmounts[i] + " items amounting " + (itemAmounts[i]*itemPrices[i]) + " PHP");
            }
            Console.WriteLine("|| Total Costs: " + totalCost);

            Console.WriteLine("\n|| 1. Continue checkout");
            Console.WriteLine("|| 2. Add more");
            Console.WriteLine("\nInput: ");

            //MAKE MORE RICH ... missing checkout screen, and exit

            Console.ReadLine();
        }

        static bool isCartEmpty()
        {
            for (int i = 0; i < itemNames.Length; i++)
                if (itemAmounts[i] >= 1)
                    return false;
            return true;
        }
    }
}
