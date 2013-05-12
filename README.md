# WinDevices

Playing with [SetupAPI] and [PnP Configuration Manager] APIs in C#. Poke me on Twitter ([@apolak]) if you find it useful.


## Backlog

Features:

* Setting up device information sets
* Enumerating devices and device interfaces
* Reading and writing device properties
* Receiving device change notifications
* Traversing the devnode tree
* Enumerating installed setup classes
* Reading setup class properties
* Enumerating drivers

Other:

* Design exception handling
* Make use of [constrained execution regions]
* Annotate members with [security attributes]
* Ensure Windows XP compatibility
* Implement platform support checks


[SetupAPI]: http://msdn.microsoft.com/en-us/library/windows/hardware/ff550897.aspx
[PnP Configuration Manager]: http://msdn.microsoft.com/pl-pl/library/windows/hardware/ff549717.aspx
[@apolak]: https://twitter.com/apolak
[constrained execution regions]: http://msdn.microsoft.com/en-us/library/ms228973.aspx
[security attributes]: http://msdn.microsoft.com/en-us/library/dd233102.aspx
