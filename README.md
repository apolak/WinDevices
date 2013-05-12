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


## License

__Simplified BSD License__

Copyright (c) 2013 Aleksander Polak. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met: 

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

The views and conclusions contained in the software and documentation are those
of the authors and should not be interpreted as representing official policies, 
either expressed or implied, of the FreeBSD Project.


[SetupAPI]: http://msdn.microsoft.com/en-us/library/windows/hardware/ff550897.aspx
[PnP Configuration Manager]: http://msdn.microsoft.com/pl-pl/library/windows/hardware/ff549717.aspx
[@apolak]: https://twitter.com/apolak
[constrained execution regions]: http://msdn.microsoft.com/en-us/library/ms228973.aspx
[security attributes]: http://msdn.microsoft.com/en-us/library/dd233102.aspx
