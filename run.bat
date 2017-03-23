REM Run tests
Jerboa\packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe --labels=All --out=TestResult.txt "--result=TestResult.xml;format=nunit2" Jerboa\Jerboa.Tests\bin\Debug\Jerboa.Tests.dll
REM Generate report
Jerboa\packages\SpecFlow.2.1.0\tools\specflow.exe nunitexecutionreport Jerboa\Jerboa.Tests\Jerboa.Tests.csproj /out:MyResult.html