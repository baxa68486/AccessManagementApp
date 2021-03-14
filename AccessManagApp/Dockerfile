#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#It means we build out Image based on mcr.microsoft.com/dotnet/aspnet_Image of version 5.0
# "AS base" -- means first stage of the building container. 
    #This is a good sideway into the concept of multi-stage wheels which is enabled here.
    #it means there will be more than one stage in building process
    #and in each stage you can specify a different set of instructions that may have nothing to do with distractions
    #executed in some other stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#Get base SDK Image from Microsoft.It comes from another "base image dotnet/sdk".
   #'As build' is used, 
   # dotnet/sdk is an image that has all build tools and all the libraries, everything
   # thas is needed to build a .NET 5 app, which is not the same that you need to just run at the .NET 5 app
   # so whatever is coming in this sdk image is potentially
   # much bigger there is much much more files compilers and stuff there need for building
   # and not needed for running it so that means that your final images that's going to go
   #actually from the base image going to be much smaller than the image that you are going to use to build your container
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src #src means we are going to place any files from here on
#Copy the CSPROJ file 
COPY ["AccessManagApp/AccessManagApp.csproj", "AccessManagApp/"]
COPY ["DataAccessLayer/DataAccessLayer.csproj", "DataAccessLayer/"]
#restore any dependecies (via NUGET)
RUN dotnet restore "AccessManagApp/AccessManagApp.csproj"
COPY . . #then we say okay we restored all the packages
         #now copy every file that's needed for our app that includes all files (which are in solution explorer directory
WORKDIR "/src/AccessManagApp"
RUN dotnet build "AccessManagApp.csproj" -c Release -o /app/build

#Now we are going from build stage (previous stage) and by running publish it in app/publish directory
FROM build AS publish
RUN dotnet publish "AccessManagApp.csproj" -c Release -o /app/publish

#in this we go the first stage which marked as "base"
#Generate runtime image
FROM base AS final
WORKDIR /app # we witch again to the app directory(from base (first) stage)
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccessManagApp.dll"] # finally we define entry point, by excuting "donet" command with the "AccessManagApp.dll" file