using System;
using System.Collections;
using System.Collections.Generic;

namespace Frontend1
{
    //VendingMachine class for defining the specific vending machine's two attributes
    public class VendingMachine
    {
        public List<int> coinKinds;
        public int selectionButtonCount;
        public List<int> popCosts;
        public List<int> coinChutes;
        public List<int> coinsInChutes;
        public List<String> popChutes;
        public List<int> popsInChutes;
        public List<String> soldPop;
        public int change;
        public int credit;

        public VendingMachine(List<int> coinKinds, int selectionButtonCount)
        {
            this.coinKinds = coinKinds;
            this.selectionButtonCount = selectionButtonCount;
            soldPop = new List<string>();
            credit = 0;
        }

        //set the coin types to their specific chutes
        public void setCoinChutes(List<int> coinChutes)
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
        public void setPopChutes(List<String> popChutes)
        {
            this.popChutes = popChutes;
        }

        //set the number of pops in each chutes
        public void setPopNum(List<int> popsInChutes)
        {
            this.popsInChutes = popsInChutes;
        }

        //add a pop to the list of pop sold but not yet extracted
        public void vendPop(String popName)
        {
            soldPop.Add(popName);
        }

        //add to credit
        public void addCredit(int credit)
        {
            this.credit = this.credit + credit;
        }

    }
}
