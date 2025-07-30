# SeVago.Jewelry
Step by step to run project
1. Clone source
 - using command git clone: "git clone https://github.com/GitHub-NguyenChiLinh/SeVago.Jewelry.git"
2. Open project via Visual Studio or Visual Studio Code
   2.1 Visual Studio
    - Open Package Manager Console type: "dotnet build" and enter
    - After the build succeced type: "dotnet ef migrations add Initial" enter and then type "dotnet ef database update" enter
    - Finally, press start (http) button, the project will be run on "http://localhost:5088" and swagger "http://localhost:5088/swagger/index.html"
   2.2 Visual Studio Code
    - Open terminal as Git bash type: "dotnet build" and enter
    - After the build succeced type: "dotnet ef migrations add Initial" enter and then type "dotnet ef database update" enter
    - Finally, type: "dotnet run" the project will be run on "http://localhost:5088" and swagger "http://localhost:5088/swagger/index.html"
3. Postman collection
  - Open Postman and click import: upload file: "Sevago.jewelry.postman_collection.json"
4. UnitTest
