# How to build
The code has been developed using Visual Studio 2017 community edition, using .Net Core 2.0. 

Note: It has not been tested in Visual Studio 2015 and based on the accepted SO answer here, will not work (as is, without modification) on that version: https://stackoverflow.com/questions/42337211/asp-net-core-support-in-visual-studio-2015

Open the solution in Visual Studio 2017, having .net core 2 installed, then select the usual Build option from the dropdown or (CTRL + SHIFT + B) to build the solution.

# How to run
TFL.App is the startup project. Being a console app, the entry point as usual is the Program -> Main method.

To run in Visual Studio, select 'Start debugging' or 'Start without debugging' from the Visual Studio Debug drop down.

To run in Powershell or Command, navigate to the following directory, having done a build: 
~\TFL\TFL.App\bin\Debug\netcoreapp2.0\netcoreapp2.0 

Then type in "dotnet TFL.App.dll". You can now request the status of a road by typing something in.

# How to run tests
Tests are contained in the TFL.Tests project and have been written using NUnit with Moq. To run the tests, use the Test drop down menu option in Visual Studio and select Run -> All tests. This should show the Test Explorer with each test listed.

# Changing keys
In the TFL.App project directory, open up the appsettings.json file and replace the XX in the Username field with your API Id and the XX in the Password field with your Api Key.

