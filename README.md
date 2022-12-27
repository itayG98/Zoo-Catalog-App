# Zoo-Blog web app
## Asp Net.Core MVC web app using MSSQL Docker image EF7 , Identity and Boostrap

<p align="center" >Main catalog page where you can scroll and choose animel to explore and comment</p>
<p align="center">
  <img width="600"  src="https://user-images.githubusercontent.com/91791115/202903498-6fb8f5dc-bd28-49b2-b7b3-53a8f4cb4667.jpg">
</p>

 ## About 

This Asp.NetCore web app demonstrates MVC pattern and microservices separation : 
- Asp.NetCore MVC server micro-service which is animel catalog web application
- MSSQL server  runs on docker images from microsoft using this command : <br/>
 `docker run -e "ACCEPT_EULA=Y" --name db --network petshop-bridge -e "MSSQL_SA_PASSWORD=1234ABCD@" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU14-ubuntu-20.04`
- netwrok bridge called petshop-bridge


This app includes Docker-file and Docker-Compose which works with images that i already published 
### application Server :
`
docker pull itayg98/zoo-app:1.2
`
### The DB :
`
docker pull itayg98/zoo-db:1.2
`  <br/>
It is very important to add an apropriate connecrion string using the MSSQL containers name , port and add TCP :
https://github.com/itayG98/Zoo-Catalog-App/blob/9667d1c89c78fc38f737d6e99b26ed7c6dde34ad/Zoo-Blog-master/Zoo/appsettings.json#L9-L12

### Docker-Compose.yml file
https://github.com/itayG98/Zoo-Catalog-App/blob/e4a3254457b378365bef261e6fccbe4aff56d701/Zoo-Blog-master/docker-compose.yml#L1-L27


 ## Model (Entity Framwork Core)
 
<p align="center" >MSSQL Diagram</p>
<p align="center">
  <img width="600"  src="https://user-images.githubusercontent.com/91791115/202903162-5d404a42-0fa5-4d17-b041-de35c044f036.jpg">

My model contain 3 objects : Category ,Animal and Comment.
I gave each of them several propetries and fitting validation attributes including Regex patterns, Data type custom messege errors etc..
I created two custom Vlidation Attributes: 
1. Birthdate to validate the animal is less than 150 years and was born in the current day or earlier
2. File validator to check wether if the file's content Type include the word "Image" and the size of the file limited to 10MB 

https://github.com/itayG98/Zoo-Catalog-App/blob/4a882f107dc46814672801a49eade8df2b704d46/Zoo-Blog-master/Model/Models/Attributes/ImageFileValidationAttribute.cs#L11-L26

In order to generate the categories i made an Enum helper model which is not mapped in to the DataBase but i use to generate apropriate select tag

The model project contains also the data access layer  Generic base Repsoitory class for each entity whom id is of type Guid and one service of image formating which help me to save the images files as bytes array and generate the image back on the client side
  
  https://github.com/itayG98/Zoo-Catalog-App/blob/4a882f107dc46814672801a49eade8df2b704d46/Zoo-Blog-master/Model/Services/ImagesFormater.cs#L29-L39
  https://github.com/itayG98/Zoo-Catalog-App/blob/4a882f107dc46814672801a49eade8df2b704d46/Zoo-Blog-master/Model/Services/ImagesFormater.cs#L83-L90


 ## View
 I've created several views for the controllers, one view component and 3 usefull partial view for layout styles and scripts and nav bar
 The nav bar is used to navigate between the diffrent views and actions
 
 <p align="center" >The app's nav bar</p>
<p align="center">
  <img width="700"  src="https://user-images.githubusercontent.com/91791115/202901618-8e64cce6-d52f-43da-82e9-15318e100dbb.jpg">
</p>

The manager view of Create and update contain a vannila JS validation of the file type and it size in order to prevent the browser to throw an error
https://github.com/itayG98/Zoo-Catalog-App/blob/fa2e0fcd62d77a1b6cb19dbb7888a489544c1a89/Zoo-Blog-master/Zoo/wwwroot/Scripts/animalcreateformvalidator.js#L23-L46

  ## Controllers
 This project contain 4 controllers :
 1. Home - displaying the two most commented animals
 2. Manager - Handling the CRUD operation on the animals data
 3. Catalog - view the animals in the blog and can sort them by category
 4. Animel Data - Explore the animals details and allow the user to leave a comment. The comment posting uses Fetch api in order to prevent the page to relload each time the user post a comment.
 
https://github.com/itayG98/Zoo-Catalog-App/blob/fa2e0fcd62d77a1b6cb19dbb7888a489544c1a89/Zoo-Blog-master/Zoo/wwwroot/Scripts/CommentFetch.js#L48-L64
 
 <p align="center" >Hello world comment</p>
<p align="center">
  <img width="700"  src="https://user-images.githubusercontent.com/91791115/202901759-39421184-95c9-4a33-bd18-38543c79cc81.jpg">
</p>

## Authentication && Authorization (Identity)

I used Identity Nuget and  seperate context in order to authenticate and authorize users in my web application and registering and loging in handels by model helpers named 
LoginModel  and SignUpModel 
In the app there are 3 types of users "Admin" , "User" adn Anonymous .
The manager role can use the Manager controller and has anchor nav-link to creation and update .
every signed user can comment on animals in the application (including managers).
Anonymous user can only scroll through the animels catalog page or Register/Log In.
  <p align="center" > Registering action :</p>
<p align="center">
https://github.com/itayG98/Zoo-Catalog-App/blob/fa2e0fcd62d77a1b6cb19dbb7888a489544c1a89/Zoo-Blog-master/Zoo/Controllers/UserController.cs#L49-L74
</p>


## Unit Testing
This web app solution includes one Xunit project of testing for the repository layer checking and validating the ReposiroeyBase class for both sync and async methods .
 
  <p align="center" > Test example :</p>
<p align="center">
 https://github.com/itayG98/Zoo-Catalog-App/blob/fa2e0fcd62d77a1b6cb19dbb7888a489544c1a89/Zoo-Blog-master/UnitTesting/AsyncRepositoryTest.cs#L129-L144
</p>

