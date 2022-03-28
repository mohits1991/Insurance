Used SQL server DB for database for Task 5.

How to setup Database :-
1. In Package Manager Console in Visual Studio - Run command -->  update-database InsuranceDB

dbconfig is set for local in appsettings.json

Rest everything is same to run the solution. Building solution should resolve any package issues. 
Let me know if something is not working.

-------------------------------------------------------------------------------------------------


To make solution working :- 
1. Updated package of Newtonsoft.Json and added package for swashbuckle.ASpNetCore
2. Added swagger settings in startup file.Swagger is activated.
3. Updated Properties/launchSettings.json with "launchUrl": "swagger"

Now I saw that project need a lot of restructuring and refactoring, 
So before fixing Task 1, I did Task 2 to refactor and restructure the code. 
Because then it is easier to fix things and implement new features by extending the solution.

Task 2 :-
1. Created 1 more project for Business logic layer and followed SOLID principles. 
Reason - Code readability, maintainability,loosely coupled classes, easy to extend, easy to test, easy to fix bugs and build new features.
2. Insurance.Bll layer will have all logic inside services including seperated interfaces. Models are also inside Bll. Ideally we can create a ViewModel structure and map it but it's too much to do so.
3. Dependency injection is used to use interfaces and unit tests are added for controller and service layers.
4. Http client is made generic and used inside ProductService.
5. Code is self explanatry and comments are added wherever necessary.
6. Exception handling and logging is added.
7. Unit test cases are also refactored and Moq is used to mock data.

Task 1 :-
1. Fixed while refactoring in CalculateProductInsuranceAsync. Used ProductTypeId to compare instead of productname.
2. Unit test cases updated.

Task 3 :-
1. Assumption - A list of products are provided with product ids. Ideal only order id should be provided and with order id all products should be fetched.
2. Implementation is Self explanatry in code. Starting point is CalculateOrderInsurance in Insurance controller.
3. Unit tests are added for controller and service.

Task 4 :-
1. Added condition in CalculateOrderInsuranceAsync and it was fixed. Used ProductTypeId to compare instead of productname.
2. Unit test cases updated.

Task 5 :-
1. Added a new layer to interact with database. Insurance.Dal :- Entityframework is used and it is code first approach.
2. Created Surcharge service under Insurance.Dll and a seperate controller for surcharge endpoints.
3. Also added extra endpoints for fetching surcharges from database.
4. Code and swagger is self explanatry.
5. Error handling and logging is done.
6. Unit test cases are added.

-- Test projects can also be seperated in different layers per project but it is also too much to do so now. More test cases can be added but I have added enough to check the knowledge base.
