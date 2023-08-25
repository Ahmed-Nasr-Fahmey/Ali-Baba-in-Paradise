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
            

            if(ValidateInputs(camelsLoad, itemsCount, weights, profits, "SolveValue")) // Check for invalid inputs
            {
                
                //Initialize
                resultOfItems = new int[itemsCount + 1, camelsLoad + 1]; // # varying parameters in the function is (camelsLoad, itemsCount)
                
                // Fill the resultOfItems and backtraceSolutionOfSelectedItems arrays by solving smaller subproblems and combining them
                for (int i = 1; i <= itemsCount; i++)  // start from 1 because the array is one based 
                {
                    for (int j = 1; j <= camelsLoad; j++) // start from 1 because the array is one based
                    {
                        if (j >= weights[i - 1]) // If the current item can fit in the remaining load
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
                        else //Retrieve
                        {
                            resultOfItems[i, j] = resultOfItems[i - 1, j]; // If the current item is too heavy to fit in the remaining load, skip it
                            
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
    
                List<Tuple<int, int>> selectedItems = new List<Tuple<int, int>>();  // I Used List because the length of selected items is not known
            
                while (itemsCount > 0 && camelsLoad > 0) // Loop through items and camels load
                {

                    
                    
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
