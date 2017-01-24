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

            //create and use a sorted list for error testing
            List<int> tempList = new List<int>(coinKinds);
            tempList.Sort();
            int temp = -1;

            //testing only; remove later
            //Console.WriteLine("Accepted Coins: ");

            for (int i = 0; i < tempList.Count; i++)
            {
                //test for invalid (negative/zero) coin types
                if (tempList[i] <= 0)
                {
                    throw new Exception("The coin value must be greater than 0. The argument passed was: " + tempList[i]);
                }

                //test for non-unique coin types
                else if (temp == tempList[i])
                {
                    throw new Exception("The coin value must be unique. The argument " + tempList[i] + " already exists");
                }

                //testing only; remove later
                else
                {
                    //Console.WriteLine("{0}", tempList[i]);
                    //Console.WriteLine(temp);
                    //Console.WriteLine(tempList[i]);
                }

                temp = tempList[i];
            }

            //test for invalid (negative) number of buttons
            if (selectionButtonCount < 1)
            {
                throw new Exception("The number of buttons must be greater than 0. The argument passed was: " + selectionButtonCount);
            }

            else
            {
                //testing only; remove later
                //Console.WriteLine("Number of Buttons:" + selectionButtonCount);

                //create vending machine if all parameters are valid
                vmList.Add(new VendingMachine(coinKinds, selectionButtonCount));
            }

            //Console.WriteLine("{0}", vmList.Count);
            return vmNum++;
        }

        public void configureVendingMachine(int vmIndex, List<string> popNames, List<int> popCosts) {

            vm = vmList[vmIndex];

            //create a list integers that records the value of the coin stored in coinChutes
            List<int> coinChutes = new List<int>();
            //create a list of strings that record the name of the pop stored in popChutes
            List<String> popChutes = new List<String>();

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

            //configure coin types to individual coin chutes
            for (int i = 0; i < vm.coinKinds.Count; i++)
            {
                coinChutes.Add(vm.coinKinds[i]);
                //Console.WriteLine(coinChutes[i]);
            }

            vm.setCoinChutes(coinChutes);

            //configure pop types to individual pop chutes
            for (int i = 0; i < vm.selectionButtonCount; i++)
            {
                popChutes.Add(popNames[i]);
                //Console.WriteLine(popChutes[i]);
            }

            vm.setPopChutes(popChutes);
            vm.setCosts(popCosts);

        }

        public void loadCoins(int vmIndex, int coinKindIndex, List<Coin> coins) {

            vm = vmList[vmIndex];
            //create list for number of coins in each chute
            List<int> coinsInChutes = new List<int>();

            //initialize number of coins in each chute to zero
            for (int i = 0; i < vm.coinChutes.Count; i++)
            {
                coinsInChutes.Add(0);
            }

            //test if there's invalid input for coinKindIndex
            if (coinKindIndex > vm.coinChutes.Count || coinKindIndex < 0)
            {
                throw new Exception("Coin kind index cannot be greater than number of coin types nor negative.");
            }

            //"add" coins to the specified chute
            coinsInChutes[coinKindIndex] = coinsInChutes[coinKindIndex] + coins.Count;
            vm.setCoinNum(coinsInChutes);

        }

        public void loadPops(int vmIndex, int popKindIndex, List<Pop> pops) {

            vm = vmList[vmIndex];
            //create list for number of pops in each chute
            List<int> popsInChutes = new List<int>();

            //initialize number of pops in each chute to zero
            for (int i = 0; i < vm.popChutes.Count; i++)
            {
                popsInChutes.Add(0);
            }

            //test if there's invalid input for popKindIndex
            if (popKindIndex > vm.popChutes.Count || popKindIndex < 0)
            {
                throw new Exception("Pop kind index cannot be greater than number of pop types nor negative.");
            }

            //"add" pops to the specified chute
            popsInChutes[popKindIndex] = popsInChutes[popKindIndex] + pops.Count;
            vm.setPopNum(popsInChutes);

        }

        public void insertCoin(int vmIndex, Coin coin) {

            vm = vmList[vmIndex];

            int i = 0;

            //test if coin value is accepted
            while (coin.Value != vm.coinKinds[i] && i < vm.coinKinds.Count)
            {
                i++;
                //if end of list reached
                if (i == vm.coinKinds.Count)
                {
                    //replace with dispense later
                    throw new Exception("Unacceptable coin!");
                }
            }

            //add accepted coin to credit
            vm.addCredit(coin.Value);
        }

        public void pressButton(int vmIndex, int value) {

            vm = vmList[vmIndex];

            if (value < 0 || value > vm.selectionButtonCount)
            {
                throw new Exception("Button index cannot be negative or bigger than number of avaliable buttons");
            }

            //case where no change is needed
            //check to see if there's one or more pop left in that chute
            if (vm.popsInChutes[value] > 0)
            {
                //case where no change is needed
                if (vm.credit == vm.popCosts[value])
                {
                    vm.popsInChutes[value] = vm.popsInChutes[value] - 1;
                    vm.vendPop(vm.popChutes[value]);
                }

                //case if change is needed
                else if (vm.credit > vm.popCosts[value])
                {
                    //vend the pop
                    vm.popsInChutes[value] = vm.popsInChutes[value] - 1;
                    vm.vendPop(vm.popChutes[value]);

                    //create and use a sorted list to find change
                    List<int> tempList = new List<int>(vm.coinKinds);
                    tempList.Sort();

                    int change = vm.credit - vm.popCosts[value];
                    int changeTemp = 0;
                    int maxTemp = 0;
                    //int i = 0;

                   /* for(int i = 0; i < tempList.Count; i++)
                    {
                        for(int j = 0; j < tempList.Count; i++)
                        {
                            changeTemp = tempList[i] + tempList[j] ;

                            if(changeTemp < change && changeTemp > maxTemp)
                            {
                                maxTemp = changeTemp;
                            }
                        }
                    }

                    //find the largest coin type that's less or equal to change 
                    while(tempList[i] < change && i < tempList.Count - 1)
                    {
                        changeTemp = changeTemp + tempList[i];
                        if(changeTemp > change && tempList[i] > changeTemp)
                        {

                        }
                        i++;
                        if(tempList[i] == change || changeTemp == change)
                        {
                            vm.change = change;
                        }

                    }*/


                }

            }
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