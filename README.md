[Odin](http://github.com/Vanaheimr/Odin) is the CEO of Vanaheimr. Therefore he is not a technical guy
providing you the latest and greatest stuff, but helps you to get familiar and comfortable with the
Vanaheimr graph processing stack. Odin comes with all neccessary Vanaheimr subprojects as
**git submodules** to make your life easier. In order to download the project type:

    git clone --recursive git://github.com/Vanaheimr/Odin.git
    
or use the more traditional way:

    git clone git://github.com/Vanaheimr/Odin.git
    git submodule init
    git submodule update
	
If you want to update all submodules to their latest version type:	
	
    git submodule foreach git pull -q origin master

**NOTE**: git submodules are controversial. Nevertheless they can help to keep things simple, when they are simple.
If you want to dive into the deepths of Vanaheimr you probably avoid them.
