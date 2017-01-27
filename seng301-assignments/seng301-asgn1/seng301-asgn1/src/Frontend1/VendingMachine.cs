using System;
using System.Collections;
using System.Collections.Generic;

namespace Frontend1
{
    //VendingMachine class for defining the specific vending machine's two attributes
    public class VendingMachine
    {
        public List<int> coinKinds;
        public List<Pop> popKinds;
        public int selectionButtonCount;
        public List<int> popCosts;
        public List<List<Coin>> coinChutes;
        public List<int> coinsInChutes;
        public List<List<Pop>> popChutes;
        public List<int> popsInChutes;
        public List<Pop> soldPop;
        public List<Coin> change;
        public int credit;
        public int moneyMade;

        public VendingMachine(List<int> coinKinds, int selectionButtonCount)
        {
            this.coinKinds = coinKinds;
            this.selectionButtonCount = selectionButtonCount;
            popKinds = new List<Pop>();
            soldPop = new List<Pop>();
            change = new List<Coin>();
            credit = 0;
            moneyMade = 0;
        }

        //set the coin types to their specific chutes
        public void setCoinChutes(List<List<Coin>> coinChutes)
        {
            this.coinChutes = coinChutes;
        }

        //set the number of coins in each chutes
        public void setCoinNum(List<int> coinsInChutes)
        {
            this.coinsInChutes = coinsInChutes;
        }

        //set the cost of each pop type
        public void setCosts(List<int> popCosts)
        {
            this.popCosts = popCosts;
        }

        //set the pop types to their specific chutes
        public void setPopChutes(List<List<Pop>> popChutes)
        {
            this.popChutes = popChutes;
        }

        //set the number of pops in each chutes
        public void setPopNum(List<int> popsInChutes)
        {
            this.popsInChutes = popsInChutes;
        }

        //add a pop to the list of pop sold
        //remove the pop from the its original chute
        public void vendPop(Pop popName, int value)
        {
            soldPop.Add(popName);
            popChutes[value].RemoveAt(0);
            popsInChutes[value] = popsInChutes[value] - 1;
        }

        //add to credit
        public void addCredit(int credit)
        {
            this.credit = this.credit + credit;
        }

    }
}
