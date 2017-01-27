using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
                //create vending machine if all parameters are valid
                vmList.Add(new VendingMachine(coinKinds, selectionButtonCount));
            }

            return vmNum++;
        }

        public void configureVendingMachine(int vmIndex, List<string> popNames, List<int> popCosts) {

            if (vmIndex > vmList.Count - 1)
            {
                throw new Exception("This vending machine doesn't exist!");
            }

            vm = vmList[vmIndex];

            //create a list coins that records the value of the coin stored in coinChutes
            List<List<Coin>> coinChutes = new List<List<Coin>>();
            //create a list of pops that record the name of the pop stored in popChutes
            List<List<Pop>> popChutes = new List<List<Pop>>();

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

            //initialize lists of coins and configure coin types to individual coin chutes
            for (int i = 0; i < vm.coinKinds.Count; i++)
            {
                List<Coin> sublist = new List<Coin>();
                coinChutes.Add(sublist);
                //Console.WriteLine(coinChutes[i]);
            }

            vm.setCoinChutes(coinChutes);

            //configure pop types to individual pop chutes
            for (int i = 0; i < vm.selectionButtonCount; i++)
            {
                List<Pop> sublist = new List<Pop>();
                popChutes.Add(sublist);
                //Console.WriteLine(popChutes[i]);
            }

            vm.setPopChutes(popChutes);
            vm.setCosts(popCosts);

            //create list for number of coins in each chute
            List<int> coinsInChutes = new List<int>();

            //initialize number of coins in each chute to zero
            for (int i = 0; i < vm.coinChutes.Count; i++)
            {
                coinsInChutes.Add(0);
            }

            //create list for number of pops in each chute
            List<int> popsInChutes = new List<int>();

            //initialize number of pops in each chute to zero
            for (int i = 0; i < vm.popChutes.Count; i++)
            {
                popsInChutes.Add(0);
                vm.popKinds.Add(new Pop(popNames[i]));
            }

            vm.setCoinNum(coinsInChutes);
            vm.setPopNum(popsInChutes);

        }

        public void loadCoins(int vmIndex, int coinKindIndex, List<Coin> coins) {

            if (vmIndex > vmList.Count - 1)
            {
                throw new Exception("This vending machine doesn't exist!");
            }

            vm = vmList[vmIndex];

            //test if there's invalid input for coinKindIndex
            if (coinKindIndex > vm.coinChutes.Count || coinKindIndex < 0)
            {
                throw new Exception("Coin kind index cannot be greater than number of coin types nor negative.");
            }

            for (int i = 0; i < coins.Count; i++)
            {
                vm.coinChutes[coinKindIndex].Add(coins[i]);
            }

            //"add" coins to the specified chute
            vm.coinsInChutes[coinKindIndex] = vm.coinsInChutes[coinKindIndex] + coins.Count;

        }

        public void loadPops(int vmIndex, int popKindIndex, List<Pop> pops) {

            if (vmIndex > vmList.Count - 1)
            {
                throw new Exception("This vending machine doesn't exist!");
            }

            vm = vmList[vmIndex];

            //test if there's invalid input for popKindIndex
            if (popKindIndex > vm.popChutes.Count || popKindIndex < 0)
            {
                throw new Exception("Pop kind index cannot be greater than number of pop types nor negative.");
            }

            for (int i = 0; i < pops.Count; i++)
            {
                vm.popChutes[popKindIndex].Add(pops[i]);
            }

            //"add" pops to the specified chute
            vm.popsInChutes[popKindIndex] = vm.popsInChutes[popKindIndex] + pops.Count;

        }

        public void insertCoin(int vmIndex, Coin coin) {

            if (vmIndex > vmList.Count - 1)
            {
                throw new Exception("This vending machine doesn't exist!");
            }

            vm = vmList[vmIndex];

            int i = 0;

            //test if coin value is accepted
            while (i < vm.coinKinds.Count)
            {
                if (coin.Value == vm.coinKinds[i])
                {
                    //add accepted coin to credit
                    vm.addCredit(coin.Value);

                    break;
                }

                //if end of list reached and no matching coin value is found
                if (i == vm.coinKinds.Count - 1)
                {
                    Coin unacceptable = new Coin(coin.Value);
                    vm.change.Add(unacceptable);
                }
                i++;
            }
        }

        public void pressButton(int vmIndex, int value) {

            if (vmIndex > vmList.Count - 1)
            {
                throw new Exception("This vending machine doesn't exist!");
            }

            vm = vmList[vmIndex];

            //Console.WriteLine(vm.credit);

            if (value < 0 || value > vm.selectionButtonCount - 1)
            {
                throw new Exception("Button index cannot be negative or bigger than number of avaliable buttons");
            }

            //case if there's one or more pop left in that chute
            if (vm.popsInChutes[value] > 0)
            {
                //case where no change is needed
                if (vm.credit == vm.popCosts[value])
                {
                    //vend the pop
                    vm.vendPop(vm.popKinds[value], value);

                    vm.moneyMade = vm.moneyMade + vm.credit;
                    vm.credit = 0;

                    //Coin totalChange = new Coin(0);
                    //vm.change.Add(totalChange);
                }

                //case if change is needed
                else if (vm.credit > vm.popCosts[value])
                {
                    //vend the pop
                    vm.vendPop(vm.popKinds[value], value);

                    int changeNeeded = vm.credit - vm.popCosts[value];
                    int changeGiven = 0;

                    //variable for holding the temporary smallest denominator
                    //used to determine ending condition
                    int tempMin = 100000000;
                    int tempMinIndex = -1;

                    do
                    {
                        //variable for holding the temporary largest denominator
                        int tempMax = 0;
                        int tempMaxIndex = -1;

                        for (int i = 0; i < vm.coinKinds.Count; i++)
                        {
                            if (vm.coinKinds[i] > tempMax && vm.coinKinds[i] <= changeNeeded && vm.coinsInChutes[i] > 0)
                            {
                                tempMax = vm.coinKinds[i];
                                tempMaxIndex = i;
                                //Console.WriteLine("current max:" + tempMax);
                            }

                            if (vm.coinKinds[i] < changeNeeded && vm.coinKinds[i] < tempMin && vm.coinsInChutes[i] > 0)
                            {
                                tempMin = vm.coinKinds[i];
                                tempMinIndex = i;
                                //Console.WriteLine("current min:" + tempMax);
                            }
                        }

                        changeNeeded = changeNeeded - tempMax;
                        if (tempMaxIndex >= 0)
                        {
                            changeGiven = changeGiven + vm.coinKinds[tempMaxIndex];
                            //decrease the number of coin value in that chute by 1
                            vm.coinsInChutes[tempMaxIndex] = vm.coinsInChutes[tempMaxIndex] - 1;
                            //remove coin from the chute
                            vm.coinChutes[tempMaxIndex].RemoveAt(0);
                        }

                        else
                        {
                            changeGiven = 0;
                        }
                        //Console.WriteLine("change needed:" + changeNeeded);
                        //Console.WriteLine("current change:" + changeGiven);

                        // case where all change chutes are empty
                        if (tempMinIndex < 0)
                        {
                            break;
                        }

                    } while (vm.coinsInChutes[tempMinIndex] > 0 && changeNeeded - vm.coinKinds[tempMinIndex] >= 0);

                    vm.moneyMade = vm.moneyMade + vm.credit;
                    vm.credit = 0;

                    if (changeGiven > 0)
                    {
                        Coin totalChange = new Coin(changeGiven);
                        vm.change.Add(totalChange);
                    }
                    //Console.WriteLine("total change: " + changeGiven);
                }

                //case credit not enough to buy pop
                else
                {
                    int remainder = vm.popCosts[value] - vm.credit;
                    //Console.WriteLine("Not enough credit. Please Insert "+ remainder + " more");
                }

            }

            //if the pop is sold out
            else
            {
                //Console.WriteLine("Sold out! Sorry :( ");
            }
        }

        public List<Deliverable> extractFromDeliveryChute(int vmIndex) {

            if (vmIndex > vmList.Count - 1)
            {
                throw new Exception("This vending machine doesn't exist!");
            }

            vm = vmList[vmIndex];

            List<List<Deliverable>> sold = new List<List<Deliverable>>();

            if(vm.soldPop.Count > 0)
            {
                sold.Add(vm.soldPop.ToList<Deliverable>());
            }

            if (vm.change.Count > 0)
            {
                int totalChange = 0;

                //add up all the change inside the delivery chute
                for (int i = 0; i < vm.change.Count; i++)
                {
                    totalChange = totalChange + vm.change[i].Value;
                    //Console.WriteLine(totalChange);
                }

                //place the sum of all change in a new list
                List<Coin> change = new List<Coin>();
                change.Add(new Coin(totalChange));

                sold.Add(change.ToList<Deliverable>());
            }

            //clear change list
            vm.change = new List<Coin>();
            //clear soldPop list
            vm.soldPop = new List<Pop>();

            List<Deliverable> deliveryChute = sold.SelectMany(x => x).ToList();

            return deliveryChute;
        }

        public List<IList> unloadVendingMachine(int vmIndex) {

            if (vmIndex > vmList.Count - 1)
            {
                throw new Exception("This vending machine doesn't exist!");
            }

            vm = vmList[vmIndex];

            List<Coin> changeUnload = new List<Coin>();

            //calculate total amount of change left over if there's change left over
            int totalChangeUnload = 0;

            for (int i = 0; i < vm.coinChutes.Count; i++)
            {
                if (vm.coinsInChutes[i] > 0)
                {
                    for (int j = 0; j < vm.coinsInChutes[i]; j++)
                    {
                        totalChangeUnload = totalChangeUnload + vm.coinChutes[i][j].Value;
                        //Console.WriteLine(totalChangeUnload);
                    }
                }
            }

            if (totalChangeUnload > 0)
            {
                changeUnload.Add(new Coin(totalChangeUnload));
            }

            List<Coin> profitUnload = new List<Coin>();
            //if some money was made
            if (vm.moneyMade > 0)
            {
                profitUnload.Add(new Coin(vm.moneyMade));
            }

            List<Pop> popUnload = new List<Pop>();

            //int k = 0;

            for (int i = 0; i < vm.popChutes.Count; i++)
            {
                if (vm.popsInChutes[i] > 0)
                {
                    for (int j = 0; j < vm.popsInChutes[i]; j++)
                    {
                        popUnload.Add(vm.popChutes[i][j]);
                        //Console.WriteLine(popUnload[k].Name);
                        //k++;
                    }
                }
            }

            return new List<IList>() {
                new List<Coin>(changeUnload),
                new List<Coin>(profitUnload),
                new List<Pop>(popUnload) };
            }
    }
}