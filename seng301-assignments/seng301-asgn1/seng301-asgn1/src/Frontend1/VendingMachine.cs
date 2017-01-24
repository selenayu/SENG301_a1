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

        public VendingMachine(List<int> coinKinds, int selectionButtonCount)
        {
            this.coinKinds = coinKinds;
            this.selectionButtonCount = selectionButtonCount;
        }

    }
}
