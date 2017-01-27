Each of these scripts is like a set of actions one could take on the vending machine.

* U01-bad-script1 should fail on the first line, because you can't configure a vending machine before you've created it
* U02-bad-script2 should fail because 0 is not allowed as a cost for a drink! Then, when you get that to work, the teardown should also be broken
* T01-good-script should succeed, but does not without a working implementation.

You ought to populate this directory with your own scripts to drive the vending machine factory, and vending machines. For instance, what happens if you create a script that has the same coin kinds in the vending machine? Is this allowed? Does it correctly output the correct pop when you press a button? What if you keep loading money in, and vend several drinks? Will it deliver it properly? Etc.

Note:
*** All the CHECK_DELIVERY commands in the added scripts are designed to PASS test cases THE MACHINE thinks is correct (not the person buying).
*** All the CHECK_TEARDOWN commands in the added scripts are designed to PASS test cases THE OPERATOR thinks is correct (not the machine).

Added Scripts:
* T02-good-script1 should succeed. Edge cases tested in this script: when one or more coins/pops are loaded into the wrong chutes, press when there's not enough credit then more is added, unsorted coin chutes (not in ascending or descending order), extract after multiple transactions, reload pop/coin after unloading
* T03-good-script2 should succeed. Edge cases tested in this script: when the pop requested is sold out, when there's not enough change in the vending machine, unload vending machine with no coins in coin chutes and no pops, unload when there's money "in limbo", unload twice in a row
* T04-good-script3 should succeed. Edge cases tested in this script: normal commands on multiple vending machines, pressing buttons twice in a row, insert unaccepted coin types, extract when there's nothing in the delivery chute
* U03-bad-script3 should fail because vending machine is configured after all other commands, also the list of accepted coins contain non-unique and negative values. If those errors are fixed, test will still fail because unload tried to unload a vending machine that doesn't exist.
* U04-bad-script4 should fail because vending machine is configured with more selections than buttons and user tried to press a button that doesn't exist. This script also fails because some genius tried to put a Coke into a coin chute. They also tried to put a coin into a pop chute

Known Bugs:
* Due to the implementation, the vending machine cannot be configured twice without "removing" all of the previously stored coins/pops.