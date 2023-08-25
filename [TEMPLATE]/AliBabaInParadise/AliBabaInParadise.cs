using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class AliBabaInParadise
    {
        #region YOUR CODE IS HERE
        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// Given the Camels maximum load and N items, each with its weight and profit 
        /// Calculate the max total profit that can be carried within the given camels' load
        /// </summary>
        /// <param name="camelsLoad">max load that can be carried by camels</param>
        /// <param name="itemsCount">number of items</param>
        /// <param name="weights">weight of each item</param>
        /// <param name="profits">profit of each item</param>
        /// <returns>Max total profit</returns>
        
        static public int[,] resultOfItems;  // Array to store the maximum profit.
        
        // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
        //static public bool[,] backtraceSolutionOfSelectedItems;  // Array to keep track of the items selected for the optimal selectedItems
        
        static public bool ValidateInputs(int camelsLoad, int itemsCount, int[] weights, int[] profits, string functionName)
        {
            if(functionName == "SolveValue")
            {
                // Check for invalid inputs 
                if (camelsLoad <= 0 || itemsCount <= 0 || weights == null || profits == null || weights.Length != itemsCount || profits.Length != itemsCount)
                {
                    return false; // invalid input
                }
                else
                {
                    return true; // valid input
                }
            }
            else if(functionName == "ConstructSolution")
            {
                // Check for invalid inputs 
                if (resultOfItems[itemsCount, camelsLoad] == 0 || camelsLoad <= 0 || itemsCount <= 0 || weights == null || profits == null || weights.Length != itemsCount || profits.Length != itemsCount)
                {
                    return false; // invalid input
                }
                else
                {
                    return true; // valid input
                }
            }
            else
            {
                return true; // valid input
            }
        }


        static public int SolveValue(int camelsLoad, int itemsCount, int[] weights, int[] profits)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            
            /*
            int[,] resultOfItems = new int[itemsCount + 1, camelsLoad + 1];
            for (int i = 1; i <= itemsCount; i++)
            {
                for (int j = 1; j <= camelsLoad; j++)
                {
                    if (j >= weights[i - 1])
                    {
                        if (profits[i - 1] + resultOfItems[i, j - weights[i - 1]] > resultOfItems[i - 1, j])
                        {
                            resultOfItems[i, j] = profits[i - 1] + resultOfItems[i, j - weights[i - 1]];
                        }
                        else
                        {
                            resultOfItems[i, j] = resultOfItems[i - 1, j];
                        }
                    }
                    else
                    {
                        resultOfItems[i, j] = resultOfItems[i - 1, j];
                    }
                }
            }
            */

            if(ValidateInputs(camelsLoad, itemsCount, weights, profits, "SolveValue")) // Check for invalid inputs
            {
                // Initialize the resultOfItems and backtraceSolutionOfSelectedItems arrays
                // Add 1 to the itemsCount and camelsLoad to make the arrays one-based (to avoid index-out-of-bounds errors)
                //Initialize
                resultOfItems = new int[itemsCount + 1, camelsLoad + 1]; // # varying parameters in the function is (camelsLoad, itemsCount)
                
                //int[,] resultOfItems = new int[itemsCount + 1, camelsLoad + 1]; // # varying parameters in the function is (camelsLoad, itemsCount)
                
                // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
                //backtraceSolutionOfSelectedItems = new bool[itemsCount + 1, camelsLoad + 1];

                // Fill the resultOfItems and backtraceSolutionOfSelectedItems arrays by solving smaller subproblems and combining them
                for (int i = 1; i <= itemsCount; i++)  // start from 1 because the array is one based 
                {
                    for (int j = 1; j <= camelsLoad; j++) // start from 1 because the array is one based
                    {
                        if (j >= weights[i - 1]) // If the current item can fit in the remaining load
                        {
                            /*
                               Calculate the maximum profit that can be obtained by either taking the current item or not
                               choose the maximum between two cases:
                               1. The current item is included in the optimal selectedItems and we add its profit to the profit of the remaining weight limit (j - weights[i-1]) which is already computed (resultOfItems[i, j - weights[i - 1]])
                               2. The current item is not included in the optimal selectedItems and we move on to the next item (resultOfItems[i - 1, j])
                            */
                            if (profits[i - 1] + resultOfItems[i, j - weights[i - 1]] > resultOfItems[i - 1, j])
                            {
                                resultOfItems[i, j] = profits[i - 1] + resultOfItems[i, j - weights[i - 1]];

                                // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
                                // backtraceSolutionOfSelectedItems[i, j] = true; // Mark the current item as selected in the optimal selectedItems
                            }
                            else
                            {
                                resultOfItems[i, j] = resultOfItems[i - 1, j];

                                // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
                                // backtraceSolutionOfSelectedItems[i, j] = false; // The default value of an array of bool is "false"
                            }
                            // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
                            
                        }
                        else //Retrieve
                        {
                            resultOfItems[i, j] = resultOfItems[i - 1, j]; // If the current item is too heavy to fit in the remaining load, skip it
                            
                            // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
                            //backtraceSolutionOfSelectedItems[i, j] = false; // The default value of an array of bool is "false"
                        }
                    }
                }
            
                return resultOfItems[itemsCount, camelsLoad]; // Return the maximum total profit that can be obtained

            }
            else
            {
                return 0; // invalid input
            }

        }
        #endregion

        #region FUNCTION#2: Construct the selectedItems
        //Your Code is Here:
        //==================
        /// <returns>Tuple array of the selected items to get MAX profit (stored in Tuple.Item1) together with the number of instances taken from each item (stored in Tuple.Item2)
        /// OR NULL if no items can be selected</returns>
        
        static public Tuple<int, int>[] ConstructSolution(int camelsLoad, int itemsCount, int[] weights, int[] profits)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();
            
            if(ValidateInputs(camelsLoad, itemsCount, weights, profits, "ConstructSolution")) // Check for invalid inputs
            {
                // #################  Old code that i was using to resolve solution that is actually solved in function SolveSolution
    
                // Initialize the resultOfItems and backtraceSolutionOfSelectedItems arrays
                // Add 1 to the itemsCount and camelsLoad to make the arrays one-based (to avoid index-out-of-bounds errors)
                //Initialize
                //resultOfItems = new int[itemsCount + 1, camelsLoad + 1]; // # varying parameters in the function is (camelsLoad, itemsCount)
                /*
                int[,] resultOfItems = new int[itemsCount + 1, camelsLoad + 1]; // # varying parameters in the function is (camelsLoad, itemsCount)
                
                // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
                //backtraceSolutionOfSelectedItems = new bool[itemsCount + 1, camelsLoad + 1];

                // Fill the resultOfItems and backtraceSolutionOfSelectedItems arrays by solving smaller subproblems and combining them
                for (int i = 1; i <= itemsCount; i++)  // start from 1 because the array is one based 
                {
                    for (int j = 1; j <= camelsLoad; j++) // start from 1 because the array is one based
                    {
                        if (j >= weights[i - 1]) // If the current item can fit in the remaining load
                        {
                            
                               //Calculate the maximum profit that can be obtained by either taking the current item or not
                               //choose the maximum between two cases:
                               //1. The current item is included in the optimal selectedItems and we add its profit to the profit of the remaining weight limit (j - weights[i-1]) which is already computed (resultOfItems[i, j - weights[i - 1]])
                               //2. The current item is not included in the optimal selectedItems and we move on to the next item (resultOfItems[i - 1, j])
                            
                            if (profits[i - 1] + resultOfItems[i, j - weights[i - 1]] > resultOfItems[i - 1, j])
                            {
                                resultOfItems[i, j] = profits[i - 1] + resultOfItems[i, j - weights[i - 1]];

                                // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
                                // backtraceSolutionOfSelectedItems[i, j] = true; // Mark the current item as selected in the optimal selectedItems
                            }
                            else
                            {
                                resultOfItems[i, j] = resultOfItems[i - 1, j];

                                // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
                                // backtraceSolutionOfSelectedItems[i, j] = false; // The default value of an array of bool is "false"
                            }
                            // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
                            
                        }
                        else //Retrieve
                        {
                            resultOfItems[i, j] = resultOfItems[i - 1, j]; // If the current item is too heavy to fit in the remaining load, skip it
                            
                            // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
                            //backtraceSolutionOfSelectedItems[i, j] = false; // The default value of an array of bool is "false"
                        }
                    }
                }
                if (resultOfItems[itemsCount, camelsLoad] == 0)
                {
                    return null;
                }
                */

                List<Tuple<int, int>> selectedItems = new List<Tuple<int, int>>();  // I Used List because the length of selected items is not known
            
                while (itemsCount > 0 && camelsLoad > 0) // Loop through items and camels load
                {

                    // ############ backtraceSolutionOfSelectedItems For using extra storage to store selected items ==> Not need this in this problem
                    
                    //if (backtraceSolutionOfSelectedItems[itemsCount, camelsLoad]) // If this item was selected ==> if (backtraceSolutionOfSelectedItems[itemsCount, camelsLoad] == true)
                    
                    if(resultOfItems[itemsCount, camelsLoad] != resultOfItems[itemsCount - 1, camelsLoad]) // If this item was selected ==> its value != previous value
                    {
                        camelsLoad -= weights[itemsCount - 1]; // Subtract the weight of this item from the camels' load

                        Tuple<int, int> selectedItem = new Tuple<int, int>(itemsCount - 1, 1);

                        selectedItems.Add(selectedItem); // Add this item to the selectedItems with a count of 1
                    }
                    else  // If this item was not selected
                    {
                        itemsCount--;  // Move to the next item
                    }
                }

                return selectedItems.ToArray(); // Return the selectedItems as an array of tuples
            
            }
            else
            {
                return null; // Invalid Input
            }
        }
        #endregion

        #endregion
    }
}
