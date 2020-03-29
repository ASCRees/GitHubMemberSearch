GitHub Member Search
--------------------

GitHub Member Search is a simple one page MVC application that allows the user to search github.com for a user, based upon their username and will display their name, location and avatar, along with the the top five repositories based upon stargazer rating.

The code demonstrates making HTTP requests to web apis through a single service layer and returning the results to the model builder.

If no results are found the system will display an appropriate message. 

Unit testing is performed using NUNIT and demonstrates the use of the MOQ framework. 
Functional testing is performed using Selenium. 
Javascript testing is performed using Jasmine.

After cloning run "Update-package -reinstall" in Package Manager console. 

![image](https://user-images.githubusercontent.com/28151071/77829651-4130e300-711b-11ea-8830-e4544b32b77f.png)
