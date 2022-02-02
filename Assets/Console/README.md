Unity Console

INFO:
Thank you for using this console! with other about 3 months in the making (only 1 month of work though, i started this a while ago
and then took a break for a while) here it is.

HOW TO IMPORT:
Import the console folder into unity and wait for the console to load.
Once loaded, click the new `Console` tab on the top (with file, edit, etc...) in here, there is an option to setup
Wait for the console to load (this shouldn't take long) and it has now been added to your game!

HOW TO USE:
Most of the instructions for using this console are included in the console / commands itself, just click `F9` to open
or the mobile button, There is a built-in help for commands and more included in the console.
By default, the console will show logs of what happens (Debug.Log() messages) but can also be used to enter commands.

CREATING CUSTOM COMMANDS:
If you want to create a command that is not already included by default, look no further. The script `CommandLayout` will help you
setup your own custom command for you to use. I personally recommend you to keep the `CommandLayout` script and duplicate it when
needed. All commands that you make can be put in one of the command folders, but i would recommend the `Custom` folder to keep them organised.
All commands (custom or default) are automatically loaded in to the console on launch of the game so you don't have to do anything!

