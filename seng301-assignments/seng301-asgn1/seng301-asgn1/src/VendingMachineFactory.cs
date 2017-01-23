using System;
using System.Collections;
using System.Collections.Generic;

using Frontend1;

namespace seng301_asgn1 {
    /// <summary>
    /// Represents the concrete virtual vending machine factory that you will implement.
    /// This implements the IVendingMachineFactory interface, and so all the functions
    /// are already stubbed out for you.
    /// 
    /// Your task will be to replace the TODO statements with actual code.
    /// 
    /// Pay particular attention to extractFromDeliveryChute and unloadVendingMachine:
    /// 
    /// 1. These are different: extractFromDeliveryChute means that you take out the stuff
    /// that has already been dispensed by the machine (e.g. pops, money) -- sometimes
    /// nothing will be dispensed yet; unloadVendingMachine is when you (virtually) open
    /// the thing up, and extract all of the stuff -- the money we've made, the money that's
    /// left over, and the unsold pops.
    /// 
    /// 2. Their return signatures are very particular. You need to adhere to this return
    /// signature to enable good integration with the other piece of code (remember:
    /// this was written by your boss). Right now, they return "empty" things, which is
    /// something you will ultimately need to modify.
    /// 
    /// 3. Each of these return signatures returns typed collections. For a quick primer
    /// on typed collections: https://www.youtube.com/watch?v=WtpoaacjLtI -- if it does not
    /// make sense, you can look up "Generic Collection" tutorials for C#.
    /// </summary>
    public class VendingMachineFactory : IVendingMachineFactory {

        int vmNum = 0;
        VendingMachine vm;
        List<VendingMachine> vmList;

        public VendingMachineFactory() {
            vmList = new List<VendingMachine>();
        }

        public int createVendingMachine(List<int> coinKinds, int selectionButtonCount) {

            coinKinds.Sort();
            int temp = -1;

            //testing only; remove later
            Console.WriteLine("Accepted Coins: ");

            for (int i = 0; i < coinKinds.Count; i++)
            {
                //test for invalid (negative/zero) coin types
                if (coinKinds[i] <= 0)
                {
                    throw new Exception("The coin value must be greater than 0. The argument passed was: " + coinKinds[i]);
                }

                //test for non-unique coin types
                else if (temp == coinKinds[i])
                {
                    throw new Exception("The coin value must be unique. The argument " + coinKinds[i] + " already exists");
                }

                //testing only; remove later
                else
                {
                    Console.WriteLine("{0}", coinKinds[i]);
                }

                temp = coinKinds[i];
            }

            //test for invalid (negative) number of buttons
            if (selectionButtonCount < 0)
            {
                throw new Exception("The number of buttons must be greater than 0. The argument passed was: " + selectionButtonCount);
            }

            else
            {
                //testing only; remove later
                Console.WriteLine("Number of Buttons:" + selectionButtonCount);

                //create vending machine if all parameters are valid
                vmList.Add(new VendingMachine(coinKinds, selectionButtonCount));
            }

            //Console.WriteLine("{0}", vmList.Count);
            return vmNum++;
        }

        public void configureVendingMachine(int vmIndex, List<string> popNames, List<int> popCosts) {

            vm = vmList[vmIndex];

            //test for negative/zero cost values
            for (int i = 0; i < popCosts.Count; i++)
            {
                if(popCosts[i] < 1)
                {
                    throw new Exception("Cost cannot be 0 or negative. The argument passed was: " + popCosts[i]);
                }
            }

            //test for different number of popNames and popCosts
            if (popNames.Count != popCosts.Count)
            {
                throw new Exception("Number of pop names and costs must be the same.");
            }

            //test for more popNames than buttons
            if (popNames.Count != vm.selectionButtonCount)
            {
                throw new Exception("Cannot have more/less pop than buttons.");
            }

            for (int i = 0; i < vm.selectionButtonCount; i++)
            {
                
            }
        }

        public void loadCoins(int vmIndex, int coinKindIndex, List<Coin> coins) {

            vm = vmList[vmIndex];
            //create list for number of coins in each chute
            List<int> coinsInChutes = new List<int>();
            //create list to keep track of the type of coin each chute contains
            List<int> coinTypesinChutes = new List<int>();

            //initialize number of coins and coin types in each chute to zero
            for (int i = 0; i < vm.coinKinds.Count; i++)
            {
                coinsInChutes.Add(0);
                coinTypesinChutes.Add(0);
            }

            //test if there's invalid input for coinKindIndex
            if (coinKindIndex > vm.coinKinds.Count || coinKindIndex < 0)
            {
                throw new Exception("Coin kind index cannot be greater than number of coin types nor negative.");
            }

            //"add" coins to the specified chute
            coinsInChutes[coinKindIndex] = coinsInChutes[coinKindIndex] + coins.Count;

            //records the coin type that was placed in the specified chute
            coinTypesinChutes[coinKindIndex] = coins[0].Value;

        }

        public void loadPops(int vmIndex, int popKindIndex, List<Pop> pops) {

            vm = vmList[vmIndex];
            //create list for number of pops in each chute
            List<int> popsInChutes = new List<int>();
            //create list to keep track of the type of pop each chute contains
            List<String> popTypesinChutes = new List<String>();

            //initialize number of pops and pop types in each chute to zero and null
            for (int i = 0; i < vm.selectionButtonCount; i++)
            {
                popsInChutes.Add(0);
                popTypesinChutes.Add(null);
            }

            //test if there's invalid input for popKindIndex
            if (popKindIndex > vm.selectionButtonCount || popKindIndex < 0)
            {
                throw new Exception("Pop kind index cannot be greater than number of pop types nor negative.");
            }

            //"add" coins to the specified chute
            popsInChutes[popKindIndex] = popsInChutes[popKindIndex] + pops.Count;

            //records the coin type that was placed in the specified chute
            popTypesinChutes[popKindIndex] = pops[0].Name;
        }

        public void insertCoin(int vmIndex, Coin coin) {
            // TODO: Implement
        }

        public void pressButton(int vmIndex, int value) {
            // TODO: Implement
        }

        public List<Deliverable> extractFromDeliveryChute(int vmIndex) {
            // TODO: Implement
            return new List<Deliverable>();
        }

        public List<IList> unloadVendingMachine(int vmIndex) {
            // TODO: Implement
            return new List<IList>() {
                new List<Coin>(),
                new List<Coin>(),
                new List<Pop>() };
            }
    }
}